using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : Bullet
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Statue"))
        {
            if (firstTouch)
            {
                Touch();
                var contact = collision.GetContact(0);
                collision.gameObject.GetComponentInParent<Statue>().Hit(contact.point, contact.normal);
                AudioManager.Instance.PlaySFX("RockHole");
            }
        }
        else if (collision.gameObject.CompareTag("Part"))
        {
            if (firstTouch)
            {
                Touch();
                AudioManager.Instance.PlaySFX("RockBroken");
            }
            collision.transform.GetComponent<BulletPart>().Hit();
            if (collision.gameObject.name == "Cup")
            {
                print("Angry!");
                GodDialogManager.Instance.PlayDialogCup();
            }
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        print(wind);
        wind = Wind.Instance.WindForce;
    }
    private void Update()
    {
        rb.AddForce(wind * Vector3.right);
    }
    protected override void Touch()
    {
        base.Touch();
        Invoke("Destroy", 5);
    }
}
