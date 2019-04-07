namespace MazeSolver.Core.MazeElements
{
    public class MazeSolutionElementTree
    {
        public MazeSolutionElementTree ParentSolutionElement { get; set; }
        public MazeGridpoint MazeGridpoint { get; set; }

        /// <summary>
        /// Returns the depth of the existing tree.
        /// </summary>
        public int TreeDepth
        {
            get
            {
                if (ParentSolutionElement == null)
                {
                    return 1;
                }

                return ParentSolutionElement.TreeDepth + 1;
            }
        }
    }
}
