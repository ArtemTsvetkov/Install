using install.Basic.ModelsParts.Types.ProgramTypes;
using install.Basic.ModelsParts.Types.TimeTypes;
using install.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Basic
{
    class Config
    {
        public int modificator;
        public string avevasPath;
        public string connection;
        public string date;
        public string programPath;
        public List<string> logs;
        public SecurityUserInterface admin;
        public ProgramType programType;
        public TimeType timeType;
        

        public Config()
        {
            modificator = -1;
        }

        public Config copy()
        {
            Config copy = new Config();
            if(admin!=null)
            {
                copy.admin = admin.copy();
            }
            if (avevasPath != null)
            {
                copy.avevasPath = avevasPath;
            }
            if (connection != null)
            {
                copy.connection = connection;
            }
            if (programType != null)
            {
                copy.programType = programType;
            }
            if (date != null)
            {
                copy.date = date;
            }
            if (logs != null)
            {
                copy.logs = new List<string>();
                for (int i=0; i< logs.Count; i++)
                {
                    copy.logs.Add(logs.ElementAt(i));
                }
            }
            if (programPath != null)
            {
                copy.programPath = programPath;
            }
            if (timeType != null)
            {
                copy.timeType = timeType;
            }
            if (modificator != -1)
            {
                copy.modificator = modificator;
            }

            return copy;
        }
    }
}
