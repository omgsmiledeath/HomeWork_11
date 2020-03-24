using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_11.Models
{
    class HighManager : Employee
    {

        public HighManager(string fname,
            string lname,
            string post,
            byte age,
            DateTime emplDate,
            Department dep
            ) :base (fname,lname,post,age,emplDate)
        {
            Salary = CalcSalary(dep);
        }

        public HighManager() :base ()
        {

        }

        public override uint CalcSalary(Department dep)
        {
            uint result = 0;

            if (dep.Departments.Count != 0)
            {
                foreach (var item in dep.Departments)
                {
                    foreach (var worker in item.Employees)
                    {
                        if (worker is HighManager)
                        {
                            var temp = worker.CalcSalary(item);

                            if (temp < 1300)
                                result += 1300;
                            else result += temp;
                        }
                        
                        if(!(worker is HighManager))
                        {
                            result += worker.Salary;

                        }
                        
                    }
                }
            }
            if(dep.Employees.Count>1)
                foreach(var item in dep.Employees)
                {
                    if (item as HighManager != this) 
                    result += item.Salary;
                }
            result = result * 15 / 100;
            if (result < 1300) this.Salary = 1300;
            else this.Salary = result;
            return result;
        }
    }
}
