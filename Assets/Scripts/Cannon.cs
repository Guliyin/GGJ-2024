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
    [SerializeField] RotationDiagram2D load;
    [SerializeField] GameObject[] projectiles;

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
            RotateCanon();
        }
    }
    void Fire()
    {
        //GameObject projectile = isPart ? projectiles : bombPrefab;
        //-------------------------------------------------------------------------------------------------------------------------------------------------
        int num = load.currentBulletIndex;

        GameObject bullet = Instantiate(projectiles[num], gunPos.position, gunPos.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(gunPos.forward * fireForce, ForceMode.Impulse);

        EventCenter.Broadcast(FunctionType.Fire);
        EventCenter.Broadcast(FunctionType.FireWithTransform, bullet.transform);
    }
    void RotateCanon()
    {
        xRot -= input.y * turnRate;
        yRot += input.x * turnRate;
        xRot = Mathf.Clamp(xRot, 310, 331);
        yRot = Mathf.Clamp(yRot, -7, 7);
        Quaternion rotation = Quaternion.Euler(xRot, yRot, 0);
        gun.localRotation = rotation;
    }
}
