using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anima : GenericMove
{
    [SerializeField]
    float damageRatio;
    public override void SetAdjust()
    {
        AreaSelectionSquareX0 = 0;
        AreaSelectionSquareY0 = 2;
        AreaSelectionSquareWidth0 = 0;
        AreaSelectionSquareHeight0 = 2;
        AreaSelectionSquareX1 = 2;
        AreaSelectionSquareY1 = 0;
        AreaSelectionSquareWidth1 = 2;
        AreaSelectionSquareHeight1 = 0;
        AreaSelectionSquareX2 = -2;
        AreaSelectionSquareY2 = 0;
        AreaSelectionSquareWidth2 = -2;
        AreaSelectionSquareHeight2 = 0;
        AreaSelectionSquareX3 = 0;
        AreaSelectionSquareY3 = -2;
        AreaSelectionSquareWidth3 = 0;
        AreaSelectionSquareHeight3 = -2;
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
        PriorityAdd = -1;
        damageRatio = 0.1f;
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
        EffectAmount = 0;
        AreaEffect((int)Character_Info.CharacterLocationIndex.x - 1, (int)Character_Info.CharacterLocationIndex.y - 1, 3, 3);
        gameObject.GetComponent<CharacterBase>().action = "inactive";
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
                    CheckedCharacterBase.Health -= damageRatio * Character_Info.ExpressedDamage;
                    InWorldText = Instantiate(HitUiSprite, new Vector3(Gridinfo.AllGrids[(int)CheckedCharacterBase.CharacterLocationIndex.y][(int)CheckedCharacterBase.CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn.transform.position.x, Gridinfo.AllGrids[(int)CheckedCharacterBase.CharacterLocationIndex.y][(int)CheckedCharacterBase.CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn.transform.position.y), Quaternion.identity.normalized);
                    InWorldText.GetComponent<FadeOutText>().BeginInitiate(1, (damageRatio * Character_Info.ExpressedDamage) + "", Color.black, new Vector2(5, 5));
                    Character_Info.SpeedMultiplier += (float)0.2;
                }
            }
        }
        EffectAmount = 0;
        Character_Info.action = "inactive";
        Character_Info.Push((int)Character_Info.LocationAction.x, (int)Character_Info.LocationAction.y);
    }
}
