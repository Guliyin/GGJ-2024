using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshFracture : MonoBehaviour
{
    [SerializeField] GameObject a;
    [SerializeField] GameObject b;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            a.SetActive(false);
            b.SetActive(true);
            Invoke("Destroy", 5);
        }
    }
    void Destroy()
    {
        Destroy(gameObject);
    }
}
