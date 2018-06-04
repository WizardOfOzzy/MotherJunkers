using System.Collections.Generic;
using UnityEngine;


public class ZoomCamera : MonoBehaviour
{
    private List<Transform> Targets;

    public Vector3 defaultOffset=new Vector3(5,5,0);
    public float smoothMove = 1f;
    public float distanceRatio = 1.5f;
    public float maxDistance=5;
    Vector3 v3LookAtPosition;


    public void Init(List<Transform> targets)
    {
        Targets = targets;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Targets == null) return;
        int numberofVehicles = 0;
        v3LookAtPosition = new Vector3(0, 0, 0);

        foreach (Transform Vhc in Targets)
        {
            if (Vhc != null)
            {
                numberofVehicles++;
                v3LookAtPosition += Vhc.position;
            }
        }
        v3LookAtPosition /= numberofVehicles;
        Vector3 pos = v3LookAtPosition + defaultOffset;
        Vector3 forward = pos - v3LookAtPosition;
        pos += forward*Mathf.Min(maxDistance,GetMaxDistance() / distanceRatio);
        transform.position = Vector3.Slerp(transform.position, pos, smoothMove * Time.deltaTime);
        transform.LookAt(v3LookAtPosition);


    }
    float GetMaxDistance()
    {
        float result = 0;
        for (int i = 0; i < Targets.Count; i++)
        {
            Transform t1 = Targets[i];
            if (t1 == null) continue;
            for (int j = i; j < Targets.Count; j++)
            {
                Transform t2 = Targets[j];
                if (t2 == null) continue;
                float dist = Vector3.Distance(t1.position, t2.position);
                if (dist > result)
                    result = dist;
            }
        }
        return result;
    }

    private Vector3 GetCenterPoint()
    {
        if (Targets.Count == 1)
        {
            return Targets[0].transform.position;
        }

        Bounds bounds = new Bounds(Targets[0].position, Vector3.zero);

        for (int i = 0; i < Targets.Count; i++)
        {
            if (Targets[i] != null)
                bounds.Encapsulate(Targets[i].position);
        }

        return bounds.center;
    }
}
