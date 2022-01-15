using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChange : MonoBehaviour
{
    public MoveSum Main;
    [SerializeField ]
    bool plus;
    public void Activate()
    {
        if (plus)
        {
            Main.EnemyCurrentMain++;
        }
        else
        {
            Main.EnemyCurrentMain--;
        }
        
    }
}
