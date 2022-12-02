using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FileLibrary
{
    public static class BinaryFileManager
    {
        private readonly static BinaryFormatter _formatter = new BinaryFormatter();
        public static void SaveObjectsToFile(string path, IEnumerable<object> objects)
        {
            using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                _formatter.Serialize(fs, objects);
            }
        }
        public static IEnumerable<object> GetObjects(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return (IEnumerable<object>)_formatter.Deserialize(fs);
            }
        }
    }
}
