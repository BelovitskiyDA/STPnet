using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using STPnet;

namespace STPnetApp
{
    [Serializable]
    public class NetApp
    {
        public Net net;
        public NetView nw;

        public NetApp()
        {
            net = new Net();
            nw = new NetView();
        }

        public void Save(string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, this);
            }
        }

        public static NetApp Load(string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                NetApp netApp = (NetApp)formatter.Deserialize(fs);
                netApp.net.InitEvents();
                return netApp;
            }
        }
    }
}
