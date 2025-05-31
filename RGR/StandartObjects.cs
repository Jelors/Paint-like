// file that contains standart objects that propose a program
using System;
using System.Drawing;

namespace RGR
{
    public class Shape : Form1
    {
        public ShapeType Type;
        public Rectangle Rect;
        public Color Color;

        public Shape(ShapeType type, Rectangle rect, Color color)
        {
            Type = type;
            Rect = rect;
            Color = color;
        }

        public void Draw(Graphics g)
        {
            using (Pen p = new Pen(Color, 3))
            {
                switch (Type)
                {
                    case ShapeType.Square:
                    case ShapeType.Rectangle:
                        g.DrawRectangle(p, Rect);
                        break;
                }
            }
        }

        public bool Contains(Point p) => Rect.Contains(p);
    }
    public class Circle
    {
        public RectangleF Rect;
        public Color Color;

        public Circle(RectangleF rect, Color color)
        {
            Rect = rect;
            Color = color;
        }

        public void Draw(Graphics g)
        {
            using (Pen p = new Pen(Color, 3))
            {
                g.DrawEllipse(p, Rect);
            }
        }

        public bool Contains(Point p)
        {
            float centerX = Rect.X + Rect.Width / 2;
            float centerY = Rect.Y + Rect.Height / 2;
            float radius = Rect.Width / 2;
            return Math.Pow(p.X - centerX, 2) + Math.Pow(p.Y - centerY, 2) <= radius * radius;
        }
    }
}
