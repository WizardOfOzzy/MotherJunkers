using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunPool : MonoBehaviour {
    public int initialPoolSize = 100;
    public GameObject prefab;
    List<Poolable> pool;
    public static MachineGunPool Instance;
    void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
        pool = new List<Poolable>();
        IncreasePoolSize(initialPoolSize);
    }
	
    void IncreasePoolSize(int val)
    {
        for (int i = 0; i < val; i++)
        {
            GameObject g = GameObject.Instantiate(prefab);
            ReturnProjectile(g.GetComponent<Poolable>());
        }
    }

    public Poolable SpawnProjectile(Vector3 position,Quaternion rotation)
    {
        if (pool.Count < initialPoolSize / 4) IncreasePoolSize(initialPoolSize);
        Poolable poolable = pool[0];
        poolable.transform.position = position;
        poolable.transform.rotation = rotation;
        poolable.gameObject.SetActive(true);
        pool.RemoveAt(0);
        poolable.transform.SetParent(null);
        poolable.OnSpawn();
        return poolable;
    }

    public void ReturnProjectile(Poolable poolable)
    {
        poolable.gameObject.SetActive(false);
        poolable.transform.SetParent(transform);
        poolable.OnDeSpawn();
        pool.Add(poolable);
    }
}
