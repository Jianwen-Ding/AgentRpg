using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : MonoBehaviour
{
    bool AllowsForBulletPenentration;
    void Update()
    {
        gameObject.GetComponent<GridControl>().AllowsForPenentration = AllowsForBulletPenentration;
        
    }
}
