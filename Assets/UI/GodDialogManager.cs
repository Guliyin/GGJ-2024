using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.AI;

public class GodDialogManager : MonoBehaviour
{

    public GameObject dialogContent;
    public string[] allDialogs;
    public string[] complimentDialogs;
    public string[] neutralDialogs;
    public string[] blameDialogs;
    public string cupDialog;
    public string thatDialog;
    public float textFadeTime;
    private int PlayingTextAniNumbers;
    private int complimentIndex;
    private int neutralIndex;
    private int blameIndex;

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

        complimentDialogs = new string[]{
            "Nice Shot!",
            "Well, well, well, look who's a sharpshooter extraordinaire!",
            "Time to apply for the Olympics shooting!",
            "Good job my fellow!",
            "Watch out fellows, we've got a sharpshooting legend in our midst!",
            "Maybe I can introduce you to Artemis next time."
        };

        neutralDialogs = new string[]{
            "What about asking Poseidon and Hermes to lower the difficulty?",
            "I kind of regret hiring you.",
            "Rub your eyes!",
        };

        blameDialogs = new string[]{
            "You mischievous bunch!",
            "Is this a comedy act?",
            "Ah, you prankster!",
            "You just cannot resist adding a touch of chaos to the mix?",
            "Your shenanigans had me in stitches!",
            "You foolish mortal!"
        };

        cupDialog = "This cup comes out of your pay.";
        thatDialog = "This is a blasphemy!";

        complimentIndex = 0;
        neutralIndex = 0;
        blameIndex = 0;

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
        GetComponent<Image>().DOColor(new(1, 1, 1, 1), 0.25f);
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

    public void playDialogBlame()
    {
        PlayingTextAniNumbers++;
        GetComponent<Image>().DOColor(new(1, 1, 1, 1), 0.25f);
        TextFade(blameDialogs[blameIndex], textFadeTime);
        blameIndex = (blameIndex + 1) % blameDialogs.Length;
        StartCoroutine(TextBoxKill());
    }
    public void playDialogNeutral()
    {
        PlayingTextAniNumbers++;
        GetComponent<Image>().DOColor(new(1, 1, 1, 1), 0.25f);
        TextFade(neutralDialogs[neutralIndex], textFadeTime);
        neutralIndex = (neutralIndex + 1) % neutralDialogs.Length;
        StartCoroutine(TextBoxKill());
    }
    public void playDialogCompliment()
    {
        PlayingTextAniNumbers++;
        GetComponent<Image>().DOColor(new(1, 1, 1, 1), 0.25f);
        TextFade(complimentDialogs[complimentIndex], textFadeTime);
        complimentIndex = (complimentIndex + 1) % complimentDialogs.Length;
        StartCoroutine(TextBoxKill());
    }
    public void playDialogCup()
    {
        PlayingTextAniNumbers++;
        GetComponent<Image>().DOColor(new(1, 1, 1, 1), 0.25f);
        TextFade(cupDialog, textFadeTime);
        StartCoroutine(TextBoxKill());
    }
    public void playDialogThat()
    {
        PlayingTextAniNumbers++;
        GetComponent<Image>().DOColor(new(1, 1, 1, 1), 0.25f);
        TextFade(thatDialog, textFadeTime);
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
            GetComponent<Image>().DOColor(new(1, 1, 1, 0), 0.1f);
            dialogContent.GetComponent<TMP_Text>().text = "";
        }
        PlayingTextAniNumbers--;
    }

}
