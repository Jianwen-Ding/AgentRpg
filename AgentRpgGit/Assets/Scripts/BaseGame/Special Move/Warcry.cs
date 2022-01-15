using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warcry : GenericMove
{
    public override void ActivateMove()
    {
        base.ActivateMove();
        Character_Info.DamageMultiplier += (float)0.25;
        Character_Info.DefenseMultiplier -= (float)0.25;
    }
}
