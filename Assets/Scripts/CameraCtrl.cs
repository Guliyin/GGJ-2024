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
    Transform projectile;

    private void OnEnable()
    {
        EventCenter.AddListener<Transform>(FunctionType.Fire, PlayFlyingAnim);
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
        isFlying = true;
        this.projectile = projectile;
    }
    void FlyingAnimOver()
    {
        isFlying = false;
        projectile = null;
    }

    private void OnDisable()
    {
        EventCenter.RemoveListener<Transform>(FunctionType.Fire, PlayFlyingAnim);
        EventCenter.RemoveListener(FunctionType.NewFire, FlyingAnimOver);
    }
}
