using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeGenerator
{
    public partial class Form1 : Form
    {
        int width, height;
        Point location;

        private void Form1_Load(object sender, EventArgs e)
        {
            width = Width;
            height = Height;
            location = Location;
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

        public Form1()
        {
            InitializeComponent();
        }
    }
}
