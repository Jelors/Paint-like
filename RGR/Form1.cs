using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RGR
{
    public enum ShapeType { None, Square, Rectangle, Circle }
    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        private Graphics graphics;
        private Pen pen = new Pen(Color.Black, 3f);
        private ShapeType currentShape = ShapeType.None;

        private List<Shape> shapes = new List<Shape>();
        private Shape selectedShape = null;

        private List<Circle> circles = new List<Circle>();
        private Circle selectedCircle = null;

        private Point offset;
        private bool isMousePressed = false;
        private ArrayPoints arrayPoints = new ArrayPoints();
        public Form1()
        {
            InitializeComponent();
            SetSize();
        }

        private class ArrayPoints
        {
            private List<Point> points = new List<Point>();
            public void SetPoint(int x, int y) => points.Add(new Point(x, y));
            public void ResetPoints() => points.Clear();
            public int GetCountOfPoints() => points.Count;
            public Point[] GetPoints() => points.ToArray();
        }
        #region Other things
        private void SetSize()
        {
            Rectangle rect = Screen.PrimaryScreen.Bounds;
            bitmap = new Bitmap(rect.Width, rect.Height);
            graphics = Graphics.FromImage(bitmap);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

        }
        private void RedrawAll()
        {
            graphics.Clear(pictureBox1.BackColor);
            foreach (var shape in shapes)
                shape.Draw(graphics);

            pictureBox1.Image = bitmap;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region PicturebBox
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (var shape in shapes)
            {
                if (shape.Contains(e.Location))
                {
                    selectedShape = shape;
                    offset = new Point(e.X - shape.Rect.X, e.Y - shape.Rect.Y);
                    return;
                }
            }
            isMousePressed = true;
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isMousePressed = false;
            selectedShape = null;
            arrayPoints.ResetPoints();
        }
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMousePressed)
            {
                if (selectedShape != null)
                {
                    selectedShape.Rect = new Rectangle(e.X - offset.X, e.Y - offset.Y,
                                                       selectedShape.Rect.Width, selectedShape.Rect.Height);
                    RedrawAll();
                }
                else
                {
                    arrayPoints.SetPoint(e.X, e.Y);
                    if (arrayPoints.GetCountOfPoints() >= 2)
                    {
                        graphics.DrawLines(pen, arrayPoints.GetPoints());
                        pictureBox1.Image = bitmap;
                    }
                }
            }
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (currentShape == ShapeType.None)
                return;

            int w = currentShape == ShapeType.Square ? 100 : 200;
            int h = 100;

            shapes.Add(new Shape(currentShape, new Rectangle(e.X, e.Y, w, h), pen.Color));
            RedrawAll();
        }
        #endregion
        #region Вибір кольору
        private void button2_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }
        #endregion

        // Очищення
        private void button1_Click(object sender, EventArgs e)
        {
            shapes.Clear();
            graphics.Clear(pictureBox1.BackColor);
            pictureBox1.Image = bitmap;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;

        }

        // TODO: доробить створення і спавн фігур, зробить шоб не можна було одночасно
        // створити декілька екземплярів фігур, після натискання різних функціональних клавіш,
        // розбить весь код на частини для кращої читабельності. Last Change: 9/5/2025

        // Квадрат
        private void button11_Click(object sender, EventArgs e)
        {
            currentShape = ShapeType.Square;
        }

        // Прямокутник
        private void button12_Click(object sender, EventArgs e)
        {
            currentShape = ShapeType.Rectangle;
        }

        // Пензлик
        private void button13_Click(object sender, EventArgs e)
        {
            currentShape = ShapeType.None;
        }

        // Коло
        private void button14_Click(object sender, EventArgs e)
        {
            currentShape = ShapeType.Circle;
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {
            currentShape = ShapeType.Circle;
        }


    }
    public class Shape
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
        public ShapeType Type;
        public RectangleF Rect;
        public Color Color;

        public Circle(ShapeType type, RectangleF rect, Color color)
        {
            Type = type;
            Rect = rect;
            Color = color;
        }
        public  void DrawCircle(Graphics g)
        {
            using (Pen p = new Pen(Color, 3))
            {
                g.DrawEllipse(p, Rect);
            }
        }
    }
    // TODO: Створення круга, трикутника. В теорії заповнення фігур кольором;
    // Трошки змінити дизайн, і спробувати пофіксить малювання на максимальному розмірі вікна(optional).
    // Last change: 23/05/2025
}
