using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathray : GenericMove
{
    public bool HasUsedCharge = false;
    public override void SetAdjust()
    {
        AreaSelectionSquareX0 = -6;
        AreaSelectionSquareY0 = -6;
        AreaSelectionSquareWidth0 = 6;
        AreaSelectionSquareHeight0 = 6;
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
        MouseFollowingUI.GroupSelection[0][0] = 0;
        MouseFollowingUI.GroupSelection[0][1] = 0;
        MouseFollowingUI.GroupSelection[0][2] = 0;
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
    // Update is called once per frame
    void Update()
    {
        if (Character_Info.IsCharging == true && BotAiCheckIfApply == null && MoveDecison.IsDisplayingHappening == false)
        {
            HasUsedCharge = true;
        }
        if (Character_Info.IsCharging == true && BotAiCheckIfApply != null && BotAiCheckIfApply.IsMakingDecison == true)
        {
            HasUsedCharge = true;
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
    }
    public void ChangeAnim()
    {
        if (Character_Info.IsCharging == false && HasUsedCharge == false)
        {
            Character_Info.CharacterSChanger.SetSprite(1, 7);
        }
        if (HasUsedCharge == true && MoveDecison.IsDisplayingHappening == true)
        {
            Character_Info.CharacterSChanger.SetSprite(1, 2);
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
            Character_Info.SpeedMultiplier -= 2;
        }
        if (HasUsedCharge == true && MoveDecison.IsDisplayingHappening == true)
        {
            HasUsedCharge = false;
            Character_Info.IsCharging = false;
            AreaHighLightToggle(AreaSoonToEffect, Character_Info.IsEnemy, false);
            EffectAmount = 0;
            AreaEffect((int)Character_Info.LocationAction.x, (int)Character_Info.LocationAction.y, 1, 1);
            gameObject.GetComponent<CharacterBase>().action = "inactive";
            for (int x = 0; x < AreaCheck((int)Character_Info.LocationAction.x - 1, (int)Character_Info.LocationAction.y, 3, 1).Length; x++)
            {
                GameObject[] AreaCharacters = AreaCheck((int)Character_Info.LocationAction.x, (int)Character_Info.LocationAction.y, 1, 1);
                CharacterBase CheckedCharacterBase;
                GameObject InWorldText;
                if (AreaCharacters[x] != null)
                {
                    CheckedCharacterBase = AreaCharacters[x].GetComponent<CharacterBase>();
                    if (CheckedCharacterBase.IsEnemy != Character_Info.IsEnemy)
                    {
                        CheckedCharacterBase.Health -= CheckedCharacterBase.DefenseProcessedDamage(50);
                        InWorldText = Instantiate(HitUiSprite, new Vector3(Gridinfo.AllGrids[(int)CheckedCharacterBase.CharacterLocationIndex.y][(int)CheckedCharacterBase.CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn.transform.position.x, Gridinfo.AllGrids[(int)CheckedCharacterBase.CharacterLocationIndex.y][(int)CheckedCharacterBase.CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn.transform.position.y), Quaternion.identity.normalized);
                        InWorldText.GetComponent<FadeOutText>().BeginInitiate(1, "" + CheckedCharacterBase.DefenseProcessedDamage(50), Color.black, new Vector2(5, 5));
                    }
                }
            }
            AreaEffect((int)Character_Info.LocationAction.x, (int)Character_Info.LocationAction.y, 1, 1);
            Character_Info.action = "inactive";
        }
    }
}