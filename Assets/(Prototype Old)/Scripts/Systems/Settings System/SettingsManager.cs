
// Unity Namespaces:
using UnityEngine;

// Custom Namespaces:
using GameMasterSystem;

namespace SettingsSystem
{
    public class SettingsManager : MonoBehaviour, IManager
    {
        public GameMaster GameMaster { get; set; }

        public void LinkMaster(GameMaster gameMaster) => GameMaster = gameMaster;

        public void InitializeManager()
        {

        }
    }
}