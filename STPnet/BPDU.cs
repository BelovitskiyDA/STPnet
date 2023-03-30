using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STPnet
{
    public class BPDU
    {
        private int memory;

        public BPDU()
        {
            memory = 0;
        }

        public int Memory
        {
            get
            {
                return memory;
            }
            set
            {
                memory = value;
            }
        }
    }
}
