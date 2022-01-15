using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpup : GenericMove
{
    [SerializeField]
    int TurnsHad = 1;
    bool WasActiveBefore;
    MoveSystem MoveTally;
    public override void Start()
    {
        MoveTally = Camera.main.GetComponent<MoveSystem>();
        base.Start();
    }
    public override void ActivateMove()
    {
        Character_Info.CharacterSChanger.SetSprite(1, 2);
        //Sucsess
        if (TurnsHad > 5)
        {
            base.ActivateMove();
            Character_Info.DamageMultiplier += (float)0.2;
        }
        //Failure
        else
        {
            EffectAmount = 0;
            gameObject.GetComponent<CharacterBase>().action = "inactive";
            Effects[0] = Instantiate(MoveSpriteSecondary, new Vector3(Gridinfo.AllGrids[(int)Character_Info.CharacterLocationIndex.y][(int)Character_Info.CharacterLocationIndex.x].transform.position.x + AdjustSprite.x, Gridinfo.AllGrids[(int)Character_Info.CharacterLocationIndex.y][(int)Character_Info.CharacterLocationIndex.x].transform.position.y + AdjustSprite.y), Quaternion.identity.normalized);
            Effects[0].GetComponent<EffectsLifeTime>().TimeTillSelfDestruct = 1;
        }
    }
    public override void Update()
    {
        if (MoveTally.IsDisplayingHappening == false && WasActiveBefore == true)
        {
            TurnsHad++;
        }
        WasActiveBefore = MoveTally.IsDisplayingHappening;
        base.Update();
    }
}
