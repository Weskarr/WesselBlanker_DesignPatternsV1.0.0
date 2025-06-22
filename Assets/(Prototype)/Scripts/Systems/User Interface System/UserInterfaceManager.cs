using UnityEngine;
using System.Collections.Generic;

// Custom Namespaces:
using GameMasterSystem;

namespace UserInterfaceSystem
{
    public class UserInterfaceManager : MonoBehaviour, IManager
    {
        [SerializeField] private TileMapSettingsBlackboard tileMapSettingsBlackboard;
        [SerializeField] private PlacementSettingsBlackboard placementSettingsBlackboard;
        private List<IBlackboardForUI> _blackboardsForUI = new();

        public GameMaster GameMaster { get; set; }

        public void LinkMaster(GameMaster gameMaster) => GameMaster = gameMaster;

        public void InitializeManager()
        {
            _blackboardsForUI.Add(tileMapSettingsBlackboard);
            _blackboardsForUI.Add(placementSettingsBlackboard);

            foreach(var blackboard in _blackboardsForUI)
            {
                blackboard.InitializeAllActions();
                blackboard.StartAllRelayListening();
            }

            tileMapSettingsBlackboard.OnApplyChanges += RequestApplyChangesToGameMaster;
        }

        public void RequestApplyChangesToGameMaster(float tileSize, Vector2Int tileMapSize)
        {
            Debug.Log("Apply Changes");
            GameMaster.RequestToGenerateNewTileMap(tileSize, tileMapSize);
        }

    }
}