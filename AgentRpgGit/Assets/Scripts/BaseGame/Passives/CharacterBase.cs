using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    //move Residue
    public GameObject MoveLeftOver;
    //Changes how much the character shakes when damages porpotional to damage taken
    public float DamageShakeRatio;
    //SpriteChanger
    public SpriteChange CharacterSChanger;
    //Previous states to find when damaged or dead
    public bool HasDied;
    public float PreviousHealth;
    //
    public bool IsEnemy;
    //Health and deathstate
    public float Health;
    public float MaxHealth;
    public bool IsDead;
    //EventSystem Actions Id 0 is for main move
    //Id 1 for Status Effect 
    //Id 2 for flavor Text 
    //Actions
    [SerializeField]
    public Vector2 LocationAction;
    //Move = "Move"
    //Shoot = "Shoot"
    //SpecialMove = "SPMove" + [Index of special move]
    //Inactive = "inactive"
    //Miss = "miss"
    [SerializeField]
    public string action;
    public bool IsCharging;
    [SerializeField]
    public EventSystem Events;
    //passive
    public bool lookingLeft;
    public bool NormalMove;
    //IMPORTANT
    //LOCATION OF CHARACTER IN GRID
    public Vector2 CharacterLocationIndex;
    public float Speed;
    //If it goes first or second or third, based on movesystem
    public int SpeedPriority;
    public float SpeedMultiplier = 1;
    public float ExpressedSpeed;
    public float Damage;
    public float DamageMultiplier = 1;
    public float ExpressedDamage;
    //Defense damage reduction is 100- Defense /100, then you get the reduction multiplier
    public float Defense;
    public float DefenseMultiplier = 1;
    public float ExpressedDefense;
    public GenericMove[] MovesAllowed = new GenericMove[4];
    [SerializeField]
    public GameObject GridSquare;
    [SerializeField]
    public GridLoad GridData;
    [SerializeField]
    public SpriteRenderer GameObjectSpriteRenderer;
    public bool IsActive;
    //Status Effects
    //find status effect controller for guide
    public int[] StatusEffects = new int[10];
    
    //Movement
    public float DistanceTillAtLocation;
    public float AnimationMoveSpeed;
    //Apearence
    public float DistanceUp;
    public float DistanceRight;
    //Hurt
    public float hurtAnimTimer;
    //Shoot 
    public float TimeUntilShootEnd;
    public bool AboutToShoot;
    public float TimeUntilShoot;
    public float TimeUntilShootLeft;
    //Move
    public bool AboutToUseMove;
    public int MoveOn;
    public float TimeUseMove;
    public float TimeUntilUseMoveLeft;
    public float TimeUntilMoveEnd;
    public float TimeUntilChargeMoveEnd;
    public float TimeUseChargeMoveEnd;
    // Start is called before the first frame update
    public virtual void Start()
    {
        gameObject.name = gameObject.name.Replace("(Clone)", "");
        CharacterSChanger = gameObject.GetComponent<SpriteChange>();
        MovesAllowed = GetComponents<GenericMove>();
        GridData = Camera.main.gameObject.GetComponent<GridLoad>();
        GameObjectSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        IsDead = false;
        if (MaxHealth == 0)
        {
            Health = 1;
            MaxHealth = 1;
        }

    }
    public void RemoveHighLights()
    {
        //Removes Area Highlight
        if (action == "Move")
        {
            if (IsEnemy == false)
            {
                GridData.AllGrids[(int)LocationAction.y][(int)LocationAction.x].GetComponent<GridControl>().IsTargeted = false;
            }
        }
        for (int x = 0; x < 4; x++)
        {
            if (action == "SPMove" + x || action == "SPMoveCharge" + x)
            {
                MovesAllowed[x].AreaHighLightToggle(MovesAllowed[x].AreaSoonToEffect, IsEnemy, false);
            }
        }
    }
    public void RemoveHighLights(Vector2 StoredLocationAction , string StoredString)
    {
        //Removes Area Highlight
        if (StoredString == "Move")
        {
            if (IsEnemy == false)
            {
                GridData.AllGrids[(int)StoredLocationAction.y][(int)StoredLocationAction.x].GetComponent<GridControl>().IsTargeted = false;
            }
        }
        for (int x = 0; x < 4; x++)
        {
            if (StoredString == "SPMove" + x || StoredString == "SPMoveCharge" + x)
            {
                MovesAllowed[x].AreaHighLightToggle(MovesAllowed[x].AreaSoonToEffect, IsEnemy, false);
            }
        }
    }
    public void PushLocationActions(int XMove, int YMove)
    {
        if (LocationAction.x + XMove >= 0 && LocationAction.x + XMove < GridData.XWidthPublic && LocationAction.y + YMove >= 0 && LocationAction.y + YMove < GridData.YWidthPublic && action != "Shoot")
        {
            LocationAction = new Vector2(LocationAction.x + XMove, LocationAction.y + YMove);
            for (int x = 0; x < 4; x++)
            {
                if (action == "SPMove" + x || action == "SPMoveCharge" + x)
                {
                    if (MovesAllowed[x].WillBeUsedForCharging && MovesAllowed[x].AreaSoonToEffect != null)
                    {
                        MovesAllowed[x].AreaSoonToEffect = MovesAllowed[x].NewAreaEffectMove(MovesAllowed[x].AreaSoonToEffect, XMove, YMove);
                    }
                }
            }
        }
        else if (action != null && action != "Shoot")
        {
            action = "miss";
            for (int x = 0; x < Events.GameObjectsInQue.Length; x++)
            {
                if (Events.GameObjectsInQue[x] == gameObject && Events.PriorityInQue[x] == 7 && x != Events.CurrentQue)
                {
                    Events.StringsInQue[x] = gameObject.name + " aimed too far out and missed his shot";
                }
            }
        }
    }
    public virtual float DefenseProcessedDamage (float Damages)
    {
        float DamageTaken = Damages * ((100-Defense)/100 );
        Health -= (float)(int)DamageTaken;
        return (float)(int)DamageTaken;
    }
    public virtual void PushAction(Vector2 ActionCoordinate, string Action, EventSystem EventCommunication)
    {
        LocationAction = ActionCoordinate;
        action = Action;
        Events = EventCommunication;
        if (Action != "inactive")
        {
            if (Action == "miss")
            {
                Events.QueEvent(gameObject, 1, gameObject.name + " aimed too far out and missed his shot",7);
            }
            if (Action == "Move")
            {
                Events.QueEvent(gameObject, 1, gameObject.name + " moved",SpeedPriority);
            }
            if (Action == "Shoot")
            {
                Events.QueEvent(gameObject, 1, gameObject.name + " shot",SpeedPriority);
            }
            for (int x = 0; x < 4; x++)
            {
                if (Action == "SPMove" + x)
                {
                    Events.QueEvent(gameObject, 1, gameObject.name + " used " + MovesAllowed[x].GetType().Name,SpeedPriority);
                }
                if (Action == "SPMoveCharge" + x)
                {
                    if(IsCharging == true)
                    {
                        Events.QueEvent(gameObject, 1, gameObject.name + " is charging " + MovesAllowed[x].GetType().Name,SpeedPriority);
                    }
                    else
                    {
                        Events.QueEvent(gameObject, 1, gameObject.name + " used " + MovesAllowed[x].GetType().Name,SpeedPriority);
                    }
                    
                }
            }
        }
    }
    public virtual void CheckIfOutOfBounds()
    {
        if (CharacterLocationIndex.y < 0)
        {
            CharacterLocationIndex.y = 0;
        }
        if (CharacterLocationIndex.x < 0)
        {
            CharacterLocationIndex.x = 0;
        }
        if (CharacterLocationIndex.y >= GridData.YWidthPublic)
        {
            CharacterLocationIndex.y = GridData.YWidthPublic - 1;
        }
        if (CharacterLocationIndex.x >= GridData.XWidthPublic)
        {
            CharacterLocationIndex.x = GridData.XWidthPublic - 1;
        }
    }
    public virtual void Push(int XLocation, int YLocation)
    {
        //For location action 
        Vector2 OriginalLocation = CharacterLocationIndex;
        int LocationDiffrenceX;
        int LocationDiffrenceY;
        Instantiate(MoveLeftOver, gameObject.transform.position, Quaternion.identity.normalized);
        CheckIfOutOfBounds();
        Vector2 PreviousPosition;
        PreviousPosition = CharacterLocationIndex;
        //Boundaries for X
        if (XLocation >= GridData.XWidthPublic)
        {
            LocationDiffrenceX = (GridData.XWidthPublic - 1) - (int)OriginalLocation.x;
            CharacterLocationIndex.x = GridData.XWidthPublic - 1;
        }
        else if (XLocation < 0)
        {
            LocationDiffrenceX = -(int)OriginalLocation.x;
            CharacterLocationIndex.x = 0;

        }
        else
        {
            LocationDiffrenceX = XLocation - (int)OriginalLocation.x;
            CharacterLocationIndex.x = XLocation;
        }
        //Boundaries for Y
        if (YLocation >= GridData.YWidthPublic)
        {
            LocationDiffrenceY = (GridData.YWidthPublic - 1) - (int)OriginalLocation.y;
            CharacterLocationIndex.y = GridData.YWidthPublic - 1;
        }
        else if (YLocation < 0)
        {
            LocationDiffrenceY = -(int)OriginalLocation.y;
            CharacterLocationIndex.y = 0;
        }
        else
        {
            LocationDiffrenceY = YLocation - (int)OriginalLocation.y;
            CharacterLocationIndex.y = YLocation;
        }
        CheckIfOutOfBounds();
        //If in obstacle afterpushed
        while (GridData.AllGrids[(int)CharacterLocationIndex.y][(int)CharacterLocationIndex.x].GetComponent<GridControl>().ObstacleIndex != 0 && GridData.AllGrids[(int)PreviousPosition.y][(int)PreviousPosition.x].GetComponent<GridControl>().ObstacleIndex == 0 || GridData.AllGrids[(int)CharacterLocationIndex.y][(int)CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn != null && GridData.AllGrids[(int)CharacterLocationIndex.y][(int)CharacterLocationIndex.x].GetComponent<GridControl>().CharacterOn != gameObject)
        {
            if (CharacterLocationIndex != PreviousPosition)
            {
                if (PreviousPosition.x > CharacterLocationIndex.x)
                {
                    CharacterLocationIndex.x++;
                }
                if (PreviousPosition.x < CharacterLocationIndex.x)
                {
                    CharacterLocationIndex.x--;
                }
                if (PreviousPosition.y > CharacterLocationIndex.y)
                {
                    CharacterLocationIndex.y++;
                }
                if (PreviousPosition.y < CharacterLocationIndex.y)
                {
                    CharacterLocationIndex.y--;
                }
            }
            else
            {
                break;
            }
        }
        RemoveHighLights();
        //Changes Location Action and changes if its out of range
        PushLocationActions(LocationDiffrenceX, LocationDiffrenceY);

    }
    // Update is called once per frame
    public virtual void Update()
    {
        if(Health < 0)
        {
            Health = 0;
        }
        ExpressedSpeed = Speed * SpeedMultiplier;
        ExpressedDamage = Damage * DamageMultiplier;
        ExpressedDefense = Defense * DefenseMultiplier;
        if(SpeedMultiplier < 0.25)
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
        if(IsDead == true && Events != null && Events.CheckQue(gameObject, 1))
        {
            Events.CurrentQue++;
        }
        if (IsDead == false)
        {
            if(Health < 1)
            {
                Camera.main.gameObject.GetComponent<ShakeObject>().StartShake((float)0.358,(float)0.258);
                IsDead = true;
            }
            if(MaxHealth < Health)
            {
                Health = MaxHealth;
            }
            CheckIfOutOfBounds();
            if (action != "inactive")
            {
                if(action == "miss" && Events.CheckQue(gameObject, 1))
                {
                    action = "inactive";
                }
                if (action == "Move" && Events.CheckQue(gameObject, 1))
                {
                    Push((int)LocationAction.x, (int)LocationAction.y);
                    CharacterSChanger.SetSprite(0.53333333333, 1);
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
                    CharacterSChanger.SetSprite(TimeUntilShootEnd, 3);
                }
                for (int x = 0; x < 4; x++)
                {
                    if (action == "SPMove" + x && Events.CheckQue(gameObject, 1) && AboutToUseMove == false || action == "SPMoveCharge" + x && Events.CheckQue(gameObject, 1) && AboutToUseMove == false)
                    {
                        if (MovesAllowed[x].WillBeUsedForCharging == false|| IsCharging && MovesAllowed[x].HasUsedCharge)
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
            gameObject.transform.position = new Vector3(GridSquare.transform.position.x + DistanceRight, DistanceUp + GridSquare.transform.position.y, 0);
            GameObjectSpriteRenderer.sortingOrder = (int)CharacterLocationIndex.y;
        }
        if (PreviousHealth > Health)
        {
            gameObject.GetComponent<ShakeObject>().StartShake((float)DamageShakeRatio * (PreviousHealth - Health), (float)0.1);
            if(hurtAnimTimer == 0)
            {
                CharacterSChanger.SetSprite(0.5, 4);
            }
            else
            {
                CharacterSChanger.SetSprite(hurtAnimTimer, 4);
            }
        }
        if (HasDied && !IsDead)
        {
            CharacterSChanger.SetSprite(-69, 0);
        }
        if (!HasDied && IsDead)
        {
            CharacterSChanger.SetSprite(-69, 5);
        }
        if (IsDead)
        {
            CharacterSChanger.SetSprite(-69, 5);
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
