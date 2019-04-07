using System;
using System.Linq;
using IO.Utilities;
using MazeSolver.Core.PreTreatments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeSolver.Tests.Core.PreTreatments
{
    [TestClass]
    public class RemoveRedundantWhiteSpaceTests
    {
        [TestMethod, Owner("Matthew Moore")]
        public void PreTreatMazeToSolve_SuperSimpleMaze_RedundantWhiteSpaceIsRemoved()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMaze.png";
            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            var mazeToPreTreat = mazeReader.ReadInMazeImage();

            Assert.AreEqual(mazeToPreTreat.MazeGridpoints.Values.Count(m => m.HasBeenVisited), 0);

            var preTreatment = new RemoveRedundantWhiteSpace(mazeToPreTreat);
            preTreatment.PreTreatMazeToSolve();

            Assert.AreEqual(mazeToPreTreat.MazeGridpoints.Values.Count(m => m.HasBeenVisited), 710);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void PreTreatMazeToSolve_AllWhiteSpaceMaze_RedundantWhiteSpaceIsRemoved()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\AllWhiteSpaceMaze.png";
            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            var mazeToPreTreat = mazeReader.ReadInMazeImage();

            Assert.AreEqual(mazeToPreTreat.MazeGridpoints.Values.Count(m => m.HasBeenVisited), 0);

            var preTreatment = new RemoveRedundantWhiteSpace(mazeToPreTreat);
            preTreatment.PreTreatMazeToSolve();

            Assert.AreEqual(mazeToPreTreat.MazeGridpoints.Values.Count(m => m.HasBeenVisited), 144);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void PreTreatMazeToSolve_SuperSimpleMaze_RedundantWhiteSpaceIsRemovedThenDeadEndsAreFound()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMaze.png";
            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            var mazeToPreTreat = mazeReader.ReadInMazeImage();

            var preTreatmentOne = new RemoveAllEasilyIdentifiableDeadEnds(mazeToPreTreat);
            preTreatmentOne.PreTreatMazeToSolve();

            Assert.AreEqual(mazeToPreTreat.MazeGridpoints.Values.Count(m => m.HasBeenVisited), 0);

            var preTreatmentTwo = new RemoveRedundantWhiteSpace(mazeToPreTreat);
            preTreatmentTwo.PreTreatMazeToSolve();

            var gridpointsRemovedAsRedundant = mazeToPreTreat.MazeGridpoints.Values.Count(m => m.HasBeenVisited);
            Assert.AreEqual(gridpointsRemovedAsRedundant, 710);

            var preTreatmentThree = new DetermineAllOpenPaths(mazeToPreTreat);
            preTreatmentThree.PreTreatMazeToSolve();

            var preTreatmentFour = new RemoveAllEasilyIdentifiableDeadEnds(mazeToPreTreat);
            preTreatmentFour.PreTreatMazeToSolve();

            Assert.IsTrue(mazeToPreTreat.MazeGridpoints.Values.Count(m => m.HasBeenVisited) > gridpointsRemovedAsRedundant);
        }
    }
}
