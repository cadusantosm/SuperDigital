using AutoMapper;
using Xunit;

namespace SuperDigital.Conta.Api.Testes
{
    public class MappingProfileTests
    {
        [Fact]
        public void MappingProfilesHaveValidConfiguration()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(Startup).Assembly));

            // Act

            // Assert
            config.AssertConfigurationIsValid();
        }
    }
}
