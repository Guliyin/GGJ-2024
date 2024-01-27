using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    private static GameMgr instance;
    public static GameMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameMgr>();
            }
            return instance;
        }
    }

    public bool enableInput;
    public float Animlength = 3;

    private void OnEnable()
    {
        EventCenter.AddListener(FunctionType.Fire, AfterFire);
    }
    void AfterFire()
    {
        enableInput = false;
        Invoke("NewFire", Animlength);
    }
    void NewFire()
    {
        enableInput = true;
        EventCenter.Broadcast(FunctionType.NewFire);
    }
    private void OnDisable()
    {
        EventCenter.RemoveListener(FunctionType.Fire, AfterFire);
    }
}
