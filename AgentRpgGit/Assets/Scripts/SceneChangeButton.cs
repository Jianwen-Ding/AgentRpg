using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{
    [SerializeField]
    string SceneName;
    public void Activate()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneName);
    }
}
