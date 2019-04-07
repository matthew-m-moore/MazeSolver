using System.Collections.Generic;
using System.Linq;

namespace MazeSolver.Common
{
    public class DirectionsAvailable
    {
        private readonly Dictionary<DirectionEnum, bool> _dictionaryOfDirections = new Dictionary<DirectionEnum, bool>();

        public DirectionsAvailable()
        {
            _dictionaryOfDirections[DirectionEnum.Up] = true;
            _dictionaryOfDirections[DirectionEnum.Down] = true;
            _dictionaryOfDirections[DirectionEnum.Left] = true;
            _dictionaryOfDirections[DirectionEnum.Right] = true;
        }

        public bool this[DirectionEnum direction]
        {
            get
            {
                return _dictionaryOfDirections[direction];
            }
            set
            {
                _dictionaryOfDirections[direction] = value;
            }
        }

        /// <summary>
        /// Returns the list of all open paths available.
        /// </summary>
        public List<DirectionEnum> OpenPaths =>
            _dictionaryOfDirections.Where(d => d.Value).Select(l => l.Key).ToList();

        /// <summary>
        /// Returns the count of any open paths available.
        /// </summary>
        public int Count =>
            OpenPaths.Count;
    }
}
