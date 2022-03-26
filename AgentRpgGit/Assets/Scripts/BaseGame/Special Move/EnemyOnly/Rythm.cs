using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rythm : GenericMove
{
    [SerializeField]
    float damageRatio;
    public override void SetAdjust()
    {
        AreaSelectionSquareX0 = -1;
        AreaSelectionSquareY0 = 1;
        AreaSelectionSquareWidth0 = 1;
        AreaSelectionSquareHeight0 = 1;
        AreaSelectionSquareX1 = -1;
        AreaSelectionSquareY1 = -1;
        AreaSelectionSquareWidth1 = 1;
        AreaSelectionSquareHeight1 = -1;
        AreaSelectionSquareX2 = -1;
        AreaSelectionSquareY2 = 0;
        AreaSelectionSquareWidth2 = -1;
        AreaSelectionSquareHeight2 = 0;
        AreaSelectionSquareX3 = 1;
        AreaSelectionSquareY3 = 0;
        AreaSelectionSquareWidth3 = 1;
        AreaSelectionSquareHeight3 = 0;
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
        willUseForGridEffect = true;
        PriorityAdd = 60;
        damageRatio = 0.3f;
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
                if (BotAiCheckIfApply.Opponents[i].IsDead == false)
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
                    if (BotAiCheckIfApply.Opponents[i] != null && Character_Info != null && LocationSpot.x != -69 && LocationSpot.y != -69 && Gridinfo.AllGrids[(int)LocationSpot.y][(int)LocationSpot.x].GetComponent<GridControl>().CharacterOn == null && Gridinfo.AllGrids[(int)LocationSpot.y][(int)LocationSpot.x].GetComponent<GridControl>().ObstacleIndex == 0)
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
    public override void ActivateMove()
    {
        EffectAmount = 0;
        bool DoesHitEnemy = false;
        Character_Info.action = "inactive";
        Character_Info.Push((int)Character_Info.LocationAction.x, (int)Character_Info.LocationAction.y);
        AreaEffect((int)Character_Info.CharacterLocationIndex.x - 1, (int)Character_Info.CharacterLocationIndex.y - 1, 3, 3);
        for (int x = 0; x < AreaCheck((int)Character_Info.CharacterLocationIndex.x - 1, (int)Character_Info.CharacterLocationIndex.y - 1, 3, 3).Length; x++)
        {
            GameObject[] AreaCharacters = AreaCheck((int)Character_Info.CharacterLocationIndex.x - 1, (int)Character_Info.CharacterLocationIndex.y - 1, 3, 3);
            CharacterBase CheckedCharacterBase;
            GameObject InWorldText;
            if (AreaCharacters[x] != null)
            {
                CheckedCharacterBase = AreaCharacters[x].GetComponent<CharacterBase>();
                if (CheckedCharacterBase.IsEnemy != Character_Info.IsEnemy)
                {
                    Character_Info.SpeedMultiplier += (float)0.1;
                    DoesHitEnemy = true;
                    CheckedCharacterBase.Health -= CheckedCharacterBase.DefenseProcessedDamage(damageRatio * Character_Info.ExpressedDamage);
                    InWorldText = Instantiate(HitUiSprite, new Vector3(Gridinfo.AllGrids[(int)CheckedCharacterBase.CharacterLocationIndex.y][(int)CheckedCharacterBase.CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn.transform.position.x, Gridinfo.AllGrids[(int)CheckedCharacterBase.CharacterLocationIndex.y][(int)CheckedCharacterBase.CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn.transform.position.y), Quaternion.identity.normalized);
                    InWorldText.GetComponent<FadeOutText>().BeginInitiate(1, "" + CheckedCharacterBase.DefenseProcessedDamage(damageRatio * Character_Info.ExpressedDamage), Color.black, new Vector2(5, 5));
                }
            }
        }
        if (DoesHitEnemy == false)
        {
            Character_Info.SpeedMultiplier -= (float)2;
        }
    }
}
