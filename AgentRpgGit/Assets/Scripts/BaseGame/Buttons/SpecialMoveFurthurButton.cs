using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpecialMoveFurthurButton : MonoBehaviour
{
    [SerializeField]
    ButtonBase ButtonBaseThing;

    [SerializeField]
    GameObject TextInGameObject;

    public GameObject Character;
    public int index;
    void Start()
    {
        if(index == -69)
        {
            TextInGameObject.GetComponent<TextMeshProUGUI>().text = "Move Not Set";
        }
        else if (index <= Character.GetComponent<CharacterBase>().MovesAllowed.Length - 1)
        {
            TextInGameObject.GetComponent<TextMeshProUGUI>().text = Character.GetComponent<CharacterBase>().MovesAllowed[index].GetType().Name;
        }
        else
        {
            TextInGameObject.GetComponent<TextMeshProUGUI>().text = "Error- Check SpecialMoveFurthurButton, index past MovesAllowedLength";
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (ButtonBaseThing.ButtonActivate && index != 69)
        {
            ButtonBaseThing.UIBase.GetComponent<MovementUI>().Scenes = "SPMove" + index;
            ButtonBaseThing.UIBase.GetComponent<MovementUI>().HasEstablishedScene = false;
        }
    }
}
