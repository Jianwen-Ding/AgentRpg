using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMoveButton : MonoBehaviour
{
    [SerializeField]
    MoveDisplay MoveDisplayChange;
    [SerializeField]
    CharacterRememberance CharacterRemembered;
    [SerializeField]
    int CharacterIndex;
    [SerializeField]
    int MoveIndexInRemember;
    [SerializeField]
    string SceneName;
    [SerializeField]
    int OverallMoveIndex = 0;
    public bool Activated = false;
    
    
    private void Start()
    {
       
        MoveDisplayChange = gameObject.GetComponent<MoveDisplay>();
        CharacterRemembered = GameObject.FindGameObjectWithTag("CharacterRemeberance").GetComponent<CharacterRememberance>();
        switch (CharacterIndex)
        {
            default:
                break;
            case 0:
                OverallMoveIndex = CharacterRemembered.MovesPutIn1[MoveIndexInRemember];
                break;
            case 1:
                OverallMoveIndex = CharacterRemembered.MovesPutIn2[MoveIndexInRemember];
                break;
            case 2:
                OverallMoveIndex = CharacterRemembered.MovesPutIn3[MoveIndexInRemember];
                break;

        }
        MoveDisplayChange.InsertMoveIndex(OverallMoveIndex);
    }
    public void Activate()
    {
        PlayerPrefs.SetInt("CharacterIndexRemember", CharacterIndex);
        PlayerPrefs.SetInt("MoveIndexRemember", MoveIndexInRemember);
        SceneManager.LoadScene(SceneName);
    }
}
