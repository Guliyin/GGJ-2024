using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class BulletPart : Bullet
{
    public int num;

    GameObject a;
    GameObject b;

    void Start()
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
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.useGravity != false)
        {
            GameMgr.Instance.CalculateDistance(num, transform.position);
        }
        rb.isKinematic = true;
        rb.useGravity = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Statue") || collision.gameObject.CompareTag("Part"))
        {
            Touch();
            AudioManager.Instance.PlaySFX("Stick");
        }
    }
}
