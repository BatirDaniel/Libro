using Libro.Infrastructure.Services.ToastHelper;

namespace Libro.Infrastructure.Services.ToastService
{
    public interface IToastService
    {
        Dictionary<string, string> GetToastData(ToastStatus status, string message);
    }
}
