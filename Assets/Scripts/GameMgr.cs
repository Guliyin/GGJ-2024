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

    CorrectPos cPos;

    public bool enableInput;
    public float Animlength = 3;

    private void OnEnable()
    {
        EventCenter.AddListener(FunctionType.Fire, AfterFire);
    }
    private void Start()
    {
        cPos = FindObjectOfType(typeof(CorrectPos)) as CorrectPos;
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
    public void CalculateDistance(int num, Vector3 position)
    {
        float score = Vector3.Distance(cPos.pos[num],position);
        print(score);
    }
    private void OnDisable()
    {
        EventCenter.RemoveListener(FunctionType.Fire, AfterFire);
    }
}
