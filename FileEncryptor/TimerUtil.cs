using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FileEncryptor
{
    class TimerUtil
    {
        Timer MyTimer;
        long TimeCount;

        public delegate void SetControlValue(long value);
    }
}
