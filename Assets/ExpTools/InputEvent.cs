using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.ExpTools
{
    class InputEvent
    {
        char inputed;
        string cummulated;
        int nth;
        int inputTime;

        public InputEvent() { }

        public InputEvent(char inputed, string cummulated, int nth, int inputTime)
        {
            this.inputed = inputed;
            this.cummulated = cummulated;
            this.nth = nth;
            this.inputTime = inputTime;
        }
        public InputEvent(InputEvent e)
        {
            inputed = e.inputed;
            cummulated = e.cummulated;
            nth = e.nth;
            inputTime = e.inputTime;

        }

        public string ToString()
        {
            return "" + nth + "," + inputed + "," + inputTime + "," + cummulated;
        }
    }
}
