using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STPnet
{
    public class Bridge
    {
        public int id;
        public int priority;
        public int status; // 0 without, 1 root,
        public Dictionary<int,Port> ports;

        public Bridge() 
        {
            ports = new Dictionary<int, Port>();
        }

        public Bridge(int id, string priority) 
        {
            ports = new Dictionary<int, Port>();
            this.id = id;
            this.priority = Int32.Parse(priority, System.Globalization.NumberStyles.HexNumber);
            status = 0;
        }
        public void AddPort(int number)
        {
            Port port = new Port(number);
            ports.Add(port.number,port);
        }

        public void DeletePort(int number)
        {
            if (!ports.ContainsKey(number)) return;
            Link link = ports[number].Link;
            ports.Remove(number);
            link.Update();
        }

        public void ClearPort(int number)
        {
            if (!ports.ContainsKey(number)) return;
            ports[number].LinkId = -1;
            ports[number].Link.Update();
        }
        public bool PortIsEmpty(int number)
        {
            if (!ports.ContainsKey(number)) return false;
            if (ports[number].LinkId == -1) return true;
            return false;
        }

        public void Delete()
        {
            foreach (var (k, p) in ports)
            {
                this.DeletePort(p.number);
            }
        }
        public void FirstPocket(int mode)
        {
            foreach (var (k, p) in ports)
            {
                if (p.LinkId != -1)
                {
                    BPDU newPocket = new BPDU();
                    p.Link.Translate(id, p.number, newPocket, mode);
                }
            }
        }

        public void HandlingPocket(int portNumber, BPDU pocket, int weight, int mode)
        {
            if (!(ports.ContainsKey(portNumber)))
            {
                return;
            }

            if (status == 1)
            {
                return;
            }

            if (mode == 0)
            {
                pocket.Memory += weight;
            }

            if (pocket.Memory < ports[portNumber].memory)
            {
                if (ports[portNumber].status == 0)
                {
                    ports[portNumber].memory = pocket.Memory;
                }
                
                foreach(var (k,p) in ports)
                {
                    if (k == portNumber) continue;
                    if (p.Link != null)
                    {
                        BPDU newPocket = new BPDU();
                        if (mode == 0)
                        {
                            newPocket.Memory = pocket.Memory;
                        }
                        else if (mode == 1)
                        {
                            newPocket.Memory = pocket.Memory + weight;
                        }

                        if (p.LinkId != -1)
                        {
                            p.Link.Translate(id, p.number, newPocket, mode);
                        }
                        
                    }
                }

            }
            else
            {
                return;
            }
        }

        public void SetRootPort()
        {
            int numberPort = Int32.MaxValue;
            int minMemory = Int32.MaxValue;
            foreach (var (k, p) in ports)
            {
                if (minMemory == p.memory)
                {
                    if (numberPort < p.number)
                    {
                        numberPort = p.number;
                    }
                }
                else if (minMemory > p.memory)
                {
                    minMemory = p.memory;
                    numberPort = p.number;
                }
            }

            if (numberPort != Int32.MaxValue)
            {
                ports[numberPort].status = 1;
            }
        }

        public void SetAsRootBridge()
        {
            status = 1;
            foreach (var (k, p) in ports)
            {
                p.status = 2;
            }
        }

        public void SetNonRootPorts()
        {
            int numberRootPort = -1;
            int rootMemory = Int32.MaxValue;
            foreach (var (k, p) in ports)
            {
                if (p.status == 1)
                {
                    numberRootPort = p.number;
                    rootMemory = p.memory;
                    break;
                }
            }

            foreach (var (k, p) in ports)
            {
                if (p.status != 0) continue;
                if (rootMemory == p.memory)
                {
                    if (p.Link.IsGreater(id,p.number))
                    {
                        p.status = 3;
                    }
                    else
                    {
                        p.status = 2;
                    }
                }
                else if (rootMemory < p.memory)
                {
                    p.status = 2; //designated
                }
                else if (rootMemory > p.memory)
                {
                    p.status = 3; //blocked
                }
            }
        }

        public void ResetMemory(int mode)
        {
            foreach (var (k, p) in ports)
            {
                if (mode == 1 && p.status != 0) continue;
                p.memory = Int32.MaxValue;
            }
        }
    }
}
