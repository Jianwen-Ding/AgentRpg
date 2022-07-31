using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMove : GenericMove
{
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
    public override void SetAdjust()
    {
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
        PriorityAdd = -3;
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
        Character_Info.action = "inactive";
        Character_Info.Push((int)Character_Info.LocationAction.x, (int)Character_Info.LocationAction.y);
    }
}

