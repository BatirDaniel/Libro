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
        private IWritableOptions<AppSettings> _appSettingsOptions;

        public SystemConfigurationService(
            IWritableOptions<AppSettings> appSettingsOptions)
        {
            settings = _appSettingsOptions.Value;
            _appSettingsOptions = appSettingsOptions;
        }

        public void SetJWTSecret (string newJwtSecret)
        {
            settings.JWT_Secret = newJwtSecret;

            this._appSettingsOptions.Update(op =>
            {
                op.JWT_Secret = newJwtSecret;
            });
        }
    }
}
