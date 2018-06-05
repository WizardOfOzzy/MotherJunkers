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
    private Vehicle[] vehicles;

    private void Awake()
	{
        Players = new List<PlayerTuple>();
        Publisher.Subscribe<KillVolumeHitEvent>(OnKillVolumeHitEvent);
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
        vehicles = new Vehicle[Players.Count];

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

    public void RespawnPlayer(Vehicle vehicle)
    {
        Vector3 position = Random.insideUnitSphere * 20;
        position.y = 0.0f;

        while (Physics.CheckSphere(position, 1))
        {
            position = Random.insideUnitSphere * 20;
        }

        vehicle.transform.position = position;
    }

    private void OnKillVolumeHitEvent(KillVolumeHitEvent e)
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
                    e.Vehicle.gameObject.SetActive(false);
                }

                break;
            }
        }
    }
}



