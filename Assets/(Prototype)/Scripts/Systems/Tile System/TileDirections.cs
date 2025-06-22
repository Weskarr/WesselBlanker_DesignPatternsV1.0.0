using System.Collections.Generic;
using TileSystem;
using UnityEngine;

namespace TileSystem
{
    public class TileDirections : MonoBehaviour
    {
        private Dictionary<FourDirectionsEnum, Vector2Int> _fourDirectionsOffsets = new();
        private Dictionary<EightDirectionsEnum, Vector2Int> _eightDirectionsOffsets = new();

        public Dictionary<FourDirectionsEnum, Vector2Int> FourDirectionsOffsets
        {
            get => _fourDirectionsOffsets;
            set => _fourDirectionsOffsets = value;
        }

        public Dictionary<EightDirectionsEnum, Vector2Int> EightDirectionsOffsets
        {
            get => _eightDirectionsOffsets;
            set => _eightDirectionsOffsets = value;
        }

        #region Initialize Directions Dictionaries
        public void InitializeAllDirectionsDictionaries()
        {
            InitializeFourDirectionsDictionary();
            InitializeEightDirectionsDictionary();
        }
        public void InitializeFourDirectionsDictionary()
        {
            _fourDirectionsOffsets = new()
            {
                { FourDirectionsEnum.North,      new Vector2Int(0, 1) },     // [0] N
                { FourDirectionsEnum.East,       new Vector2Int(1, 0) },     // [2] E
                { FourDirectionsEnum.South,      new Vector2Int(0, -1) },    // [4] S
                { FourDirectionsEnum.West,       new Vector2Int(-1, 0) },    // [6] W
            };
        }
        public void InitializeEightDirectionsDictionary()
        {
            _eightDirectionsOffsets = new()
            {
                { EightDirectionsEnum.North,      new Vector2Int(0, 1) },   // [0] N
                { EightDirectionsEnum.NorthEast,  new Vector2Int(1, 1) },   // [1] NE
                { EightDirectionsEnum.East,       new Vector2Int(1, 0) },   // [2] E
                { EightDirectionsEnum.SouthEast,  new Vector2Int(1, -1) },  // [3] SE
                { EightDirectionsEnum.South,      new Vector2Int(0, -1) },  // [4] S
                { EightDirectionsEnum.SouthWest,  new Vector2Int(-1, -1) }, // [5] SW
                { EightDirectionsEnum.West,       new Vector2Int(-1, 0) },  // [6] W
                { EightDirectionsEnum.NorthWest,  new Vector2Int(-1, 1) },  // [7] NW
            };
        }
        #endregion
    }
}