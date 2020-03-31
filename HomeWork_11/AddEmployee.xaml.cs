using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HomeWork_11.Models;
namespace HomeWork_11
{
    /// <summary>
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    partial class AddEmployee : Window
    {
        private Department dep;
        private Employee worker;
        public AddEmployee(Department dep)
        {
            InitializeComponent();
            this.Dep = dep;

        }

        public AddEmployee(Department dep,Employee worker)
        {
            InitializeComponent();
            this.Dep = dep;
            this.worker = worker;
        }


        internal Department Dep { get => dep; set => dep = value; }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee result = null;
            switch (EmplTypes.Text)
            {
                case "Менеджер":
                    result = getManager();
                    Dep.AddWorker(result);
                    break;
                case "Высший менеджер":
                    result = getHighManager();
                    Dep.AddWorker(result);
                    break;
                default:
                    result = getInternt();
                    Dep.AddWorker(result);
                    break;

            }

            
            this.Close();
        }


        /// <summary>
        /// Метод для получения данных и формирования экземпляра класса Менеджер
        /// </summary>
        /// <returns></returns>
        private Employee getManager()
        {
            string fname = LnameBox.Text;
            string lname = FnameBox.Text;
            string post = PostBox.Text;
            byte age = Convert.ToByte(AgeBox.Text);
            ushort workHour = Convert.ToUInt16(WorkHBox.Text);
            ushort payment = Convert.ToUInt16(PaymentBox.Text);
            DateTime empldate = Convert.ToDateTime(EmplDateBox.Text);

            return new Manager(fname, lname, post, age, empldate, workHour, payment);
        }
        /// <summary>
        /// Метод для получения данных и формирования экземпляра класса Интерн
        /// </summary>
        /// <returns></returns>
        private Employee getInternt()
        {
            string fname = LnameBox.Text;
            string lname = FnameBox.Text;
            byte age = Convert.ToByte(AgeBox.Text);
            DateTime empldate = Convert.ToDateTime(EmplDateBox.Text);
            DateTime endofinter = Convert.ToDateTime(EndOfInternDate.Text);
            return new Intern(fname,lname,age,empldate,endofinter);
        }
        /// <summary>
        /// Метод для получения данных и формирования экземпляра класса Топ Менеджер
        /// </summary>
        /// <returns></returns>
        private Employee getHighManager()
        {
            string fname = LnameBox.Text;
            string lname = FnameBox.Text;
            string post = PostBox.Text;
            byte age = Convert.ToByte(AgeBox.Text);
            DateTime empldate = Convert.ToDateTime(EmplDateBox.Text);
            return new HighManager(fname,lname,post,age,empldate,dep);
        }

 

        private void EditModeFillFields()
        {
            LnameBox.Text = worker.First_Name;
            FnameBox.Text = worker.Last_Name;
            PostBox.Text = worker.Post;
            AgeBox.Text = $"{worker.Age}";
            EmplDateBox.Text = $"{worker.EmploymentDate}";
            
            if(worker is Manager)
            {

            }
        }

        private void EmplTypes_DropDownClosed(object sender, EventArgs e)
        {
            if (EmplTypes.Text == "Интерн")
            {
                PostBox.Visibility = Visibility.Hidden;
                EndOfInternDate.Visibility = Visibility.Visible;
                WorkHBox.Visibility = Visibility.Hidden;
                PaymentBox.Visibility = Visibility.Hidden;
                
            }
            if (EmplTypes.Text == "Высший менеджер")
            {
                PostBox.Visibility = Visibility.Visible;
                EndOfInternDate.Visibility = Visibility.Hidden;
                WorkHBox.Visibility = Visibility.Hidden;
                PaymentBox.Visibility = Visibility.Hidden;
            }

            if (EmplTypes.Text == "Менеджер")
            {
                PostBox.Visibility = Visibility.Visible;
                EndOfInternDate.Visibility = Visibility.Hidden;
                WorkHBox.Visibility = Visibility.Visible;
                PaymentBox.Visibility = Visibility.Visible;
            }
        }


    }
}
