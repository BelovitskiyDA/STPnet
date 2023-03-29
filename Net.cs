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

            Bridge bridge1 = new Bridge(1,1);
            bridges.Add(bridge1.id, bridge1);
            bridge1.AddPort(1);

            Bridge bridge2 = new Bridge(2, 2);
            bridges.Add(bridge2.id, bridge2);
            bridge2.AddPort(1);

            Link link1 = new Link(1, bridge1, 1, bridge2, 1, 19);
            links.Add(link1.id, link1);
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
                    if (idBridge < b.id)
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
                    Console.WriteLine($"\tport id: {kb}");
                    Console.WriteLine($"\tmemory: {p.memory}");
                    Console.WriteLine($"\tstatus: {p.status}");
                }
            }

            Console.WriteLine("\n\n");

            foreach (var (kl,l) in links)
            {
                Console.WriteLine($"link id: {kl}");
                Console.WriteLine($"bridge id: {l.InBridge.id} port number: {l.inputPortNumber}");
                Console.WriteLine($"bridge id: {l.OutBridge.id} port number: {l.outputPortNumber}");
            }

            Console.WriteLine("\n\n\n\n");
        }
    }
}
