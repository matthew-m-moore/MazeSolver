using System.Linq;
using IO.Utilities;
using MazeSolver.Core.PreTreatments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeSolver.Tests.Core.PreTreatments
{
    [TestClass]
    public class DetermineAllOpenPathsTests
    {
        [TestMethod, Owner("Matthew Moore")]
        public void PreTreatMazeToSolve_UltraTinyMaze_CellsHaveFourOpenPaths()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\UltraTinyMaze.png";
            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            var mazeToPreTreat = mazeReader.ReadInMazeImage();

            Assert.IsTrue(mazeToPreTreat.MazeGridpoints
                .All(g => g.Value.DirectionsAvailable.Count == 4));

            var preTreatment = new DetermineAllOpenPaths(mazeToPreTreat);
            preTreatment.PreTreatMazeToSolve();

            Assert.IsTrue(mazeToPreTreat.MazeGridpoints
                .All(g => g.Value.DirectionsAvailable.Count != 4));
        }
    }
}
