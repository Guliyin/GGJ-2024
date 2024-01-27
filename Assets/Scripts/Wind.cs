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

    [Range(0,50)]
    [SerializeField] int windMax;
    [SerializeField] TMP_Text text;
    [SerializeField] bool enableWind = true;

    float windForce;
    public float WindForce => windForce;
    private void OnEnable()
    {
        NewWind();
        EventCenter.AddListener(FunctionType.NewFire, NewWind);
    }
    void NewWind()
    {
        windForce = enableWind ? Random.Range(-windMax, windMax) / 10.0f : 0;

        text.text = "Wind: " + windForce;
    }
    private void OnDisable()
    {
        EventCenter.RemoveListener(FunctionType.NewFire, NewWind);
    }
}
