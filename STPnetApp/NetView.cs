﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using STPnet;

namespace STPnetApp
{
    public class PointStruct
    {
        public int x;
        public int y;
        public Dictionary<int, Point> ports;
    }
    public class Point
    {
        public int id;
        public int x;
        public int y;
    }
    public class NetView
    {
        public Dictionary<int, PointStruct> bridges;
        public Dictionary<int, PointStruct> links;
        public int hBridge = 200;
        public int hPort = 40;
        public int hLink = 10;

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
            //edit pos for ports
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
            PointStruct ps = new();
            ps.ports.Add(idb, p);
            links.Add(id, ps);
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

        public void UpdateLinks()
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
                AddBridge(bridge.id, 400, 400);
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
            Rectangle rect = new Rectangle(bx - hBridge/2, by - hBridge/2, hBridge, hBridge);
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
            Rectangle rect = new Rectangle(px - hPort/2, py - hPort/2, hPort, hPort);
            g.DrawRectangle(pen, rect);
            g.DrawString(drawString, drawFont, drawBrush, px, py);
        }

        public void PrintLink(Graphics g, Link link)
        {
            Pen pen = new Pen(Color.Gray, (int)(hLink/2));

            if (!links.ContainsKey(link.id))
            {
                var pair1 = link.connections.ElementAt(0);
                var pair2 = link.connections.ElementAt(1);

                if (pair1.Key.ports[pair1.Value].LinkId == -1 || pair2.Key.ports[pair2.Value].LinkId == -1) return;

                Point p1 = bridges[pair1.Key.id].ports[pair1.Value];
                p1.id = pair1.Value;
                Point p2 = bridges[pair2.Key.id].ports[pair2.Value];
                p2.id = pair2.Value;
                AddLink(link.id, pair1.Key.id, p1, pair2.Key.id, p2);

                int count = link.connections.Count;
                for (int i = 2; count > i; i++)
                {
                    var pair = link.connections.ElementAt(i-1);
                    if (pair.Key.ports[pair.Value].LinkId == -1) continue;
                    Point p = bridges[pair.Key.id].ports[pair.Value];
                    p.id = pair.Value;
                    AddConnectionLink(link.id, pair.Key.id, p);
                }
            }

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
