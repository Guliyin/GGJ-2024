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

    float windForce;
    public float WindForce => windForce;
    private void OnEnable()
    {
        NewWind();
        EventCenter.AddListener(FunctionType.NewFire, NewWind);
    }
    void NewWind()
    {
        windForce = Random.Range(-windMax, windMax) / 10.0f;

        text.text = "wind: " + windForce;
    }
    private void OnDisable()
    {
        EventCenter.RemoveListener(FunctionType.NewFire, NewWind);
    }
}
