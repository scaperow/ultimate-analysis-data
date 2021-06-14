using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldSoft.Identiter.Common
{
    public class IdentityResultArgs : EventArgs
    {
        public IdentityResultArgs(Excel excel)
        {
            Excel = excel;
        }

        public Excel Excel { set; get; }
    }
}
