using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_11.Models
{
    class Improver : Employee
    {
        #region Поля
        private DateTime endOfImprove;
        #endregion

        public Improver(string fname,
            string lname,
            byte age,
            DateTime emplDate,
            int payment,
            DateTime endOfImp) : base(fname,lname,"СТАЖЕР",age)
            {
            EndOfImprove = endOfImp;
            }


        #region Свойства
        public DateTime EndOfImprove { get => endOfImprove; set => endOfImprove = value; }
        public override long CalcSalary()
        {
            salary = 500;
            return salary;
        }
        #endregion
    }
}
