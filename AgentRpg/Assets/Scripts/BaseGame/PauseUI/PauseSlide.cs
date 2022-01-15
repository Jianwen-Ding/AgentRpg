using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSlide : MonoBehaviour
{
    [SerializeField]
    float MaxX;
    [SerializeField]
    float Speed;
    // Update is called once per frame
    void Update()
    {
            if(gameObject.transform.position.x > MaxX)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - (Time.fixedDeltaTime * Speed), gameObject.transform.position.y, 0);
        }
        else
        {
            gameObject.transform.position = new Vector3(MaxX, gameObject.transform.position.y, 0);
        }
    }
}
