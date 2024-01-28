using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPart : Bullet
{
    public int num;

    float wind;

    GameObject a;
    GameObject b;
    Rigidbody rb;

    private void Start()
    {
        a = transform.GetChild(0).gameObject;
        b = transform.GetChild(1).gameObject;
        rb = GetComponent<Rigidbody>();
        wind = Wind.Instance.WindForce;
    }
    private void Update()
    {
        rb.AddForce(wind * Vector3.right);
    }
    public void Hit()
    {
        a.SetActive(false);
        b.SetActive(true);
        Invoke("Destroy", 5);
    }
    protected override void Touch()
    {
        base.Touch();
        GameMgr.Instance.CalculateDistance(num, transform.position);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Statue") || collision.gameObject.CompareTag("Part"))
        {
            Touch();
        }
    }
}
