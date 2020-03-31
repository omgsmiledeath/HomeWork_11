
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

            dep = new Department("");
            Load("base.json");
        }

        public OrganizationBase(string path)
        {
            dep = new Department("");
            Load(path);
        }


        public void Load(string path)
        {
            currentPath = path;
            string json;
            using (StreamReader sr = new StreamReader(path))
            {
                json = sr.ReadToEnd();
            }

            dep = JsonConvert.DeserializeObject<Department>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            IsSaved = true;
        }

        public void Save()
        {
            var jset = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
            string json = JsonConvert.SerializeObject(dep, Formatting.Indented, jset);
            if (currentPath == string.Empty || currentPath =="base.json")
            {
                currentPath = "base.json";
                
                using (StreamWriter sw = new StreamWriter("base.json"))
                {
                    sw.WriteLine(json);
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(currentPath))
                {
                    sw.WriteLine(json);
                }
                File.Copy(currentPath, "base.json", true);
            }
        
            IsSaved = true;
        }

        public void Save(string path)
        {
            var jset = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
            string json = JsonConvert.SerializeObject(dep, Formatting.Indented, jset);

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(json);
            }

            File.Copy(path, "base.json", true);
            IsSaved = true;
        }


    }
}
