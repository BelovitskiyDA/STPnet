using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STPnet
{
    public class Link
    {
        public int id;
        public Dictionary<Bridge, int> connections;
        /*public Bridge InBridge;
        public int inputPortNumber;
        public Bridge OutBridge;
        public int outputPortNumber;*/
        public int weight;

        public Link() 
        {
            connections = new Dictionary<Bridge, int>();
        }

        public Link(int id,
            Bridge InBridge, 
            int inputPortNumber,
            Bridge OutBridge, 
            int outputPortNumber, 
            int weight) 
        {
            connections = new Dictionary<Bridge, int>();

            this.id = id;
            connections.Add(InBridge, inputPortNumber);
            /*this.InBridge = InBridge;
            this.inputPortNumber = inputPortNumber;*/
            

            connections.Add(OutBridge, outputPortNumber);
            /*this.OutBridge = OutBridge;
            this.outputPortNumber = outputPortNumber;*/

            InBridge.ports[inputPortNumber].LinkId = this.id;
            InBridge.ports[inputPortNumber].Link = this;
            OutBridge.ports[outputPortNumber].LinkId = this.id;
            OutBridge.ports[outputPortNumber].Link = this;

            this.weight = weight;

        }

        public bool IsHub()
        {
            if (connections.Count == 2)
            {
                return false;
            }
            return true;
        }

        public void Update()
        {
            foreach (var (b, pn) in connections)
            {
                if (b.ports[pn].LinkId == -1)
                {
                    if (connections.Count == 2)
                    {
                        foreach(var (b2, pn2) in connections)
                        {
                            b2.ports[pn2].LinkId = -1;
                        }
                    }
                    else
                    {
                        connections.Remove(b);
                    }
                }
            }
        }

        public void Delete()
        {
            foreach (var (b, pn) in connections)
            {
                b.ports[pn].LinkId = -1;       
            }
        }

        public void Disconnect(Bridge bridge, int portNumber)
        {
            bridge.ports[portNumber].LinkId = -1;
            this.Update();
        }
        public void Translate(int bridgeId, int portNumber, BPDU pocket, int mode)
        {
            /*if (bridgeId == InBridge.id && PortNumber == inputPortNumber)
            {
                OutBridge.HandlingPocket(outputPortNumber, pocket, weight, mode);
            }
            else if(bridgeId == OutBridge.id && PortNumber == outputPortNumber)
            {
                InBridge.HandlingPocket(inputPortNumber, pocket, weight, mode);
            }*/

            if (!ConnectionExist(bridgeId, portNumber)) return;

            foreach(var (b,pn) in connections)
            {
                if (b.id == bridgeId && pn == portNumber) continue;
                b.HandlingPocket(pn, pocket, weight, mode);
            }
        }

        public bool ConnectionExist(int bridgeId, int portNumber)
        {
            foreach (var (b, pn) in connections)
            {
                if (b.id == bridgeId && pn == portNumber) return true;
            }
            return false;
        }

        public bool IsGreater(int bridgeId, int portNumber)
        {
            if (!ConnectionExist(bridgeId, portNumber)) return false;

            int maxId = -1;

            foreach (var (b, pn) in connections)
            {
                if (b.id == bridgeId && pn == portNumber) continue;
                if (b.id > bridgeId)
                {
                    maxId = b.id;
                }
            }

            if(maxId == -1)
            {
                return true;
            }
            return false;

            /*if (bridgeId == InBridge.id && PortNumber == inputPortNumber)
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

            return false;*/
        }
    }
}
