using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    float horizontalInput => Input.GetAxis("Horizontal");
    float verticalInput => Input.GetAxis("Vertical");

    Vector2 input => new Vector2(horizontalInput, verticalInput);

    float xRot, yRot;

    [SerializeField] public bool isPart;

    [SerializeField] public Transform gun;
    [SerializeField] public Transform gunPos;
    [SerializeField] GameObject partPrefab;
    [SerializeField] GameObject bombPrefab;

    [SerializeField][Range(30, 32)] public float fireForce = 30;
    [SerializeField][Range(0, 1)] float turnRate = 0.1f;

    private void Start()
    {
        xRot = gun.rotation.eulerAngles.x;
        yRot = gun.rotation.eulerAngles.y;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
        if (input != Vector2.zero)
        {
            RotateCamera();
        }
    }
    void Fire()
    {
        GameObject projectile = isPart ? partPrefab : bombPrefab;

        GameObject bullet = Instantiate(projectile, gunPos.position, gunPos.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(gunPos.forward * fireForce, ForceMode.Impulse);
    }
    void RotateCamera()
    {
        xRot -= input.y * turnRate;
        yRot += input.x * turnRate;
        xRot = Mathf.Clamp(xRot, 310, 320);
        yRot = Mathf.Clamp(yRot, -5, 5);
        Quaternion rotation = Quaternion.Euler(xRot, yRot, 0);
        gun.rotation = rotation;
    }
}
