using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardCry : GenericMove
{
    public float priorityChange;
    public override void SetAdjust()
    {
        AreaSelectionSquareX0 = 0;
        AreaSelectionSquareY0 = 0;
        AreaSelectionSquareWidth0 = 0;
        AreaSelectionSquareHeight0 = 0;
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
        WillUseForSquareX0 = -3;
        WillUseForSquareY0 = -3;
        WillUseForSquareWidth0 = 3;
        WillUseForSquareHeight0 = 3;
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
        willUseForGridEffect = true;
        priorityChange = 90;
        PriorityAdd = 0;
    }
    public override int[] CheckIfConditionsApply(Vector2 areaCheckFrom)
    {
        int[] DoesConditionsApply = new int[3];
        DoesConditionsApply[0] = -69;
        DoesConditionsApply[1] = -69;
        DoesConditionsApply[2] = -69;
        if (willUseForGridEffect && Character_Info.Health < Character_Info.MaxHealth - 40)
        {
            for (int i = 0; i < BotAiCheckIfApply.Allys.Length; i++)
            {
                if (BotAiCheckIfApply.Allys[i].IsDead == false)
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
                    Vector2 LocationSpot = CheckIfLocationCorrespondsToAction((int)BotAiCheckIfApply.Allys[i].CharacterLocationIndex.x, (int)BotAiCheckIfApply.Allys[i].CharacterLocationIndex.y, PlayerSpacesAllowedAdjust, newAreaCanSelect);
                    if (BotAiCheckIfApply.Allys[i] != null && Character_Info != null && LocationSpot.x != -69 && LocationSpot.y != -69)
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
        
        EffectAmount = 0;
        AreaEffect((int)Character_Info.CharacterLocationIndex.x - 3, (int)Character_Info.CharacterLocationIndex.y - 3, 7, 7);
        gameObject.GetComponent<CharacterBase>().action = "inactive";
        for (int x = 0; x < AreaCheck((int)Character_Info.CharacterLocationIndex.x - 3, (int)Character_Info.CharacterLocationIndex.y - 3, 7, 7).Length; x++)
        {
            GameObject[] AreaCharacters = AreaCheck((int)Character_Info.CharacterLocationIndex.x - 3, (int)Character_Info.CharacterLocationIndex.y - 3, 7, 7);
            CharacterBase CheckedCharacterBase;
            GameObject InWorldText;
            if (AreaCharacters[x] != null)
            {
                CheckedCharacterBase = AreaCharacters[x].GetComponent<CharacterBase>();
                if (CheckedCharacterBase.IsEnemy == Character_Info.IsEnemy)
                {
                    Character_Info.Health += 40;
                    InWorldText = Instantiate(HitUiSprite, new Vector3(Gridinfo.AllGrids[(int)Character_Info.CharacterLocationIndex.y][(int)Character_Info.CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn.transform.position.x, Gridinfo.AllGrids[(int)Character_Info.CharacterLocationIndex.y][(int)Character_Info.CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn.transform.position.y), Quaternion.identity.normalized);
                    InWorldText.GetComponent<FadeOutText>().BeginInitiate(1, "40", Color.black, new Vector2(5, 5));
                }
            }
        }
    }
    public override void Update()
    {
        base.Update();
        PriorityAdd = (int)(((Character_Info.MaxHealth - Character_Info.Health) / Character_Info.MaxHealth) * priorityChange);
    }
}
