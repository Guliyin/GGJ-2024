using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Statue"))
        {
            var contact = collision.GetContact(0);
            collision.gameObject.GetComponentInParent<Statue>().Hit(contact.point, contact.normal);
        }
        else if (collision.gameObject.CompareTag("Part"))
        {
            collision.transform.GetComponent<BulletPart>().Hit();
        }
    }
}
