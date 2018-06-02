using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    private VFXManager Instance;
    public GameObject tempVFX;
    public Transform transformTemp;
    void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SpawnVFX(tempVFX, transformTemp.position, transformTemp.forward).Play();
        }
    }

    public static VFX SpawnVFX(GameObject toSpawn, Vector3 pos,Vector3 forward)
    {

        GameObject g= GameObject.Instantiate(toSpawn);
        g.transform.position = pos;
        g.transform.forward = forward;
        VFX vfx = g.GetComponent<VFX>();
        vfx.Init();
        return vfx;
    }
}
