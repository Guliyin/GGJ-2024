using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectPos : MonoBehaviour
{
    public List<Vector3> pos = new List<Vector3>();

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            pos.Add(transform.GetChild(i).position);
        }
    }
}
