using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour
{
    public SpriteRenderer CharacterRender;
    //69 = nothing
    //0 = idle
    //1 = move
    //2 = special move default
    //Unique special move animations(just for enemies ai)
    // -1 =special move1
    // -2 =special move2
    // -3 =special move3
    // -4 =special move4
    //3 = gun
    //4 = hurt
    //5 = down
    //6 = base highlight
    //7 = ChargeMove
    public double TimeLeftTillDissapear;
    public int currentFunction = 0;
    public bool timed;
    public bool hasSet = true;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        CharacterRender = gameObject.GetComponent<SpriteRenderer>(); 
    }
    //Write in -69 in the time section in order to trigger 
    public void SetSprite(double time, int functionChangeTo)
    {
        if(currentFunction != functionChangeTo)
        {
            currentFunction = functionChangeTo;
            if (time != -69)
            {
                timed = true;
                TimeLeftTillDissapear = time;
            }
            else
            {
                timed = false;
            }
            anim.SetInteger("CurrentState", functionChangeTo);
            anim.SetInteger("TrueState", functionChangeTo);
            hasSet = false;
        }
        if(time != TimeLeftTillDissapear)
        {
            TimeLeftTillDissapear = time;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (hasSet == true)
        {
            anim.SetInteger("CurrentState", 69);
        }
        hasSet = true;
        if (timed)
        {
            if (TimeLeftTillDissapear <= 0)
            {
                SetSprite(-69,0);
            }
            else
            {
                TimeLeftTillDissapear -= Time.deltaTime;
            }
        }
        
    }
}
