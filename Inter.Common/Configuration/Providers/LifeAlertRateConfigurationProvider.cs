using Inter.Common.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Inter.Common.Configuration.Providers
{
    public class LifeAlertRateConfigurationProvider : ILifeAlertRateConfiguration
    {
        public int Rate {get; set;}

        public LifeAlertRateConfigurationProvider(IConfiguration configuration)
        {
            var rateConfig = configuration.GetSection("Rate").Value;

            if(string.IsNullOrEmpty(rateConfig))
            {
                throw new ConfigurationException();
            }

            Rate = int.Parse(rateConfig);
        }
    }
}