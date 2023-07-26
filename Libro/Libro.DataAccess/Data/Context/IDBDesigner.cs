using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Libro.DataAccess.Data
{
    public interface IDBDesigner
    {
        void ConfigureModels(ModelBuilder options);
    }
}
