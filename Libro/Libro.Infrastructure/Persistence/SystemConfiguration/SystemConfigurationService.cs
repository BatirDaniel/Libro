﻿using Libro.Infrastructure.Persistence.SystemConfiguration.AppSettings;
using Microsoft.Extensions.Options;
using System.Text;

namespace Libro.Infrastructure.Persistence.SystemConfiguration
{
    public class SystemConfigurationService
    {
        public AppSettings.AppSettings AppSettings { get; private set; }
        private IWritableOptions<AppSettings.AppSettings> _appSettingsOptions;

        public SystemConfigurationService(IOptions<AppSettings.AppSettings> appSettings, IWritableOptions<AppSettings.AppSettings> appSettingsOptions)
        {
            AppSettings = appSettings.Value;
            _appSettingsOptions = appSettingsOptions;
        }

        public string GetLibraryPasswordMd5()
        {
            return CreateMD5(AppSettings.AdminPassword ?? "");
        }

        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}