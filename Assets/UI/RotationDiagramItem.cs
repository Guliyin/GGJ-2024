using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class RotationDiagramItem : MonoBehaviour
{
    public int PosId;
    private float offset;
    private float aniTime = 0.5f;

    //private Action<float> moveAction;

    private Image image;
    private Image Image
    {
        get
        {
            if (image == null)
            {
                image = GetComponent<Image>();
            }
            return image;
        }
    }

    private RectTransform rect;
    private RectTransform Rect
    {
        get
        {
            if (rect == null)
            {
                rect = GetComponent<RectTransform>();
            }
            return rect;
        }
    }

    private void Change(int index)
    {
        Debug.Log(index);
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    public void SetSprite(Sprite sprite)
    {
        Image.sprite = sprite;
    }

    public void SetPosData(ItemPosData data)
    {
        Rect.DOAnchorPos(Vector2.right * data.X, aniTime);
        Rect.DOScale(Vector3.one * data.ScaleTimes, aniTime);
        //Rect.anchoredPosition = Vector2.right*data.X;
        //Rect.localScale = Vector3.one * data.ScaleTimes;
        StartCoroutine(Wait(data));
    }

    private IEnumerator Wait(ItemPosData data)
    {
        yield return new WaitForSeconds(aniTime * 0.5f);
        transform.SetSiblingIndex(data.Order);
    }

    // public void OnDrag(PointerEventData eventData)
    // {
    //     offset += eventData.delta.x;
    // }

    // public void OnEndDrag(PointerEventData eventData)
    // {
    //     moveAction(offset);
    //     offset = 0;
    // }

    // public void AddMoveListener(Action<float> onMove)
    // {
    //     moveAction = onMove;
    // }

    public void ChangeId(int symbol, int totalItemNum)
    {
        int id = PosId;
        id += symbol;
        if (id < 0)
        {
            id += totalItemNum;
        }
        PosId = id % totalItemNum;

        setTransparency();
    }

    public void setTransparency()
    {
        switch (PosId)
        {
            case 0:
                GetComponent<Image>().DOColor(new(1, 1, 1, 1), 0.1f);
                break;
            case 1:
                GetComponent<Image>().DOColor(new(1, 1, 1, 0.66f), 0.1f);
                break;
            case 2:
                GetComponent<Image>().DOColor(new(1, 1, 1, 0.33f), 0.1f);
                break;
            case 3:
                GetComponent<Image>().DOColor(new(1, 1, 1, 0), 0.1f);
                break;
            case 4:
                GetComponent<Image>().DOColor(new(1, 1, 1, 0), 0.1f);
                break;
            case 5:
                GetComponent<Image>().DOColor(new(1, 1, 1, 0), 0.1f);
                break;
            case 6:
                GetComponent<Image>().DOColor(new(1, 1, 1, 0), 0.1f);
                break;
            case 7:
                GetComponent<Image>().DOColor(new(1, 1, 1, 0), 0.1f);
                break;
            case 8:
                GetComponent<Image>().DOColor(new(1, 1, 1, 0.33f), 0.1f);
                break;
            case 9:
                GetComponent<Image>().DOColor(new(1, 1, 1, 0.66f), 0.1f);
                break;
            default: break;
        }
    }
}
