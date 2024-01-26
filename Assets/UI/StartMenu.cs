using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

    public GameObject startMenuBG;

    public void PressStartGame()
    {
        startMenuBG.SetActive(false);
    }

}
