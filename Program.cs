using System;

namespace STPnet
{
    class Program
    {
        static void Main(string[] args)
        {
            Net net = new Net();
            net.RootBridge();
            net.Print();

            net.RootPorts();
            net.Print();

            net.NonRootPorts();
            net.Print();

            var s = Console.ReadLine();
            
        }
    }
}
