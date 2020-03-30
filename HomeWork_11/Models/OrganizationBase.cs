using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
namespace HomeWork_11.Models
{
    class OrganizationBase 
    {
        private Department dep;
        private string currentPath;
        public bool IsSaved { get; set; }
        public Department GetOrganization()
        {
            return dep;
        }

        public OrganizationBase()
        {
            LoadLastState();
            dep = null;
            Load(currentPath);
        }


        private void LoadLastState()
        {
            var oldBase = JsonConvert.DeserializeObject<OrganizationBase>("base.json");
            this.currentPath = oldBase.currentPath;
        }

        public void Load(string path)
        {
            currentPath = path;
            dep = JsonConvert.DeserializeObject<Department>(path);
            IsSaved = true;
        }

        public void Save()
        {
            string serDep = JsonConvert.SerializeObject(dep);
            if (currentPath == string.Empty)
            {
                currentPath = "base.json";
                using (StreamWriter sw = new StreamWriter(currentPath))
                {
                    sw.WriteLine(serDep);
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(currentPath))
                {
                    sw.WriteLine(serDep);
                }
            }
        
            IsSaved = true;
        }

        public void Save(string path)
        {
            string serDep = JsonConvert.SerializeObject(dep);

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(serDep);
            }
            IsSaved = true;
        }


    }
}
