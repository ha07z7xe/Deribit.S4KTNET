using AutoMapper;
using Deribit.S4KTNET.Core.Meta;
using NUnit.Framework;

namespace Deribit.S4KTNET.Test.Mapping
{
    [TestFixture]
    [Category(TestCategories.mapping)]
    [Category(TestCategories.unit)]
    class MappingTestFixture
    {
        [Test]
        public void AssertMappingConfigurationIsValid()
        {
            // build configuration
            var mapperconfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(DeribitS4KTNETCoreMarkerType));
            });
            // assert
            mapperconfiguration.AssertConfigurationIsValid();
        }
    }
}
