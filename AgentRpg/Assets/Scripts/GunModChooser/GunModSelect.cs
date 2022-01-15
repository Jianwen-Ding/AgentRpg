using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GunModSelect : MonoBehaviour
{
    public string SceneName;
    public int CurrentIndex;
    public void Activate()
    {
        GameObject.FindGameObjectWithTag("CharacterRemeberance").GetComponent<CharacterRememberance>().GunFunctionIndex3[PlayerPrefs.GetInt("CharacterIndexGunModRemember", 0)] = CurrentIndex;
        SceneManager.LoadScene(SceneName);
    }
}
