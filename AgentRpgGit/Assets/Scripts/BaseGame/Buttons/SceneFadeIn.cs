using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneFadeIn : MonoBehaviour
{
    [SerializeField]
    bool hasActivated = false;
    [SerializeField]
    GameObject fadeInObject;
    [SerializeField]
    float timeUntilChange;
    [SerializeField]
    string SceneName;
    public void Activate()
    {
        Time.timeScale = 1;
        hasActivated = true;
        Instantiate(fadeInObject);
    }
    public void Update()
    {
        if(hasActivated == true)
        {
            timeUntilChange -= Time.deltaTime;
            if (timeUntilChange < 0)
            {
                SceneManager.LoadScene(SceneName);
            }
            
        }
    }
}
