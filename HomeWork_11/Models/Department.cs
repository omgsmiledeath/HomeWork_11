using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HomeWork_11.Models
{
    class Department : INotifyPropertyChanged
    {
        #region Статические элементы
        static protected ushort DepCount;
        static protected List<string> DepNames;

        static Department()
        {
            DepCount = 0;
            DepNames = new List<string>();
        }
        #endregion


        private ObservableCollection<Employee> employees;
        private ObservableCollection<Department> departments;
        private string id;
        private string departmentName;

        public Department(string name)
        {
            Employees = new ObservableCollection<Employee>();
            Departments = new ObservableCollection<Department>();
            DepartmentName = name;
            Id = Guid.NewGuid().ToString().Substring(0, 5) + (++DepCount);
        }

        ObservableCollection<Employee> Employees { get => employees; set
            {
                employees = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Employees)));
            }
        }
        ObservableCollection<Department> Departments { get => departments;
            set
            {
                departments = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Departments)));
            }
        }

        public string Id { get => id; set => id = value; }

        public string DepartmentName { get => departmentName; set => departmentName = value; }
        

        public void AddSubDepartment(Department dep)
        {

        }

        public void AddWorker(Employee newWorker)
        {
            if(!employees.Contains(newWorker)) Employees.Add(newWorker);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Employees)));
        }



        public event PropertyChangedEventHandler PropertyChanged;
    }
}
