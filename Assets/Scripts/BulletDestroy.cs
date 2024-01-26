using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Statue"))
        {
            print("Hole");
        }
        else if (collision.gameObject.CompareTag("Part"))
        {
            collision.transform.GetComponent<BulletPart>().Hit();
        }
    }
}
