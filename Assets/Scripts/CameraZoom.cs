using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraZoom : MonoBehaviour
{
    Camera m_Camera;
    [SerializeField] Transform camPos1;
    [SerializeField] Transform camPos2;

    [SerializeField] float animLength;

    void Start()
    {
        m_Camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_Camera.DOFieldOfView(7, animLength);
            m_Camera.transform.DOMove(camPos2.position, animLength);
            m_Camera.transform.DORotate(camPos2.rotation.eulerAngles, animLength);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_Camera.DOFieldOfView(40, animLength);
            m_Camera.transform.DOMove(camPos1.position, animLength);
            m_Camera.transform.DORotate(camPos1.rotation.eulerAngles, animLength);
        }
    }
}
