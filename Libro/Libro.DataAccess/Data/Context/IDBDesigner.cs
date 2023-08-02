using Microsoft.EntityFrameworkCore;

namespace Libro.DataAccess.Data
{
    public interface IDBDesigner
    {
        void ConfigureModels(ModelBuilder options);
    }
}
