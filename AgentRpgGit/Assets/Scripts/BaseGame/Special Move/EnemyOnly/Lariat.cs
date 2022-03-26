using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lariat : GenericMove
{
    //Base Ability shoot method
    public float damageRatio;
    public int bulletDistance;
    public int AmountOfBullets;
    public bool canPenentrateObstacle;
    public bool canPenentrateCharacter;
    CharacterBase CheckedCharacterBase;
    GunFunction localGunFunction;
    public override int[] CheckIfConditionsApply(Vector2 areaCheckFrom)
    {
        localGunFunction = gameObject.GetComponent<GunFunction>();
        if (gameObject.name == "name")
        {
            print("wow");
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
                    Vector2[][] GetGridAreasOfFunction = null;
                    for(int z = 0; z < 4; z++)
                    {
                        if(z == 0)
                        {
                            GetGridAreasOfFunction = localGunFunction.ShootAbility(areaCheckFrom, new Vector2(-1, 0), !Character_Info.IsEnemy, bulletDistance, canPenentrateObstacle, canPenentrateCharacter, 0, 0, AmountOfBullets, null, null);
                        }
                        if (z == 1)
                        {
                            GetGridAreasOfFunction = localGunFunction.ShootAbility(areaCheckFrom, new Vector2(1, 0), !Character_Info.IsEnemy, bulletDistance, canPenentrateObstacle, canPenentrateCharacter, 0, 0, AmountOfBullets, null, null);
                        }
                        if(z == 2)
                        {
                            GetGridAreasOfFunction = localGunFunction.ShootAbility(areaCheckFrom, new Vector2(0, -1), !Character_Info.IsEnemy, bulletDistance, canPenentrateObstacle, canPenentrateCharacter, 0, 0, AmountOfBullets, null, null);
                        }
                        if (z == 3)
                        {
                            GetGridAreasOfFunction = localGunFunction.ShootAbility(areaCheckFrom, new Vector2(0, 1), !Character_Info.IsEnemy, bulletDistance, canPenentrateObstacle, canPenentrateCharacter, 0, 0, AmountOfBullets, null, null);
                        }
                        for (int x = 0; x < GetGridAreasOfFunction.Length; x++)
                        {
                            for (int y = 0; y < GetGridAreasOfFunction[0].Length; y++)
                            {
                                if ((int)GetGridAreasOfFunction[x][y].y >= 0 && (int)GetGridAreasOfFunction[x][y].y < Gridinfo.YWidthPublic && (int)GetGridAreasOfFunction[x][y].x >= 0 && (int)GetGridAreasOfFunction[x][y].x < Gridinfo.XWidthPublic)
                                {
                                    GameObject FoundCharacter = Gridinfo.AllGrids[(int)GetGridAreasOfFunction[x][y].y][(int)GetGridAreasOfFunction[x][y].x].GetComponent<GridControl>().CharacterOn;
                                    if (FoundCharacter != null && FoundCharacter.GetComponent<CharacterBase>().IsDead == false)
                                    {
                                        CheckedCharacterBase = FoundCharacter.GetComponent<CharacterBase>();
                                        if (CheckedCharacterBase.IsEnemy != Character_Info.IsEnemy)
                                        {
                                            DoesConditionsApply[2] = PriorityAdd;
                                            Vector2 Location = new Vector2((int)GetGridAreasOfFunction[x][y].x, (int)GetGridAreasOfFunction[x][y].y);
                                            DoesConditionsApply[0] = (int)Location.x;
                                            DoesConditionsApply[1] = (int)Location.y;
                                        }
                                    }
                                }
                            }
                        }
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
    public override void SetAdjust()
    {
        damageRatio = 1f;
        bulletDistance = 3;
        AmountOfBullets = 1;
        canPenentrateCharacter = false;
        canPenentrateObstacle = false;
        AreaSelectionSquareX0 = -1;
        AreaSelectionSquareY0 = 0;
        AreaSelectionSquareWidth0 = -1;
        AreaSelectionSquareHeight0 = 0;
        AreaSelectionSquareX1 = 1;
        AreaSelectionSquareY1 = 0;
        AreaSelectionSquareWidth1 = 1;
        AreaSelectionSquareHeight1 = 0;
        AreaSelectionSquareX2 = 0;
        AreaSelectionSquareY2 = -1;
        AreaSelectionSquareWidth2 = 0;
        AreaSelectionSquareHeight2 = -1;
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
        willUseForGridEffect = true;
        PriorityAdd = 150;
    }

    // Update is called once per frame
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
                    GameObject FoundCharacter = Gridinfo.AllGrids[(int)GetGridAreasOfFunction[x][y].y][(int)GetGridAreasOfFunction[x][y].x].GetComponent<GridControl>().CharacterOn;
                    if (FoundCharacter != null)
                    {
                        CheckedCharacterBase = FoundCharacter.GetComponent<CharacterBase>();
                        if (CheckedCharacterBase.IsEnemy != Character_Info.IsEnemy)
                        {
                            Character_Info.GetComponent<CharacterBase>().Push((int)CheckedCharacterBase.CharacterLocationIndex.x, (int)CheckedCharacterBase.CharacterLocationIndex.y);
                            CheckedCharacterBase.Health -= CheckedCharacterBase.DefenseProcessedDamage( damageRatio * Character_Info.ExpressedDamage);
                            GameObject InWorldText = Instantiate(HitUiSprite, new Vector3(Gridinfo.AllGrids[(int)CheckedCharacterBase.CharacterLocationIndex.y][(int)CheckedCharacterBase.CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn.transform.position.x, Gridinfo.AllGrids[(int)CheckedCharacterBase.CharacterLocationIndex.y][(int)CheckedCharacterBase.CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn.transform.position.y), Quaternion.identity.normalized);
                            InWorldText.GetComponent<FadeOutText>().BeginInitiate(1, "" + CheckedCharacterBase.DefenseProcessedDamage(damageRatio * Character_Info.ExpressedDamage), Color.black, new Vector2(5, 5));
                        }
                    }
                }
            }
        }
    }
}
