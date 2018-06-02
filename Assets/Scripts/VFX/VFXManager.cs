using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    private VFXManager Instance;
    void Awake()
    {
        Instance = this;
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
