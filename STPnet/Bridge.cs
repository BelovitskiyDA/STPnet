using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace STPnet
{
    [Serializable]
    public class Bridge
    {
        public int id;
        public int priority;
        public int status; // 0 without, 1 root,
        public Dictionary<int,Port> ports;
        [NonSerialized]
        public EventWaitHandle ev1, ev2, ev3;

        public Bridge() 
        {
            ports = new Dictionary<int, Port>();
        }

        public Bridge(int id, string priority) 
        {
            ports = new Dictionary<int, Port>();
            this.id = id;
            try
            {
                this.priority = Int32.Parse(priority, System.Globalization.NumberStyles.HexNumber);
            }
            catch
            {
                this.priority = 0;
            }
            
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
            ports.Remove(number);
        }

        public void ClearPort(int number)
        {
            if (!ports.ContainsKey(number)) return;
            /*ports[number].LinkId = -1;
            ports[number].Link.Update();*/
            Link link = ports[number].Link;
            if (link.IsHub()) link = null;
            else link.Disconnect(this, number);
        }
        public bool PortIsEmpty(int number)
        {
            if (!ports.ContainsKey(number)) return false;
            //if (ports[number].LinkId == -1) return true;
            if (ports[number].Link == null) return true;
            return false;
        }

        public void Delete()
        {
            foreach (var (k, p) in ports)
            {
                DeletePort(p.number);
            }
        }

        public void FirstPocket(int mode)
        {
            foreach (var (k, p) in ports)
            {
                if (p.Link != null)
                {
                    BPDU newPocket = new BPDU();
                    p.Link.Translate(this, p.number, newPocket, mode);
                    p.statusPrint = 0;
                }
            }


            ev2 = EventWaitHandle.OpenExisting("ev2");
            ev2.Set();
            ev2.Dispose();
            return;
        }

        public void HandlingPocket(int portNumber, BPDU pocket, int weight, int mode)
        {
            

            if (!(ports.ContainsKey(portNumber)))
            {
                return;
            }

            int savePocketMemory = pocket.Memory;
            if (mode == 0)
            {
                savePocketMemory += weight;
            }

            int saveProgMemory = ports[portNumber].progMemory;
            ports[portNumber].progMemory = savePocketMemory;

            ev1 = EventWaitHandle.OpenExisting("ev1");
            ev3 = EventWaitHandle.OpenExisting("ev3");
            WaitHandle[] waitHandles = new WaitHandle[] { ev1, ev3 };

            if (status == 1)
            {
                //ports[portNumber].progMemory = savePocketMemory;
                if (savePocketMemory < ports[portNumber].memory)
                {
                    ports[portNumber].prevMemory = ports[portNumber].memory;
                    ports[portNumber].memory = savePocketMemory;
                }

                //int oldStatus = ports[portNumber].statusPrint;
                //ports[portNumber].statusPrint = 1;
                //ev1.WaitOne();
                EventWaitHandle.WaitAny(waitHandles);
                
                //ports[portNumber].statusPrint = oldStatus;

                ev1.Dispose();
                ev3.Dispose();
                return;
            }

            

            if (savePocketMemory < ports[portNumber].memory)
            {
                if (ports[portNumber].status == 0)
                {
                    ports[portNumber].prevMemory = ports[portNumber].memory;
                    ports[portNumber].memory = savePocketMemory;
                }
                /*if (ports[portNumber].status == 0)
                {

                }*/


                foreach (var (k,p) in ports)
                { 
                    if (k == portNumber)
                    {
                        if (ports.Count == 1)
                        {
                            //int oldStatus = ports[portNumber].statusPrint;
                            //ports[portNumber].statusPrint = 1;
                            //ev1.WaitOne();
                            EventWaitHandle.WaitAny(waitHandles);
                            return;
                            //ev1.WaitOne();
                            //ports[portNumber].statusPrint = oldStatus;
                        }
                        continue;
                    }

                    if (p.Link != null)
                    {
                        BPDU newPocket = new BPDU();
                        if (mode == 0)
                        {
                            newPocket.Memory = savePocketMemory;
                        }
                        else if (mode == 1)
                        {
                            newPocket.Memory = savePocketMemory + weight;
                        }

                        if (p.Link != null)
                        {
                            
                            //int oldStatus = ports[portNumber].statusPrint;
                            //ports[portNumber].statusPrint = 1;
                            
                            EventWaitHandle.WaitAny(waitHandles);
                            //ev1.WaitOne();
                            //ev1.WaitOne();
                            //ports[portNumber].statusPrint = oldStatus;

                            p.Link.Translate(this, p.number, newPocket, mode);
                            
                        }
                        
                        
                    }
                }
                EventWaitHandle.WaitAny(waitHandles);
                foreach (var (pnn, p) in ports)
                {
                    p.statusPrint = 0;
                }
            }
            else
            {
                //ports[portNumber].prevMemory = savePocketMemory;
                //ports[portNumber].progMemory = savePocketMemory;
                

                //int oldStatus = ports[portNumber].statusPrint;
                //ports[portNumber].statusPrint = 1;
                //ev1.WaitOne();
                EventWaitHandle.WaitAny(waitHandles);
                //ev1.WaitOne();
                ports[portNumber].progMemory = saveProgMemory;
                //ports[portNumber].statusPrint = oldStatus;

                ev1.Dispose();
                ev3.Dispose();
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
                    if (numberPort > p.number)
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

            foreach (var (k, p) in ports)
            {
                p.statusPrint = 0;
                p.statusArrow = 0;
                p.prevMemory = 0;
                p.progMemory = 0;
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
                    if (p.Link == null) continue;
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

            foreach (var (k, p) in ports)
            {
                p.statusPrint = 0;
                p.prevMemory = 0;
                p.progMemory = 0;
            }
        }

        public void ResetMemory(int mode)
        {
            foreach (var (k, p) in ports)
            {
                if (mode == 1 && p.status != 0) continue;
                p.memory = Int32.MaxValue;

                /*p.statusPrint = 0;
                p.prevMemory = 0;
                p.progMemory = 0;*/
            }
        }
    }
}
