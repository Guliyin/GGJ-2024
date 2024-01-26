using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPart : Bullet
{
    GameObject a;
    GameObject b;

    private void Start()
    {
        a = transform.GetChild(0).gameObject;
        b = transform.GetChild(1).gameObject;
    }
    public void Hit()
    {
        a.SetActive(false);
        b.SetActive(true);
        Invoke("Destroy", 5);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Statue"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }
}
