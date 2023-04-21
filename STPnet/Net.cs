using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace STPnet
{
    [Serializable]
    public class Net
    {
        public Dictionary<int, Link> links;
        public Dictionary<int, Bridge> bridges;

        private int maxLinkId;
        [NonSerialized]
        public EventWaitHandle ev1, ev2, ev3;

        [NonSerialized]
        Thread myThread;
        [NonSerialized]
        Thread CheckThread;
        [NonSerialized]
        public bool isCompleted;
        public Net()
        {
            maxLinkId = 0;
            links = new Dictionary<int, Link>();
            bridges = new Dictionary<int, Bridge>();
            InitEvents();
        }

        public void InitEvents()
        {
            isCompleted = true;
            ev1 = new EventWaitHandle(false, EventResetMode.AutoReset, "ev1");
            ev2 = new EventWaitHandle(false, EventResetMode.AutoReset, "ev2");
            ev3 = new EventWaitHandle(false, EventResetMode.ManualReset, "ev3");
        }
        /*public void Save(string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                using (FileStream fs = new FileStream(filename + "net", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, this);
                }
            }
            catch
            {
                return;
            }
        }

        public static Net Load(string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            Net net;

            using (FileStream fs = new FileStream(filename + "net", FileMode.OpenOrCreate))
            {
                try
                {
                    net = (Net)formatter.Deserialize(fs);
                }
                catch
                {
                    net = new Net();
                }
                net.isCompleted = true;
                net.ev1 = new EventWaitHandle(false, EventResetMode.AutoReset, "ev1");
                net.ev2 = new EventWaitHandle(false, EventResetMode.AutoReset, "ev2");
                net.ev3 = new EventWaitHandle(false, EventResetMode.ManualReset, "ev3");
                return net;
            }
            
        }*/

        public void AddBridge(int id, string priority)
        {
            if (bridges.ContainsKey(id)) return;
            Bridge bridge = new Bridge(id, priority);
            bridges.Add(bridge.id, bridge);
        }

        public void EditBridge(int id, string priority)
        {
            if (!bridges.ContainsKey(id)) return;
            bridges[id].priority = Int32.Parse(priority, System.Globalization.NumberStyles.HexNumber);
        }
        public void DeleteBridge(int idBridge)
        {
            if (!bridges.ContainsKey(idBridge)) return;
            foreach(var (np,p) in bridges[idBridge].ports)
            {
                if (p.Link != null)
                {
                    if (!p.Link.IsHub())
                    {
                        int idLink = p.Link.id;
                        p.Link.Delete();
                        links.Remove(idLink);
                    }
                    else
                    {
                        p.Link.Disconnect(bridges[idBridge], np);
                    }
                }
            }
            bridges[idBridge].Delete();
            //bridges[idBridge] = null;
            bridges.Remove(idBridge);
        }
        public void AddPort(int idBridge, int number)
        {
            if (!bridges.ContainsKey(idBridge)) return;
            if (bridges[idBridge].ports.ContainsKey(number)) return;
            bridges[idBridge].AddPort(number);
        }

        public void DeletePort(int idBridge, int number)
        {
            if (!bridges.ContainsKey(idBridge)) return;
            if (!bridges[idBridge].ports.ContainsKey(number)) return;
            
            Port p = bridges[idBridge].ports[number];
            if (p.Link != null)
            {
                if (!p.Link.IsHub())
                {
                    int idLink = p.Link.id;
                    p.Link.Delete();
                    links.Remove(idLink);
                }
                else
                {
                    p.Link.Disconnect(bridges[idBridge], p.number);
                }
            }
            bridges[idBridge].DeletePort(number);
        }

        public bool PortIsEmpty(int idBridge, int number)
        {
            if (!bridges.ContainsKey(idBridge)) return false;
            if (!bridges[idBridge].ports.ContainsKey(number)) return false;
            if (bridges[idBridge].ports[number].Link == null) return true;
            return false;
        }
        public void AddLink(int idBridge1, int portNumber1, int idBridge2, int portNumber2, int weight)
        {
            if (!bridges.ContainsKey(idBridge1) || !bridges.ContainsKey(idBridge2)) return;
            if (bridges[idBridge1].ports[portNumber1].Link != null || bridges[idBridge2].ports[portNumber2].Link != null) return;

            Link link = new Link(++maxLinkId, bridges[idBridge1], portNumber1, bridges[idBridge2], portNumber2, weight);
            links.Add(link.id, link);
        }
        public void EditLink(int idLink, int weight)
        {
            if (!links.ContainsKey(idLink)) return;
            links[idLink].weight = weight;
        }
        public void DeleteLink(int idLink)
        {
            if (!links.ContainsKey(idLink)) return;
            //links[idLink].Delete();
            links[idLink].Delete();
            links.Remove(idLink);

            //links[idLink] = null;
        }

        public void AddConnect(int idLink, int idBridge, int portNumber)
        {
            if (!links.ContainsKey(idLink)) return;
            if (!bridges.ContainsKey(idBridge)) return;
            if (bridges[idBridge].ports[portNumber].Link != null) return; //isEmpty
            if (links[idLink].connections.ContainsKey(bridges[idBridge])) return;
            links[idLink].connections.Add(bridges[idBridge], portNumber);
            //bridges[idBridge].ports[portNumber].LinkId = links[idLink].id;
            bridges[idBridge].ports[portNumber].Link = links[idLink];
        }

        public void DisconnectLink(int idLink, int idBridge, int portNumber)
        {
            if (!bridges.ContainsKey(idBridge)) return;
            if (!links.ContainsKey(idLink)) return;
            
            if (!links[idLink].IsHub())
            {
                links[idLink].Delete();
                links.Remove(idLink);
            }
            else
            {
                links[idLink].Disconnect(bridges[idBridge], portNumber);
            }
            
        }

        public void RootBridge()
        {
            if (links == null || bridges == null)
            {
                Console.WriteLine("Add minimum one link and two bridges");
                return;
            }

            int idBridge = Int32.MaxValue;
            int minPriority = Int32.MaxValue;
            foreach(var (kb, b) in bridges)
            {
                if (minPriority == b.priority)
                {
                    if (idBridge > b.id)
                    {
                        idBridge = b.id;
                    }
                }
                else if (minPriority > b.priority)
                {
                    minPriority = b.priority;
                    idBridge = b.id;
                }
            }

            if (idBridge != Int32.MaxValue)
            {
                bridges[idBridge].SetAsRootBridge();
            }
        }

        public void NextStep()
        {
            if (ev1 == null) return;
            ev1.Set();
        }
        
        

        public void CheckEnd(int flag, int threadMode)
        {
            myThread.Join();
            if (flag == 0)
            {
                SetRootPorts();
            }
            else if (flag == 1)
            {
                SetNonRootPorts();
            }
            isCompleted = true;
            myThread = null;
            if (threadMode != 0) CheckThread = null;

            return;
        }

        public void waitComplete()
        {
            CheckThread.Join();
            CheckThread = null;

        }

        void StartModeling(int mode, int threadMode, Bridge b)
        {
            if (threadMode == 0)
            {
                ev1.Set();
                ev3.Set();
            }
            else
            {
                ev1.Reset();
                ev3.Reset();
            }

            ev2.Reset();

            isCompleted = false;

            myThread = new Thread(() => b.FirstPocket(mode));
            myThread.Start();

            CheckThread = new Thread(() => CheckEnd(mode, threadMode));
            CheckThread.Start();
            return;
        }


        public void RootPorts(int threadMode)
        {
            if (links == null || bridges == null)
            {
                Console.WriteLine("Add minimum one link and two bridges");
                return;
            }

            int mode = 0;
            foreach (var (kb, b) in bridges)
            {
                if (b.status == 1)
                {
                    StartModeling(mode, threadMode, b);
                    break;
                }
            }
        }

        public void SetRootPorts()
        {
            foreach (var (kb, b) in bridges)
            {
                if (b.status == 1) continue;
                b.SetRootPort();
            }
        }

        public void SetNonRootPorts()
        {
            foreach (var (kb, b) in bridges)
            {
                b.SetNonRootPorts();
            }
        }


        public void ResetMemory(int mode)
        {
            foreach (var (kb, b) in bridges)
            {
                b.ResetMemory(mode);
            }
        }

        public void NonRootPorts(int threadMode)
        {            
            if (links == null || bridges == null)
            {
                Console.WriteLine("Add minimum one link and two bridges");
                return;
            }

            int mode = 1;
            ResetMemory(mode);
            foreach (var (kb, b) in bridges)
            {
                if (b.status == 1)
                {
                    StartModeling(mode, threadMode, b);
                    break;
                }
            }
        }

        public void CompleteModeling()
        {
            if (myThread != null || CheckThread != null)
            {
                ev3.Set();
                //ev1.Close();
                //ev4.WaitOne();
                waitComplete();
                //ev4.Reset();
                
            }
            
        }

        public void Reset()
        {
            if (myThread != null || CheckThread != null)
            {
                ev3.Set();
                //ev1.Close();
                waitComplete();
                //ev4.WaitOne();
            }

            
            //SetRootPorts();

            foreach (var (i,b) in bridges)
            {
                b.status = 0;
                foreach(var (j,p) in b.ports)
                {
                    p.status = 0;
                    p.statusPrint = 0;
                    p.statusArrow = 0;
                    p.prevMemory = 0;
                    p.progMemory = 0;
                }
            }
            ResetMemory(0);
            /*if (myThread != null || CheckThread != null)
            {
                //ev3.Reset();
                //ev4.Reset();
                myThread = null;
                CheckThread = null;
            }*/
                
        }


        public void Print()
        {
            if (links == null || bridges == null)
            {
                Console.WriteLine("Add minimum one link and two bridges");
                return;
            }

            foreach(var (kb,b) in bridges)
            {
                Console.WriteLine($"bridge id: {kb}");
                Console.WriteLine($"priority: {b.priority}");
                Console.WriteLine($"status: {b.status}");
                foreach (var (kp,p) in b.ports)
                {
                    Console.WriteLine($"\tport id: {kp}");
                    Console.WriteLine($"\tmemory: {p.memory}");
                    Console.WriteLine($"\tstatus: {p.status}");
                }
            }

            Console.WriteLine("\n\n");

            foreach (var (kl,l) in links)
            {
                Console.WriteLine($"link id: {kl}");
                foreach (var (b, pn) in l.connections)
                {
                    Console.WriteLine($"bridge id: {b.id} port number: {pn}");
                }
            }

            Console.WriteLine("\n\n\n\n");
        }
    }
}
