using UnityEngine;
using GameMasterSystem;

public interface IManager
{
    GameMaster GameMaster { get; set; }

    public void LinkMaster(GameMaster gameMaster) => GameMaster = gameMaster;

    public void InitializeManager();
}
