using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KellyRevengePassive : MonoBehaviour
{
    BotAi Refrence;
    MoveSystem Move;
    public CharacterBase baseC;
    public EventSystem eventC;
    bool hasEnragedBefore;
    // Start is called before the first frame update
    void Start()
    {
        Refrence = gameObject.GetComponent<BotAi>();
        baseC = gameObject.GetComponent<CharacterBase>();
        Move = Camera.main.gameObject.GetComponent<MoveSystem>();
        eventC = GameObject.Find("EventDisplayer").GetComponent<EventSystem>();
        hasEnragedBefore = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (hasEnragedBefore == false)
        {
            CharacterBase[] Allys = Refrence.Allys;
            CharacterBase selfRef;
            selfRef = gameObject.GetComponent<CharacterBase>();
            bool restDead;
            restDead = true;
            if (Allys != null)
            {
                for (int i = 0; i < Allys.Length; i++)
                {
                    if (Allys[i].IsDead == false && Allys[i] != selfRef)
                    {
                        restDead = false;
                    }
                }
            }
            if (restDead && selfRef.IsDead == false)
            {
                Refrence.Priorities = new string[1];
                Refrence.Priorities[0] = "Shoot";
                Refrence.RangeOfRandomness = new int[1];
                Refrence.RangeOfRandomness[0] = 0;
                Refrence.ShootAdd = 10000;
                eventC.QueEvent(gameObject, 4, "Kelly is enraged", 9);
                hasEnragedBefore = true;
            }
        }
    }
}
