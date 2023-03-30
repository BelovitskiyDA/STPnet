using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using STPnet;

namespace STPnetApp
{
    class PointStruct
    {
        public int x;
        public int y;
        public Dictionary<int, Point> ports;
    }
    public class Point
    {
        public int x;
        public int y;
    }
    class NetView
    {
        public Dictionary<int, PointStruct> bridges;
        public Dictionary<int, PointStruct> links;
        public int hBridge = 40;
        public int hPort = 5;
        public int hLink = 2;

        public NetView()
        {
            bridges = new Dictionary<int, PointStruct>();
            links = new Dictionary<int, PointStruct>();
        }

        public static int Funct(int x, double k, double b)
        {
            return (int)(k * x + b);
        }
        public static int FunctReverse(int y, double k, double b)
        {
            return (int)((y - b)/k);
        }

        public static void FunctParam(int x1, int x2, int y1, int y2, out double k, out double b)
        {
            k = (double)(y2 - y1) / (x2 - x1);
            b = y1 - k * x1;
        }
        public void Find(int x, int y, out int type, out int id)
        {
            type = 0;
            id = 0;

            foreach (var (i, ps) in bridges)
            {
                if ((x > ps.x - hBridge) && (x < ps.x + hBridge) && (y > ps.y - hBridge) && (y < ps.y + hBridge))
                {
                    type = 1;
                    id = i;
                    foreach (var (j, p) in ps.ports)
                    {
                        if ((x > p.x - hPort) && (x < p.x + hPort) && (y > p.y - hPort) && (y < p.y + hPort))
                        {
                            type = 2;
                            id = j;
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
                    FunctParam(x1, y1, x2, y2, out double k, out double b);

                    if (Math.Abs(x-Funct(x,k,b))<hLink || Math.Abs(y - FunctReverse(y, k, b)) < hLink)
                    {
                        type = 3;
                        id = i;
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
                p.x = p.x + (oldx - x);
                p.y = p.y + (oldy - y);
            }
            //edit pos for ports
        }
        public void AddPort(int idBridge, int idPort, Point p)
        {
            int x = p.x;
            int y = p.y;
            if (!bridges.ContainsKey(idBridge)) return;
            int bx = bridges[idBridge].x;
            if (Math.Abs(x - bx)>(hBridge-hPort))
            {
                x = (int)(bx + (x - bx) / (Math.Abs(x - bx)) * (hBridge - hPort));
            }
            int by = bridges[idBridge].y;
            if (Math.Abs(y - by) > (hBridge - hPort))
            {
                y = (int)(by + (y - by) / (Math.Abs(y - by)) * (hBridge - hPort));
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
            if (Math.Abs(x - bx) > (hBridge - hPort))
            {
                x = (int)(bx + (x - bx) / (Math.Abs(x - bx)) * (hBridge - hPort));
            }
            int by = bridges[idBridge].y;
            if (Math.Abs(y - by) > (hBridge - hPort))
            {
                y = (int)(by + (y - by) / (Math.Abs(y - by)) * (hBridge - hPort));
            }

            bridges[idBridge].ports[idPort].x = x;
            bridges[idBridge].ports[idPort].y = y;
        }
        public void AddLink(int id, int idp1, Point p1, int idp2, Point p2)
        {
            PointStruct ps = new();
            ps.x = (int)((p1.x+p2.x)/2);
            ps.y = (int)((p1.y + p2.y)/2);
            ps.ports = new Dictionary<int, Point>();
            ps.ports.Add(idp1, p1);
            ps.ports.Add(idp2, p2);
            links.Add(id, ps);
        }
        public void DeleteLink(int id)
        {
            if (!links.ContainsKey(id)) return;
            links.Remove(id);
        }

        public void AddConnectionLink(int id, int idp, Point p)
        {
            PointStruct ps = new();
            ps.ports.Add(idp, p);
            links.Add(id, ps);
        }

        public void DeleteConnectionLink(int id, int idp)
        {
            if (!links.ContainsKey(id)) return;
            if (!links[id].ports.ContainsKey(idp)) return;
            links[id].ports.Remove(idp);
        }

        public void EditPosLink(int id, int x, int y)
        {
            if (!links.ContainsKey(id)) return;
            links[id].x = x;
            links[id].y = y;
        }


        public void Print(Graphics g, Net net)
        {
            foreach(var (i,b) in net.bridges)
            {
                PrintBridge(g, b);
                foreach(var (j,p) in b.ports)
                {
                    PrintPort(g,b.id, p);
                }
            }

            foreach (var (i, l) in net.links)
            {
                PrintLink(g, l);
            }
        }

        public void PrintBridge(Graphics g, Bridge bridge)
        {
            Pen pen = new Pen(Color.Black, 1);
            if (bridge.status == 1)
            {
                pen = new Pen(Color.Green, 1);
            }

            String drawString = bridge.id.ToString();
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            if (!bridges.ContainsKey(bridge.id))
            {
                AddBridge(bridge.id, 10, 10);
                foreach(var (i,p) in bridge.ports)
                {
                    Point point = new Point();
                    point.x = 30;
                    point.y = 30;
                    AddPort(bridge.id, i, point);
                }
            }

            int bx = bridges[bridge.id].x;
            int by = bridges[bridge.id].y;
            Rectangle rect = new Rectangle(bx - hBridge, bx + hBridge, by - hBridge, by + hBridge);
            g.DrawRectangle(pen, rect);
            g.DrawString(drawString, drawFont, drawBrush, bx, by);
        }

        public void PrintPort(Graphics g, int idBridge, Port port)
        {
            Pen pen = new Pen(Color.Black, 1);
            if (port.status == 1)
            {
                pen = new Pen(Color.Green, 1);
            }
            else if (port.status == 2)
            {
                pen = new Pen(Color.Blue, 1);
            }
            else if (port.status == 3)
            {
                pen = new Pen(Color.Red, 1);
            }

            String drawString = port.number.ToString();
            Font drawFont = new Font("Arial", 5);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            Point point = bridges[idBridge].ports[port.number];
            int px = point.x;
            int py = point.y;
            Rectangle rect = new Rectangle(px - hPort, px + hPort, py - hPort, py + hPort);
            g.DrawRectangle(pen, rect);
            g.DrawString(drawString, drawFont, drawBrush, px, py);
        }

        public void PrintLink(Graphics g, Link link)
        {
            Pen pen = new Pen(Color.Black, 1);

            if (!links.ContainsKey(link.id))
            {
                var pair1 = link.connections.ElementAt(0);
                var pair2 = link.connections.ElementAt(1);
                AddLink(link.id, pair1.Value, bridges[pair1.Key.id].ports[pair1.Value], pair2.Value, bridges[pair2.Key.id].ports[pair2.Value]);

                int count = link.connections.Count;
                for (int i = 2; count > i; i++)
                {
                    var pair = link.connections.ElementAt(i-1);
                    AddConnectionLink(link.id, pair.Value, bridges[pair.Key.id].ports[pair.Value]);
                }
            }

            int lx = links[link.id].x;
            int ly = links[link.id].y;
            foreach (var (i,p) in links[link.id].ports)
            {
                g.DrawLine(pen, lx,ly,p.x,p.y);
            }


            Rectangle rect = new Rectangle(lx - hLink, lx + hLink, ly - hLink, ly + hLink);
            g.DrawRectangle(pen, rect);
            String drawString = link.weight.ToString();
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.DrawString(drawString, drawFont, drawBrush, lx, ly);
        }
    }
}
