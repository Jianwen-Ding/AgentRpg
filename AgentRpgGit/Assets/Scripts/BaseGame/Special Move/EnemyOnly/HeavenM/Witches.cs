using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witches : GenericMove
{
    public int moveBot;
    public override void SetAdjust()
    {
        moveBot = -69;
        AreaSelectionSquareX0 = -10;
        AreaSelectionSquareY0 = -10;
        AreaSelectionSquareWidth0 = 10;
        AreaSelectionSquareHeight0 = 10;
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
        MouseFollowingUI.GroupSelection[0][0] = -1;
        MouseFollowingUI.GroupSelection[0][1] = 0;
        MouseFollowingUI.GroupSelection[0][2] = 2;
        MouseFollowingUI.GroupSelection[0][3] = 0;
        PriorityAdd = 35;
        willUseForGridEffect = true;
    }
    public override void SelectionAdjustment()
    {
        MouseFollowingUI.GroupSelection[0][0] = -1;
        MouseFollowingUI.GroupSelection[0][1] = 0;
        MouseFollowingUI.GroupSelection[0][2] = 2;
        MouseFollowingUI.GroupSelection[0][3] = 1;
        MouseFollowingUI.IsSelecting = true;
        MouseFollowingUI.ObstacleSelectAllowed = false;
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
    public override int[] CheckIfConditionsApply(Vector2 areaCheckFrom)
    {
        int[] DoesConditionsApply = new int[3];
        DoesConditionsApply[0] = -69;
        DoesConditionsApply[1] = -69;
        DoesConditionsApply[2] = -69;
        if (willUseForGridEffect)
        {
            for (int i = 0; i < BotAiCheckIfApply.Opponents.Length; i++)
            {
                if (BotAiCheckIfApply.Opponents[i].IsDead == false && Character_Info != null && BotAiCheckIfApply.Opponents[i].ExpressedSpeed > Character_Info.ExpressedSpeed)
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
                    Vector2 LocationSpot = CheckIfLocationCorrespondsToAction((int)BotAiCheckIfApply.Opponents[i].CharacterLocationIndex.x, (int)BotAiCheckIfApply.Opponents[i].CharacterLocationIndex.y, PlayerSpacesAllowedAdjust, newAreaCanSelect);
                    if (BotAiCheckIfApply.Opponents[i] != null && Character_Info != null && LocationSpot.x != -69 && LocationSpot.y != -69)
                    {
                        DoesConditionsApply[2] = PriorityAdd;
                        Vector2 Location = LocationSpot;
                        DoesConditionsApply[0] = (int)Location.x;
                        DoesConditionsApply[1] = (int)Location.y;
                    }
                }
            }

        }
        return DoesConditionsApply;

    }
    // Update is called once per frame
    public override void Update()
    {
        if (moveBot == -69 && BotAiCheckIfApply != null)
        {
            for (int x = 0; x < BotAiCheckIfApply.SpecialMoves.Length; x++)
            {
                if (BotAiCheckIfApply.SpecialMoves[x] == this)
                {
                    moveBot = x;
                }
            }
        }
        if (Character_Info.IsCharging == true && BotAiCheckIfApply == null && MoveDecison.IsDisplayingHappening == false)
        {
            HasUsedCharge = true;
        }
        if (moveBot != -69 && Character_Info.IsCharging == true && BotAiCheckIfApply != null && BotAiCheckIfApply.IsMakingDecison == true)
        {
            if (BotAiCheckIfApply.SuggestedActionFinal.Substring(BotAiCheckIfApply.SuggestedActionFinal.Length - 1) == moveBot + "")
            {
                HasUsedCharge = true;
            }

        }
        if (AreaCanClick != null)
        {
            for (int i = 0; i < AreaCanClick.Length; i++)
            {
                AreaCanSelect[i] = new int[4];
                //-69 is the signal to null out a SelectionSquare
                if (AreaCanClick[i] != null && AreaCanClick[i][0] != -69 && AreaCanClick[i][1] != -69 && AreaCanClick[i][2] != -69 && AreaCanClick[i][3] != -69)
                {
                    AreaCanSelect[i][1] = (int)gameObject.GetComponent<CharacterBase>().CharacterLocationIndex.y + AreaCanClick[i][1];
                    AreaCanSelect[i][2] = (int)gameObject.GetComponent<CharacterBase>().CharacterLocationIndex.x + AreaCanClick[i][2];
                    AreaCanSelect[i][3] = (int)gameObject.GetComponent<CharacterBase>().CharacterLocationIndex.y + AreaCanClick[i][3];
                    AreaCanSelect[i][0] = (int)gameObject.GetComponent<CharacterBase>().CharacterLocationIndex.x + AreaCanClick[i][0];
                }
                else
                {
                    AreaCanSelect[i][1] = -69;
                    AreaCanSelect[i][2] = -69;
                    AreaCanSelect[i][3] = -69;
                    AreaCanSelect[i][0] = -69;
                }
            }
        }
        //Purely Debug
        if (willUseForMove && MoveSpacesAllowed != null)
        {
            /*
            print(Character_Info.LocationAction.x + " , " + Character_Info.LocationAction.y);'[
            lp
            print(AreaCanSelect[0][0] + " , " + AreaCanSelect[0][1] + " , " + AreaCanSelect[0][2] + " , " + AreaCanSelect[0][3] + " // " + AreaCanSelect[1][0] + " , " + AreaCanSelect[1][1] + " , " + AreaCanSelect[1][2] + " , " + AreaCanSelect[1][3] + " // " + AreaCanSelect[2][0] + " , " + AreaCanSelect[2][1] + " , " + AreaCanSelect[2][2] + " , " + AreaCanSelect[2][3] + " // " + AreaCanSelect[3][0] + " , " + AreaCanSelect[3][1] + " , " + AreaCanSelect[3][2] + " , " + AreaCanSelect[3][3] + " // " + AreaCanSelect[4][0] + " , " + AreaCanSelect[4][1] + " , " + AreaCanSelect[4][2] + " , " + AreaCanSelect[4][3]);
            print(MoveSpacesAllowedAdjust[0][0] + " , " + MoveSpacesAllowedAdjust[0][1] + " , " + MoveSpacesAllowedAdjust[0][2] + " , " + MoveSpacesAllowedAdjust[0][3] + " // " + MoveSpacesAllowedAdjust[1][0] + " , " + MoveSpacesAllowedAdjust[1][1] + " , " + MoveSpacesAllowedAdjust[1][2] + " , " + MoveSpacesAllowedAdjust[1][3] + " // " + MoveSpacesAllowedAdjust[2][0] + " , " + MoveSpacesAllowedAdjust[2][1] + " , " + MoveSpacesAllowedAdjust[2][2] + " , " + MoveSpacesAllowedAdjust[2][3] + " // " + MoveSpacesAllowedAdjust[3][0] + " , " + MoveSpacesAllowedAdjust[3][1] + " , " + MoveSpacesAllowedAdjust[3][2] + " , " + MoveSpacesAllowedAdjust[3][3] + " // " + MoveSpacesAllowedAdjust[4][0] + " , " + MoveSpacesAllowedAdjust[4][1] + " , " + MoveSpacesAllowedAdjust[4][2] + " , " + MoveSpacesAllowedAdjust[4][3]);
            print(CheckIfLocationCorrespondsToAction(3, 0, MoveSpacesAllowedAdjust, AreaCanSelect));
            print("START________________________________________________________________________________________________________________");
            for ( int i = 0; i < MoveSpacesAllowed.Length; i++)
            {
            print(MoveSpacesAllowed[i][0] + " , " + MoveSpacesAllowed[i][1] + " , " + MoveSpacesAllowed[i][2] + " , " + MoveSpacesAllowed[i][3] + " // ");
            }
            print("END________________________________________________________________________________________________________________");
           */
        }

        /*if(Character_Info.IsEnemy == true)
        {
        */
        if (willUseForMove)
        {
            MoveSpaceBackTrackAdjust = MoveSpacesAllowedAdjust;
            for (int i = 0; i < MoveSpaceBackTrackAdjust.Length; i++)
            {
                for (int z = 0; z < MoveSpaceBackTrackAdjust[i].Length; z++)
                {
                    MoveSpaceBackTrackAdjust[i][z] = MoveSpaceBackTrackAdjust[i][z] * -1;
                }
            }
            MoveSpacesAllowed = NewAreaEffectAdjust(AreaCanSelect, MoveSpacesAllowedAdjust);
            MoveSpaceBackTrackAllowed = NewAreaEffectAdjust(AreaCanSelect, MoveSpaceBackTrackAdjust);
        }
        if (willUseForGridEffect)
        {
            PlayerSpacesAllowed = NewAreaEffectAdjust(AreaCanSelect, PlayerSpacesAllowedAdjust);
        }
        if (willUseForEnemyMove)
        {
            EnemyMoveSpacesAllowed = NewAreaEffectAdjust(AreaCanSelect, EnemyMoveAllowedAdjust);
        }
        if (willUseForAllyMove)
        {
            AllyMoveSpacesAllowed = NewAreaEffectAdjust(AreaCanSelect, AllyMoveAllowedAdjust);
        }
        /*
    }
        */
    }
    public override void ChangeAnim(float time)
    {
        if (Character_Info.IsCharging == false && HasUsedCharge == false)
        {
            Character_Info.CharacterSChanger.SetSprite(time, 7);
        }
        if (HasUsedCharge == true && MoveDecison.IsDisplayingHappening == true)
        {
            Character_Info.CharacterSChanger.SetSprite(time, 2);
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

        }
        if (HasUsedCharge == true && MoveDecison.IsDisplayingHappening == true)
        {
            HasUsedCharge = false;
            Character_Info.IsCharging = false;
            AreaHighLightToggle(AreaSoonToEffect, Character_Info.IsEnemy, false);
            EffectAmount = 0;
            AreaEffect((int)Character_Info.LocationAction.x - 1, (int)Character_Info.LocationAction.y-1, 3, 3);
            gameObject.GetComponent<CharacterBase>().action = "inactive";
            for (int x = 0; x < AreaCheck((int)Character_Info.LocationAction.x - 1, (int)Character_Info.LocationAction.y-1, 3, 3).Length; x++)
            {
                GameObject[] AreaCharacters = AreaCheck((int)Character_Info.LocationAction.x - 1, (int)Character_Info.LocationAction.y-1, 3, 3);
                CharacterBase CheckedCharacterBase;
                if (AreaCharacters[x] != null)
                {
                    CheckedCharacterBase = AreaCharacters[x].GetComponent<CharacterBase>();
                    if (CheckedCharacterBase.IsEnemy != Character_Info.IsEnemy)
                    {

                        CheckedCharacterBase.SpeedMultiplier -= 0.5f;
                    }
                }
            }
            AreaEffect((int)Character_Info.LocationAction.x - 1, (int)Character_Info.LocationAction.y-1, 3, 3);
            Character_Info.action = "inactive";
        }
    }
}
