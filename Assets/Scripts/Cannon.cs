using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    Vector2 input => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    [SerializeField] public Transform gun;
    [SerializeField] public Transform gunPos;
    [SerializeField] GameObject bulletPrefab;

    [SerializeField][Range(30, 32)] public float fireForce = 30;
    [SerializeField][Range(0, 1)] float turnRate = 0.1f;

    private void Update()
    {
        print(gun.localEulerAngles + " " + gun.rotation * Vector3.forward);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
        if (input != Vector2.zero)
        {
            Quaternion q = Quaternion.LookRotation(gun.localRotation * Vector3.forward + new Vector3(input.x, input.y, 0));
            gun.rotation = Quaternion.Slerp(gun.rotation, q, Time.deltaTime * turnRate);
        }
    }
    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunPos.position, gunPos.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(gunPos.forward * fireForce, ForceMode.Impulse);
    }
}
