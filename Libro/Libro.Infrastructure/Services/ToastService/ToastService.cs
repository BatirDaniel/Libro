using Libro.Infrastructure.Services.ToastHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Infrastructure.Services.ToastService
{
    public class ToastService
    {
        public Dictionary<string, string> GetToastData(ToastStatus status, string message)
        {
            string svg = GetSvg(status);
            return new Dictionary<string, string>
            {
                {"svg", svg },
                {"message", message }
            };
        }

        private string GetSvg(ToastStatus status)
        {
            switch (status)
            {
                case ToastStatus.Success:
                    return " <div class=\"inline-flex items-center justify-center flex-shrink-0 w-8 h-8 text" +
                        "-green-500 bg-green-100 rounded-lg dark:bg-green-800 dark:text-green-200\">" +
                        "<svg class=\"w-5 h-5\" aria-hidden=\"true\" xmlns=\"http://www.w3.org/2000/svg\"" +
                        " fill=\"currentColor\" viewBox=\"0 0 20 20\">\r\n<path d=\"M10 .5a9.5 9.5 0 1 0 9.5 9." +
                        "5A9.51 9.51 0 0 0 10 .5Zm3.707 8.207-4 4a1 1 0 0 1-1.414 0l-2-2a1 1 0 0 1 1.414-1.414L" +
                        "9 10.586l3.293-3.293a1 1 0 0 1 1.414 1.414Z\"/>\r\n</svg></div>";
                case ToastStatus.Error:
                    return "<div class=\"inline-flex items-center justify-center flex-shrink-0 w-8 h-8" +
                        " text-red-500 bg-red-100 rounded-lg dark:bg-red-800 dark:text-red-200\"><svg " +
                        "class=\"w-5 h-5\" aria-hidden=\"true\" xmlns=\"http://www.w3.org/2000/svg\"" +
                        " fill=\"currentColor\" viewBox=\"0 0 20 20\">\r\n<path d=\"M10 .5a9.5 9.5 0 1 0 9.5 9.5A9." +
                        "51 9.51 0 0 0 10 .5Zm3.707 11.793a1 1 0 1 1-1.414 1.414L10 11.414l-2.293 2.293a1 1 0 0 1-1." +
                        "414-1.414L8.586 10 6.293 7.707a1 1 0 0 1 1.414-1.414L10 8.586l2.293-2.293a1 1 0 0 1 1.414 1." +
                        "414L11.414 10l2.293 2.293Z\"/>\r\n</svg></div>";
                case ToastStatus.Warning:
                    return "<div class=\"inline-flex items-center justify-center flex-shrink-0 w-8 h-8 text-orange" +
                        "-500 bg-orange-100 rounded-lg dark:bg-orange-700 dark:text-orange-200\"><svg class=\"w-5 h-5\" " +
                        "aria-hidden=\"true\" xmlns=\"http://www.w3.org/2000/svg\" " +
                        "fill=\"currentColor\" viewBox=\"0 0 20 20\">\r\n<path d=\"M10 .5a9.5 9.5 0 1 0 9.5 9.5A9." +
                        "51 9.51 0 0 0 10 .5ZM10 15a1 1 0 1 1 0-2 1 1 0 0 1 0 2Zm1-4a1 1 0 0 1-2 0V6a1 1 0 0 1 2 0v5Z\"/>\r\n</svg></div>";
                case ToastStatus.Info:
                    return "<div class=\"inline-flex items-center justify-center flex-shrink-0 w-8 h-8 text-blue-500 bg-blue-100" +
                        " rounded-lg dark:bg-blue-800 dark:text-blue-200\"><svg class=\"w-4 h-4\" aria-hidden=\"true\" xmlns=\"http://www.w3.org/2000/svg\" " +
                        "fill=\"none\" viewBox=\"0 0 18 20\">\r\n<path stroke=\"currentColor\" stroke-linecap=\"round\" " +
                        "stroke-linejoin=\"round\" stroke-width=\"2\" d=\"M15.147 15.085a7.159 7.159 0 0 1-6.189 3.307A6.713 6" +
                        ".713 0 0 1 3.1 15.444c-2.679-4.513.287-8.737.888-9.548A4.373 4.373 0 0 0 5 1.608c1.287.953 6.445 3.218" +
                        " 5.537 10.5 1.5-1.122 2.706-3.01 2.853-6.14 1.433 1.049 3.993 5.395 1.757 9.117Z\"/>\r\n</svg></div>";
                default:
                    return "";
            }
        }
    }
}
