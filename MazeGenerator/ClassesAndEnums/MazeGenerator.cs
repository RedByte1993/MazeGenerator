using System;
using System.Collections.Generic;
using System.Drawing;

namespace Maze.ClassesAndEnums
{
    public class MazeGenerator
    {
        #region Members
        private Cell[] maze;
        private List<int> visitedPos;
        private Stack<int> path;
        private int startPos = 0;
        private int endPos = 0;
        private static Random rnd;
        #endregion
        #region Properties
        public int Width { get; set; }
        public int Height { get; set; }
        public int Size { get; set; }
        public Point Location { get; set; }
        #endregion
        #region Ctor
        public MazeGenerator()
        {
            rnd = new Random();
            visitedPos = new List<int>();
            path = new Stack<int>();
            InitMaze();
            InitStartPosAndEndPos();
        }
        public MazeGenerator(Point location, int width, int height, int size)
        {
            rnd = new Random();
            visitedPos = new List<int>();
            path = new Stack<int>();
            Location = location;
            Width = width;
            Height = height;
            Size = size;

            InitMaze();
            InitStartPosAndEndPos();
        }
        #endregion
        #region Methodes
        private void InitMaze()
        {
            maze = new Cell[Size * Size];
            bool up = true, left = true, right = true, down = true;
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {

                    maze[y * Size + x] = new Cell(up, left, right, down);
                    maze[y * Size + x].Width = Width / Size;
                    maze[y * Size + x].Height = Height / Size;
                    maze[y * Size + x].Location = new Point(10 + Location.X + x * (maze[y * Size + x].Width),
                                                             10 + Location.Y + y * (maze[y * Size + x].Height));
                }
            }
        }
        public void Draw(Graphics graphics)
        {
            for (int i = 0; i < maze.Length; i++)
            {
                maze[i].Draw(graphics);
            }
        }
        private void InitStartPosAndEndPos()
        {
            startPos = 0;
            endPos = Size * Size - 1;
            maze[startPos].Walls[(int)Direction.LEFT] = false;
            maze[endPos].Walls[(int)Direction.RIGHT] = false;
        }
        private List<int> GetNeighbours(int currentPos)
        {
            List<int> neighbours = new List<int>();
            int x = currentPos % Size;
            int y = currentPos / Size;
            #region UP
            if (y - 1 >= 0)
            {
                if (!visitedPos.Contains((y - 1) * Size + (x + 0)))
                {
                    neighbours.Add((y - 1) * Size + (x + 0));
                }
            }
            #endregion
            #region LEFT
            if (x - 1 >= 0)
            {
                if (!visitedPos.Contains((y + 0) * Size + (x - 1)))
                {
                    neighbours.Add((y + 0) * Size + (x - 1));
                }
            }
            #endregion
            #region RIGHT
            if (x + 1 < Size)
            {
                if (!visitedPos.Contains((y + 0) * Size + (x + 1)))
                {
                    neighbours.Add((y + 0) * Size + (x + 1));

                }
            }
            #endregion
            #region Down
            if (y + 1 < Size)
            {
                if (!visitedPos.Contains((y + 1) * Size + (x + 0)))
                {
                    neighbours.Add((y + 1) * Size + (x + 0));
                }
            }
            #endregion
            return neighbours;
        }
        public void Generate(int currentPos)
        {
            List<int> neighbours = GetNeighbours(currentPos);
            if (!visitedPos.Contains(currentPos))
            {
                visitedPos.Add(currentPos);
            }
            path.Push(currentPos);
            int newPos = 0;
            int posX = currentPos % Size, newX = 0, posY = currentPos / Size, newY = 0;

            if (visitedPos.Count == maze.Length)
            {
                return;
            }
            else if (currentPos == endPos)
            {
                int rndPos = 0;
                bool checkForPossiblePassage = false;

                while (!checkForPossiblePassage)
                {
                    rndPos = rnd.Next(0, (int)Math.Pow(Size, 4)) % (Size * Size);
                    while (visitedPos.Contains(rndPos))
                    {
                        rndPos = rnd.Next(0, (int)Math.Pow(Size, 4)) % (Size * Size);
                    }

                    if (visitedPos.Contains(rndPos + 1) && (rndPos % Size) + 1 < Size)
                    {
                        maze[rndPos].Walls[(int)Direction.RIGHT] = false;
                        maze[rndPos + 1].Walls[(int)Direction.LEFT] = false;
                        checkForPossiblePassage = true;
                    }
                    else if (visitedPos.Contains(rndPos - 1) && (rndPos % Size) - 1 >= 0)
                    {
                        maze[rndPos].Walls[(int)Direction.LEFT] = false;
                        maze[rndPos - 1].Walls[(int)Direction.RIGHT] = false;
                        checkForPossiblePassage = true;
                    }
                    else if (visitedPos.Contains(rndPos - Size))
                    {
                        maze[rndPos].Walls[(int)Direction.UP] = false;
                        maze[rndPos - Size].Walls[(int)Direction.DOWN] = false;
                        checkForPossiblePassage = true;
                    }
                    else if (visitedPos.Contains(rndPos + Size))
                    {
                        maze[rndPos].Walls[(int)Direction.DOWN] = false;
                        maze[rndPos + Size].Walls[(int)Direction.UP] = false;
                        checkForPossiblePassage = true;
                    }
                }

                Generate(rndPos);
            }
            else
            {
                if (neighbours.Count > 0)
                {
                    newPos = neighbours[rnd.Next(0, 63) % neighbours.Count];
                    newX = newPos % Size;
                    newY = newPos / Size;

                    if (posX != newX)
                    {
                        //Left
                        if (posX > newX)
                        {
                            maze[currentPos].Walls[(int)Direction.LEFT] = false;
                            maze[newPos].Walls[(int)Direction.RIGHT] = false;
                            neighbours.Clear();
                            Generate(newPos);
                        }
                        //Right
                        else
                        {
                            maze[currentPos].Walls[(int)Direction.RIGHT] = false;
                            maze[newPos].Walls[(int)Direction.LEFT] = false;
                            neighbours.Clear();
                            Generate(newPos);
                        }
                    }
                    else
                    {
                        //Up
                        if (posY > newY)
                        {
                            maze[currentPos].Walls[(int)Direction.UP] = false;
                            maze[newPos].Walls[(int)Direction.DOWN] = false;

                            neighbours.Clear();
                            Generate(newPos);
                        }
                        //Down
                        else
                        {
                            maze[currentPos].Walls[(int)Direction.DOWN] = false;
                            maze[newPos].Walls[(int)Direction.UP] = false;
                            neighbours.Clear();
                            Generate(newPos);
                        }
                    }
                }
                else
                {
                    int prevPos = path.Pop();
                    while (GetNeighbours(prevPos).Count == 0)
                    {
                        prevPos = path.Pop();
                    }
                    Generate(prevPos);
                }
            }
        }
        #endregion
    }
}
