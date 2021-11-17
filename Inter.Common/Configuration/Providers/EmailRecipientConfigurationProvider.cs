using Inter.Common.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Inter.Common.Configuration.Providers
{
    public class EmailRecipientConfigurationProvider : IEmailRecipientConfiguration
    {
        public string Recipient {get; set;} 

        public EmailRecipientConfigurationProvider(IConfiguration configuration)
        {
            Recipient = configuration.GetSection("Email:Recipient").Value;

            if(string.IsNullOrEmpty(Recipient))
            {
                throw new ConfigurationException();
            }
        }
    }
}