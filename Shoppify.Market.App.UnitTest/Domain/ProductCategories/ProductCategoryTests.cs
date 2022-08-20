using FluentValidation.TestHelper;
using Microsoft.Extensions.Options;
using Shoppify.Market.App.Domain.Entites;
using Shoppify.Market.App.Service.Commands.ProductCategories;
using Shoppify.Market.App.Service.Resources;

namespace Shoppify.Market.App.UnitTest.Domain.ProductCategories
{
    public class ProductCategoryTests
    {
        private CancellationTokenSource _cancellationToken;
        public ProductCategoryTests()
        {
            _cancellationToken = new CancellationTokenSource();
        }

        [Fact]
        public void Should_Make_A_New_Object()
        {
            // Arrange
            ProductCategory? productCategory = null;
            // Act
            productCategory = ProductCategory.New(null, "Title Test", "Description test", new Guid());
            // Assert
            productCategory.ShouldNotBe(null);
        }

        [Fact]
        public void Should_Edit_An_Object()
        {
            // Arrange
            ProductCategory productCategory = ProductCategory.New(null, "Title Test", "Description test", new Guid());
            string title = "title not test";
            string? description = "desc not test";
            Guid image = new Guid();
            // Act
            productCategory.Edit(null, title, description, image);
            // Assert
            productCategory.Description.ShouldBe(description);
            productCategory.Title.ShouldBe(title);
            productCategory.Image.ShouldBe(image);
            productCategory.Parent.ShouldBe(null);
        }

        [Theory]
        [InlineData("Test Text", "Test Description", true)]
        [InlineData("Test Text", "", false)]
        [InlineData("Test Text", "Tes", false)]
        public void Command_Validation(string title,string descriptions,bool isValid)
        {
            // arrange
            var fakeRes = A.Fake<IOptions<GlobalResources>>();
            var command = new AddProductCategoryCommand(null, title, descriptions, Guid.NewGuid());
            //act
            A.CallTo(() => fakeRes.Value).Returns(new GlobalResources
            {
                LengthExceeded = "asdf",
                LessThanMinLength = "asdfasfd",
                ParentNotfound = "asdfdsfe",
                RequiredField = "asdfaf",
                ProductCategoryResources = new ProductCategoryResources
                {
                    DescriptionName = "توضحیحات",
                    ImageName = "عکس",
                    TitleName = "عنوان"
                }
            });
            var commandValidator = new AddProductCategoryCommandValidator(fakeRes);
            var result = commandValidator.TestValidate(command);
            //assert
            result.IsValid.ShouldBe(isValid);
        }
    }
}
