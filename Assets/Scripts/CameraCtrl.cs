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

    [Space]
    [Header("FlyingAnim")]
    //[SerializeField] Transform facePos;
    [SerializeField] Transform showcasePos;
    [SerializeField] float flyingDistance = 1;
    [SerializeField] Vector3 flyingWorldOffsetDir;
    [SerializeField] float flyingAnimLength = 0.2f;
    Transform projectile;
    bool isFlying;

    private void OnEnable()
    {
        EventCenter.AddListener<Transform>(FunctionType.FireWithTransform, PlayFlyingAnim);
        EventCenter.AddListener(FunctionType.Touch, TouchCallback);
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
            m_Camera.transform.position = projectile.position + flyingWorldOffsetDir.normalized * flyingDistance;
            m_Camera.transform.LookAt(projectile.position);
        }
    }
    void ZoomIn()
    {
        m_Camera.DOFieldOfView(6, zoomAnimLength);
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
    void TouchCallback()
    {
        Invoke("ShowcasePosition", 0.5f);
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
