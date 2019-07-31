using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ajax_Minimal.Utils
{
    public class PercConverter
    {
        public double YToPerc(double num)
        {
            return (num + 90) / 1.8;
        }

        public double XToPerc(double num)
        {
            return (num + 180) / 3.6;
        }
        

        // singleton
        private static PercConverter m_Instance = null;
        public static PercConverter Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new PercConverter();
                }
                return m_Instance;
            }
        }
    }
}