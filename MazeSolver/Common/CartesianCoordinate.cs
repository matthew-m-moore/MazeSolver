using System;

namespace MazeSolver.Common
{
    public class CartesianCoordinate
    {
        public int X { get; }
        public int Y { get; }

        public CartesianCoordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Return coordinates corresponding to moving in the direction chosen where the origin of
        /// the cartesian coordinates (i.e. (0, 0)) is situated in the upper-left corner of a grid, 
        /// as in a Bitmap image.
        /// </summary>
        public CartesianCoordinate MoveInDirection(DirectionEnum direction)
        {
            var xPosition = X;
            var yPosition = Y;

            if (direction == DirectionEnum.Up)
            {
                yPosition--;
            }
            if (direction == DirectionEnum.Down)
            {
                yPosition++;
            }
            if (direction == DirectionEnum.Right)
            {
                xPosition++;
            }
            if (direction == DirectionEnum.Left)
            {
                xPosition--;
            }

            var newCoordinates = new CartesianCoordinate(xPosition, yPosition);

            return newCoordinates;
        }

        /// <summary>
        /// Returns the distance between this coordinate and any coordiate provided.
        /// </summary>
        public double Distance(CartesianCoordinate coordinate)
        {
            var x1 = X;
            var x2 = coordinate.X;
            var y1 = Y;
            var y2 = coordinate.Y;

            var distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            return distance;
        }

        public override bool Equals(object obj)
        {
            // This is a safe caste, it will return null if the object cannot be caste to this type
            var coordinate = obj as CartesianCoordinate;
            if (coordinate == null)
            {
                return false;
            }

            return (X == coordinate.X)
                && (Y == coordinate.Y);
        }

        public override int GetHashCode()
        {
            // Thirteen is my lucky number, since my first, last, and middle names start with "M", which is the thirteenth letter of the alphabet
            var primeNumberOne = 13;
            var primeNumberTwo = 31; //It's just 13 backwards ;)

            // This may exceed the value of an integer, so don't bother checking it
            unchecked
            {
                var hashValue = primeNumberOne;

                hashValue = (hashValue * primeNumberTwo) + X.GetHashCode();
                hashValue = (hashValue * primeNumberTwo) + Y.GetHashCode();

                return hashValue;
            }

            // This is basically how C# natively makes hash codes
        }

        public static bool operator ==(CartesianCoordinate coordinateA, CartesianCoordinate coordinateB)
        {
            if ((object) coordinateA == null || (object) coordinateB == null)
            {
                return false;
            }

            return (coordinateA.X == coordinateB.X)
                && (coordinateA.Y == coordinateB.Y);
        }

        public static bool operator !=(CartesianCoordinate coordinateA, CartesianCoordinate coordinateB)
        {
            return !(coordinateA == coordinateB);
        }
    }
}
