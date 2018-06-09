using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public struct PlayerTuple
    {
        public EController controller;
        public Color color;
    }

    public static List<PlayerTuple> Players = new List<PlayerTuple>();

    private static  GameObject VehicleToSpawn;
    private static Vehicle[] vehicles;

    private void Awake()
	{
        Players = new List<PlayerTuple>();
        Publisher.Subscribe<KillVolumeHitEvent>(OnKillVolumeHitEvent);
	    VehicleToSpawn = Resources.Load<GameObject>("Vehicles\\Vehicle");
	}

    public static void ClearPlayers()
    {
        for (int i = vehicles.Length - 1; i >= 0; i--)
        {
            if (vehicles[i] != null)
            {
                Destroy(vehicles[i].gameObject);
            }
        }
       
        Players.Clear();
    }

    public static void AddPlayer(EController controller)
    {
        if (Players == null) return;

        foreach (PlayerTuple player in Players)
        {
            if (player.controller == controller)
            {
                return;
            }
        }
    
        PlayerTuple tuple = new PlayerTuple
        {
            controller = controller,
            color = GetPlayerColor(controller)
        };
        Players.Add(tuple);
    }

    public static void RemovePlayer(EController controller)
    {
        if (Players == null) return;

        foreach (PlayerTuple player in Players)
        {
            if (player.controller == controller)
            {
                Players.Remove(player);
                break;
            }
        }
    }

    public static void SpawnAllPlayers()
    {
        VehicleSpawnLocation[] spawnLocs = FindObjectsOfType<VehicleSpawnLocation>();
        vehicles = new Vehicle[Players.Count];

        List<Transform> transforms = new List<Transform>();

        for(int i = 0; i < Players.Count; ++i)
        {
            GameObject go = Instantiate(VehicleToSpawn);
            go.transform.position = spawnLocs[i].transform.position;

            Vehicle vehicle = go.GetComponent<Vehicle>();
            vehicle._controller = Players[i].controller;
            vehicle.SetColor(vehicle._controller);

            vehicles[i] = vehicle;
            transforms.Add(vehicle.transform);
        }

        SmashCamera smashCamera = FindObjectOfType<SmashCamera>();
        smashCamera.Init(transforms);

        ZoomCamera zoomCamera = FindObjectOfType<ZoomCamera>();
        if(zoomCamera!=null)
        zoomCamera.Init(transforms);
    }

    public static void RespawnPlayer(Vehicle vehicle)
    {
        Vector3 position = Random.insideUnitSphere * 20;
        position.y = 0.0f;

        while (Physics.CheckSphere(position, 1))
        {
            position = Random.insideUnitSphere * 20;
        }

        vehicle.transform.position = position;
        VehicleHealth health = vehicle.GetComponent<VehicleHealth>();
        health.SetHealth(0);
        health.SetRespawnInvulnerability();
    }

    public static Color GetPlayerColor(EController player)
    {
        switch (player)
        {
            case EController.Controller1:
                return Color.red;
            case EController.Controller2:
                return Color.green;
            case EController.Controller3:
                return Color.blue;
            default:
                return Color.yellow;
        }
    }

    private static void OnKillVolumeHitEvent(KillVolumeHitEvent e)
    {
        foreach (Vehicle v in vehicles)
        {
            if (e.PlayerIndex == v._controller)
            {
                VehicleHealth health = v.GetComponent<VehicleHealth>();
                health.UpdateStock();

                if (health.Stock > 0)
                {
                    RespawnPlayer(v);
                }
                else
                {
                    RemovePlayer(v._controller);
                    Destroy(e.Vehicle.gameObject);
                    Publisher.Raise(new PlayerCountEvent(Players.Count));
                }
                break;
            }
        }
    }
}



