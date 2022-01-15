using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcharoo : GenericMove
{
    public override void SetAdjust()
    {
        AreaSelectionSquareX0 = -5;
        AreaSelectionSquareY0 = -5;
        AreaSelectionSquareWidth0 = 5;
        AreaSelectionSquareHeight0 = 5;
        AreaSelectionSquareX1 = -69;
        AreaSelectionSquareY1 = -69;
        AreaSelectionSquareWidth1 = -69;
        AreaSelectionSquareHeight1 = -69;
        AreaSelectionSquareX2 = -69;
        AreaSelectionSquareY2 = -69;
        AreaSelectionSquareWidth2 = -69;
        AreaSelectionSquareHeight2 = -69;
        AreaSelectionSquareX3 = -69;
        AreaSelectionSquareY3 = -69;
        AreaSelectionSquareWidth3 = -69;
        AreaSelectionSquareHeight3 = -69;
        AreaSelectionSquareX4 = -69;
        AreaSelectionSquareY4 = -69;
        AreaSelectionSquareWidth4 = -69;
        AreaSelectionSquareHeight4 = -69;
        WillUseForSquareX0 = -1;
        WillUseForSquareY0 = -1;
        WillUseForSquareWidth0 = 1;
        WillUseForSquareHeight0 = 1;
        WillUseForSquareX1 = -69;
        WillUseForSquareY1 = -69;
        WillUseForSquareWidth1 = -69;
        WillUseForSquareHeight1 = -69;
        WillUseForSquareX2 = -69;
        WillUseForSquareY2 = -69;
        WillUseForSquareWidth2 = -69;
        WillUseForSquareHeight2 = -69;
        WillUseForSquareX3 = -69;
        WillUseForSquareY3 = -69;
        WillUseForSquareWidth3 = -69;
        WillUseForSquareHeight3 = -69;
        WillUseForSquareX4 = -69;
        WillUseForSquareY4 = -69;
        WillUseForSquareWidth4 = -69;
        WillUseForSquareHeight4 = -69;
    }
    public override void SelectionAdjustment()
    {
        MouseFollowingUI.IsSelecting = true;
        MouseFollowingUI.ObstacleSelectAllowed = false;
        MouseFollowingUI.CharacterSelectAllowed = true;
        MouseFollowingUI.WillGroupSelect = false;
        for (int i = 0; i < AreaCanClick.Length; i++)
        {
            //-69 is the signal to null out a SelectionSquare
            if (AreaCanClick[i][0] != -69 && AreaCanClick[i][1] != -69 && AreaCanClick[i][2] != -69 && AreaCanClick[i][3] != -69)
            {
                MouseFollowingUI.AllowedSelected[i][1] = (int)gameObject.GetComponent<CharacterBase>().CharacterLocationIndex.y + AreaCanClick[i][1];
                MouseFollowingUI.AllowedSelected[i][2] = (int)gameObject.GetComponent<CharacterBase>().CharacterLocationIndex.x + AreaCanClick[i][2];
                MouseFollowingUI.AllowedSelected[i][3] = (int)gameObject.GetComponent<CharacterBase>().CharacterLocationIndex.y + AreaCanClick[i][3];
                MouseFollowingUI.AllowedSelected[i][0] = (int)gameObject.GetComponent<CharacterBase>().CharacterLocationIndex.x + AreaCanClick[i][0];
            }
        }
    }
    public override void ActivateMove()
    {
        Character_Info.CharacterSChanger.SetSprite(1, 2);
        EffectAmount = 0;
        AreaEffect((int)Character_Info.LocationAction.x, (int)Character_Info.LocationAction.y, 1, 1);
        gameObject.GetComponent<CharacterBase>().action = "inactive";
        for (int x = 0; x < AreaCheck((int)Character_Info.LocationAction.x, (int)Character_Info.LocationAction.y, 1, 1).Length; x++)
        {
            GameObject[] AreaCharacters = AreaCheck((int)Character_Info.LocationAction.x, (int)Character_Info.LocationAction.y, 1, 1);
            CharacterBase CheckedCharacterBase;
            GameObject InWorldText;
            if (AreaCharacters[x] != null)
            {
                CheckedCharacterBase = AreaCharacters[x].GetComponent<CharacterBase>();
                if (CheckedCharacterBase.IsEnemy == Character_Info.IsEnemy)
                {
                    Vector2 temp = CheckedCharacterBase.CharacterLocationIndex;
                    CheckedCharacterBase.RemoveHighLights();
                    CheckedCharacterBase.PushLocationActions((int)Character_Info.CharacterLocationIndex.x - (int)CheckedCharacterBase.CharacterLocationIndex.x, (int)Character_Info.CharacterLocationIndex.y - (int)CheckedCharacterBase.CharacterLocationIndex.y);
                    CheckedCharacterBase.CharacterLocationIndex = Character_Info.CharacterLocationIndex;
                    Character_Info.RemoveHighLights();
                    Character_Info.PushLocationActions((int)temp.x - (int)Character_Info.CharacterLocationIndex.x , (int)temp.y - (int)Character_Info.CharacterLocationIndex.y);
                    Character_Info.CharacterLocationIndex = temp;
                }
            }
        }
    }
}
