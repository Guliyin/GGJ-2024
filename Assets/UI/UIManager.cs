using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public enum GameProgress { MainMenu, MainGame, EndGame };
    public GameProgress currentProgress;
    public GameObject startMenuBG;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        currentProgress = GameProgress.MainMenu;
    }


    public void StartGame()
    {
        startMenuBG.SetActive(false);
        EventCenter.Broadcast(FunctionType.StartGame);
        currentProgress = GameProgress.MainGame;
        AudioManager.Instance.PlayMusic("Sea");
    }

    public void FinishGame()
    {
        currentProgress = GameProgress.EndGame;
    }
}
