using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public struct PlayerTuple
    {
        public EController controller;
        public Color color;
    }

    public List<PlayerTuple> Players;

    public GameObject VehicleToSpawn;

	private void Awake()
	{
        Players = new List<PlayerTuple>();
	}

	public void AddPlayer(EController peController, Color pColor)
    {
        if (Players == null)
        {
            Players = new List<PlayerTuple>();
        }

        PlayerTuple tuple = new PlayerTuple
        {
            controller = peController,
            color = pColor
        };
        Players.Add(tuple);
    }

    public void SpawnAllPlayers()
    {
        VehicleSpawnLocation[] spawnLocs = FindObjectsOfType<VehicleSpawnLocation>();
        Vehicle[] vehicles = new Vehicle[Players.Count];
        List<Transform> transforms = new List<Transform>();

        for(int i = 0; i < Players.Count; ++i)
        {
            GameObject go = Instantiate(VehicleToSpawn);
            go.transform.position = spawnLocs[i].transform.position;

            Vehicle vehicle = go.GetComponent<Vehicle>();
            vehicle._controller = Players[i].controller;
            vehicle.SetColor(Players[i].color);

            vehicles[i] = vehicle;
            transforms.Add(vehicle.transform);
        }

        SmashCamera smashCamera = FindObjectOfType<SmashCamera>();
        smashCamera.Init(transforms);

        ZoomCamera zoomCamera = FindObjectOfType<ZoomCamera>();
        if(zoomCamera!=null)
        zoomCamera.Init(transforms);
    }
}



