using System;
using System.Collections.Generic;

namespace HomeWork_11.Models
{
    abstract class Employee
    {
        #region Static поля и конструктор
        static protected List<string> IdList; // Список всех ID 
        static protected long idCount;// Число сотрудников
        /// <summary>
        /// Статический конструктор
        /// </summary>
        static Employee()
        {
            IdList = new List<string>();
            idCount = 1;
        }
        #endregion

        #region Поля
        private string first_name; //Имя сотрудника
        private string last_name; //Фамилия сотрудника
        private string id; // Уникальный ID сотрудника
        private string post; // Должность сотрудника
        private DateTime employmentDate; //Дата приема на работу
        private byte age; //Возраст сотрудника

        #endregion

        #region Конструкторы
        public Employee(string fname,string lname,string post,DateTime emplDate,byte age)
        {
            First_Name = fname;
            Last_Name = lname;
            Id = Guid.NewGuid().ToString().Substring(0, 5)+(++idCount);
            IdList.Add(Id);
            Post = post;
            EmploymentDate = emplDate;
            Age = age;
        }

        public Employee() : this("John", "Doe","Уборщик",new DateTime(1900,01,01),18) { }
        #endregion

        #region Автосвойства
        public string First_Name { get => first_name; set => first_name = value; }
        public string Last_Name { get => last_name; set => last_name = value; }
        public string Id { get => id; set => id = value; }
        public string Post { get => post; set => post = value; }
        public DateTime EmploymentDate { get => employmentDate; set => employmentDate = value; }
        public byte Age { get => age; set => age = value; }
        #endregion

        #region Свойства

        #endregion

    }
}
