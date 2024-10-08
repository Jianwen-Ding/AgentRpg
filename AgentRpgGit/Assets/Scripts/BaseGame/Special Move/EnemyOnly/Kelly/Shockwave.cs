﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : GenericMove
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
        WillUseForSquareX0 = -5;
        WillUseForSquareY0 = -5;
        WillUseForSquareWidth0 = 5;
        WillUseForSquareHeight0 = 5;
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
        PriorityAdd = 9;
    }
    public override int[] CheckIfConditionsApply(Vector2 areaCheckFrom)
    {
        int[] DoesConditionsApply = new int[3];
        DoesConditionsApply[0] = -69;
        DoesConditionsApply[1] = -69;
        DoesConditionsApply[2] = -69;
        if (Gridinfo.AllGrids[(int)areaCheckFrom.y][(int)areaCheckFrom.x].GetComponent<GridControl>().StatusIndex != 9 && ShockwaveMain.totalLeft <= 0)
        {
            DoesConditionsApply[0] = (int)areaCheckFrom.x;
            DoesConditionsApply[1] = (int)areaCheckFrom.y;
            DoesConditionsApply[2] = PriorityAdd;
        }
        return DoesConditionsApply;
    }
    public override void ActivateMove()
    {
        EffectAmount = 0;
        Gridinfo.AllGrids[(int)Character_Info.LocationAction.y][(int)Character_Info.LocationAction.x].GetComponent<GridControl>().StatusIndex = 9;
        gameObject.GetComponent<CharacterBase>().action = "inactive";
        Effects[0] = Instantiate(MoveSprite, new Vector3(Gridinfo.AllGrids[(int)Character_Info.CharacterLocationIndex.y][(int)Character_Info.CharacterLocationIndex.x].transform.position.x + AdjustSprite.x, Gridinfo.AllGrids[(int)Character_Info.CharacterLocationIndex.y][(int)Character_Info.CharacterLocationIndex.x].transform.position.y + AdjustSprite.y), Quaternion.identity.normalized);
        Effects[0].GetComponent<EffectsLifeTime>().TimeTillSelfDestruct = 1;
        ShockwaveMain.totalLeft = 7; 
    }
}
