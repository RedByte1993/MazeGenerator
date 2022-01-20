using System.Drawing;

namespace Maze.ClassesAndEnums
{
    public class Cell
    {
        #region Members
        private bool[] walls;
        #endregion
        #region Properties
        public bool[] Walls => walls;
        public int Width { get; set; }
        public int Height { get; set; }
        public Point Location { get; set; }
        #endregion
        #region Ctor
        public Cell(bool upperWall = false, 
                    bool leftWall = false, 
                    bool rightWall = false, 
                    bool lowerWall = false)
        {
            Location = new Point(0, 0);
            Width = 0;
            Height = 0;
            walls = new bool[4];
            walls[(int)Direction.UP] = upperWall;
            walls[(int)Direction.LEFT] = leftWall;
            walls[(int)Direction.RIGHT] = rightWall;
            walls[(int)Direction.DOWN] = lowerWall;
        }
        public Cell(Point location,
                    int width, 
                    int height,
                    bool upperWall = false,
                    bool leftWall = false,
                    bool rightWall = false,
                    bool lowerWall = false)
        {
            Location = location;
            Width = width;
            Height = height;
            walls = new bool[4];
            walls[(int)Direction.UP] = upperWall;
            walls[(int)Direction.LEFT] = leftWall;
            walls[(int)Direction.RIGHT] = rightWall;
            walls[(int)Direction.DOWN] = lowerWall;
        }
        #endregion
        #region Methodes
        public void Draw(Graphics g)
        {
            #region UP
            if (Walls[(int)Direction.UP])
            {
                g.DrawLine(new Pen(Color.Black, 2), Location, new Point(Location.X + Width, Location.Y));
            }
            #endregion
            #region LEFT
            if (Walls[(int)Direction.LEFT])
            {
                g.DrawLine(new Pen(Color.Black, 2), Location, new Point(Location.X, Location.Y + Height));
            }
            #endregion
            #region RIGHT
            if (Walls[(int)Direction.RIGHT])
            {
                g.DrawLine(new Pen(Color.Black, 2), new Point(Location.X + Width, Location.Y), new Point(Location.X + Width, Location.Y + Height));
            }
            #endregion
            #region DOWN
            if (Walls[(int)Direction.DOWN])
            {
                g.DrawLine(new Pen(Color.Black, 2), new Point(Location.X, Location.Y + Height), new Point(Location.X + Width, Location.Y + Height));
            }
            #endregion
        }
        #endregion
    }
}
