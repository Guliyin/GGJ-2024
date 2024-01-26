using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] Cannon cannon;
    float Strength;

    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform gunPos;

    [Header("LineSetting")]
    [SerializeField]
    [Range(10, 100)] int linePoints = 25;
    [SerializeField]
    [Range(0.01f, 0.25f)] float timeBetweenPoints = 0.1f;

    void Start()
    {
        cannon = GetComponent<Cannon>();
    }

    void Update()
    {
        DrawProjection();
    }
    void DrawProjection()
    {
        Strength = cannon.force;

        lineRenderer.enabled = true;
        lineRenderer.positionCount = Mathf.CeilToInt(linePoints / timeBetweenPoints) + 1;
        Vector3 startPos = gunPos.position;
        Vector3 startVel = Strength * gunPos.forward;
        int i = 0;
        lineRenderer.SetPosition(i, startPos);
        for (float time = 0; time < linePoints; time += timeBetweenPoints)
        {
            i++;
            Vector3 point = startPos + time * startVel;
            point.y = startPos.y + startVel.y * time + (Physics.gravity.y / 2f * time * time);

            lineRenderer.SetPosition(i, point);
        }
    }
    private void OnDrawGizmos()
    {
        DrawProjection();
    }
}
