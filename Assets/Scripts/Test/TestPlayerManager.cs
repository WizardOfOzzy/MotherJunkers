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
            MotherJunkers.PlayerManager pm = this.GetComponent<MotherJunkers.PlayerManager>();
            pm.AddPlayer(MotherJunkers.EController.Controller1, Color.red);
            pm.AddPlayer(MotherJunkers.EController.Controller2, Color.green);
            pm.AddPlayer(MotherJunkers.EController.Controller3, Color.blue);
            pm.AddPlayer(MotherJunkers.EController.Controller4, Color.yellow);
            pm.SpawnAllPlayers();
        }
	}
}
