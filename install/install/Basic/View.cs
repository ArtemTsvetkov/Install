using install.Interfaces.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Basic
{
    class View : Observer
    {
        private Form1 form;
        private ModelInterface model;
        private int serialNumber;
        //Номер в массиве-порядок показа, значение-номер tab
        private List<int> currentTabMappedNext;

        public View(Form1 form, ModelInterface model)
        {
            this.form = form;
            this.model = model;
            model.subscribe(this);
            serialNumber = 0;
        }

        public void setMap(List<int> currentTabMappedNext)
        {
            this.currentTabMappedNext = currentTabMappedNext;
            serialNumber = -1;
            serialNumber++;
            form.selectTab(currentTabMappedNext.ElementAt(serialNumber));
        }

        private void getNextTab()
        {
            serialNumber++;
            form.selectTab(currentTabMappedNext.ElementAt(serialNumber));
        }

        public void getPreviousTab()
        {
            serialNumber--;
            form.selectTab(currentTabMappedNext.ElementAt(serialNumber));
        }

        public void notify()
        {
            if(model.getResult().type().Equals("ok"))
            {
                getNextTab();
            }
            if (model.getResult().type().Equals("DataBaseHaventTables"))
            {
                currentTabMappedNext.Insert(currentTabMappedNext.Count - 1, 8);
                currentTabMappedNext.Add(6);
                currentTabMappedNext.Add(6);
                getNextTab();
            }
            if (model.getResult().type().Equals("Cancel"))
            {
                //Общий тип ошибок, сейчас он никак не обрабатывается, нет
                //смысла, сделал на всякий случай, если он (смысл появится).
            }
            if(model.getResult().type().Equals("ConnectionReady"))
            {
                form.button6Elem.Enabled = true;
                form.button28Elem.Enabled = true;
                getNextTab();
            }
            if(model.getResult().type().Equals("ConnectionNotReady"))
            {
                form.button6Elem.Enabled = false;
                form.button28Elem.Enabled = false;
            }
        }
    }
}
