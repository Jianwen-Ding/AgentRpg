using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMove : MonoBehaviour
{
    public int MoveIndex;
    [SerializeField]
    CharacterRememberance CharacterRemembered;
    [SerializeField]
    string LoadScene;
    // Start is called before the first frame update
    void Start()
    {
        CharacterRemembered = GameObject.FindGameObjectWithTag("CharacterRemeberance").GetComponent<CharacterRememberance>();
    }

    // Update is called once per frame
    public void Activate()
    {
        switch(PlayerPrefs.GetInt("CharacterIndexRemember", -69))
        {
            case -69:
                print("Error- PlayerPrefNotFOund, check Select Move");
                break;
            case 0:
                CharacterRemembered.MovesPutIn1[PlayerPrefs.GetInt("MoveIndexRemember",0)] = MoveIndex;
                break;
            case 1:
                CharacterRemembered.MovesPutIn2[PlayerPrefs.GetInt("MoveIndexRemember", 0)] = MoveIndex;
                break;
            case 2:
                CharacterRemembered.MovesPutIn3[PlayerPrefs.GetInt("MoveIndexRemember", 0)] = MoveIndex;
                break;
        }
        SceneManager.LoadScene(LoadScene);
    }
}
