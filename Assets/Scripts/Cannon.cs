using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] public Transform gunPos;
    [SerializeField] GameObject bulletPrefab;

    [SerializeField][Range(30, 60)] public float force;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }
    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunPos.position, gunPos.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(gunPos.forward * force, ForceMode.Impulse);
    }
}
