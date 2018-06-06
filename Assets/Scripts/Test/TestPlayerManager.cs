using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerManager : MonoBehaviour 
{

	// Use this for initialization
    private void Start () 
    {
        GameObject gsm = GameObject.Find("GameStateManager");
        if (gsm == null)
        {
            PlayerManager pm = this.GetComponent<PlayerManager>();
            pm.AddPlayer(EController.Controller1);
            pm.AddPlayer(EController.Controller2);
            pm.AddPlayer(EController.Controller3);
            pm.AddPlayer(EController.Controller4);
            pm.SpawnAllPlayers();
        }
	}
}
