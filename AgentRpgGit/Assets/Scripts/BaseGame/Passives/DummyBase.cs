using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBase : CharacterBase
{
    public override void Update()
    {
        Health = -100;
        IsDead = true;
        if (Health < 0)
        {
            Health = 0;
        }
        ExpressedSpeed = Speed * SpeedMultiplier;
        ExpressedDamage = Damage * DamageMultiplier;
        ExpressedDefense = Defense * DefenseMultiplier;
        if (SpeedMultiplier < 0.25)
        {
            SpeedMultiplier = (float)0.25;
        }
        if (SpeedMultiplier > 2)
        {
            SpeedMultiplier = (float)2;
        }
        if (DamageMultiplier < 0.25)
        {
            DamageMultiplier = (float)0.25;
        }
        if (DamageMultiplier > 2)
        {
            DamageMultiplier = (float)2;
        }
        if (DefenseMultiplier < 0.25)
        {
            DefenseMultiplier = (float)0.25;
        }
        if (DefenseMultiplier > 2)
        {
            DefenseMultiplier = (float)2;
        }
        if (IsDead == true && Events != null && Events.CheckQue(gameObject, 1))
        {
            Events.CurrentQue++;
        }
        if (IsDead == false)
        {
            if (Health < 1)
            {
                Camera.main.gameObject.GetComponent<ShakeObject>().StartShake((float)0.358, (float)0.258);
                IsDead = true;
            }
            if (MaxHealth < Health)
            {
                Health = MaxHealth;
            }
            CheckIfOutOfBounds();
            if (action != "inactive")
            {
                if (action == "miss" && Events.CheckQue(gameObject, 1))
                {
                    action = "inactive";
                }
                if (action == "Move" && Events.CheckQue(gameObject, 1))
                {
                    Push((int)LocationAction.x, (int)LocationAction.y);
                    action = "inactive";
                    if (IsEnemy == false)
                    {
                        GridData.AllGrids[(int)LocationAction.y][(int)LocationAction.x].GetComponent<GridControl>().IsTargeted = false;
                    }
                }
                if (action == "Shoot" && Events.CheckQue(gameObject, 1) && AboutToShoot == false)
                {
                    AboutToShoot = true;
                    TimeUntilShootLeft = TimeUntilShoot;
                }
                for (int x = 0; x < 4; x++)
                {
                    if (action == "SPMove" + x && Events.CheckQue(gameObject, 1) && AboutToUseMove == false || action == "SPMoveCharge" + x && Events.CheckQue(gameObject, 1) && AboutToUseMove == false)
                    {
                        if (MovesAllowed[x].WillBeUsedForCharging == false || IsCharging && MovesAllowed[x].HasUsedCharge)
                        {
                            AboutToUseMove = true;
                            MoveOn = x;
                            MovesAllowed[x].ChangeAnim(TimeUntilMoveEnd);
                            TimeUntilUseMoveLeft = TimeUseMove;
                        }
                        else if (IsCharging == false && MovesAllowed[x].HasUsedCharge == false)
                        {
                            AboutToUseMove = true;
                            MoveOn = x;
                            MovesAllowed[x].ChangeAnim(TimeUntilChargeMoveEnd);
                            TimeUntilUseMoveLeft = TimeUseChargeMoveEnd;
                        }
                    }
                }
            }
            //DebugFailsafe if Grid outside parameter
            if (GridData != null && GridData.AllGrids[(int)CharacterLocationIndex.y][(int)CharacterLocationIndex.x] == null)
            {
                print("Error- GridPlace does not exist");
                print(gameObject.name + " Is the requesting character");
                print(gameObject.name + " Is at [ " + CharacterLocationIndex.x + ", " + CharacterLocationIndex.y + "]");
                if ((int)CharacterLocationIndex.x > GridData.XWidthPublic - 1)
                {
                    CharacterLocationIndex.x = GridData.XWidthPublic - 1;
                }
                if ((int)CharacterLocationIndex.x < 0)
                {
                    CharacterLocationIndex.x = 0;
                }
                if ((int)CharacterLocationIndex.y > GridData.YWidthPublic - 1)
                {
                    CharacterLocationIndex.y = GridData.YWidthPublic - 1;
                }
                if ((int)CharacterLocationIndex.y < 0)
                {
                    CharacterLocationIndex.y = 0;
                }
            }
            GridSquare = GridData.AllGrids[(int)CharacterLocationIndex.y][(int)CharacterLocationIndex.x];
            GridSquare.GetComponent<GridControl>().CharacterOn = gameObject;
            gameObject.transform.position = new Vector3(GridSquare.transform.position.x, DistanceUp + GridSquare.transform.position.y, 0);
            GameObjectSpriteRenderer.sortingOrder = (int)CharacterLocationIndex.y;
        }
        HasDied = IsDead;
        PreviousHealth = Health;
        if (AboutToShoot == true)
        {
            if (TimeUntilShootLeft < 0)
            {
                AboutToShoot = false;
                gameObject.GetComponent<GunFunction>().ShootMain(LocationAction, !IsEnemy);
                action = "inactive";

            }
            else
            {
                TimeUntilShootLeft -= Time.deltaTime;
            }
        }
        if (AboutToUseMove == true)
        {
            if (TimeUntilUseMoveLeft < 0)
            {
                AboutToUseMove = false;
                MovesAllowed[MoveOn].ActivateMove();

            }
            else
            {
                TimeUntilUseMoveLeft -= Time.deltaTime;
            }
        }
    }
}
