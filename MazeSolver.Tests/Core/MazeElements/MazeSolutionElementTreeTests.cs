using MazeSolver.Core.MazeElements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeSolver.Tests.Core.MazeElements
{
    [TestClass]
    public class MazeSolutionElementTreeTests
    {
        [TestMethod, Owner("Matthew Moore")]
        public void TreeDepth_TreeHasThreeLayers_ReturnsThree()
        {
            var solutionTree = new MazeSolutionElementTree
            {
                MazeGridpoint = null,
                ParentSolutionElement = new MazeSolutionElementTree
                {
                    MazeGridpoint = null,
                    ParentSolutionElement = new MazeSolutionElementTree
                    {
                        MazeGridpoint = null,
                        ParentSolutionElement = null
                    }
                }
            };

            var treeDepth = solutionTree.TreeDepth;

            Assert.AreEqual(treeDepth, 3);
        }
    }
}
