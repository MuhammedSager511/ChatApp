using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extentions
{
   public  static class DateTimeExtentions
    {
        public static int CalculateAge(this DateTime dateOfBirth)
        {
            var todey = DateTime.Today;
            var age = todey.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > todey.AddYears(-age)) return age--;
            return age;
        }
    }
}
