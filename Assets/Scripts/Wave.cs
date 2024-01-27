using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wave : MonoBehaviour
{
    [SerializeField] bool enableWave = true;
    [Range(0, 50)]
    [SerializeField] int waveMax;
    [Range(0, 50)]
    [SerializeField] int waveMin;
    [Range(0.5f, 1.5f)]
    [SerializeField] float waveSpeed = 1;
    [SerializeField] TMP_Text text;

    float waveForce;

    private void OnEnable()
    {
        NewWind();
        EventCenter.AddListener(FunctionType.NewFire, NewWind);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x + Mathf.Sin(Time.time*waveSpeed) * waveForce, 0, 0);
    }
    void NewWind()
    {
        waveForce = enableWave ? Random.Range(waveMin, waveMax) / 10.0f : 0;

        text.text = "Wave: " + waveForce;
    }
}
