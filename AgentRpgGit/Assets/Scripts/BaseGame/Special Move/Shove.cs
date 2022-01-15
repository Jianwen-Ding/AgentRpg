using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shove : GenericMove
{
    public override void SetAdjust()
    {
        AreaSelectionSquareX0 = -1;
        AreaSelectionSquareY0 = 0;
        AreaSelectionSquareWidth0 = -1;
        AreaSelectionSquareHeight0 = 0;
        AreaSelectionSquareX1 = 0;
        AreaSelectionSquareY1 = -1;
        AreaSelectionSquareWidth1 = 0;
        AreaSelectionSquareHeight1 = -1;
        AreaSelectionSquareX2 = 1;
        AreaSelectionSquareY2 = 0;
        AreaSelectionSquareWidth2 = 1;
        AreaSelectionSquareHeight2 = 0;
        AreaSelectionSquareX3 = 0;
        AreaSelectionSquareY3 = 1;
        AreaSelectionSquareWidth3 = 0;
        AreaSelectionSquareHeight3 = 1;
        AreaSelectionSquareX4 = -69;
        AreaSelectionSquareY4 = -69;
        AreaSelectionSquareWidth4 = -69;
        AreaSelectionSquareHeight4 = -69;
        WillUseForSquareX0 = 0;
        WillUseForSquareY0 = 0;
        WillUseForSquareWidth0 = 0;
        WillUseForSquareHeight0 = 0;
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
        MouseFollowingUI.GroupSelection[0][0] = -1;
        MouseFollowingUI.GroupSelection[0][1] = 0;
        MouseFollowingUI.GroupSelection[0][2] = 2;
        MouseFollowingUI.GroupSelection[0][3] = 0;
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
        Vector2 CharacterReturn = new Vector2(0, 0);
        if (Character_Info.LocationAction.x < Character_Info.CharacterLocationIndex.x)
        {
            CharacterReturn = new Vector2(Character_Info.CharacterLocationIndex.x - 4, Character_Info.CharacterLocationIndex.y);
        }
        if (Character_Info.LocationAction.x > Character_Info.CharacterLocationIndex.x)
        {
            CharacterReturn = new Vector2(Character_Info.CharacterLocationIndex.x + 4, Character_Info.CharacterLocationIndex.y);
        }
        if (Character_Info.LocationAction.y < Character_Info.CharacterLocationIndex.y)
        {
            CharacterReturn = new Vector2(Character_Info.CharacterLocationIndex.x, Character_Info.CharacterLocationIndex.y - 4);
        }
        if (Character_Info.LocationAction.y > Character_Info.CharacterLocationIndex.y)
        {
            CharacterReturn = new Vector2(Character_Info.CharacterLocationIndex.x, Character_Info.CharacterLocationIndex.y + 4);
        }
        Character_Info.CharacterSChanger.SetSprite(1, 2);
        EffectAmount = 0;
        AreaEffect((int)Character_Info.LocationAction.x, (int)Character_Info.LocationAction.y, 1, 1);
        gameObject.GetComponent<CharacterBase>().action = "inactive";
        for (int x = 0; x < AreaCheck((int)Character_Info.LocationAction.x, (int)Character_Info.LocationAction.y, 1, 1).Length; x++)
        {
            GameObject[] AreaCharacters = AreaCheck((int)Character_Info.LocationAction.x, (int)Character_Info.LocationAction.y, 1, 1);
            CharacterBase CheckedCharacterBase;
            if (AreaCharacters[x] != null)
            {
                CheckedCharacterBase = AreaCharacters[x].GetComponent<CharacterBase>();
                CheckedCharacterBase.Push((int)CharacterReturn.x, (int)CharacterReturn.y);
            }
        }
    }
}