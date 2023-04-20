using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using STPnet;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Drawing2D;

namespace STPnetApp
{
    [Serializable]
    public class PointStruct
    {
        public int x;
        public int y;
        //public Point point;
        public Dictionary<int, myPoint> ports;
    }
    [Serializable]
    public class myPoint
    {
        public int id;
        public int x;
        public int y;
        //public Point point;
    }
    [Serializable]
    public class NetView
    {
        public Dictionary<int, PointStruct> bridges;
        public Dictionary<int, PointStruct> links;
        public static int hBridge = 200; // need %2
        public static int hPort = 60; // need %2
        public static int hLink = 10; // need %2

        public NetView()
        {
            bridges = new Dictionary<int, PointStruct>();
            links = new Dictionary<int, PointStruct>();
        }

        public void Save(string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                using FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
                formatter.Serialize(fs, this);
            }
            catch
            {
                return;
            }
        }

        public static NetView Load(string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            NetView nw;

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                try
                {
                    nw = (NetView)formatter.Deserialize(fs);
                }
                catch
                {
                    nw = new NetView();
                }
                return nw;
            }
        }

        public static int Funct(int x, double k, double b)
        {
            return (int)(k * x + b);
        }
        public static int FunctReverse(int y, double k, double b)
        {
            return (int)((y - b)/k);
        }

        public static void FunctParam(int x1, int y1, int x2, int y2, out double k, out double b)
        {
            k = (double)(y2 - y1) / (x2 - x1);
            b = y1 - k * x1;
        }
        public void Find(int x, int y, out int type, out int id, out int idb, out int idp)
        {
            type = 0;
            id = 0;
            idb = -1;
            idp = -1;
            foreach (var (i, ps) in bridges)
            {
                if ((x > ps.x - hBridge/2) && (x < ps.x + hBridge/2) && (y > ps.y - hBridge/2) && (y < ps.y + hBridge/2))
                {
                    type = 1;
                    id = i;
                    foreach (var (j, p) in ps.ports)
                    {
                        if ((x > p.x - hPort/2) && (x < p.x + hPort/2) && (y > p.y - hPort/2) && (y < p.y + hPort/2))
                        {
                            type = 2;
                            id = j;
                            idb = i;
                            return;
                        }
                    }
                }
                if (type == 1) return;
            }

            foreach (var (i, ps) in links)
            {
                int x1 = ps.x;
                int y1 = ps.y;
                foreach (var (j, p) in ps.ports)
                {
                    int x2 = p.x;
                    int y2 = p.y;
                    if (!((x - x1) * (x - x2) <= 0 && (y - y1) * (y - y2) <= 0)) continue;

                    FunctParam(x1, y1, x2, y2, out double k, out double b);

                    if ((Math.Abs(y-Funct(x,k,b))<hLink/2) || (Math.Abs(x - FunctReverse(y, k, b)) < hLink/2))
                    {
                        type = 3;
                        id = i;
                        idb = j;
                        idp = p.id;
                        return;
                    }
                }
            }
        }


        public void AddBridge(int id, int x, int y)
        {
            PointStruct ps = new();
            ps.x = x;
            ps.y = y;
            ps.ports = new Dictionary<int, myPoint>();
            bridges.Add(id, ps);
        }
        public void DeleteBridge(int id)
        {
            if (!bridges.ContainsKey(id)) return;
            bridges.Remove(id);
        }

        public void EditPosBridge(int id, int x, int y)
        {
            if (!bridges.ContainsKey(id)) return;
            int oldx = bridges[id].x;
            int oldy = bridges[id].y;
            bridges[id].x = x;
            bridges[id].y = y;

            foreach(var (i,p) in bridges[id].ports)
            {
                p.x = p.x + (x - oldx);
                p.y = p.y + (y - oldy);
            }
        }
        public void AddPort(int idBridge, int idPort, myPoint p)
        {
            int x = p.x;
            int y = p.y;
            if (!bridges.ContainsKey(idBridge)) return;
            int bx = bridges[idBridge].x;
            if (Math.Abs(x - bx)>(hBridge/2 - hPort/2))
            {
                x = (int)(bx + (x - bx) / (Math.Abs(x - bx)) * (hBridge/2 - hPort/2));
            }
            int by = bridges[idBridge].y;
            if (Math.Abs(y - by) > (hBridge/2 - hPort/2))
            {
                y = (int)(by + (y - by) / (Math.Abs(y - by)) * (hBridge/2 - hPort/2));
            }

            p.x = x;
            p.y = y;
            bridges[idBridge].ports.Add(idPort, p);
        }
        public void DeletePort(int idBridge, int idPort)
        {
            if (!bridges.ContainsKey(idBridge)) return;
            if (!bridges[idBridge].ports.ContainsKey(idPort)) return;
            bridges[idBridge].ports.Remove(idPort);
        }
        public void EditPosPort(int idBridge, int idPort, int x, int y)
        {
            if (!bridges.ContainsKey(idBridge)) return;
            if (!bridges[idBridge].ports.ContainsKey(idPort)) return;

            int bx = bridges[idBridge].x;
            if (Math.Abs(x - bx) > (hBridge/2 - hPort/2))
            {
                x = (int)(bx + (x - bx) / (Math.Abs(x - bx)) * (hBridge/2 - hPort/2));
            }
            int by = bridges[idBridge].y;
            if (Math.Abs(y - by) > (hBridge/2 - hPort/2))
            {
                y = (int)(by + (y - by) / (Math.Abs(y - by)) * (hBridge/2 - hPort/2));
            }

            bridges[idBridge].ports[idPort].x = x;
            bridges[idBridge].ports[idPort].y = y;
        }
        public void AddLink(int id, int idb1, myPoint p1, int idb2, myPoint p2)
        {
            PointStruct ps = new();
            ps.x = (int)((p1.x+p2.x)/2);
            ps.y = (int)((p1.y + p2.y)/2);
            ps.ports = new Dictionary<int, myPoint>();
            ps.ports.Add(idb1, p1);
            ps.ports.Add(idb2, p2);
            links.Add(id, ps);
        }
        public void DeleteLink(int id)
        {
            if (!links.ContainsKey(id)) return;
            links.Remove(id);
        }

        public void AddConnectionLink(int id, int idb, myPoint p)
        {
            if (links[id].ports.ContainsKey(idb)) return;
            links[id].ports.Add(idb, p);
        }

        public void DeleteConnectionLink(int id, int idb, int idp)
        {
            if (!links.ContainsKey(id)) return;
            if (!links[id].ports.ContainsKey(idb)) return;
            if (links[id].ports[idb].id != idp) return;
            links[id].ports.Remove(idb);
            if (links[id].ports.Count == 1) DeleteLink(id);
        }

        public void EditPosLink(int id, int x, int y)
        {
            if (!links.ContainsKey(id)) return;
            links[id].x = x;
            links[id].y = y;
        }

        public void Update(Net net)
        {
            foreach (var (i, bps) in bridges)
            {
                if (!net.bridges.ContainsKey(i))
                {
                    DeleteBridge(i);
                }
            }

            foreach (var (i, b) in net.bridges)
            {
                if (!bridges.ContainsKey(b.id))
                {
                    AddBridge(b.id, 400, 400);
                }
            }

            foreach (var (idb, b) in bridges)
            {
                foreach (var (idp, p) in b.ports)
                {
                    if (!net.bridges[idb].ports.ContainsKey(p.id))
                    {
                        DeletePort(idb, idp);
                        //b.ports.Remove(idb);
                    }
                }
            }

            foreach (var (i, b) in net.bridges)
            {
                foreach (var (j, p) in b.ports)
                {
                    if (!bridges[i].ports.ContainsKey(j))
                    {
                        myPoint point = new();
                        point.id = j;
                        /*point.x = 30;
                        point.y = 30;*/
                        point.x = bridges[i].x;
                        point.y = bridges[i].y;
                        AddPort(i, j, point);
                    }
                }
            }

            foreach (var (i, lps) in links)
            {
                if (!net.links.ContainsKey(i))
                {
                    DeleteLink(i);
                    continue;
                }
                foreach (var (idb,p) in lps.ports)
                {
                    if (!net.bridges.ContainsKey(idb) || !net.links[i].connections.ContainsKey(net.bridges[idb]))
                    {
                        DeleteConnectionLink(i, idb, p.id);
                    }

                }
            }

            foreach (var (idl, l) in net.links)
            {
                if (!links.ContainsKey(idl))
                {
                    var pair1 = l.connections.ElementAt(0);
                    var pair2 = l.connections.ElementAt(1);

                    if (pair1.Key.ports[pair1.Value].Link == null || pair2.Key.ports[pair2.Value].Link == null) return;

                    myPoint p1 = bridges[pair1.Key.id].ports[pair1.Value];
                    p1.id = pair1.Value;
                    myPoint p2 = bridges[pair2.Key.id].ports[pair2.Value];
                    p2.id = pair2.Value;
                    AddLink(l.id, pair1.Key.id, p1, pair2.Key.id, p2);

                    int count = l.connections.Count;
                    for (int i = 2; count > i; i++)
                    {
                        var pair = l.connections.ElementAt(i - 1);
                        if (pair.Key.ports[pair.Value].Link == null) continue;
                        myPoint p = bridges[pair.Key.id].ports[pair.Value];
                        p.id = pair.Value;
                        AddConnectionLink(l.id, pair.Key.id, p);
                    }
                }

            }

        }

        public void ScaleTransform(float scale)
        {
            hBridge = (int)(200*scale); // need %2
            if (hBridge % 2 != 0) hBridge++;
            hPort = (int)(60 * scale); // need %2
            if (hPort % 2 != 0) hPort++;
            hLink = (int)(10 * scale); // need %2
            if (hLink % 2 != 0) hLink++;

            foreach (var (ib, ps) in bridges)
            {
                foreach (var (ip, p) in ps.ports)
                {
                    EditPosPort(ib,ip,p.x,p.y);
                }
            }
        }


        public void Print(Graphics g, Net net)
        {
            Update(net);

            foreach (var (i, b) in net.bridges)
            {
                PrintBridge(g, b);
            }

            foreach (var (idl, l) in net.links)
            {
                PrintLink(g, net, l);
            }
            //PrintLinkLists(g, net);


            foreach (var (i, b) in net.bridges)
            {
                foreach (var (j, p) in b.ports)
                {
                    PrintPort(g, b.id, p, !net.isCompleted);
                }
            }

        }

        public void PrintBridge(Graphics g, Bridge bridge)
        {
            int penWeight = 5;
            Pen pen = new Pen(Color.Black, penWeight);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            if (bridge.status == 1)
            {
                pen = new Pen(Color.Green, penWeight);
                drawBrush = new SolidBrush(Color.Green);
            }

            String drawString = $"{bridge.id} ({bridge.priority.ToString("X")})";
            Font drawFont = new Font("Arial", 14);

            int bx = bridges[bridge.id].x;
            int by = bridges[bridge.id].y;
            Rectangle rect = new Rectangle(bx - hBridge/2, by - hBridge/2, hBridge, hBridge);
            g.DrawRectangle(pen, rect);
            var stringSize = g.MeasureString(drawString, drawFont);
            g.DrawString(drawString, drawFont, drawBrush, bx - (int)(stringSize.Width / 2), by - (int)(stringSize.Height / 2));
        }

        public void PrintPort(Graphics g, int idBridge, Port port, bool modeling)
        {
            int penWeight = 3;
            Pen pen = new Pen(Color.Black, penWeight);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            if (port.status == 1)
            {
                pen = new Pen(Color.Green, penWeight);
                drawBrush = new SolidBrush(Color.Green);
            }
            else if (port.status == 2)
            {
                pen = new Pen(Color.Blue, penWeight);
                drawBrush = new SolidBrush(Color.Blue);
            }
            else if (port.status == 3)
            {
                pen = new Pen(Color.Red, penWeight);
                drawBrush = new SolidBrush(Color.Red);
            }

            if (port.statusPrint == 1)
            {
                pen = new Pen(Color.Orange, penWeight);
                drawBrush = new SolidBrush(Color.Orange);
            }
            else if (port.statusPrint == 2)
            {
                pen = new Pen(Color.FromArgb(181, 0, 181), penWeight);
                drawBrush = new SolidBrush(Color.FromArgb(181, 0, 181));
            }

            myPoint point = bridges[idBridge].ports[port.number];
            int px = point.x;
            int py = point.y;
            Rectangle rect = new Rectangle(px - hPort / 2, py - hPort / 2, hPort, hPort);
            g.DrawRectangle(pen, rect);


            
            string memory = (port.memory == Int32.MaxValue ? "inf" : port.memory.ToString());
            String drawNumber = $"{port.number}";
            String drawMemory = $"{memory}";
            Font drawFont = new Font("Arial", 8, FontStyle.Bold);

            g.DrawString(drawNumber, drawFont, drawBrush, px - hPort / 2, py - hPort / 2);

            var stringSize = g.MeasureString(drawMemory, drawFont);
            if (modeling)
            {
                g.DrawString(drawMemory, drawFont, drawBrush, px + hPort / 2 - stringSize.Width, py - hPort / 2);
            }
            else
            {
                g.DrawString(drawMemory, drawFont, drawBrush, px - (int)(stringSize.Width/2), py - (int)(stringSize.Height/2));
            }
                

            if (modeling)
            {

                string progMemory = (port.progMemory == Int32.MaxValue ? "inf" : port.progMemory.ToString());
                String drawProgMemory = $"{progMemory}";
                var stringSize2 = g.MeasureString(drawProgMemory, drawFont);


                if (port.statusPrint == 1 && port.status != 1)
                {
                    var drawBrush1 = new SolidBrush(Color.Green);
                    if (port.progMemory > port.memory)
                        drawBrush1 = new SolidBrush(Color.Red);

                    g.DrawString(drawProgMemory, drawFont, drawBrush1, px + hPort / 2 - stringSize2.Width, py - hPort / 2 + stringSize.Height);
                }
                else if (port.status == 1)
                {
                    String text = "root";
                    var stringSizeText = g.MeasureString(text, drawFont);
                    g.DrawString(text, drawFont, new SolidBrush(Color.Green), px - hPort / 2 , py + hPort / 2 - stringSizeText.Height);

                    g.DrawString(drawProgMemory, drawFont, drawBrush, px + hPort / 2 - stringSize2.Width, py - hPort / 2 + stringSize.Height);
                }
                else
                {
                    g.DrawString(drawProgMemory, drawFont, drawBrush, px + hPort / 2 - stringSize2.Width, py - hPort / 2 + stringSize.Height);
                }

                string prevMemory = (port.prevMemory == Int32.MaxValue ? "inf" : port.prevMemory.ToString());
                String drawPrevMemory = $"{prevMemory}";
                var stringSize3 = g.MeasureString(drawPrevMemory, drawFont);
                g.DrawString(drawPrevMemory, drawFont, drawBrush, px + hPort / 2 - stringSize3.Width, py + hPort / 2 - stringSize3.Height);
            }
        }

        public static void LinkEnding(int xl, int yl, ref int xp, ref int yp)
        {
            double k;
            double deltax = (xl - xp);
            double deltay = (yl - yp);
            if (deltax == 0)
            {
                //deltax = 0.0001;
                k = 99999;
            }
            else if (deltay == 0)
            {
                //deltay = 0.0001;
                k = 0.00001;
            }
            else
            {
                k = deltay / deltax;
            }
            

            

            if (Math.Abs(k) <= 1)
            {
                xp = (int)(xp + Math.Sign(deltax) * hPort / 2);
                yp = (int)(yp + Math.Sign(deltay) * Math.Abs(k) * hPort / 2);
            }
            else
            {
                xp = (int)Math.Round(xp + Math.Sign(deltax) * Math.Abs(1/k) * hPort / 2);
                yp = (int)(yp + Math.Sign(deltay) * hPort / 2);
            }
        }

        public void PrintLink(Graphics g, Net net, Link link)
        {

            Pen pen = new Pen(Color.FromArgb(190, 190, 190), (int)(hLink/2));

            int lx, ly;
            if (links[link.id].ports.Count == 2)
            {    
                var ps1 = links[link.id].ports.ElementAt(0);
                var p1 = ps1.Value;
                var ps2 = links[link.id].ports.ElementAt(1);
                var p2 = ps2.Value;
                lx = links[link.id].x = (int)((p1.x + p2.x) / 2);
                ly = links[link.id].y = (int)((p1.y + p2.y) / 2);

                Pen penCopy = (Pen)pen.Clone();
                if (net.bridges[ps1.Key].ports[p1.id].statusArrow > 0)
                {
                    penCopy.CustomStartCap = new AdjustableArrowCap(4, 7);
                }
                if (net.bridges[ps2.Key].ports[p2.id].statusArrow > 0)
                {
                    penCopy.CustomEndCap = new AdjustableArrowCap(4, 7);
                }

                int p1x = p1.x;
                int p1y = p1.y;
                int p2x = p2.x;
                int p2y = p2.y;

                LinkEnding(lx, ly, ref p1x, ref p1y);
                LinkEnding(lx, ly, ref p2x, ref p2y);

                g.DrawLine(penCopy, p1x, p1y, p2x, p2y);

            }
            else
            {
                lx = links[link.id].x;
                ly = links[link.id].y;
                //Pen penCopy = pen;
                foreach (var (i, p) in links[link.id].ports)
                {
                    Pen penCopy = (Pen)pen.Clone();
                    if (net.bridges[i].ports[p.id].statusArrow > 0)
                    {
                        penCopy.CustomEndCap = new AdjustableArrowCap(4, 7);
                    }


                    int px = p.x;
                    int py = p.y;

                    LinkEnding(lx, ly, ref px, ref py);

                    g.DrawLine(penCopy, lx, ly, px, py);

                }

                Rectangle rect = new Rectangle(lx - hLink / 2, ly - hLink / 2, hLink, hLink);
                g.DrawRectangle(pen, rect);
            }

            String drawString = link.weight.ToString();
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.DrawString(drawString, drawFont, drawBrush, lx, ly);
        }

    }
}
