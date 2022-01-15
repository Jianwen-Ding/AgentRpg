using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickMoveSlide : MonoBehaviour
{
    public float MaxY;
    public float MinY;
    public float SpeedSlide;
    // Update is called once per frame
    void Start()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, MaxY, 0);
    }
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            if(gameObject.transform.position.y + (SpeedSlide * Time.deltaTime) <= MaxY)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (SpeedSlide * Time.deltaTime), 0);
            }
            else
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, MaxY , 0);
            }
            
        }
        if (Input.GetAxisRaw("Vertical") > 0 )
        {
            if (gameObject.transform.position.y - (SpeedSlide * Time.deltaTime) >= MinY)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - (SpeedSlide * Time.deltaTime), 0);
            }
            else
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, MinY, 0);
            }
        }
    }
}
