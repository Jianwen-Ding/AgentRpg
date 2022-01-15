using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GunModChangeButton : MonoBehaviour
{
    [SerializeField]
    int CharacterIndex;
    [SerializeField]
    GameObject TextObject;
    [SerializeField]
    string SceneLoad;
    // Start is called before the first frame update
    void Start()
    {
        TextObject.GetComponent<TextMeshProUGUI>().text =  "Gun Mod: "+ GameObject.FindGameObjectWithTag("CharacterRemeberance").GetComponent<CharacterRememberance>().GunName[GameObject.FindGameObjectWithTag("CharacterRemeberance").GetComponent<CharacterRememberance>().GunFunctionIndex3[CharacterIndex]];
    }
    public void Activate()
    {
        PlayerPrefs.SetInt("CharacterIndexGunModRemember", CharacterIndex);
        PlayerPrefs.SetInt("IndexGunModRemember", GameObject.FindGameObjectWithTag("CharacterRemeberance").GetComponent<CharacterRememberance>().GunFunctionIndex3[CharacterIndex]);
        SceneManager.LoadScene(SceneLoad);
    }
}
