using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RotationDiagram2D : MonoBehaviour
{
    public Vector2 ItemSize;
    public Sprite[] ItemSprites;
    public float offset;
    public float ScaleTimesMin;
    public float ScaleTimesMax;

    private List<RotationDiagramItem> itemList;
    private List<ItemPosDate> posDateList;

    private void Awake()
    {
        itemList = new List<RotationDiagramItem>();
        posDateList = new List<ItemPosDate>();
        CreateItem();
        CalulateDate();
        SetItemDate();
    }

    private void Start()
    {
        ///btns = transform.GetComponentInChildren<Button>();
    }

    private GameObject CreateTemplate()
    {
        GameObject item = new GameObject("Template");
        item.AddComponent<RectTransform>().sizeDelta = ItemSize;
        item.AddComponent<Image>();
        item.AddComponent<RotationDiagramItem>();
        //item.AddComponent<Button>();
        return item;
    }

    private void CreateItem()
    {
        GameObject template = CreateTemplate();
        RotationDiagramItem itemTemplate = null;
        //Resurces->prefab->实例化->gameObject
        foreach (Sprite sprite in ItemSprites)
        {
            itemTemplate = Instantiate(template).GetComponent<RotationDiagramItem>();
            itemTemplate.SetParent(transform);
            itemTemplate.SetSprite(sprite);
            //itemTemplate.AddMoveListener(Change);
            itemList.Add(itemTemplate);
        }
        Destroy(template);
    }

    private void Change(float offsetX)
    {
        int symbol = offsetX > 0 ? 1 : -1;
        Change(symbol);
    }

    public void Change(int symbol)
    {
        foreach (RotationDiagramItem item in itemList)
        {
            item.ChangeId(symbol, itemList.Count);
        }

        for (int i = 0; i < posDateList.Count; i++)
        {
            itemList[i].SetPosDate(posDateList[itemList[i].PosId]);
        }
    }

    private void CalulateDate()
    {

        List<ItemDate> itemDateList = new List<ItemDate>();

        float length = (ItemSize.x + offset) * itemList.Count;
        float radioOffset = 1 / (float)itemList.Count;
        float radio = 0;

        for (int i = 0; i < itemList.Count; i++)
        {
            ItemDate itemDate = new ItemDate();
            itemDate.PosId = i;
            itemDateList.Add(itemDate);
            itemList[i].PosId = i;


            ItemPosDate date = new ItemPosDate();
            date.X = GetX(radio, length);
            date.ScaleTimes = GetScaleTimes(radio, ScaleTimesMin, ScaleTimesMax);

            radio += radioOffset;
            posDateList.Add(date);
        }

        itemDateList = itemDateList.OrderBy(u => posDateList[u.PosId].ScaleTimes).ToList();

        for (int i = 0; i < itemDateList.Count; i++)
        {
            posDateList[itemDateList[i].PosId].Order = i;
        }
    }

    private void SetItemDate()
    {
        for (int i = 0; i < posDateList.Count; i++)
        {
            itemList[i].SetPosDate(posDateList[i]);
        }
    }

    private float GetX(float radio, float length)
    {
        if (radio > 1 || radio < 0)
        {
            Debug.LogError("当前比例必须是0-1");
            return 0;
        }

        if (radio >= 0 && radio < 0.25f)
        {
            return length * radio;
        }
        else if (radio >= 0.25f && radio < 0.75f)
        {
            return length * (0.5f - radio);
        }
        else
        {
            return length * (radio - 1);
        }
    }

    private float GetScaleTimes(float radio, float min, float max)
    {
        if (radio > 1 || radio < 0)
        {
            Debug.LogError("当前比例必须是0-1");
            return 0;
        }

        float scaleOffset = (max - min) / 0.5f;
        if (radio < 0.5f)
        {
            return max - scaleOffset * radio;
        }
        else
        {
            return max - scaleOffset * (1 - radio);
        }
    }
}

public class ItemPosDate
{
    public float X;
    public float ScaleTimes;
    public int Order;
};

public struct ItemDate
{
    public int PosId;
};
