using System;
using MazeSolver.Core.MazeElements;

namespace MazeSolver.Events
{
    public class MazeRedrawnEventArgs : EventArgs
    {
        public Maze MazeToBeRedrawn { get; private set; }

        public MazeRedrawnEventArgs(Maze mazeToBeRedrawn)
        {
            MazeToBeRedrawn = mazeToBeRedrawn;
        }
    }
}
