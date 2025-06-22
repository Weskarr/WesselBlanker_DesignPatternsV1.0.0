using UnityEngine;

// Custom Namespaces:
using GameMasterSystem;

namespace PlacementsSystem
{
    public class PlacementsManager : MonoBehaviour, IManager
    {
        public GameMaster GameMaster { get; set; }

        public void LinkMaster(GameMaster gameMaster) => GameMaster = gameMaster;

        public void InitializeManager()
        {

        }
    }
}