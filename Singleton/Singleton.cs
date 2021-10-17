using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    public sealed class Singleton
    {
        private static int intance_counter = 0;
        private static readonly object Instancelock = new object();
        private Singleton()
        {
            intance_counter++;            
        }
        private static Singleton instance = null;

        public static Singleton GetInstance
        {
            get
            {
                if (instance == null)
                    lock (Instancelock)
                    {
                        if (instance == null)
                            instance = new Singleton();
                    }

                return instance;
            }
        }
    }
}
