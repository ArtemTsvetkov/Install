using install.Basic.ModelsParts.Types.ProgramTypes;
using install.Basic.ModelsParts.Types.TimeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Interfaces.Basic
{
    interface ControllerInterface
    {
        void setProgramPath(string path);
        void setConnectionString(string connection);
        void setInstalledProgramType(ProgramType programType);
        void setLastDate(string date);
        void setLogsPath(List<string> logs);
        void setTimeModidicatorType(TimeType timeType);
        void setTimeModificator(int modificator);
        void setAvevasPath(string path);
        void install();
        void initDataBase(SecurityUserInterface admin);
    }
}
