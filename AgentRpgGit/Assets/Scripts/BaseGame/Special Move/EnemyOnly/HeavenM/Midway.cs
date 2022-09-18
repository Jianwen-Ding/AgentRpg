using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Midway : GenericMove
{
    [SerializeField]
    float damageRatio;
    public override void SetAdjust()
    {
        AreaSelectionSquareX0 = -4;
        AreaSelectionSquareY0 = -4;
        AreaSelectionSquareWidth0 = -3;
        AreaSelectionSquareHeight0 = 4;
        AreaSelectionSquareX1 = 3;
        AreaSelectionSquareY1 = -4;
        AreaSelectionSquareWidth1 = 4;
        AreaSelectionSquareHeight1 = 4;
        AreaSelectionSquareX2 = -4;
        AreaSelectionSquareY2 = -4;
        AreaSelectionSquareWidth2 = 4;
        AreaSelectionSquareHeight2 = -3;
        AreaSelectionSquareX3 = -4;
        AreaSelectionSquareY3 = 3;
        AreaSelectionSquareWidth3 = 4;
        AreaSelectionSquareHeight3 = 4;
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
        willUseForGridEffect = true;
        PriorityAdd = 70;
        damageRatio = 3f;
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
                if (CheckedCharacterBase.IsEnemy != Character_Info.IsEnemy && Gridinfo.AllGrids[(int)Character_Info.LocationAction.y][(int)Character_Info.LocationAction.x].GetComponent<GridControl>().CharacterOn != null)
                {
                    CheckedCharacterBase.Health -= CheckedCharacterBase.DefenseProcessedDamage(damageRatio * Character_Info.ExpressedDamage);
                    InWorldText = Instantiate(HitUiSprite, new Vector3(Gridinfo.AllGrids[(int)CheckedCharacterBase.CharacterLocationIndex.y][(int)CheckedCharacterBase.CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn.transform.position.x, Gridinfo.AllGrids[(int)CheckedCharacterBase.CharacterLocationIndex.y][(int)CheckedCharacterBase.CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn.transform.position.y), Quaternion.identity.normalized);
                    InWorldText.GetComponent<FadeOutText>().BeginInitiate(1, CheckedCharacterBase.DefenseProcessedDamage(damageRatio * Character_Info.ExpressedDamage) + "", Color.black, new Vector2(5, 5));
                }
            }
        }
    }
}
