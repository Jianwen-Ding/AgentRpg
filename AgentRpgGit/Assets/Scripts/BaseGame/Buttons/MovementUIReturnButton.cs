using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUIReturnButton : MonoBehaviour
{
    [SerializeField]
    ButtonBase ButtonBaseThing;
    // Update is called once per frame
    void Update()
    {
        if (ButtonBaseThing.ButtonActivate && ButtonBaseThing.UIBase.GetComponent<MovementUI>().CurrentCharacterInPlay != 0)
        {
            MovementUI MovementInfo = ButtonBaseThing.UIBase.GetComponent<MovementUI>();
            MovementInfo.CurrentCharactersInPlay[MovementInfo.CurrentCharacterInPlay].GetComponent<CharacterBase>().CharacterSChanger.SetSprite(-69, 0);
            MovementInfo.Scenes = "main";
            MovementInfo.HasEstablishedScene = false;
            MovementInfo.CurrentCharacterInPlay--;
            if (MovementInfo.CurrentCharactersInPlay[MovementInfo.CurrentCharacterInPlay].GetComponent<CharacterBase>().IsCharging != true)
            {
                MovementInfo.CurrentCharactersInPlay[MovementInfo.CurrentCharacterInPlay].GetComponent<CharacterBase>().RemoveHighLights(MovementInfo.StoredTargetCoordinates[MovementInfo.CurrentCharacterInPlay], MovementInfo.StoredActions[MovementInfo.CurrentCharacterInPlay]);
            }
        }
    }
}
