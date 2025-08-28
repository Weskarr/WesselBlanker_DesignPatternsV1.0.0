using UnityEngine;

// Custom Namespaces:
using GameMasterSystem;

namespace PlayerSystem
{
    public class PlayerManager : MonoBehaviour, IManager
    {
        public GameMaster GameMaster { get; set; }

        public void LinkMaster(GameMaster gameMaster) => GameMaster = gameMaster;

        public void InitializeManager()
        {

        }
    }
}