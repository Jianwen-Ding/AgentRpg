using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresenceBase : CharacterBase
{
    public virtual void PushAction(Vector2 ActionCoordinate, string Action, EventSystem EventCommunication)
    {
        LocationAction = ActionCoordinate;
        action = Action;
        Events = EventCommunication;
        if (Action != "inactive")
        {
            if (Action == "miss")
            {
                Events.QueEvent(gameObject, 1, gameObject.name + " aimed too far out and missed his shot", 7);
            }
            if (Action == "Move")
            {
                Events.QueEvent(gameObject, 1, gameObject.name + " moved", SpeedPriority);
            }
            if (Action == "Shoot")
            {
                Events.QueEvent(gameObject, 1, gameObject.name + " shot", SpeedPriority);
            }
            for (int x = 0; x < 4; x++)
            {
                if (Action == "SPMove" + x)
                {
                    Events.QueEvent(gameObject, 1, gameObject.name + " used " + MovesAllowed[x].GetType().Name, SpeedPriority);
                }
                if (Action == "SPMoveCharge" + x)
                {
                    if (IsCharging == true)
                    {
                        Events.QueEvent(gameObject, 1, gameObject.name + " is charging " + MovesAllowed[x].GetType().Name, SpeedPriority);
                    }
                    else
                    {
                        Events.QueEvent(gameObject, 1, gameObject.name + " used " + MovesAllowed[x].GetType().Name, SpeedPriority);
                    }

                }
            }
        }
    }
}
