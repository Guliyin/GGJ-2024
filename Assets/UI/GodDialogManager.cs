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
    public static GodDialogManager Instance;
    public GameObject dialogContent;
    public string[] allDialogs;
    public string[] complimentDialogs;
    public string[] neutralDialogs;
    public string[] blameDialogs;
    public string[] introDialogs;
    public string cupDialog;
    public string thatDialog;
    public float textFadeTime;
    private int playingTextAniNumbers;
    private int complimentIndex;
    private int neutralIndex;
    private int blameIndex;
    private int introIndex;
    public bool canRespond;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }

        textFadeTime = 0.5f;
        playingTextAniNumbers = 0;
        canRespond = false;

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
            "Time to apply for the Olympics shooting match!",
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
            "You mischief-maker!",
            "Is this a comedy act?",
            "Ah, you prankster!",
            "You just cannot resist adding a touch of chaos to the mix?",
            "Your shenanigans had me in stitches!",
            "You foolish mortal!"
        };

        cupDialog = "This cup comes out of your pay.";
        thatDialog = "This is a blasphemy!";

        introDialogs = new string[]{
            "Well hello Pygmalion. I'm Dyonysus, the God of Wine.",
            "They said you're the best sculptor in the world.",
            "I now command you to complete all the facial features for me within 7 days.",
            "Take out your blueprint (press [TAB]) and examine it carefully!",
            "Win my favour or I will send you to greet Hades!!",
        };

        complimentIndex = 0;
        neutralIndex = 0;
        blameIndex = 0;
        introIndex = 0;

    }


    
    public void TextFade(string toText, float fadeTime)
    {
        string temp = dialogContent.GetComponent<TMP_Text>().text;
        var t = DOTween.To(() => string.Empty, value => dialogContent.GetComponent<TMP_Text>().text = value, toText, fadeTime).SetEase(Ease.Linear);
        t.SetOptions(true);
    }

    public void PlayRandomDialog()
    {
        playingTextAniNumbers++;
        GetComponent<Image>().DOColor(new(1, 1, 1, 1), 0.25f);
        int dialogNumbers = allDialogs.Length;
        int randomPlayIndex = Random.Range(0, dialogNumbers);
        TextFade(allDialogs[randomPlayIndex], textFadeTime);
        StartCoroutine(TextBoxKill());
    }



    public void PlayDialogBlame()
    {
        if (canRespond)
        {
            StartCoroutine(Blame());
        }

    }
    public void PlayDialogNeutral()
    {
        if (canRespond)
        {
            StartCoroutine(Neutral());
        }
            
    }
    public void PlayDialogCompliment()
    {
        if (canRespond)
        {
            StartCoroutine(Compliment());
        }
            
    }
    public void PlayDialogIntro()
    {
        playingTextAniNumbers++;
        GetComponent<Image>().DOColor(new(1, 1, 1, 1), 0.25f);
        TextFade(introDialogs[introIndex], textFadeTime);
        string temp = "intro" + (introIndex + 1).ToString();
        AudioManager.Instance.PlaySFX(temp);
        introIndex = (introIndex + 1) % introDialogs.Length;
        StartCoroutine(TextBoxKill());
    }
    public void PlayDialogCup()
    {
        if (canRespond)
        {
            StartCoroutine(Cup());
        }
            
    }
    public void PlayDialogThat()
    {
        if (canRespond)
        {
            StartCoroutine(That());
        }
            
    }

    public void OpeningDialog()
    {
        StartCoroutine(StartIntroDialog());
    }

    public void EndIntro()
    {

    }






    IEnumerator TextBoxKill()
    {
        yield return new WaitForSeconds(8f);
        if (playingTextAniNumbers == 1)
        {
            GetComponent<Image>().DOColor(new(1, 1, 1, 0), 0.1f);
            dialogContent.GetComponent<TMP_Text>().text = "";
        }
        playingTextAniNumbers--;
    }

    IEnumerator StartIntroDialog()
    {
        yield return new WaitForSeconds(1f);
        PlayDialogIntro();
        yield return new WaitForSeconds(7.8f);
        PlayDialogIntro();
        yield return new WaitForSeconds(4.5f);
        PlayDialogIntro();
        yield return new WaitForSeconds(7.8f);
        PlayDialogIntro();
        yield return new WaitForSeconds(7f);
        PlayDialogIntro();
        yield return new WaitForSeconds(7f);
        canRespond = true;
        EndIntro();
    }

    IEnumerator Blame()
    {
        yield return new WaitForSeconds(1f);
        playingTextAniNumbers++;
        GetComponent<Image>().DOColor(new(1, 1, 1, 1), 0.25f);
        TextFade(blameDialogs[blameIndex], textFadeTime);
        string temp = "blame" + (blameIndex + 1).ToString();
        AudioManager.Instance.PlaySFX(temp);
        blameIndex = (blameIndex + 1) % blameDialogs.Length;
        StartCoroutine(TextBoxKill());
    }
    IEnumerator Compliment()
    {
        yield return new WaitForSeconds(1f);
        playingTextAniNumbers++;
        GetComponent<Image>().DOColor(new(1, 1, 1, 1), 0.25f);
        TextFade(complimentDialogs[complimentIndex], textFadeTime);
        string temp = "compliment" + (complimentIndex + 1).ToString();
        AudioManager.Instance.PlaySFX(temp);
        complimentIndex = (complimentIndex + 1) % complimentDialogs.Length;
        StartCoroutine(TextBoxKill());
    }
    IEnumerator Neutral()
    {
        yield return new WaitForSeconds(1f);
        playingTextAniNumbers++;
        GetComponent<Image>().DOColor(new(1, 1, 1, 1), 0.25f);
        TextFade(neutralDialogs[neutralIndex], textFadeTime);
        string temp = "neutral" + (neutralIndex + 1).ToString();
        AudioManager.Instance.PlaySFX(temp);
        neutralIndex = (neutralIndex + 1) % neutralDialogs.Length;
        StartCoroutine(TextBoxKill());
    }
    IEnumerator Cup()
    {
        yield return new WaitForSeconds(1f);
        playingTextAniNumbers++;
        GetComponent<Image>().DOColor(new(1, 1, 1, 1), 0.25f);
        TextFade(cupDialog, textFadeTime);
        AudioManager.Instance.PlaySFX("cup");
        StartCoroutine(TextBoxKill());
    }
    IEnumerator That()
    {
        yield return new WaitForSeconds(1f);
        playingTextAniNumbers++;
        GetComponent<Image>().DOColor(new(1, 1, 1, 1), 0.25f);
        TextFade(thatDialog, textFadeTime);
        AudioManager.Instance.PlaySFX("that");
        StartCoroutine(TextBoxKill());
    }

}
