using System;
using IO.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IO.Tests.Utilities
{
    [TestClass]
    public class MazeReaderUtilityTests
    {
        [TestMethod, Owner("Matthew Moore")]
        [ExpectedException(typeof(Exception))]
        public void ReadInMazeImage_ColorsAreInvalid_ThrowsException()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SmileyFace.png";
            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            mazeReader.ReadInMazeImage();
        }

        [TestMethod, Owner("Matthew Moore")]
        [ExpectedException(typeof(Exception))]
        public void ReadInMazeImage_ColorsAreUnsupportedShadeOfRed_ThrowsException()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMazePinkStart.png";
            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            mazeReader.ReadInMazeImage();
        }

        [TestMethod, Owner("Matthew Moore")]
        [ExpectedException(typeof(Exception))]
        public void ReadInMazeImage_ColorsAreUnsupportedShadeOfBlue_ThrowsException()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMazePurpleEnd.png";
            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            mazeReader.ReadInMazeImage();
        }

        [TestMethod, Owner("Matthew Moore")]
        [ExpectedException(typeof(Exception))]
        public void ReadInMazeImage_MazeHasNoStartingPoint_ThrowsException()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMazeNoStart.png";
            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            mazeReader.ReadInMazeImage();
        }

        [TestMethod, Owner("Matthew Moore")]
        [ExpectedException(typeof(Exception))]
        public void ReadInMazeImage_MazeHasNoFinishPoint_ThrowsException()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMazeNoEnd.png";
            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            mazeReader.ReadInMazeImage();
        }

        [TestMethod, Owner("Matthew Moore")]
        public void ReadInMazeImage_SuperSimpleMaze_MazeGridpointsMatchHeightTimesWidth()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMaze.png";
            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            var maze = mazeReader.ReadInMazeImage();

            var mazeHeight = maze.MazeHeight;
            var mazeWidth = maze.MazeWidth;

            Assert.AreEqual(maze.MazeGridpoints.Count, mazeHeight * mazeWidth);
        }
    }
}
