using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cripple : GenericMove
{
    public override void SelectionAdjustment()
    {
        base.SelectionAdjustment();
        MouseFollowingUI.WillGroupSelect = true;
        MouseFollowingUI.GroupSelection[0][0] = -1;
        MouseFollowingUI.GroupSelection[0][1] = -1;
        MouseFollowingUI.GroupSelection[0][2] = 2;
        MouseFollowingUI.GroupSelection[0][3] = 2;
    }
    public override void SetAdjust()
    {
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
        PriorityAdd = 20;
    }
    
    // Update is called once per frame
    public override void ActivateMove()
    {
        EffectAmount = 0;
        AreaEffect((int)Character_Info.CharacterLocationIndex.x-1, (int)Character_Info.CharacterLocationIndex.y-1, 3, 3);
        gameObject.GetComponent<CharacterBase>().action = "inactive";
        for(int x = 0; x < AreaCheck((int)Character_Info.CharacterLocationIndex.x-1, (int)Character_Info.CharacterLocationIndex.y-1, 3, 3).Length; x++)
        {
            GameObject[] AreaCharacters = AreaCheck((int)Character_Info.CharacterLocationIndex.x - 1, (int)Character_Info.CharacterLocationIndex.y - 1, 3, 3);
            CharacterBase CheckedCharacterBase;
            GameObject InWorldText;
            if(AreaCharacters[x] != null)
            {
                CheckedCharacterBase = AreaCharacters[x].GetComponent<CharacterBase>();
                if (CheckedCharacterBase.IsEnemy != Character_Info.IsEnemy)
                {
                    CheckedCharacterBase.Health -= 10;
                    CheckedCharacterBase.SpeedMultiplier -= (float)0.2;
                    InWorldText = Instantiate(HitUiSprite, new Vector3(Gridinfo.AllGrids[(int)CheckedCharacterBase.CharacterLocationIndex.y][(int)CheckedCharacterBase.CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn.transform.position.x, Gridinfo.AllGrids[(int)CheckedCharacterBase.CharacterLocationIndex.y][(int)CheckedCharacterBase.CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn.transform.position.y), Quaternion.identity.normalized);
                    InWorldText.GetComponent<FadeOutText>().BeginInitiate(1, "10", Color.black, new Vector2(5, 5));
                }
            }
        }
    }
}
