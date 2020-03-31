using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using Newtonsoft.Json;

namespace HomeWork_11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Department organization;
        private OrganizationBase repo;
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
             repo = new OrganizationBase();
            //fillDepartment();
            //string json;
            //using (StreamReader sr = new StreamReader("base.json"))
            //{
            //    json = sr.ReadToEnd();
            //}

            //organization = JsonConvert.DeserializeObject<Department>(json, new JsonSerializerSettings
            //{
            //    TypeNameHandling = TypeNameHandling.All
            //});

             organization = repo.GetOrganization();



            // OrganizationTree.Items.Add(organization);
            // MessageBox.Show($"{organization.GetDepartmens().Count}");

            mainorg_expanded();
                      
           
        }

        private void mainorg_expanded()
        {
            var mainItem = new TreeViewItem
            {
                Header = organization.DepartmentName,
                DataContext = organization
            };
            mainItem.Selected += OrganizationTree_Selected;
            mainItem.Expanded += org_expanded;
            mainItem.Collapsed += org_colapsed;

            OrganizationTree.Items.Clear();
            OrganizationTree.Items.Add(mainItem);
            

            foreach (var item in organization.GetDepartmens())
            {
                var trItem = new TreeViewItem
                {
                    Header = item.DepartmentName,
                    DataContext = item
                };
                if (item.Departments.Count > 0)
                {
                    trItem.Items.Add(null);
                }
                trItem.Expanded += org_expanded;
                trItem.Selected += OrganizationTree_Selected;
                trItem.Collapsed += org_colapsed;
                mainItem.Items.Add(trItem);
            }
        }


        private void org_colapsed(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)e.Source;
            //if (item.Items.Count == 0)
            item.Items.Refresh();
            item.Items.Clear();
            item.Items.Add(null);
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
                            Header = subdep.DepartmentName,
                            DataContext = subdep
                        };

                    newItem.Items.Add(null);
                    newItem.Expanded += org_expanded;
                    newItem.Selected += OrganizationTree_Selected;
                    newItem.Collapsed += org_colapsed;
                    item.Items.Add(newItem);
                }   
        }

        private void fillDepartment()
        {
            organization = new Department("ОГКУ ДИРЕКЦИЯ АВТОДОРОГ");
            organization.AddWorker(new HighManager());
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
                item.AddWorker(new HighManager());
                item.AddWorker(new Manager());
                item.AddSubDepartment(new Department("Отдел Олега"));
                item.Departments[0].AddWorker(new Manager());

            }
            organization.Departments[1].Departments[0].AddSubDepartment(new Department("Еще отдел олега"));
            organization.Departments[1].Departments[0].AddWorker(new Manager());
            organization.Departments[1].Departments[0].AddWorker(new Manager());
            organization.Departments[1].Departments[0].AddWorker(new Manager());
            organization.Departments[1].Departments[0].AddWorker(new Manager());
            organization.Departments[1].Departments[0].AddWorker(new Manager());
            organization.Departments[1].Departments[0].AddWorker(new Manager());
            organization.Departments[1].Departments[0].AddWorker(new Manager());

            var jset = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
            string json = JsonConvert.SerializeObject(organization, Formatting.Indented, jset);
            using (StreamWriter sw = new StreamWriter("base.json"))
            {
                sw.WriteLine(json);
            }

        }

        private void OrganizationTree_Selected(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)e.OriginalSource;
            var dep = (Department)item.DataContext;
            ListView1.ItemsSource = dep.Employees;
            txt2.DataContext = item;
            
            txt1.DataContext = dep;

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

        private void AddDepartment_Click(object sender, RoutedEventArgs e)
        {

           



            if(NameDepBox.Text == String.Empty)
            {
                MessageBox.Show("Укажите название для департамента");
            }
            else { 
            Department dep = new Department(null);
            if ((Department)txt1.DataContext is Department)
            {
                dep = (Department)txt1.DataContext;
            }
            dep.AddSubDepartment(new Department(NameDepBox.Text));
            var CurrentTree = (TreeViewItem)txt2.DataContext;
                if (CurrentTree is null) MessageBox.Show("Не выбран управляющий департамент");
                else { 
            if(CurrentTree.Items.Count==0)
            CurrentTree.Items.Add(null);

                    CurrentTree.IsExpanded = true;
                    CurrentTree.IsExpanded = false;
                    CurrentTree.IsExpanded = true;
                }
            }

        }

        private void DelWorkerButton_Click(object sender, RoutedEventArgs e)
        {

            var choise = MessageBox.Show("Уверены что хотите удалить данного сотрудника?", "Внимание", MessageBoxButton.YesNo);
            if (choise == MessageBoxResult.Yes)
            {
                var selectEmployee = (Employee)ListView1.SelectedItem;
                var dep = (Department)txt1.DataContext;

                dep.Employees.Remove(selectEmployee);
            }
        }

        private void DelDepartment_Click(object sender, RoutedEventArgs e)
        {

            var choise = MessageBox.Show("Уверены что хотите удалить данный департамент?", "Внимание", MessageBoxButton.YesNo);
            if (choise == MessageBoxResult.Yes)
            {
                var selectedDep = (TreeViewItem)OrganizationTree.SelectedItem;
                if (selectedDep.Parent is TreeViewItem)
                {
                    var parent = (TreeViewItem)selectedDep.Parent;
                    Department parentDep = (Department)parent.DataContext;
                    parentDep.Departments.Remove((Department)txt1.DataContext);
                    parent.IsExpanded = false;
                    parent.IsExpanded = true;
                }
                else
                {
                    MessageBox.Show("Вы собираетесь удалить всю организацию.");
                }
            }
            

        }

        private void CalcSalaryButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(var el in organization.Employees)
            {
                if(el is HighManager)
                {
                    el.Salary = el.CalcSalary(organization);
                    MessageBox.Show(Convert.ToString(el.Salary));
                    //ListView1.
                    return;
                }
            }
            MessageBox.Show("Директора организации не обнаружено");
            
        }

        private void settingsCheck()
        {
            
            if(File.Exists("base.json"))
            {
                repo = JsonConvert.DeserializeObject<OrganizationBase>("base.json");
            }
            else
            {
                
            }
        }

        private void startFileDialogToLoad()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            Nullable<bool> result = ofd.ShowDialog();
            string path = ofd.FileName;
            if(path != string.Empty) repo.Load(path);
            organization = repo.GetOrganization();
            mainorg_expanded();
        }

        private void LoadBaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (repo.IsSaved == false)
            {
                var choise = MessageBox.Show("Хотите сохранить изменения в текущей базе?", "Внимание", MessageBoxButton.YesNo);
                if (choise == MessageBoxResult.Yes)
                {
                    repo.Save();
                    startFileDialogToLoad();
                }
                else if(choise == MessageBoxResult.No)
                {
                    startFileDialogToLoad();
                }
            }
            else
            {
                startFileDialogToLoad();
            }

            }

        private void SaveBaseButton_Click(object sender, RoutedEventArgs e)
        {
            repo.Save();
        }

        private void SaveAsBaseButton_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            Nullable<bool> result = sfd.ShowDialog();
            string path = sfd.FileName;
            repo.Save(path);
        }
    }
}
