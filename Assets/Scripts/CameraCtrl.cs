using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraCtrl : MonoBehaviour
{
    Camera m_Camera;

    [Header("ZoomAnim")]
    [SerializeField] Transform camPos1;
    [SerializeField] Transform camPos2;
    [SerializeField] float zoomAnimLength = 0.5f;


    [Header("FlyingAnim")]
    bool isFlying;
    [SerializeField] Transform facePos;
    [SerializeField] Transform showcasePos;
    [SerializeField] float flyingDistance = 1;
    [SerializeField] float flyingAnimLength = 0.2f;
    Transform projectile;

    private void OnEnable()
    {
        EventCenter.AddListener<Transform>(FunctionType.FireWithTransform, PlayFlyingAnim);
        EventCenter.AddListener(FunctionType.Touch, ShowcasePosition);
        EventCenter.AddListener(FunctionType.NewFire, FlyingAnimOver);
    }

    void Start()
    {
        m_Camera = Camera.main;
    }

    void Update()
    {
        if (GameMgr.Instance.enableInput && Input.GetKeyDown(KeyCode.LeftShift))
        {
            ZoomIn();
        }
        if (GameMgr.Instance.enableInput && Input.GetKeyUp(KeyCode.LeftShift))
        {
            ResetCamera();
        }
    }
    private void LateUpdate()
    {
        if (isFlying)
        {
            Vector3 dir = projectile.position - facePos.position;
            m_Camera.transform.position = projectile.position + dir.normalized * flyingDistance;
        }
    }
    void ZoomIn()
    {
        m_Camera.DOFieldOfView(7, zoomAnimLength);
        m_Camera.transform.DOMove(camPos2.position, zoomAnimLength);
        m_Camera.transform.DORotate(camPos2.rotation.eulerAngles, zoomAnimLength);
    }
    public void ResetCamera()
    {
        m_Camera.DOFieldOfView(40, zoomAnimLength);
        m_Camera.transform.DOMove(camPos1.position, zoomAnimLength);
        m_Camera.transform.DORotate(camPos1.rotation.eulerAngles, zoomAnimLength);
    }
    void PlayFlyingAnim(Transform projectile)
    {
        m_Camera.DOFieldOfView(40, flyingAnimLength);
        isFlying = true;
        this.projectile = projectile;
    }
    void ShowcasePosition()
    {
        isFlying = false;
        m_Camera.DOFieldOfView(60, flyingAnimLength);
        m_Camera.transform.DOMove(showcasePos.position, flyingAnimLength);
        m_Camera.transform.DORotate(showcasePos.rotation.eulerAngles, flyingAnimLength);
    }
    void FlyingAnimOver()
    {
        isFlying = false;
        projectile = null;
        ResetCamera();
    }

    private void OnDisable()
    {
        EventCenter.RemoveListener<Transform>(FunctionType.FireWithTransform, PlayFlyingAnim);
        EventCenter.RemoveListener(FunctionType.Touch, ShowcasePosition);
        EventCenter.RemoveListener(FunctionType.NewFire, FlyingAnimOver);
    }
}
