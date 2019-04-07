using MazeSolver.Common;

namespace MazeSolver.Core.MazeElements
{
    public class MazeSolutionElement
    {
        public int StepNumber { get; set; }
        public MazeGridpoint MazeGridpoint { get; set; }
        public MazeGridpoint ParentMazeGridpoint { get; set; }
        public DirectionEnum DirectionProceeded { get; set; }
    }
}
