using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misty : GenericMove
{
    public override void SetAdjust()
    {
        AreaSelectionSquareX0 = -2;
        AreaSelectionSquareY0 = 0;
        AreaSelectionSquareWidth0 = -2;
        AreaSelectionSquareHeight0 = 0;
        AreaSelectionSquareX1 = 2;
        AreaSelectionSquareY1 = 0;
        AreaSelectionSquareWidth1 = 2;
        AreaSelectionSquareHeight1 = 0;
        AreaSelectionSquareX2 = 0;
        AreaSelectionSquareY2 = 2;
        AreaSelectionSquareWidth2 = 0;
        AreaSelectionSquareHeight2 = 2;
        AreaSelectionSquareX3 = 0;
        AreaSelectionSquareY3 = -2;
        AreaSelectionSquareWidth3 = 0;
        AreaSelectionSquareHeight3 = -2;
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
        MouseFollowingUI.GroupSelection[0][0] = -1;
        MouseFollowingUI.GroupSelection[0][1] = -1;
        MouseFollowingUI.GroupSelection[0][2] = 2;
        MouseFollowingUI.GroupSelection[0][3] = 2;
    }
    public override void SelectionAdjustment()
    {
        MouseFollowingUI.GroupSelection[0][0] = -1;
        MouseFollowingUI.GroupSelection[0][1] = -1;
        MouseFollowingUI.GroupSelection[0][2] = 2;
        MouseFollowingUI.GroupSelection[0][3] = 2;
        MouseFollowingUI.IsSelecting = true;
        MouseFollowingUI.ObstacleSelectAllowed = true;
        MouseFollowingUI.CharacterSelectAllowed = true;
        MouseFollowingUI.WillGroupSelect = true;
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
        EffectAmount = 0;
        AreaEffect((int)Character_Info.LocationAction.x - 1, (int)Character_Info.LocationAction.y - 1, 3, 3);
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (Character_Info.LocationAction.x + x < Gridinfo.XWidthPublic && Character_Info.LocationAction.x + x >= 0 && Character_Info.LocationAction.y + y < Gridinfo.YWidthPublic && Character_Info.LocationAction.y + y >= 0)
                {
                    Gridinfo.AllGrids[(int)Character_Info.LocationAction.y + y][(int)Character_Info.LocationAction.x + x].GetComponent<GridControl>().StatusIndex = 5;
                }
            }
        }
        gameObject.GetComponent<CharacterBase>().action = "inactive";
    }
}
