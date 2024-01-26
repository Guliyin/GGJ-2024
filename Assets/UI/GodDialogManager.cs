using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GodDialogManager : MonoBehaviour
{

    public GameObject dialogContent;
    public string[] allDialogs;
    public float textFadeTime;

    void Awake()
    {
        textFadeTime = 0.5f;

        allDialogs = new string[]{
            "1111111111",
            "2222222222",
            "3333333333",
            "4444444444",
            "5555555555"
        };

    }

    public void TextFade(string toText, float fadeTime)
    {
        string temp = dialogContent.GetComponent<TMP_Text>().text;
        var t = DOTween.To(() => string.Empty, value => dialogContent.GetComponent<TMP_Text>().text = value, toText, fadeTime).SetEase(Ease.Linear);
        t.SetOptions(true);
    }

    public void PlayRandomDialog()
    {
        int dialogNumbers = allDialogs.Length;
        int randomPlayIndex = Random.Range(0, dialogNumbers);
        TextFade(allDialogs[randomPlayIndex], textFadeTime);
    }

    public void PlayDialogByIndex(int index)
    {
        TextFade(allDialogs[index], textFadeTime);
        PlayDialogVoice(index);
    }

    public void PlayDialogVoice(int index)
    {
        print("voice play" + index.ToString());
    }

}
