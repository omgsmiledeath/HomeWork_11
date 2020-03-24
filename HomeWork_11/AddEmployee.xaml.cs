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

        public AddEmployee(Department dep)
        {
            InitializeComponent();
            this.Dep = dep;
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
            return new Intern();
        }
        /// <summary>
        /// Метод для получения данных и формирования экземпляра класса Топ Менеджер
        /// </summary>
        /// <returns></returns>
        private Employee getHighManager()
        {
            return new HighManager();
        }

        private void EmplTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // if(EmplTypes.Text =="Интерн")
        }
    }
}
