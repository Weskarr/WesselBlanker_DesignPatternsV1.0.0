using UnityEngine;

// Custom Namespaces:
using GameMasterSystem;

namespace MaterialSystem
{
    public class MaterialManager : MonoBehaviour, IManager
    {
        [SerializeField] MaterialBoard materialBoard;

        public GameMaster GameMaster { get; set; }

        public void LinkMaster(GameMaster gameMaster) => GameMaster = gameMaster;

        public void InitializeManager()
        {
            materialBoard.InitializeAll();
        }
    }
}