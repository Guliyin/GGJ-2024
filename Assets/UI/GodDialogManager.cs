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
    private int PlayingTextAniNumbers;

    void Awake()
    {
        textFadeTime = 0.5f;
        PlayingTextAniNumbers = 0;

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
        PlayingTextAniNumbers++;
        GetComponent<Image>().DOColor(new(0, 0, 0, 1), 0.25f);
        int dialogNumbers = allDialogs.Length;
        int randomPlayIndex = Random.Range(0, dialogNumbers);
        TextFade(allDialogs[randomPlayIndex], textFadeTime);
        StartCoroutine(TextBoxKill());
    }

    public void PlayDialogByIndex(int index)
    {
        PlayingTextAniNumbers++;
        TextFade(allDialogs[index], textFadeTime);
        PlayDialogVoice(index);
        StartCoroutine(TextBoxKill());
    }

    public void PlayDialogVoice(int index)
    {
        print("voice play" + index.ToString());
    }




    IEnumerator TextBoxKill()
    {
        yield return new WaitForSeconds(3f);
        if (PlayingTextAniNumbers == 1)
        {
            GetComponent<Image>().DOColor(new(0, 0, 0, 0), 0.1f);
            dialogContent.GetComponent<TMP_Text>().text = "";
        }
        PlayingTextAniNumbers--;
    }

}
