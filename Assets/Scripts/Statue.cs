using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    [SerializeField] GameObject holePrefab;

    public void Hit(Vector3 pos, Vector3 rot)
    {
        Quaternion rotation = Quaternion.LookRotation(rot);
        GameObject g = Instantiate(holePrefab, pos, rotation, transform);
        g.transform.LookAt(rot);
    }
}
