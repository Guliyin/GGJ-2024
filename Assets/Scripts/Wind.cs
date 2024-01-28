using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wind : MonoBehaviour
{
    private static Wind instance;
    public static Wind Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Wind>();
            }
            return instance;
        }
    }

    [Range(0, 100)]
    [SerializeField] int windMax;
    [SerializeField] TMP_Text text;
    [SerializeField] bool enableWind = true;
    [SerializeField] Material windMat;
    [SerializeField] Transform windUI;

    float windForce;
    public float WindForce => windForce;
    private void OnEnable()
    {
        NewWind();
        EventCenter.AddListener(FunctionType.NewFire, NewWind);
    }
    void NewWind()
    {
        windForce = enableWind ? Random.Range(-windMax, windMax) / 100.0f * 5 : 0;
        //得到windforce 从0到1

        text.text = "Wind: " + (windForce * 5).ToString("0.0");
        //显示风从0到5

        UpdateWindMat();
    }
    void UpdateWindMat()
    {
        print("wtf");
        float n = windForce >= 0 ? -0.5f : 0.5f;
        windMat.SetFloat("_Dir", n);
        windMat.SetFloat("_Number", windForce * 10);

        windUI.localScale = new Vector3(windForce * 10 * 0.025f, 1, 0.02f);
        windUI.localPosition = new Vector3(Mathf.Abs(windForce), 0, 0);
    }
    private void OnDisable()
    {
        EventCenter.RemoveListener(FunctionType.NewFire, NewWind);
    }
}
