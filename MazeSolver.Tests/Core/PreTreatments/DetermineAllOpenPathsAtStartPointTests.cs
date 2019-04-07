using System.Linq;
using IO.Utilities;
using MazeSolver.Core.PreTreatments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeSolver.Tests.Core.PreTreatments
{
    [TestClass]
    public class DetermineAllOpenPathsAtStartPointTests
    {
        [TestMethod, Owner("Matthew Moore")]
        public void PreTreatMazeToSolve_SuperSimpleMaze_NoStartPointCellsHaveFourOpenPaths()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMaze.png";
            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            var mazeToPreTreat = mazeReader.ReadInMazeImage();

            Assert.AreEqual(mazeToPreTreat.MazeGridpoints
                .Where(m => m.Value.IsStartPoint)
                .Count(g => g.Value.DirectionsAvailable.Count == 4), 6);

            var preTreatment = new DetermineAllOpenPathsAtStartPoint(mazeToPreTreat);
            preTreatment.PreTreatMazeToSolve();

            Assert.AreEqual(mazeToPreTreat.MazeGridpoints
                .Where(m => m.Value.IsStartPoint)
                .Count(g => g.Value.DirectionsAvailable.Count != 4), 5);
        }
    }
}
