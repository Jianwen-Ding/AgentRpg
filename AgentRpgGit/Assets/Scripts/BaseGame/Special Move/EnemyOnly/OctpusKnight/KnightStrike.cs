using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightStrike : GenericMove
{
    public override void SetAdjust()
    {
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
        willUseForGridEffect = true;
        PriorityAdd = 120;
    }
    public override void Start()
    {
        base.Start();
        AreaCanSelect = new int[8][];
        AreaCanClick = new int[8][];
        for (int i = 0; i < AreaCanClick.Length; i++)
        {
            AreaCanClick[i] = new int[4];
        }
        AreaCanClick[0][0] = -1;
        AreaCanClick[0][1] = 2;
        AreaCanClick[0][2] = -1;
        AreaCanClick[0][3] = 2;
        AreaCanClick[1][0] = 1;
        AreaCanClick[1][1] = 2;
        AreaCanClick[1][2] = 1;
        AreaCanClick[1][3] = 2;
        AreaCanClick[2][0] = -1;
        AreaCanClick[2][1] = -2;
        AreaCanClick[2][2] = -1;
        AreaCanClick[2][3] = -2;
        AreaCanClick[3][0] = 1;
        AreaCanClick[3][1] = -2;
        AreaCanClick[3][2] = 1;
        AreaCanClick[3][3] = -2;
        AreaCanClick[4][0] = 2;
        AreaCanClick[4][1] = 1;
        AreaCanClick[4][2] = 2;
        AreaCanClick[4][3] = 1;
        AreaCanClick[5][0] = 2;
        AreaCanClick[5][1] = -1;
        AreaCanClick[5][2] = 2;
        AreaCanClick[5][3] = -1;
        AreaCanClick[6][0] = -2;
        AreaCanClick[6][1] = 1;
        AreaCanClick[6][2] = -2;
        AreaCanClick[6][3] = 1;
        AreaCanClick[7][0] = -2;
        AreaCanClick[7][1] = -1;
        AreaCanClick[7][2] = -2;
        AreaCanClick[7][3] = -1;
    }
    public override int[] CheckIfConditionsApply(Vector2 areaCheckFrom)
    {
        if(areaCheckFrom == new Vector2(7, 1))
        {
            //d
            Debug.Log("wow");
        }
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
                    int[][] newAreaCanSelect = new int[8][];
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
    public override Vector2 CheckIfLocationCorrespondsToAction(int XCheck, int YCheck, int[][] AreaEffectAdjust, int[][] OldAreaEffect)
    {
        Vector2 LocationAction = new Vector2(-69, -69);
        if (AreaEffectAdjust.Length != null && OldAreaEffect.Length != null)
        {
            for (int i = 0; i < OldAreaEffect.Length; i++)
            {
                for (int x = 0; x <= OldAreaEffect[i][2] - OldAreaEffect[i][0]; x++)
                {
                    for (int y = 0; y <= OldAreaEffect[i][3] - OldAreaEffect[i][1]; y++)
                    {
                        for (int z = 0; z < AreaEffectAdjust.Length; z++)
                        {
                            if (OldAreaEffect[i][0] + x >= 0 && OldAreaEffect[i][1] + y >= 0 && OldAreaEffect[i][0] + x < Gridinfo.XWidthPublic && OldAreaEffect[i][1] + y < Gridinfo.YWidthPublic && OldAreaEffect[i][0] != -69 && OldAreaEffect[i][1] != -69 && OldAreaEffect[i][2] != -69 && OldAreaEffect[i][3] != -69 && AreaEffectAdjust[z][0] + OldAreaEffect[i][0] + x <= XCheck && AreaEffectAdjust[z][2] + OldAreaEffect[i][0] + x >= XCheck && AreaEffectAdjust[z][1] + OldAreaEffect[i][1] + y <= YCheck && AreaEffectAdjust[z][3] + OldAreaEffect[i][1] + y >= YCheck)
                            {
                                LocationAction = new Vector2(OldAreaEffect[i][0] + x, OldAreaEffect[i][01] + y);
                            }
                        }
                    }
                }
            }
        }
        return LocationAction;
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
                if (CheckedCharacterBase.IsEnemy != Character_Info.IsEnemy)
                {
                    CheckedCharacterBase.Health -= CheckedCharacterBase.DefenseProcessedDamage(200);
                    InWorldText = Instantiate(HitUiSprite, new Vector3(Gridinfo.AllGrids[(int)CheckedCharacterBase.CharacterLocationIndex.y][(int)CheckedCharacterBase.CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn.transform.position.x, Gridinfo.AllGrids[(int)CheckedCharacterBase.CharacterLocationIndex.y][(int)CheckedCharacterBase.CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn.transform.position.y), Quaternion.identity.normalized);
                    InWorldText.GetComponent<FadeOutText>().BeginInitiate(1, "" + CheckedCharacterBase.DefenseProcessedDamage(200), Color.black, new Vector2(5, 5));
                }
            }
        }
        Character_Info.Push((int)Character_Info.LocationAction.x, (int)Character_Info.LocationAction.y);
    }
}
