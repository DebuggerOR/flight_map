using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Ajax_Minimal.Models
{
    public class DBHandler
    {
        public void Save(string path, string data)
        {
            try
            {
                if (!File.Exists(path))
                {
                    File.Create(path);
                    File.WriteAllText(path, data);
                }
                else
                {
                    File.AppendAllText(path, data);
                }
            }
            catch { }
        }

        public string Load(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return null;
                }
                
                string data = File.ReadAllText(path);
                return data;
            }
            catch { }
            return null;
        }

        // singleton
        private static DBHandler m_Instance = null;
        public static DBHandler Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new DBHandler();
                }
                return m_Instance;
            }
        }
    }
}