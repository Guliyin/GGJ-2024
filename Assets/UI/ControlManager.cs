using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ControlManager : MonoBehaviour
{
    private bool isDraftShowing;
    public GameObject draft;


    void Awake()
    {
        isDraftShowing = false;
    }

    void Update()
    {
        if (UIManager.Instance.currentProgress == UIManager.GameProgress.MainGame)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                DraftKeyPress();
            }
        }
    }

    public void DraftKeyPress()
    {
        if (!isDraftShowing)
        {
            AudioManager.Instance.PlaySFX("DraftOpen");
            Sequence showSequence = DOTween.Sequence();
            showSequence.Append(draft.transform.DOMoveY(590, 0.1f));
            showSequence.Append(draft.transform.DOMoveY(540, 0.05f));
            showSequence.OnComplete(() => isDraftShowing = !isDraftShowing);

        }
        else
        {
            AudioManager.Instance.PlaySFX("DraftClose");
            Sequence hideSequence = DOTween.Sequence();
            hideSequence.Append(draft.transform.DOMoveY(590, 0.05f));
            hideSequence.Append(draft.transform.DOMoveY(-1000, 0.1f));
            hideSequence.OnComplete(() => isDraftShowing = !isDraftShowing);

        }
    }
}
