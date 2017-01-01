using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMagic.Logging
{
    interface ISimpleLogger
    {
        void Log(string value);
        void LogError(string value);
    }
}
