using UnityEngine;

public class TestPlayerManager : MonoBehaviour 
{
    private void Start () 
    {
        GameObject gsm = GameObject.Find("GameStateManager");
        if (gsm == null)
        {
            PlayerManager.AddPlayer(EController.Controller1);
            PlayerManager.AddPlayer(EController.Controller2);
            PlayerManager.AddPlayer(EController.Controller3);
            PlayerManager.AddPlayer(EController.Controller4);
            PlayerManager.SpawnAllPlayers();
        }
	}
}
