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
    public int currentBulletIndex;
    public ArrayList bulletSequence;
    public enum BulletType { Nose, Mouth, EyeL, EyeR, EyebrowL, EyebrowR, Fringes, GrapeL, GrapeR, Bomb};

    private List<RotationDiagramItem> itemList;
    private List<ItemPosData> posDataList;


    private void Awake()
    {
        itemList = new List<RotationDiagramItem>();
        posDataList = new List<ItemPosData>();
        CreateItem();
        CalulateData();
        SetItemData();

        bulletSequence = new ArrayList
        {
            BulletType.Nose,
            BulletType.Mouth,
            BulletType.EyeL,
            BulletType.EyeR,
            BulletType.EyebrowL,
            BulletType.EyebrowR,
            BulletType.Fringes,
            BulletType.GrapeL,
            BulletType.GrapeR,
            BulletType.Bomb,
        };
        currentBulletIndex = 0;
    }

    private void Start()
    {
        ///btns = transform.GetComponentInChildren<Button>();
    }
    private void Update()
    {
        if (GameMgr.Instance.enableInput && Input.GetKeyDown(KeyCode.Q))
        {
            BulletLeftSwitch();
        }

        if (GameMgr.Instance.enableInput && Input.GetKeyDown(KeyCode.E))
        {
            BulletRightSwitch();
        }
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

        for (int i = 0; i < posDataList.Count; i++)
        {
            itemList[i].SetPosData(posDataList[itemList[i].PosId]);
        }
    }

    private void CalulateData()
    {

        List<ItemData> itemDataList = new List<ItemData>();

        float length = (ItemSize.x + offset) * itemList.Count;
        float radioOffset = 1 / (float)itemList.Count;
        float radio = 0;

        for (int i = 0; i < itemList.Count; i++)
        {
            ItemData itemData = new ItemData();
            itemData.PosId = i;
            itemDataList.Add(itemData);
            itemList[i].PosId = i;


            ItemPosData data = new ItemPosData();
            data.X = GetX(radio, length);
            data.ScaleTimes = GetScaleTimes(radio, ScaleTimesMin, ScaleTimesMax);

            radio += radioOffset;
            posDataList.Add(data);
        }

        itemDataList = itemDataList.OrderBy(u => posDataList[u.PosId].ScaleTimes).ToList();

        for (int i = 0; i < itemDataList.Count; i++)
        {
            posDataList[itemDataList[i].PosId].Order = i;
        }
    }

    private void SetItemData()
    {
        for (int i = 0; i < posDataList.Count; i++)
        {
            itemList[i].SetPosData(posDataList[i]);
            itemList[i].setTransparency();
        }
    }

    private float GetX(float ratio, float length)
    {
        if (ratio > 1 || ratio < 0)
        {
            Debug.LogError("当前比例必须是0-1");
            return 0;
        }

        if (ratio >= 0 && ratio < 0.25f)
        {
            return length * ratio;
        }
        else if (ratio >= 0.25f && ratio < 0.75f)
        {
            return length * (0.5f - ratio);
        }
        else
        {
            return length * (ratio - 1);
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
        Change(1);
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
        Change(-1);
        print(bulletSequence[currentBulletIndex]);
    }
}

public class ItemPosData
{
    public float X;
    public float ScaleTimes;
    public int Order;
};

public struct ItemData
{
    public int PosId;
};
