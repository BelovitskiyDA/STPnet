using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STPnet
{
    class Net
    {
        public Dictionary<int, Link> links;
        public Dictionary<int, Bridge> bridges;

        public Net() 
        {
            links = new Dictionary<int, Link>();
            bridges = new Dictionary<int, Bridge>();

            Bridge bridge1 = new Bridge(1,"A7");
            bridges.Add(bridge1.id, bridge1);
            bridge1.AddPort(1);
            bridge1.AddPort(2);

            Bridge bridge2 = new Bridge(2, "C1");
            bridges.Add(bridge2.id, bridge2);
            bridge2.AddPort(1);
            bridge2.AddPort(2);
            bridge2.AddPort(3);

            Bridge bridge3 = new Bridge(3, "64");
            bridges.Add(bridge3.id, bridge3);
            bridge3.AddPort(1);
            bridge3.AddPort(2);

            Bridge bridge4 = new Bridge(4, "F5");
            bridges.Add(bridge4.id, bridge4);
            bridge4.AddPort(1);
            bridge4.AddPort(2);
            bridge4.AddPort(3);

            Bridge bridge5 = new Bridge(5, "54");
            bridges.Add(bridge5.id, bridge5);
            bridge5.AddPort(1);
            bridge5.AddPort(2);


            Link link1 = new Link(1, bridge1, 1, bridge2, 1, 19);
            links.Add(link1.id, link1);

            Link link2 = new Link(2, bridge1, 2, bridge3, 1, 19);
            links.Add(link2.id, link2);

            Link link3 = new Link(3, bridge3, 2, bridge4, 1, 19);
            links.Add(link3.id, link3);

            Link link4 = new Link(4, bridge4, 3, bridge5, 2, 4);
            links.Add(link4.id, link4);

            Link link5 = new Link(5, bridge5, 1, bridge2, 3, 19);
            links.Add(link5.id, link5);

            Link link6 = new Link(6, bridge2, 2, bridge4, 2, 100);
            links.Add(link6.id, link6);
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

        public void RootPorts()
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
                    b.FirstPocket(mode);
                    break;
                }
            }

            foreach (var (kb, b) in bridges)
            {
                b.SetRootPort();
            }
        }

        public void ResetMemory()
        {
            int mode = 1;
            foreach (var (kb, b) in bridges)
            {
                b.ResetMemory(mode);
            }
        }

        public void NonRootPorts()
        {            
            if (links == null || bridges == null)
            {
                Console.WriteLine("Add minimum one link and two bridges");
                return;
            }

            ResetMemory();

            int mode = 1;
            foreach (var (kb, b) in bridges)
            {
                if (b.status == 1)
                {
                    b.FirstPocket(mode);
                    break;
                }
            }

            foreach (var (kb, b) in bridges)
            {
                b.SetNonRootPorts();
            }
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
