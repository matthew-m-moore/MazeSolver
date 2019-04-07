using System;
using System.Linq;
using IO.Utilities;
using MazeSolver.Core.PreTreatments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeSolver.Tests.Core.PreTreatments
{
    [TestClass]
    public class RemoveAllEasilyIdentifiableDeadEndsTests
    {
        [TestMethod, Owner("Matthew Moore")]
        public void PreTreatMazeToSolve_UltraTinyMaze_TwentyNineDeadEndPointsAreMarked()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\UltraTinyMaze.png";
            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            var mazeToPreTreat = mazeReader.ReadInMazeImage();

            var preTreatmentOne = new DetermineAllOpenPaths(mazeToPreTreat);
            preTreatmentOne.PreTreatMazeToSolve();

            var preTreatmentTwo = new RemoveAllEasilyIdentifiableDeadEnds(mazeToPreTreat);
            preTreatmentTwo.PreTreatMazeToSolve();

            var deadEndsMarked = mazeToPreTreat.MazeGridpoints.Values.Count(m => m.HasBeenVisited);

            Assert.AreEqual(deadEndsMarked, 29);
        }
    }
}
