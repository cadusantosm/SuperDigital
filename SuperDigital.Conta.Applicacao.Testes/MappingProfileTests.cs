using AutoMapper;
using Xunit;

namespace SuperDigital.Conta.Applicacao.Testes
{
    public class MappingProfileTests
    {
        [Fact]
        public void MappingProfilesHaveValidConfiguration()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(ApplicationServiceCollectionExtensions).Assembly));

            // Act

            // Assert
            config.AssertConfigurationIsValid();
        }
    }
}
