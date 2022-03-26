using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leap : GenericMove
{
    public override void SetAdjust()
    {
            AreaSelectionSquareX0 = 0;
            AreaSelectionSquareY0 = 3;
            AreaSelectionSquareWidth0 = 0;
            AreaSelectionSquareHeight0 = 3;
            AreaSelectionSquareX1 = 3;
            AreaSelectionSquareY1 = 0;
            AreaSelectionSquareWidth1 = 3;
            AreaSelectionSquareHeight1 = 0;
            AreaSelectionSquareX2 = -3;
            AreaSelectionSquareY2 = 0;
            AreaSelectionSquareWidth2 = -3;
            AreaSelectionSquareHeight2 = 0;
            AreaSelectionSquareX3 = 0;
            AreaSelectionSquareY3 = -3;
            AreaSelectionSquareWidth3 = 0;
            AreaSelectionSquareHeight3 = -3;
            AreaSelectionSquareX4 = -69;
            AreaSelectionSquareY4 = -69;
            AreaSelectionSquareWidth4 = -69;
            AreaSelectionSquareHeight4 = -69;
            AreaSelectMoveSquareX0 = 0;
            AreaSelectMoveSquareY0 = 0;
            AreaSelectMoveSquareWidth0 = 0;
            AreaSelectMoveSquareHeight0 = 0;
            AreaSelectMoveSquareX1 = -69;
            AreaSelectMoveSquareY1 = -69;
            AreaSelectMoveSquareWidth1 = -69;
            AreaSelectMoveSquareHeight1 = -69;
            AreaSelectMoveSquareX2 = -69;
            AreaSelectMoveSquareY2 = -69;
            AreaSelectMoveSquareWidth2 = -69;
            AreaSelectMoveSquareHeight2 = -69;
            AreaSelectMoveSquareX3 = -69;
            AreaSelectMoveSquareY3 = -69;
            AreaSelectMoveSquareWidth3 = -69;
            AreaSelectMoveSquareHeight3 = -69;
            AreaSelectMoveSquareX4 = -69;
            AreaSelectMoveSquareY4 = -69;
            AreaSelectMoveSquareWidth4 = -69;
            AreaSelectMoveSquareHeight4 = -69;
            willUseForMove = true;
            PriorityAdd = -10;
    }
    public override void SelectionAdjustment()
    {
        MouseFollowingUI.IsSelecting = true;
        MouseFollowingUI.ObstacleSelectAllowed = false;
        MouseFollowingUI.CharacterSelectAllowed = false;
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
    // Update is called once per frame
    public override void ActivateMove()
    {
        Character_Info.CharacterSChanger.SetSprite(1, 2);
        EffectAmount = 0;
        Character_Info.action = "inactive";
        Character_Info.Push((int)Character_Info.LocationAction.x, (int)Character_Info.LocationAction.y);
        Character_Info.DefenseMultiplier -= (float)0.5;
    }
}
