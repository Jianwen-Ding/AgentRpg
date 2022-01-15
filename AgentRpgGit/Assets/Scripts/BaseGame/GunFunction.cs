using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFunction : MonoBehaviour
{
    public GameObject EffectTrial;
    public Vector2 EffectTrialAdjust;
    public GameObject EffectHit;
    public Vector2 EffectHitAdjust;
    public string Name;
    //To be used by its own gameObject
    //Between 1,0,-1 rate of change
    public int XChangePerSquare;
    //Between 1,0,-1 rate of change
    public int YChangePerSquare;
    public bool CanPenentrateObstacle;
    public bool CanPenentrateCharacters;
    [SerializeField]
    public int bulletAddition;
    [SerializeField]
    public int DistanceSquaresAllowed;
    public bool ShootForEnemy;
    [SerializeField]
    public GameObject Text;
    [SerializeField]
    public GameObject InWorldText;
    [SerializeField]
    public float DamageRatio;
    [SerializeField]
    public float DamageFallOff;
    GridLoad GridData;
    public CharacterBase CharacterCode;
    // This is used for ShootFunction
    public GameObject Effect;
    //ShootAtEnemy is true when it damages enemies
    //Finds end of shot
    //Amount of damage converted by gun is DamageScale

    Vector2 ShootFunction(bool ShootAtEnemy, int XVelocity,int YVelocity, int SquareDistance, Vector2 StartingPosition, bool AllowObstaclePenentration, bool CharacterPiercing, float DamageScale, float DamageFallOffSet, bool WillShowEffect, bool WillDamage)
    {

        float DamageOnHit = (DamageScale * CharacterCode.ExpressedDamage);
        float DamageInflicted;
        Vector2 CurrentSquare;
        CurrentSquare = StartingPosition;
        for (int X = 0; X < SquareDistance; X++)
        {
            Effect = null;
            if (CurrentSquare.x < 0 || CurrentSquare.y < 0 || CurrentSquare.y > GridData.YWidthPublic - 1 || CurrentSquare.x > GridData.XWidthPublic - 1)
            {
                break;
            }
            if (GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].GetComponent<GridControl>().AllowsForPenentration == false)
            {
                if(AllowObstaclePenentration != true)
                {
                    if (EffectHit != null && WillShowEffect == true)
                    {
                        Effect = Instantiate(EffectHit, new Vector3(GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].gameObject.transform.position.x + EffectTrialAdjust.x, GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].gameObject.transform.position.y + EffectTrialAdjust.y), Quaternion.identity.normalized);
                    }
                    if(Effect != null && Effect.GetComponent<EffectsLifeTime>() != null)
                    {
                        Effect.GetComponent<EffectsLifeTime>().TimeTillSelfDestruct = (float)0.25;
                    }
                    return CurrentSquare;
                }
            }
            if (GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].GetComponent<GridControl>().CharacterOn != null && GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].GetComponent<GridControl>().CharacterOn != gameObject)
            {
                if (ShootAtEnemy == GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].GetComponent<GridControl>().CharacterOn.GetComponent<CharacterBase>().IsEnemy)
                {
                    if(WillDamage == true)
                    {
                        DamageInflicted = GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].GetComponent<GridControl>().CharacterOn.GetComponent<CharacterBase>().DefenseProcessedDamage(DamageOnHit);
                        InWorldText = Instantiate(Text, GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].GetComponent<GridControl>().CharacterOn.transform);
                        InWorldText.transform.position = GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].GetComponent<GridControl>().CharacterOn.transform.position;
                        if (CurrentSquare.x > CharacterCode.GetComponent<CharacterBase>().CharacterLocationIndex.x)
                        {
                            InWorldText.GetComponent<FadeOutText>().BeginInitiate(1, DamageInflicted.ToString(), Color.black, new Vector2(5, 5));
                        }
                        else
                        {
                            InWorldText.GetComponent<FadeOutText>().BeginInitiate(1, DamageInflicted.ToString(), Color.black, new Vector2(-5, 5));
                        }
                    }
                    if (CharacterPiercing != true)
                    {
                        if(EffectHit != null && WillShowEffect == true)
                        {
                            Effect = Instantiate(EffectHit, new Vector3(GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].gameObject.transform.position.x + EffectTrialAdjust.x, GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].gameObject.transform.position.y + EffectTrialAdjust.y), Quaternion.identity.normalized);
                        }
                        if (Effect != null && Effect.GetComponent<EffectsLifeTime>() != null)
                        {
                            Effect.GetComponent<EffectsLifeTime>().TimeTillSelfDestruct = (float)0.25;
                        }
                        return CurrentSquare;
                    }
                }
            }
            if(EffectTrial != null)
            {
                Effect = Instantiate(EffectTrial, new Vector3(GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].gameObject.transform.position.x + EffectTrialAdjust.x, GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].gameObject.transform.position.y + EffectTrialAdjust.y), Quaternion.identity.normalized);
            }
            if(X + 1 < SquareDistance)
            {
                CurrentSquare.x += XVelocity;
                CurrentSquare.y += YVelocity;
            }
            DamageOnHit *= DamageFallOffSet;
            if(Effect != null && Effect.GetComponent<EffectsLifeTime>() != null)
            {
                Effect.GetComponent<EffectsLifeTime>().TimeTillSelfDestruct = (float)0.25;
            }
            if(WillShowEffect != true)
            {
                Destroy(Effect);
            }
        }
        return CurrentSquare;
    }
    //Returns all of the squares it has passed through
    Vector2[] ShootCheck(bool ShootAtEnemy, int XVelocity, int YVelocity, int SquareDistance, Vector2 StartingPosition, bool AllowObstaclePenentration, bool CharacterPiercing, float DamageScale, float DamageFallOffSet, bool WillShowEffect, bool WillDamage, GameObject EffectHitPrefab, GameObject EffectPrefab)
    {
        if(CharacterCode != null)
        {
            float DamageOnHit = (DamageScale * CharacterCode.ExpressedDamage);
            float DamageInflicted;
            Vector2[] CheckArea = new Vector2[SquareDistance];
            for (int x = 0; x < CheckArea.Length; x++)
            {
                CheckArea[x] = new Vector2(-69, -69);
            }
            Vector2 CurrentSquare;
            CurrentSquare = StartingPosition;
            for (int X = 0; X < SquareDistance; X++)
            {
                CheckArea[X] = CurrentSquare;
                Effect = null;
                if (CurrentSquare.x < 0 || CurrentSquare.y < 0 || CurrentSquare.y > GridData.YWidthPublic - 1 || CurrentSquare.x > GridData.XWidthPublic - 1)
                {
                    break;
                }
                if (GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].GetComponent<GridControl>().AllowsForPenentration == false)
                {
                    if (AllowObstaclePenentration != true)
                    {
                        if (EffectHitPrefab != null && WillShowEffect == true)
                        {
                            Effect = Instantiate(EffectHitPrefab, new Vector3(GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].gameObject.transform.position.x + EffectTrialAdjust.x, GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].gameObject.transform.position.y + EffectTrialAdjust.y), Quaternion.identity.normalized);
                        }
                        if (Effect != null && Effect.GetComponent<EffectsLifeTime>() != null)
                        {
                            Effect.GetComponent<EffectsLifeTime>().TimeTillSelfDestruct = (float)0.25;
                        }
                        return CheckArea;
                    }
                }
                if (GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].GetComponent<GridControl>().CharacterOn != null && GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].GetComponent<GridControl>().CharacterOn != gameObject)
                {
                    if (ShootAtEnemy == GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].GetComponent<GridControl>().CharacterOn.GetComponent<CharacterBase>().IsEnemy)
                    {
                        if (WillDamage == true)
                        {
                            DamageInflicted = GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].GetComponent<GridControl>().CharacterOn.GetComponent<CharacterBase>().DefenseProcessedDamage(DamageOnHit);
                            InWorldText = Instantiate(Text, GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].GetComponent<GridControl>().CharacterOn.transform);
                            InWorldText.transform.position = GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].GetComponent<GridControl>().CharacterOn.transform.position;
                            if (CurrentSquare.x > CharacterCode.GetComponent<CharacterBase>().CharacterLocationIndex.x)
                            {
                                InWorldText.GetComponent<FadeOutText>().BeginInitiate(1, DamageInflicted.ToString(), Color.black, new Vector2(5, 5));
                            }
                            else
                            {
                                InWorldText.GetComponent<FadeOutText>().BeginInitiate(1, DamageInflicted.ToString(), Color.black, new Vector2(-5, 5));
                            }
                        }
                        if (CharacterPiercing != true)
                        {
                            if (EffectHitPrefab != null && WillShowEffect == true)
                            {
                                Effect = Instantiate(EffectHitPrefab, new Vector3(GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].gameObject.transform.position.x + EffectTrialAdjust.x, GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].gameObject.transform.position.y + EffectTrialAdjust.y), Quaternion.identity.normalized);
                            }
                            if (Effect != null && Effect.GetComponent<EffectsLifeTime>() != null)
                            {
                                Effect.GetComponent<EffectsLifeTime>().TimeTillSelfDestruct = (float)0.25;
                            }
                            return CheckArea;
                        }
                    }
                }
                if (EffectPrefab != null)
                {
                    Effect = Instantiate(EffectPrefab, new Vector3(GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].gameObject.transform.position.x + EffectTrialAdjust.x, GridData.AllGrids[(int)CurrentSquare.y][(int)CurrentSquare.x].gameObject.transform.position.y + EffectTrialAdjust.y), Quaternion.identity.normalized);
                }
                if (X + 1 < SquareDistance)
                {
                    CurrentSquare.x += XVelocity;
                    CurrentSquare.y += YVelocity;
                }
                DamageOnHit *= DamageFallOffSet;
                if (Effect != null && Effect.GetComponent<EffectsLifeTime>() != null)
                {
                    Effect.GetComponent<EffectsLifeTime>().TimeTillSelfDestruct = (float)0.25;
                }
                if (WillShowEffect != true)
                {
                    Destroy(Effect);
                }
            }
            return CheckArea;
        }
        else
        {
            return null;
        }
    }
    public void EstablishGun(int SquareDistance, bool ObstacleCanPenentrate, bool CharacterPenentration, float DamagePercentage, float DamageFallOffSet, int BulletsAmounts, GameObject GunEffectTrial, Vector2 GunEffectTrialAdjust, GameObject GunEffectHit, Vector2 GunHitAdjust)
    {
        DistanceSquaresAllowed = SquareDistance;
        CanPenentrateCharacters  = ObstacleCanPenentrate;
        CanPenentrateCharacters = CharacterPenentration;
        DamageRatio = DamagePercentage;
        DamageFallOff = DamageFallOffSet;
        bulletAddition = BulletsAmounts;
        EffectHit = GunEffectHit;
        EffectHitAdjust = GunHitAdjust;
        EffectTrial = GunEffectTrial;
        EffectTrialAdjust = GunEffectTrialAdjust;
    }
    //Diffrent Guns Ovveride it
    public void ShootMain(Vector2 Velocity, bool ShootAtEnemy)
    {

        XChangePerSquare = (int)Velocity.x;
        YChangePerSquare = (int)Velocity.y;
        ShootFunction(ShootAtEnemy, (int)Velocity.x, (int)Velocity.y, DistanceSquaresAllowed, new Vector2(CharacterCode.GetComponent<CharacterBase>().CharacterLocationIndex.x + XChangePerSquare , CharacterCode.GetComponent<CharacterBase>().CharacterLocationIndex.y + YChangePerSquare), CanPenentrateObstacle, CanPenentrateCharacters, DamageRatio,DamageFallOff, true, true);
        for (float x = 1; x < bulletAddition; x += 1 )
        {
            if((float)((int)(x/2)) == x / 2)
            {
                if(XChangePerSquare == 0)
                {
                    ShootFunction(ShootAtEnemy, (int)Velocity.x, (int)Velocity.y, DistanceSquaresAllowed, new Vector2(CharacterCode.GetComponent<CharacterBase>().CharacterLocationIndex.x + XChangePerSquare + (x/2), CharacterCode.GetComponent<CharacterBase>().CharacterLocationIndex.y + YChangePerSquare), CanPenentrateObstacle, CanPenentrateCharacters, DamageRatio, DamageFallOff, true, true);
                }
                if (YChangePerSquare == 0)
                {
                    ShootFunction(ShootAtEnemy, (int)Velocity.x, (int)Velocity.y, DistanceSquaresAllowed, new Vector2(CharacterCode.GetComponent<CharacterBase>().CharacterLocationIndex.x + XChangePerSquare, CharacterCode.GetComponent<CharacterBase>().CharacterLocationIndex.y + YChangePerSquare + (x/2)), CanPenentrateObstacle, CanPenentrateCharacters, DamageRatio, DamageFallOff, true, true);
                }
            }
            else
            {
                if (XChangePerSquare == 0)
                {
                    ShootFunction(ShootAtEnemy, (int)Velocity.x, (int)Velocity.y, DistanceSquaresAllowed, new Vector2(CharacterCode.GetComponent<CharacterBase>().CharacterLocationIndex.x + XChangePerSquare - x, CharacterCode.GetComponent<CharacterBase>().CharacterLocationIndex.y + YChangePerSquare), CanPenentrateObstacle, CanPenentrateCharacters, DamageRatio, DamageFallOff, true, true);
                }
                if (YChangePerSquare == 0)
                {
                    ShootFunction(ShootAtEnemy, (int)Velocity.x, (int)Velocity.y, DistanceSquaresAllowed, new Vector2(CharacterCode.GetComponent<CharacterBase>().CharacterLocationIndex.x + XChangePerSquare, CharacterCode.GetComponent<CharacterBase>().CharacterLocationIndex.y + YChangePerSquare - x), CanPenentrateObstacle, CanPenentrateCharacters, DamageRatio, DamageFallOff, true, true);
                }
            }
        }
    }
    //IMPLEMENT DOUBLE VARIBLES INTO SHOOTABILITY
    // DO IT
    //
    //
    public Vector2[][] ShootAbility(Vector2 StartLocation, Vector2 Velocity, bool ShootAtEnemy, int SquareDistance, bool AllowObstaclePenentration, bool CharacterPiercing, float DamageScale, float DamageFallOffSet, int AdditionalBullets, GameObject GunEffectPrefab, GameObject GunEffectHitPrefab)
    {
        if(CharacterCode != null)
        {
            Vector2[][] EffectedSquares;
            EffectedSquares = new Vector2[AdditionalBullets][];
            XChangePerSquare = (int)Velocity.x;
            YChangePerSquare = (int)Velocity.y;
            EffectedSquares[0] = new Vector2[ShootCheck(ShootAtEnemy, (int)Velocity.x, (int)Velocity.y, SquareDistance, new Vector2(StartLocation.x + XChangePerSquare, StartLocation.y + YChangePerSquare), AllowObstaclePenentration, AllowObstaclePenentration, DamageScale, DamageFallOffSet, false, false, null, null).Length];
            EffectedSquares[0] = ShootCheck(ShootAtEnemy, (int)Velocity.x, (int)Velocity.y, SquareDistance, new Vector2(StartLocation.x + XChangePerSquare, StartLocation.y + YChangePerSquare), AllowObstaclePenentration, AllowObstaclePenentration, DamageScale, DamageFallOffSet, true, false, GunEffectHitPrefab, GunEffectPrefab);
            for (float x = 1; x < AdditionalBullets; x += 1)
            {
                if ((float)((int)(x / 2)) == x / 2)
                {
                    if (XChangePerSquare == 0)
                    {
                        EffectedSquares[(int)x] = new Vector2[ShootCheck(ShootAtEnemy, (int)Velocity.x, (int)Velocity.y, SquareDistance, new Vector2(StartLocation.x + XChangePerSquare + (x / 2), StartLocation.y + YChangePerSquare), AllowObstaclePenentration, CharacterPiercing, DamageScale, DamageFallOffSet, false, false, null, null).Length];
                        EffectedSquares[(int)x] = ShootCheck(ShootAtEnemy, (int)Velocity.x, (int)Velocity.y, SquareDistance, new Vector2(StartLocation.x + XChangePerSquare + (x / 2), StartLocation.y + YChangePerSquare), AllowObstaclePenentration, CharacterPiercing, DamageScale, DamageFallOffSet, true, false, GunEffectHitPrefab, GunEffectPrefab);
                    }
                    if (YChangePerSquare == 0)
                    {
                        EffectedSquares[(int)x] = new Vector2[ShootCheck(ShootAtEnemy, (int)Velocity.x, (int)Velocity.y, SquareDistance, new Vector2(StartLocation.x + XChangePerSquare, StartLocation.y + YChangePerSquare + (x / 2)), AllowObstaclePenentration, CharacterPiercing, DamageScale, DamageFallOffSet, false, false, null, null).Length];
                        EffectedSquares[(int)x] = ShootCheck(ShootAtEnemy, (int)Velocity.x, (int)Velocity.y, SquareDistance, new Vector2(StartLocation.x + XChangePerSquare, StartLocation.y + YChangePerSquare + (x / 2)), AllowObstaclePenentration, CharacterPiercing, DamageScale, DamageFallOffSet, true, false, GunEffectHitPrefab, GunEffectPrefab);
                    }
                }
                else
                {
                    if (XChangePerSquare == 0)
                    {
                        EffectedSquares[(int)x] = new Vector2[ShootCheck(ShootAtEnemy, (int)Velocity.x, (int)Velocity.y, SquareDistance, new Vector2(StartLocation.x + XChangePerSquare - x, StartLocation.y + YChangePerSquare), AllowObstaclePenentration, CharacterPiercing, DamageScale, DamageFallOffSet, true, false, null, null).Length];
                        EffectedSquares[(int)x] = ShootCheck(ShootAtEnemy, (int)Velocity.x, (int)Velocity.y, SquareDistance, new Vector2(StartLocation.x + XChangePerSquare - x, StartLocation.y + YChangePerSquare), AllowObstaclePenentration, CharacterPiercing, DamageScale, DamageFallOffSet, true, false, GunEffectHitPrefab, GunEffectPrefab);
                    }
                    if (YChangePerSquare == 0)
                    {
                        EffectedSquares[(int)x] = new Vector2[ShootCheck(ShootAtEnemy, (int)Velocity.x, (int)Velocity.y, SquareDistance, new Vector2(StartLocation.x + XChangePerSquare, StartLocation.y + YChangePerSquare - x), AllowObstaclePenentration, CharacterPiercing, DamageScale, DamageFallOffSet, true, false, null, null).Length];
                        EffectedSquares[(int)x] = ShootCheck(ShootAtEnemy, (int)Velocity.x, (int)Velocity.y, SquareDistance, new Vector2(StartLocation.x + XChangePerSquare, StartLocation.y + YChangePerSquare - x), AllowObstaclePenentration, CharacterPiercing, DamageScale, DamageFallOffSet, true, false, GunEffectHitPrefab, GunEffectPrefab);
                    }
                }
            }
            return EffectedSquares;
        }
        else
        {
            return null;
        }
        

    }

    void Start()
    {
        GridData = Camera.main.gameObject.GetComponent<GridLoad>();
        CharacterCode = gameObject.GetComponent<CharacterBase>();
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
