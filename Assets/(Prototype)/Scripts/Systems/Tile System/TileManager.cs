using UnityEngine;

// Custom Namespaces:
using GameMasterSystem;
using System.Collections.Generic;

namespace TileSystem
{
    public class TileManager : MonoBehaviour, IManager
    {
        [SerializeField] private TileMapGenerator tileGenerator;
        [SerializeField] private TileDirections tileDirections;

        public GameMaster GameMaster { get; set; }

        public void LinkMaster(GameMaster gameMaster) => GameMaster = gameMaster;

        public void InitializeManager()
        {
            tileDirections.InitializeAllDirectionsDictionaries();
        }

        public void GenerateNewTileMap(float tileSize, Vector2Int tileMapSize)
        {
            tileGenerator.NewTileMap(tileSize, tileMapSize);
        }

        public Dictionary<FourDirectionsEnum, Vector2Int> RelayFourDirectionsDictionary() => tileDirections.FourDirectionsOffsets;
        public Dictionary<EightDirectionsEnum, Vector2Int> RelayEightDirectionsDictionary() => tileDirections.EightDirectionsOffsets;
    }
}