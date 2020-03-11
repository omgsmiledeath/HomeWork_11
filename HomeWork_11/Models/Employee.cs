using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HomeWork_11.Models
{
    abstract class Employee : IEquatable<Employee>
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
            idCount = 0;
        }
        #endregion

        #region Поля
        private string first_name; //Имя сотрудника
        private string last_name; //Фамилия сотрудника
        private string id; // Уникальный ID сотрудника
        private string post; // Должность сотрудника
 
        private byte age; //Возраст сотрудника
        protected long salary; //Зарплата сотрудника



        #endregion

        #region Конструкторы
        public  Employee(string fname,string lname,string post,byte age)
        {
            First_Name = fname;
            Last_Name = lname;
            Id = Guid.NewGuid().ToString().Substring(0, 5)+(++idCount);
            IdList.Add(Id);
            Post = post;
            Age = age;

            IdList.Add(Id);
        }

        public Employee() : this("John", "Doe","Уборщик",18) { }
        #endregion

        #region Автосвойства
        /// <summary>
        /// Имя
        /// </summary>
        public string First_Name { get => first_name; set => first_name = value; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Last_Name { get => last_name; set => last_name = value; } 
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get => id; set => id = value; }
        /// <summary>
        /// Должность
        /// </summary>
        public string Post { get => post; set => post = value; }
        /// <summary>
        /// Возраст
        /// </summary>
        public byte Age { get => age; set => age = value; }
        /// <summary>
        /// Свойство подсчета зарплаты
        /// </summary>
        public abstract long CalcSalary();

        public bool Equals(Employee other)
        {
            return (this.Id == other.Id);
        }
        #endregion



    }
}
