using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backblast : GenericMove
{
    // Start is called before the first frame update
    public override void SetAdjust()
    {
        AreaSelectionSquareX0 = -1;
        AreaSelectionSquareY0 = 0;
        AreaSelectionSquareWidth0 = -1;
        AreaSelectionSquareHeight0 = 0;
        AreaSelectionSquareX1 = 0;
        AreaSelectionSquareY1 = -1;
        AreaSelectionSquareWidth1 = 0;
        AreaSelectionSquareHeight1 = -1;
        AreaSelectionSquareX2 = 1;
        AreaSelectionSquareY2 = 0;
        AreaSelectionSquareWidth2 = 1;
        AreaSelectionSquareHeight2 = 0;
        AreaSelectionSquareX3 = 0;
        AreaSelectionSquareY3 = 1;
        AreaSelectionSquareWidth3 = 0;
        AreaSelectionSquareHeight3 = 1;
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
        MouseFollowingUI.GroupSelection[0][1] = -1;
        MouseFollowingUI.GroupSelection[0][2] = 2;
        MouseFollowingUI.GroupSelection[0][3] = 2;
        willUseForMove = true;
    }
    public override void SelectionAdjustment()
    {
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
        Character_Info.CharacterSChanger.SetSprite(1, 2);
        EffectAmount = 0;
        gameObject.GetComponent<CharacterBase>().action = "inactive";
        AreaEffect((int)Character_Info.LocationAction.x - 1, (int)Character_Info.LocationAction.y - 1, 3, 3);
        Character_Info.DefenseMultiplier -= (float)0.2;
        for (int x = 0; x < AreaCheck((int)Character_Info.LocationAction.x - 1, (int)Character_Info.LocationAction.y - 1, 3, 3).Length; x++)
        {
            GameObject[] AreaCharacters = AreaCheck((int)Character_Info.LocationAction.x - 1, (int)Character_Info.LocationAction.y- 1, 3, 3);
            CharacterBase CheckedCharacterBase;
            GameObject InWorldText;
            if (AreaCharacters[x] != null)
            {
                CheckedCharacterBase = AreaCharacters[x].GetComponent<CharacterBase>();
                if (CheckedCharacterBase.IsEnemy != Character_Info.IsEnemy)
                {
                    CheckedCharacterBase.Health -= 5;
                    InWorldText = Instantiate(HitUiSprite, new Vector3(Gridinfo.AllGrids[(int)CheckedCharacterBase.CharacterLocationIndex.y][(int)CheckedCharacterBase.CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn.transform.position.x, Gridinfo.AllGrids[(int)CheckedCharacterBase.CharacterLocationIndex.y][(int)CheckedCharacterBase.CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn.transform.position.y), Quaternion.identity.normalized);
                    InWorldText.GetComponent<FadeOutText>().BeginInitiate(1, "5", Color.black, new Vector2(5, 5));
                }
            }
        }
        Character_Info.Push((2*(int)Character_Info.CharacterLocationIndex.x) - (int)Character_Info.LocationAction.x, (2 * (int)Character_Info.CharacterLocationIndex.y) - (int)Character_Info.LocationAction.y);

    }
}
