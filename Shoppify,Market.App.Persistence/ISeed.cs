using Shoppify.Market.App.Domain.Markers;

namespace Shoppify.Market.App.Persistence
{
    public interface ISeed : IScopedDependency
    {
        public Task Seed();
    }
}
