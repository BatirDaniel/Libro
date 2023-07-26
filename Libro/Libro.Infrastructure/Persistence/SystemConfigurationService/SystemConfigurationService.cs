using Libro.Infrastructure.Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Infrastructure.SystemConfiguration
{
    public class SystemConfigurationService
    {
        public AppSettings? settings { get; private set; }

        public SystemConfigurationService(
            AppSettings _settings)
        {
            settings = _settings;
        }

        public void SetJWTSecret (string secret)
        {
            settings.JWT_Secret = secret;
        }
    }
}
