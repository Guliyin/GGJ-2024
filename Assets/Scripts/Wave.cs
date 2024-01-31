using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wave : MonoBehaviour
{
    [SerializeField] bool enableWave = true;

    [Space]
    [Range(0, 50)]
    [SerializeField] int waveMax;
    [Range(0, 50)]
    [SerializeField] int waveMin;
    [Range(0.5f, 1.5f)]
    [SerializeField] float waveSpeed = 1;

    [Space]
    [Range(0, 50)]
    [SerializeField] int horizontalWaveMax;
    [Range(0, 50)]
    [SerializeField] int horizontalWaveMin;
    [Range(0.5f, 1.5f)]
    [SerializeField] float horizontalWaveSpeed = 1;
    //[SerializeField] TMP_Text text;

    float waveForce;
    float horizontalWaveForce;

    private void OnEnable()
    {
        NewWave();
        EventCenter.AddListener(FunctionType.NewFire, NewWave);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x + Mathf.Sin(Time.time*waveSpeed) * waveForce, transform.rotation.y + Mathf.Sin(Time.time * horizontalWaveSpeed) * horizontalWaveForce, 0);
    }
    void NewWave()
    {
        waveForce = enableWave ? Random.Range(waveMin, waveMax) / 10.0f : 0;
        horizontalWaveForce = enableWave ? Random.Range(horizontalWaveMin, horizontalWaveMax) / 10.0f : 0;

        //text.text = "Wave: " + waveForce;
    }
}
