using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Maze.ClassesAndEnums;

namespace Maze
{
    public partial class MainWindow : Form
    {
        Thread thread;
        int width, height;
        Point location;
        MazeGenerator generator;
        Graphics graphics;
        bool threadStarted = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            width = Width;
            height = Height;
            location = Location;
            graphics = Surface.CreateGraphics();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Width = width;
            Height = height;
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            Location = location;
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            
            graphics.Clear(Color.LightGray);
            int stackSize = 1024 * 1024 * 64;
            generator = new MazeGenerator(new Point(Surface.Location.X + 10, Surface.Location.Y + 10), 600, 600, (int)NUDSize.Value);

            thread = new Thread(() =>
            {
                generator.Generate(0);
            }, stackSize);


            thread.Start();
            thread.Join();
            thread.Abort();

            generator.Draw(graphics);
            
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
