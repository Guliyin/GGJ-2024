using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ControlManager : MonoBehaviour
{
    private bool isDraftShowing;
    public GameObject draft;
    public GameObject bulletSwitchPanel;
    public ArrayList bulletSequence;
    public enum BulletType { Eye, Nose, Mouth, Ear };
    private int currentBulletIndex;

    void Awake()
    {
        isDraftShowing = false;
        bulletSequence = new ArrayList
        {
            BulletType.Eye,
            BulletType.Nose,
            BulletType.Mouth,
            BulletType.Ear
        };
        currentBulletIndex = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            DraftKeyPress();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            BulletLeftSwitch();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            BulletRightSwitch();
        }
    }

    public void DraftKeyPress()
    {
        if (!isDraftShowing)
        {
            Sequence showSequence = DOTween.Sequence();
            showSequence.Append(draft.transform.DOMoveY(590, 0.1f));
            showSequence.Append(draft.transform.DOMoveY(540, 0.05f));
            showSequence.OnComplete(() => isDraftShowing = !isDraftShowing);
        }
        else
        {
            Sequence hideSequence = DOTween.Sequence();
            hideSequence.Append(draft.transform.DOMoveY(590, 0.05f));
            hideSequence.Append(draft.transform.DOMoveY(-1000, 0.1f));
            hideSequence.OnComplete(() => isDraftShowing = !isDraftShowing);
        }
    }

    public void BulletLeftSwitch()
    {
        if (currentBulletIndex == 0)
        {
            currentBulletIndex = bulletSequence.Count - 1;
        }
        else
        {
            currentBulletIndex--;
        }
        bulletSwitchPanel.GetComponent<RotationDiagram2D>().Change(1);
        print(bulletSequence[currentBulletIndex]);
    }

    public void BulletRightSwitch()
    {
        if (currentBulletIndex == bulletSequence.Count - 1)
        {
            currentBulletIndex = 0;
        }
        else
        {
            currentBulletIndex++;
        }
        bulletSwitchPanel.GetComponent<RotationDiagram2D>().Change(-1);
        print(bulletSequence[currentBulletIndex]);
    }
}
