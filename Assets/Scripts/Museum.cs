using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Museum : MonoBehaviour
{
    public void BackToSceneOne()
    {
        SceneManager.LoadScene(0);
    }
}
