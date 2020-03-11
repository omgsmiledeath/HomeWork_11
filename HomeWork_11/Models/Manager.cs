
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HomeWork_11.Models
{
    class Manager : Employee
    {
        #region Поля
        private DateTime employmentDate; //Дата приема на работу
        private int hourPayment; //Оплата в час 
        #endregion

        #region Конструктор
        /// <summary>
        /// Конструктор менеджера
        /// </summary>
        /// <param name="fname">Имя</param>
        /// <param name="lname">Фамилия</param>
        /// <param name="post">Должность</param>
        /// <param name="age">Возраст</param>
        /// <param name="emplDate">Дата приема на работу</param>
        public Manager (string fname, 
            string lname, 
            string post, 
            byte age,
            DateTime emplDate,
            int payment) :base (fname,lname,post,age)
        {
            EmploymentDate = emplDate;
            HourPayment = payment;
        }

        public Manager() : base()
        {
            this.EmploymentDate = DateTime.Now;
        }
        #endregion


        #region Свойства
        public DateTime EmploymentDate { get => employmentDate; set => employmentDate = value; }
        public int HourPayment{ get => hourPayment; set => hourPayment = value;}

        public override long CalcSalary()
        {
            salary = hourPayment * 160;
            return salary;
        }
        #endregion
    }
}