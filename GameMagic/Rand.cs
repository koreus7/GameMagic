using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMagic
{
    class Rand
    {

        private static Rand _instance;

        public static Rand Inst
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Rand();
                }

                return _instance;
            }
        }

        private Random sysRan;

        public Rand()
        {
            sysRan = new Random();
        }

        public int Int(int max)
        {
            return sysRan.Next(max);
        }
    }
}
