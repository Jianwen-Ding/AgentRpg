using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresenceCountdown : SpecialInteractions
{
    public override void Activate()
    {
        BotAi botC = gameObject.GetComponent<BotAi>();
        int DistanceMax = 100;
        for (int i = 0; i < botC.Opponents.Length; i++)
        {
            if (botC.Opponents[i].IsDead == false)
            {
                if(Mathf.Abs(botC.Opponents[i].CharacterLocationIndex.y - baseC.CharacterLocationIndex.y) > Mathf.Abs(botC.Opponents[i].CharacterLocationIndex.x - baseC.CharacterLocationIndex.x)){
                    if ( DistanceMax > Mathf.Abs(botC.Opponents[i].CharacterLocationIndex.y - baseC.CharacterLocationIndex.y))
                    {
                        DistanceMax = (int)Mathf.Abs(botC.Opponents[i].CharacterLocationIndex.y - baseC.CharacterLocationIndex.y);
                    }
                }
                else
                {
                    if (DistanceMax > Mathf.Abs(botC.Opponents[i].CharacterLocationIndex.x - baseC.CharacterLocationIndex.x))
                    {
                        DistanceMax = (int)Mathf.Abs(botC.Opponents[i].CharacterLocationIndex.x - baseC.CharacterLocationIndex.x);
                    }
                }
            }

        }
        eventC.QueEvent(gameObject, 4, " Presence is " + DistanceMax + " tiles away", 9);
    }
}
