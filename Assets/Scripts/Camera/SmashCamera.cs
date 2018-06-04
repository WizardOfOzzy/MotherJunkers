using System.Collections.Generic;
using UnityEngine;

public class SmashCamera : MonoBehaviour
{
    private List<Transform> Targets;
    private Camera Camera;

    public Vector3 Offset;
    public float SmoothTime = 0.5f;

    public float MinFieldOfField = 30.0f;
    public float MaxFieldOfField = 15.0f;
    public float FieldOfViewLimit = 20.0f;

    private Vector3 velocity;

    public void Init(List<Transform> targets)
    {
        Targets = targets;
        Camera = GetComponent<Camera>();
    }


    private void LateUpdate()
    {
        if (Targets != null && Camera != null)
        {
            Move();
            Zoom();
        }
    }

    private void Zoom()
    {
        Bounds bounds = new Bounds(Targets[0].position, Vector3.zero);
        for (int i = 0; i < Targets.Count; i++)
        {
            if (Targets[i] != null)
                bounds.Encapsulate(Targets[i].position);
        }

        float fov = Mathf.Lerp(MaxFieldOfField, MinFieldOfField, bounds.size.x / FieldOfViewLimit);
        Camera.fieldOfView = Mathf.Lerp(Camera.fieldOfView, fov, Time.deltaTime);
    }

    private void Move()
    {
        if (Targets.Count == 0) return;

        Vector3 center = GetCenterPoint();
        Vector3 position = center - Offset;
        transform.position = Vector3.SmoothDamp(transform.position, position, ref velocity, SmoothTime);
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