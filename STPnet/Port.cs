using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STPnet
{
    [Serializable]
    public class Port
    {
        public int number;
        public int status;
        public int statusPrint;
        public int memory;
        public int prevMemory;
        public int progMemory;
        //public int LinkId;
        /*private Link link;
        public ref Link Link
        {
            get { return ref link; }
        }*/
        public virtual Link Link { get; set; }

        public Port() { }

        public Port(int number)
        {
            this.number = number;
            status = 0;
            statusPrint = 0;
            memory = Int32.MaxValue;
            Link = null;
            prevMemory = 0;
            prevMemory = 0;
        //LinkId = -1;
    }

    }
}
