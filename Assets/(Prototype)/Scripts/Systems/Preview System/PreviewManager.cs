using UnityEngine;

// Custom Namespaces:
using GameMasterSystem;

namespace PreviewSystem
{
    public class PreviewManager : MonoBehaviour, IManager
    {
        public GameMaster GameMaster { get; set; }

        public void LinkMaster(GameMaster gameMaster) => GameMaster = gameMaster;

        public void InitializeManager()
        {

        }
    }
}