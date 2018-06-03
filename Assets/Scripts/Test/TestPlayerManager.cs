using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerManager : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
        GameObject gsm = GameObject.Find("GameStateManager");
        if (gsm == null)
        {
            PlayerManager pm = this.GetComponent<PlayerManager>();
            pm.AddPlayer(EController.Controller1, Color.red);
            pm.AddPlayer(EController.Controller2, Color.green);
            pm.AddPlayer(EController.Controller3, Color.blue);
            pm.AddPlayer(EController.Controller4, Color.yellow);
            pm.SpawnAllPlayers();
        }
	}
}
