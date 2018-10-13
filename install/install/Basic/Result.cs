using install.Interfaces.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Basic
{
    class Result
    {
        private ResultInterface result;

        public Result(ResultInterface result)
        {
            this.result = result;
        }

        public string type()
        {
            return result.getType();
        }
    }
}
