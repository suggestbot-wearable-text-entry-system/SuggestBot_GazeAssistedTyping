using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Keyboards
{
    public class AverageFilter
    {
        //const int windowSize = 4;
        const int windowSize = 6; //12
        int index;
        float sum;
        float value;
        float[] data;
        bool isFull;
        public AverageFilter()
        {
            data = new float[windowSize];
            index = 0;
            sum = 0;
            isFull = false;
        }

        public float updateData(float val)
        {

            sum += val;
            sum -= data[index];
            data[index] = val;
            index++;
            if (index == windowSize)
            {
                isFull = true;
                index = 0;
            }

            if (isFull) return sum / windowSize;
            else return sum / index;
        }
    }
}
