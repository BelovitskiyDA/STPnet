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

namespace STPnetApp
{
    [Serializable]
    public class PointStruct
    {
        public int x;
        public int y;
        public Dictionary<int, Point> ports;
    }
    [Serializable]
    public class Point
    {
        public int id;
        public int x;
        public int y;
    }
    [Serializable]
    public class NetView
    {
        public Dictionary<int, PointStruct> bridges;
        public Dictionary<int, PointStruct> links;
        public int hBridge = 200; // need %2
        public int hPort = 50; // need %2
        public int hLink = 10; // need %2

        public NetView()
        {
            bridges = new Dictionary<int, PointStruct>();
            links = new Dictionary<int, PointStruct>();
        }

        public void Save(string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, this);
            }
        }

        public NetView Load(string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                NetView nw = (NetView)formatter.Deserialize(fs);
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
            ps.ports = new Dictionary<int, Point>();
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
        public void AddPort(int idBridge, int idPort, Point p)
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
        public void AddLink(int id, int idb1, Point p1, int idb2, Point p2)
        {
            PointStruct ps = new();
            ps.x = (int)((p1.x+p2.x)/2);
            ps.y = (int)((p1.y + p2.y)/2);
            ps.ports = new Dictionary<int, Point>();
            ps.ports.Add(idb1, p1);
            ps.ports.Add(idb2, p2);
            links.Add(id, ps);
        }
        public void DeleteLink(int id)
        {
            if (!links.ContainsKey(id)) return;
            links.Remove(id);
        }

        public void AddConnectionLink(int id, int idb, Point p)
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

        /*public void UpdateLinks()
        {
            foreach (var (i, ps) in links)
            {
                bool flagDelete = true;
                foreach (var (idb,point) in ps.ports)
                {
                    if (!bridges.ContainsKey(idb)) continue;
                    if (!bridges[idb].ports.ContainsKey(point.id)) continue;
                }
                if (flagDelete) links.Remove(i);
            }
        }*/

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
                        b.ports.Remove(idb);
                    }
                }
            }

            foreach (var (i, b) in net.bridges)
            {
                foreach (var (j, p) in b.ports)
                {
                    if (!bridges[i].ports.ContainsKey(j))
                    {
                        Point point = new();
                        point.id = j;
                        point.x = 30;
                        point.y = 30;
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

                    Point p1 = bridges[pair1.Key.id].ports[pair1.Value];
                    p1.id = pair1.Value;
                    Point p2 = bridges[pair2.Key.id].ports[pair2.Value];
                    p2.id = pair2.Value;
                    AddLink(l.id, pair1.Key.id, p1, pair2.Key.id, p2);

                    int count = l.connections.Count;
                    for (int i = 2; count > i; i++)
                    {
                        var pair = l.connections.ElementAt(i - 1);
                        if (pair.Key.ports[pair.Value].Link == null) continue;
                        Point p = bridges[pair.Key.id].ports[pair.Value];
                        p.id = pair.Value;
                        AddConnectionLink(l.id, pair.Key.id, p);
                    }
                }

                /*foreach (var (b, pn) in l.connections)
                {
                    if (!links[l.id].ports.ContainsKey(b.id))
                    {
                        Point p = bridges[b.id].ports[pn];
                        AddConnectionLink(l.id, b.id, p);
                    }
                }*/
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
                PrintLink(g, l);
            }

            foreach (var (i, b) in net.bridges)
            {
                foreach (var (j, p) in b.ports)
                {
                    PrintPort(g, b.id, p);
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
            Font drawFont = new Font("Arial", 20);

            int bx = bridges[bridge.id].x;
            int by = bridges[bridge.id].y;
            Rectangle rect = new Rectangle(bx - hBridge/2, by - hBridge/2, hBridge, hBridge);
            g.DrawRectangle(pen, rect);
            g.DrawString(drawString, drawFont, drawBrush, bx - (int)(hBridge / 4), by - (int)(hBridge / 4));
        }

        public void PrintPort(Graphics g, int idBridge, Port port)
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

            string memory = (port.memory == Int32.MaxValue ? "inf" : port.memory.ToString());
            String drawString = $"{port.number} ({memory})";
            Font drawFont = new Font("Arial", 8, FontStyle.Bold);
            

            Point point = bridges[idBridge].ports[port.number];
            int px = point.x;
            int py = point.y;
            Rectangle rect = new Rectangle(px - hPort/2, py - hPort/2, hPort, hPort);
            g.DrawRectangle(pen, rect);
            g.DrawString(drawString, drawFont, drawBrush, px - hPort / 2, py - hPort / 2);
        }

        public void PrintLink(Graphics g, Link link)
        {
            Pen pen = new Pen(Color.FromArgb(190, 190, 190), (int)(hLink/2));

            int lx, ly;
            if (links[link.id].ports.Count == 2)
            {    
                var l1 = links[link.id].ports.ElementAt(0).Value;
                var l2 = links[link.id].ports.ElementAt(1).Value;
                lx = links[link.id].x = (int)((l1.x + l2.x) / 2);
                ly = links[link.id].y = (int)((l1.y + l2.y) / 2);
                g.DrawLine(pen, l1.x, l1.y, l2.x, l2.y);
            }
            else
            {
                lx = links[link.id].x;
                ly = links[link.id].y;
                foreach (var (i, p) in links[link.id].ports)
                {
                    g.DrawLine(pen, lx, ly, p.x, p.y);
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
