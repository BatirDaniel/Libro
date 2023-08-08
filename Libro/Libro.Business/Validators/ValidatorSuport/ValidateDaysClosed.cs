using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Validators.ValidatorSuport
{
    public class ValidateDaysClosed
    {
        public bool ValidateDaysClosedM(string daysClosed)
        {
            string[] dayStrings = daysClosed.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string dayString in dayStrings)
            {
                if (!int.TryParse(dayString, out int day))
                {
                    return false;
                }

                if (day < 1 || day > 7)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
