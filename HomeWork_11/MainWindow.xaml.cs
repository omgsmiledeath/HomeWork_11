﻿using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
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
            BaseCheck();
        }
        #endregion

        /// <summary>
        /// Первый запуск окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainorg_expanded();
        }

        private void BaseCheck()
        {
            if(File.Exists("base.json"))
            {
                repo = new OrganizationBase();
                organization = repo.GetOrganization();
            }
            else
            {
                MessageBox.Show("База в месте по умолчанию не обнаруженна,введите название для организации!");
                repo = new OrganizationBase("base.json");
                organization = repo.GetOrganization();
                AddDepartment adddep = new AddDepartment(organization);
   
                if (adddep.ShowDialog() == true)
                {
                    mainorg_expanded();
                }
                
            }
        }

        public void mainorg_expanded()
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
            repo.IsSaved = false;
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
            repo.IsSaved = false;
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
            repo.IsSaved = false;
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
                    choise = MessageBox.Show("Вы собираетесь удалить всю организацию.","Внимание",MessageBoxButton.YesNo);
                    if(choise == MessageBoxResult.Yes)
                    {
                        organization.Departments.Clear();
                        organization.DepartmentName = "Новый";
                        AddDepartment adddep = new AddDepartment(organization);
                        adddep.Owner = this;
                        if(adddep.ShowDialog() ==true)
                        {
                            mainorg_expanded();
                        }
                        
                        
                    }
                }
            }
            repo.IsSaved = false;


        }

        private void CalcSalaryButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var el in organization.Employees)
            {
                if (el is HighManager)
                {
                    (el as HighManager).CalcSalary(organization);
                    MessageBox.Show(Convert.ToString(el.Salary));
                    return;
                }

            }
            foreach (var item in organization.Departments) firstHightManager(item);
            repo.IsSaved = false;
        }

        private void firstHightManager(Department dep)
        {
            foreach (var item in dep.Departments)
            {
                foreach (var el in item.Employees)
                {
                    if (el is HighManager)
                    {
                        el.Salary = el.CalcSalary(organization);
                        MessageBox.Show(Convert.ToString(el.Salary));
                        return;
                    }

                }
                firstHightManager(item);
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
            repo.IsSaved = false;
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
            if (path != string.Empty) repo.Save(path);
        }
    }
}
