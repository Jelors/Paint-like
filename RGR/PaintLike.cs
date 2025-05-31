using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RGR
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        private Graphics graphics;
        private ShapeType currentShape = ShapeType.None;
        private Pen pen = new Pen(Color.Black, 3f);

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

            foreach (var circle in circles)
                circle.Draw(graphics);

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
            foreach (var circle in circles)
            {
                if (circle.Contains(e.Location))
                {
                    selectedCircle = circle;
                    offset = new Point(e.X - (int)circle.Rect.X, e.Y - (int)circle.Rect.Y);
                    return;
                }
            }
            isMousePressed = true;
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isMousePressed = false;
            selectedShape = null;
            selectedCircle = null;
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
                if (selectedCircle != null)
                {
                    selectedCircle.Rect = new RectangleF(e.X - offset.X, e.Y - offset.Y,
                                                         selectedCircle.Rect.Width, selectedCircle.Rect.Height);
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

            int w = currentShape == ShapeType.Square ? 100 :
                    currentShape == ShapeType.Rectangle ? 200 : 100;
            int h = 100;

            if (currentShape == ShapeType.Circle)
            {
                circles.Add(new Circle(new RectangleF(e.X, e.Y, w, h), pen.Color));
            }
            else
            {
                shapes.Add(new Shape(currentShape, new Rectangle(e.X, e.Y, w, h), pen.Color));
            }

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
            circles.Clear();
            graphics.Clear(pictureBox1.BackColor);
            pictureBox1.Image = bitmap;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }

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

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }
    }
}
