using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STPnet
{
    public class Port
    {
        public int number;
        public int status;
        public int memory;
        public int LinkId;
        public Link Link;

        public Port() { }

        public Port(int number)
        {
            this.number = number;
            status = 0;
            memory = Int32.MaxValue;
            LinkId = -1;
        }

    }
}
