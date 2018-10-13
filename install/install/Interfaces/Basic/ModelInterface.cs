using install.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Interfaces.Basic
{
    interface ModelInterface
    {
        void install();
        void subscribe(Observer observer);
        //Просто устанавливает конфиг и все
        void setConfig(Config config);
        //Изменяет некоторые поля конфига(отстальные-null)
        //Модель при помощи цепочки читателей конфига обновляется
        //Цепочка прервется при первом же читателе, который сможет
        //обработать новый конфиг
        void updateConfig(Config config);
        Result getResult();
    }
}
