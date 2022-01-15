using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dartshot : GenericMove
{
    //Base Ability shoot method
    public int bulletDistance;
    public int AmountOfBullets;
    public bool canPenentrateObstacle;
    public bool canPenentrateCharacter;
    GunFunction localGunFunction;
    public override void SetAdjust()
    {
        bulletDistance = 6;
        AmountOfBullets = 1;
        canPenentrateCharacter = false;
        canPenentrateObstacle = false;
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
        MouseFollowingUI.GroupSelection[0][0] = -1;
        MouseFollowingUI.GroupSelection[0][1] = 0;
        MouseFollowingUI.GroupSelection[0][2] = 2;
        MouseFollowingUI.GroupSelection[0][3] = 0;
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
        localGunFunction = gameObject.GetComponent<GunFunction>();
        Vector2[][] GetGridAreasOfFunction = null;
        gameObject.GetComponent<CharacterBase>().action = "inactive";
        if (Character_Info.LocationAction.x < Character_Info.CharacterLocationIndex.x)
        {
            GetGridAreasOfFunction = localGunFunction.ShootAbility(new Vector2(Character_Info.CharacterLocationIndex.x, Character_Info.CharacterLocationIndex.y), new Vector2(-1, 0), !Character_Info.IsEnemy, bulletDistance, canPenentrateObstacle, canPenentrateCharacter, 0, 0, AmountOfBullets, MoveSprite, MoveSpriteSecondary);
        }
        if (Character_Info.LocationAction.x > Character_Info.CharacterLocationIndex.x)
        {
            GetGridAreasOfFunction = localGunFunction.ShootAbility(new Vector2(Character_Info.CharacterLocationIndex.x, Character_Info.CharacterLocationIndex.y), new Vector2(1, 0), !Character_Info.IsEnemy, bulletDistance, canPenentrateObstacle, canPenentrateCharacter, 0, 0, AmountOfBullets, MoveSprite, MoveSpriteSecondary);
        }
        if (Character_Info.LocationAction.y < Character_Info.CharacterLocationIndex.y)
        {
            GetGridAreasOfFunction = localGunFunction.ShootAbility(new Vector2(Character_Info.CharacterLocationIndex.x, Character_Info.CharacterLocationIndex.y), new Vector2(0, -1), !Character_Info.IsEnemy, bulletDistance, canPenentrateObstacle, canPenentrateCharacter, 0, 0, AmountOfBullets, MoveSprite, MoveSpriteSecondary);
        }
        if (Character_Info.LocationAction.y > Character_Info.CharacterLocationIndex.y)
        {
            GetGridAreasOfFunction = localGunFunction.ShootAbility(new Vector2(Character_Info.CharacterLocationIndex.x, Character_Info.CharacterLocationIndex.y), new Vector2(0, 1), !Character_Info.IsEnemy, bulletDistance, canPenentrateObstacle, canPenentrateCharacter, 0, 0, AmountOfBullets, MoveSprite, MoveSpriteSecondary);
        }
        for (int x = 0; x < GetGridAreasOfFunction.Length; x++)
        {
            for (int y = 0; y < GetGridAreasOfFunction[0].Length; y++)
            {
                if ((int)GetGridAreasOfFunction[x][y].y >= 0 && (int)GetGridAreasOfFunction[x][y].y < Gridinfo.YWidthPublic && (int)GetGridAreasOfFunction[x][y].x >= 0 && (int)GetGridAreasOfFunction[x][y].x < Gridinfo.XWidthPublic)
                {
                    if(Gridinfo.AllGrids[(int)GetGridAreasOfFunction[x][y].y][(int)GetGridAreasOfFunction[x][y].x].GetComponent<GridControl>().CharacterOn != null)
                    {
                        CharacterBase FoundCharacter = Gridinfo.AllGrids[(int)GetGridAreasOfFunction[x][y].y][(int)GetGridAreasOfFunction[x][y].x].GetComponent<GridControl>().CharacterOn.GetComponent<CharacterBase>();
                        if (FoundCharacter != null && FoundCharacter.IsEnemy != Character_Info.IsEnemy)
                        {
                            for (int i = 0; i < FoundCharacter.StatusEffects.Length; i++)
                            {
                                if (FoundCharacter.StatusEffects[i] == 0)
                                {
                                    FoundCharacter.StatusEffects[i] = 4;
                                    break;
                                }
                            }
                        }
                    }                }
            }
        }
    }
}
