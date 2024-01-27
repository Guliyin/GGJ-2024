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
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform crosshair;
    [SerializeField] Material dottedLine;

    void Start()
    {
        cannon = GetComponent<Cannon>();
    }

    void Update()
    {
        if (lineRenderer.enabled)
        {
            DrawProjection();
        }
    }
    void DrawProjection()
    {
        Strength = cannon.fireForce;

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

            Vector3 lastPos = lineRenderer.GetPosition(i - 1);

            if(Physics.Raycast(lastPos,(point-lastPos).normalized,out RaycastHit hit,(point - lastPos).magnitude, layerMask))
            {
                crosshair.position = Vector3.Lerp(crosshair.position, lastPos, 0.2f);
                crosshair.rotation = Quaternion.LookRotation((point - lastPos).normalized);
                //crosshair.rotation = Quaternion.Slerp(crosshair.rotation, Quaternion.Euler((point - lastPos).normalized), 0.5f);
                lineRenderer.SetPosition(i, hit.point);
                lineRenderer.positionCount = i;
                dottedLine.SetFloat("Length", i);
                return;
            }
        }
    }
    private void OnDrawGizmos()
    {
        DrawProjection();
    }
}
