using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresenceAnim : MonoBehaviour
{
    public Vector2 loc;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = loc;
    }
}
