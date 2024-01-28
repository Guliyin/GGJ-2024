using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    private int selection;
    public GameObject[] allButtons;
    public GameObject[] gameUI;
    public GameObject credits;
    private bool isCreditsShowing;

    void Awake()
    {
        selection = 0;
        isCreditsShowing = false;
    }

    void Update()
    {
        if (UIManager.Instance.currentProgress == UIManager.GameProgress.MainMenu)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (selection > 0)
                {
                    selection--;
                    AudioManager.Instance.PlaySFX("MenuSwitch");
                    UpdateSelection();
                }
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (selection < 2)
                {
                    selection++;
                    AudioManager.Instance.PlaySFX("MenuSwitch");
                    UpdateSelection();
                }
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                AudioManager.Instance.PlaySFX("ButtonPress");
                Selected();
            }
        }
    }

    public void UpdateSelection()
    {
        foreach (GameObject a in allButtons)
        {
            a.transform.GetChild(0).gameObject.SetActive(false);
        }
        allButtons[selection].transform.GetChild(0).gameObject.SetActive(true);
    }
    public void Selected()
    {
        switch (selection)
        {
            case 0:
                UIManager.Instance.StartGame();
                foreach (GameObject ui in gameUI)
                {
                    ui.SetActive(true);
                }
                break;
            case 1:
                //credits.SetActive(!credits.activeSelf);
                if (!isCreditsShowing)
                {
                    credits.GetComponent<Image>().DOColor(new(1, 1, 1, 1), 0.2f);
                }
                else
                {
                    credits.GetComponent<Image>().DOColor(new(1, 1, 1, 0), 0.2f);
                }
                isCreditsShowing = !isCreditsShowing;

                break;
            case 2:
                //UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
                break;
            default: break;
        }

    }


}
