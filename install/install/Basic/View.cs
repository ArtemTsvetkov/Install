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

        public View(Form1 form, ModelInterface model)
        {
            this.form = form;
            this.model = model;
            model.subscribe(this);
        }

        public void notify()
        {
            throw new NotImplementedException();
        }
    }
}
