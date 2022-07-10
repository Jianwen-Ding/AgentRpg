using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardBreath : GenericMove
{
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
        PriorityAdd = 15;
    }
    public override int[] CheckIfConditionsApply(Vector2 areaCheckFrom)
    {
        int[] DoesConditionsApply = new int[3];
        DoesConditionsApply[0] = (int)areaCheckFrom.x;
        DoesConditionsApply[1] = (int)areaCheckFrom.y;
        DoesConditionsApply[2] = PriorityAdd;
        return DoesConditionsApply;
    }
    public override void ActivateMove()
    {
        EffectAmount = 0;
        gameObject.GetComponent<CharacterBase>().action = "inactive";
        for (int i = 0; i < BotAiCheckIfApply.Opponents.Length; i++)
        {
            if (BotAiCheckIfApply.Opponents[i].IsDead == false)
            {
                AreaEffect((int)Character_Info.CharacterLocationIndex.x, (int)Character_Info.CharacterLocationIndex.y, 1, 1);
                BotAiCheckIfApply.Opponents[i].Push((int)BotAiCheckIfApply.Opponents[i].CharacterLocationIndex.x - 1, (int)BotAiCheckIfApply.Opponents[i].CharacterLocationIndex.y);
            }
        }
    }

}
