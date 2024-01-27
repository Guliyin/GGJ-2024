using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    float horizontalInput => Input.GetAxis("Horizontal");
    float verticalInput => Input.GetAxis("Vertical");

    Vector2 input => new Vector2(horizontalInput, verticalInput);

    float xRot = 315, yRot = 0;

    [SerializeField] public bool isPart;

    [SerializeField] public Transform gun;
    [SerializeField] public Transform gunPos;
    [SerializeField] GameObject partPrefab;
    [SerializeField] GameObject bombPrefab;

    [SerializeField][Range(30, 32)] public float fireForce = 30;
    [SerializeField][Range(0, 1)] float turnRate = 0.1f;

    private void Update()
    {
        if (GameMgr.Instance.enableInput && Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
        if (GameMgr.Instance.enableInput && input != Vector2.zero)
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

        EventCenter.Broadcast(FunctionType.Fire, bullet.transform);
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
