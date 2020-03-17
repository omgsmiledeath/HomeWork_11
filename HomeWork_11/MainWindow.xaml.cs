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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HomeWork_11.Models;

namespace HomeWork_11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Department organization;
        
        #region Конструктор
        /// <summary>
        /// При запуске приложения
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// Первый запуск окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            organization = new Department("ОГКУ ДИРЕКЦИЯ АВТОДОРОГ");
            organization.AddWorker(new Manager());
            organization.AddSubDepartment(new Department("Отдел МТО"));
            
            organization.AddSubDepartment(new Department("Отдел МТО2"));
            
            
            organization.AddSubDepartment(new Department("Отдел МТО3"));
            organization.AddSubDepartment(new Department("Отдел МТО4"));
            organization.AddSubDepartment(new Department("Отдел МТО5"));
            organization.AddSubDepartment(new Department("Отдел МТО6"));
            organization.AddSubDepartment(new Department("Отдел МТО7"));
            foreach (var item in organization.Departments)
            {
                item.AddWorker(new Manager());
                item.AddWorker(new Manager());
                item.AddWorker(new Manager());
                item.AddSubDepartment(new Department("Отдел Олега"));
                item.Departments[0].AddWorker(new Manager());
                
            }
            organization.Departments[1].Departments[0].AddSubDepartment(new Department("Еще отдел олега"));

            // OrganizationTree.Items.Add(organization);
            // MessageBox.Show($"{organization.GetDepartmens().Count}");
            var mainItem = new TreeViewItem
            {
                Header = organization,
                DataContext = organization
            };
            mainItem.Selected += OrganizationTree_Selected;
            OrganizationTree.Items.Add(mainItem);
            foreach (var item in organization.GetDepartmens())
            {
                var trItem = new TreeViewItem
                {
                    Header = item,
                    DataContext = item
                };               
                if (item.Departments.Count > 0)
                {
                    trItem.Items.Add(null);
                }
                trItem.Expanded += org_expanded;
                trItem.Selected += OrganizationTree_Selected;
                mainItem.Items.Add(trItem);
            }            
           
        }

        private void org_expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = new TreeViewItem();
        
            item = (TreeViewItem)e.Source;
            
            if(item.Items.Count!=1 || item.Items[0] != null )
            {
                return;
            }
            Department dep = (Department)item.DataContext;
            
            item.Items.Clear();
           


                if(dep.Departments.Count>0)
                foreach (var subdep in dep.GetDepartmens())
                {
                        var newItem = new TreeViewItem()
                        {
                            Header = subdep,
                            DataContext = subdep
                        };

                        newItem.Items.Add(null);
                    item.Items.Add(newItem);
                    newItem.Expanded += org_expanded;
                        newItem.Selected += OrganizationTree_Selected;
                        
                }
           
            
            
        }

        private void fillDepartment(Department subDepartment)
        {
            
        }

        private void OrganizationTree_Selected(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)e.OriginalSource;
            var dep = (Department)item.DataContext;
            ListBox1.ItemsSource = dep.Employees;
           
            //dep.AddWorker(new Manager());
            txt1.DataContext = dep;
            //OrganizationTree.Tag = item.DataContext;
        }

        private void AddWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            Department dep = new Department(null);
            if((Department) txt1.DataContext is Department)
            {
                dep = (Department)txt1.DataContext;
            }

            AddEmployee AddPageEmpl = new AddEmployee(dep)
            {
                Owner = this
            };
            AddPageEmpl.Show();
        }
    }
}
