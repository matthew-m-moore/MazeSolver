using MazeSolver.Common;

namespace MazeSolver.Core.MazeElements
{
    public class MazeGridpoint
    {
        public CartesianCoordinate Position { get; set; }
        public DirectionsAvailable DirectionsAvailable { get; set; }

        public bool IsStartPoint { get; }
        public bool IsFinishPoint { get; }
        public bool IsWall { get; }

        public bool IsOnSolutionPath { get; set; }
        public bool HasBeenVisited { get; set; }

        public MazeGridpoint(
            CartesianCoordinate position, 
            DirectionsAvailable directions, 
            bool isStartPoint, 
            bool isFinishPoint,
            bool isWall)
        {
            Position = position;
            DirectionsAvailable = directions;

            IsStartPoint = isStartPoint;
            IsFinishPoint = isFinishPoint;

            IsWall = isWall;
        }
    }
}
