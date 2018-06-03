using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MotherJunkers
{
    public class PlayerManager : MonoBehaviour
    {
        public List<Vehicle> Vehicles;

        public GameObject VehicleToSpawn;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddPlayer(EController peController, Color pColor)
        {
            Vehicle Vehc = new Vehicle();
            Vehc._controller = peController;
            Vehc._Color = pColor;

            Vehicles.Add(Vehc);

        }


        public void SpawnAllPlayers()
        {
            VehicleSpawnLocation [] VSLs = FindObjectsOfType<VehicleSpawnLocation>();

            for(int i = 0; i < Vehicles.Count; ++i)
            {
                GameObject newgo = Instantiate(VehicleToSpawn, VSLs[i].transform);

                newgo.GetComponent<Vehicle>()._controller = Vehicles[i]._controller;

                newgo.GetComponent<Vehicle>().SetColor(Vehicles[i]._Color);

                
            }

        }
    }
}
