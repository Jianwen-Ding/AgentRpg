using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsLifeTime : MonoBehaviour
{
    public float TimeTillSelfDestruct;
    public float TimeTillSelfDestructLeft;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if(TimeTillSelfDestruct != 0)
        {
            TimeTillSelfDestructLeft += Time.deltaTime;
            if(TimeTillSelfDestructLeft > TimeTillSelfDestruct)
            {
                Destroy(gameObject);
            }
        }
    }
}
