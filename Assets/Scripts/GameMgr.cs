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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        float score = Vector3.Distance(cPos.pos[num], position);
        if (score <= 0.2f)
        {
            print(score + " Good!");
            GodDialogManager.Instance.PlayDialogCompliment();
        }
        else if (score <= 1 && score > 0.2f)
        {
            print(score + " Normal");
            GodDialogManager.Instance.PlayDialogNeutral();
        }
        else
        {
            print(score + " Bad!");
            GodDialogManager.Instance.PlayDialogBlame();
        }
    }
    private void OnDisable()
    {
        EventCenter.RemoveListener(FunctionType.Fire, AfterFire);
    }
}
