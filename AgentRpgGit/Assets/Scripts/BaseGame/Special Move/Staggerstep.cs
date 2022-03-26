using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staggerstep : Artillery
{
    public float speedDownTimes;
    public override int[] CheckIfConditionsApply(Vector2 areaCheckFrom)
    {
        int[] DoesConditionsApply = new int[3];
        DoesConditionsApply[0] = -69;
        DoesConditionsApply[1] = -69;
        DoesConditionsApply[2] = -69;
        if (willUseForGridEffect)
        {
            for (int x = -1; x <= 1; x++)
            {
                for(int y = -1; y <= 1; y++)
                {
                    int[][] newAreaCanSelect = new int[5][];
                    for (int z = 0; z < newAreaCanSelect.Length; z++)
                    {
                        newAreaCanSelect[z] = new int[4];
                        //-69 is the signal to null out a SelectionSquare
                        if (newAreaCanSelect[z][0] != -69 && newAreaCanSelect[z][1] != -69 && newAreaCanSelect[z][2] != -69 && newAreaCanSelect[z][3] != -69)
                        {
                            newAreaCanSelect[z][1] = (int)areaCheckFrom.y + AreaCanClick[z][1];
                            newAreaCanSelect[z][2] = (int)areaCheckFrom.x + AreaCanClick[z][2];
                            newAreaCanSelect[z][3] = (int)areaCheckFrom.y + AreaCanClick[z][3];
                            newAreaCanSelect[z][0] = (int)areaCheckFrom.x + AreaCanClick[z][0];
                        }
                    }
                    Vector2 LocationSpot = new Vector2(areaCheckFrom.x + x , areaCheckFrom.y + y);
                    if (LocationSpot.x >= 0 && LocationSpot.y >= 0&& LocationSpot.x < Gridinfo.XWidthPublic && LocationSpot.y < Gridinfo.YWidthPublic && Character_Info != null && LocationSpot.x != -69 && LocationSpot.y != -69 && Gridinfo.AllGrids[(int)LocationSpot.y][(int)LocationSpot.x].GetComponent<GridControl>().CharacterOn == null && Gridinfo.AllGrids[(int)LocationSpot.y][(int)LocationSpot.x].GetComponent<GridControl>().ObstacleIndex == 0)
                    {
                        DoesConditionsApply[2] = (int)(speedDownTimes * (-Character_Info.SpeedMultiplier + 1));
                        Vector2 Location = LocationSpot;
                        DoesConditionsApply[0] = (int)Location.x;
                        DoesConditionsApply[1] = (int)Location.y;
                    }
                }
               
            }

        }
        return DoesConditionsApply;

    }
    public override void SetAdjust()
    {
        AreaSelectionSquareX0 = -1;
        AreaSelectionSquareY0 = -1;
        AreaSelectionSquareWidth0 = 1;
        AreaSelectionSquareHeight0 = 1;
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
        willUseForGridEffect = true;
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
        if (Character_Info.IsCharging == false && HasUsedCharge == false)
        {
            int[][] NewSelectedArea;
            NewSelectedArea = NewAreaEffectMove(PlayerSpacesAllowedAdjust, (int)Character_Info.LocationAction.x, (int)Character_Info.LocationAction.y);
            AreaSoonToEffect = NewSelectedArea;
            AreaHighLightToggle(NewSelectedArea, Character_Info.IsEnemy, true);
            Character_Info.IsCharging = true;
            if(Character_Info.SpeedMultiplier < 1)
            {
                Character_Info.SpeedMultiplier = 1;
            }
            else
            {
                Character_Info.SpeedMultiplier += (float)0.1;
            }
        }
        if (HasUsedCharge == true && MoveDecison.IsDisplayingHappening == true)
        {
            HasUsedCharge = false;
            Character_Info.IsCharging = false;
            AreaHighLightToggle(AreaSoonToEffect, Character_Info.IsEnemy, false);
            Character_Info.Push((int)Character_Info.LocationAction.x, (int)Character_Info.LocationAction.y);
            gameObject.GetComponent<CharacterBase>().action = "inactive";
            Character_Info.action = "inactive";
        }
    }
}
