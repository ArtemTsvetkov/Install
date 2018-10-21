using install.Basic.Model.Chain;
using install.Basic.ModelsParts.Types.ResultTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace install.Basic.ModelsParts.Chain.Concrete
{
    class LogsConfig : BaseConfigReader
    {
        public override Config read(ModelsState modelsState, Config newConfig)
        {
            if (newConfig.logs != null)
            {
                modelsState.config.logs = new List<string>();
                for (int i = 0; i < newConfig.logs.Count; i++)
                {
                    if(!File.Exists(newConfig.logs.ElementAt(i)))
                    {
                        showErrorMessage("Один или несколько указанных файлов с логами"+
                            " не сущестует(ют)");
                        modelsState.result = new Result(new CancelType());
                        return modelsState.config;
                    }
                }
                for (int i=0; i<newConfig.logs.Count; i++)
                {
                    modelsState.config.logs.Add(newConfig.logs.ElementAt(i));
                }
                modelsState.result = new Result(new OkType());
                return modelsState.config;
            }
            else
            {
                return giveNext(modelsState, newConfig);
            }
        }

        private void showErrorMessage(string error)
        {
            string message = error;
            string caption = "Ошибка";
            DialogResult result;
            result = MessageBox.Show(message, caption);
        }
    }
}