using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Presentation.Helpers.ViewHelper
{
    public interface IViewRenderer
    {
        string RenderPartialViewToString(string viewName, object model);
    }
}
