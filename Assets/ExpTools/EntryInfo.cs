using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.ExpTools
{
    class EntryInfo
    {
        char input;
        long current;
        public EntryInfo(char inputed, long time)
        {
            input = inputed;
            current = time;
        }
    }
}
