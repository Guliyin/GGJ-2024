using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    //[SerializeField] Camera museumCam;
    //[SerializeField] Animator museumCamAnim;
    [SerializeField] GameObject FinalCutScene;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject OldCam;
    [SerializeField] GameObject NewCam;
    [SerializeField] GameObject Cannon;
    [SerializeField] float startTime;
    [SerializeField] bool startGame;

    float time;

    private void OnEnable()
    {
        EventCenter.AddListener(FunctionType.StartGame, StartGame);
    }

    void Update()
    {
        if (!startGame) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            EndGame();
        }

        time += Time.deltaTime;
        float timeLeft = startTime * 60 - time;

        if (timeLeft <= 0)
        {
            EndGame();
        }

        int sec = (int)timeLeft % 60;
        int min = (int)timeLeft / 60;
        text.text = "Time left: " + min + ":" + sec + "\nF to Submit";
    }
    void StartGame()
    {
        startGame = true;
    }
    void EndGame()
    {
        //
        AudioManager.Instance.PlayMusic("Museum20s");
        FinalCutScene.SetActive(true);
        UI.SetActive(false);
        OldCam.SetActive(false);
        NewCam.SetActive(true);
        Cannon.SetActive(false);
        GameMgr.Instance.enableInput = false;
        //museumCam.
        //museumCamAnim.SetTrigger("End");
        //

    }
    private void OnDisable()
    {
        EventCenter.RemoveListener(FunctionType.StartGame, StartGame);
    }
}
