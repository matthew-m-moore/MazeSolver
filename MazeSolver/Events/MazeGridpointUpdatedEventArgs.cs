using System;
using MazeSolver.Core.MazeElements;

namespace MazeSolver.Events
{
    public class MazeGridpointUpdatedEventArgs : EventArgs
    {
        public Maze UpdatedMaze { get; private set; }

        public MazeGridpointUpdatedEventArgs(Maze updatedMaze)
        {
            UpdatedMaze = updatedMaze;
        }
    }
}
