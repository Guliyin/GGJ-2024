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
            }
        }
        else if (collision.gameObject.CompareTag("Part"))
        {
            if(firstTouch) Touch();
            collision.transform.GetComponent<BulletPart>().Hit();
            if (collision.gameObject.name == "Cup")
            {
                print("Angry!");
            }
        }
    }
    protected override void Touch()
    {
        base.Touch();
        Invoke("Destroy", 5);
    }
}
