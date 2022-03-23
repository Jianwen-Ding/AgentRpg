using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueScript : MonoBehaviour
{
    [SerializeField]
    ButtonBase ButtonBaseThing;
    // Update is called once per frame
    void Update()
    {
        if (ButtonBaseThing.ButtonActivate && ButtonBaseThing.UIBase.GetComponent<MovementUI>().CurrentCharactersInPlay.Length > ButtonBaseThing.UIBase.GetComponent<MovementUI>().CurrentCharacterInPlay)
        {
            MovementUI MovementInfo = ButtonBaseThing.UIBase.GetComponent<MovementUI>();
            MovementInfo.CurrentCharactersInPlay[MovementInfo.CurrentCharacterInPlay].GetComponent<CharacterBase>().CharacterSChanger.SetSprite(-69, 0);
            MovementInfo.CurrentCharacterInPlay++;
            MovementInfo.Scenes = "main";
            MovementInfo.MouseFollowingUI.IsSelecting = false;
            MovementInfo.HasEstablishedScene = false;
        }
    }
}
