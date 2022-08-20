using System.ComponentModel.DataAnnotations;

namespace Shoppify.Market.App.Domain.ValueTypes.EnumTypes
{
    public enum GenderType : sbyte
    {
        [Display(Name = "مرد")]
        Male = 1,
        [Display(Name ="زن")]
        Female = 2
    }
}
