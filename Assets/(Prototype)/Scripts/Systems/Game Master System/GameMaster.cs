
// Unity Namespaces:
using System.Collections.Generic;
using UnityEngine;

// Custom Namespaces:
using MaterialSystem;
using PlacementsSystem;
using PlayerSystem;
using PreviewSystem;
using SettingsSystem;
using TileSystem;
using UserInterfaceSystem;

namespace GameMasterSystem
{
    public class GameMaster : MonoBehaviour
    {
        [SerializeField] MaterialManager materialManager;
        [SerializeField] PlacementsManager placementsManager;
        [SerializeField] PlayerManager playerManager;
        [SerializeField] PreviewManager previewManager;
        [SerializeField] SettingsManager settingsManager;
        [SerializeField] TileManager tileManager;
        [SerializeField] UserInterfaceManager interfaceManager;

        private HashSet<IManager> _managers;

        private void ListInterfaceManagers()
        {
            _managers = new();
            {
                _managers.Add(materialManager);
                _managers.Add(placementsManager);
                _managers.Add(playerManager);
                _managers.Add(previewManager);
                _managers.Add(settingsManager);
                _managers.Add(tileManager);
                _managers.Add(interfaceManager);
            }
        }

        private void LinkManagers()
        {
            if (_managers == null || _managers.Count == 0)
                return;

            GameMaster master = this;

            foreach (var manager in _managers)
            {
                manager.LinkMaster(master);
            }
        }

        private void InitializeManagers()
        {
            if (_managers == null || _managers.Count == 0)
                return;

            foreach (var manager in _managers)
            {
                manager.InitializeManager();
            }
        }

        void Start()
        {
            ListInterfaceManagers();
            LinkManagers();
            InitializeManagers();
        }

        public void RequestToGenerateNewTileMap(float tileSize, Vector2Int tileMapSize)
        {
            tileManager.GenerateNewTileMap(tileSize, tileMapSize);
        }
    }
}