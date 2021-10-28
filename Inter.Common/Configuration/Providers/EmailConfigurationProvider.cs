using Microsoft.Extensions.Configuration;

namespace Inter.Common.Configuration.Providers
{
    public class EmailConfigurationProvider : IEmailConfiguration
    {
        public EmailConfigurationProvider(IConfiguration configuration)
        {
            Email = configuration.GetSection("Email:Email").Value;
            Password = configuration.GetSection("Email:Password").Value; 
        }

        public string Email { get ; set ; }
        public string Password { get ; set ; }
    }
}
