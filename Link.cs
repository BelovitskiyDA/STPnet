using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STPnet
{
    class Link
    {
        public int id;
        public Bridge InBridge;
        public int inputPortNumber;
        public Bridge OutBridge;
        public int outputPortNumber;
        public int weight;

        public Link() { }

        public Link(int id,
            Bridge InBridge, 
            int inputPortNumber,
            Bridge OutBridge, 
            int outputPortNumber, 
            int weight) 
        {
            this.id = id;
            this.InBridge = InBridge;
            this.inputPortNumber = inputPortNumber;
            this.InBridge.ports[inputPortNumber].Link = this;

            this.OutBridge = OutBridge;
            this.outputPortNumber = outputPortNumber;
            this.OutBridge.ports[outputPortNumber].Link = this;

            this.weight = weight;

        }

        public void Translate(int bridgeId, int PortNumber, BPDU pocket, int mode)
        {
            if (bridgeId == InBridge.id && PortNumber == inputPortNumber)
            {
                OutBridge.HandlingPocket(outputPortNumber, pocket, weight, mode);
            }
            else if(bridgeId == OutBridge.id && PortNumber == outputPortNumber)
            {
                InBridge.HandlingPocket(inputPortNumber, pocket, weight, mode);
            }
        }

        public bool IsGreater(int bridgeId, int PortNumber)
        {
            if (bridgeId == InBridge.id && PortNumber == inputPortNumber)
            {
                if (InBridge.id>OutBridge.id)
                {
                    return true;
                }
                return false;
            }
            else if (bridgeId == OutBridge.id && PortNumber == outputPortNumber)
            {
                if (OutBridge.id > InBridge.id)
                {
                    return true;
                }
                return false;
            }

            return false;
        }
    }
}
