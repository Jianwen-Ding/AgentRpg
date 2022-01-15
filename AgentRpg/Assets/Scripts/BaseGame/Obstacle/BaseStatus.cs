using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatus : MonoBehaviour
{
    public int enemyMinusPriority = 100;
    public int TurnsTillDissapearLeft;
    public bool PreviouslyActiveEvent;
    public EventSystem EventAcsess;
    public GridControl Grid_Info;
    public bool HasTriggered;
    public bool HasSetUp;
    // Start is called before the first frame update
    public virtual void Start()
    {
        
        EventAcsess = Camera.main.gameObject.GetComponent<MoveSystem>().EventDisplayer.GetComponent<EventSystem>();
        Grid_Info = gameObject.GetComponent<GridControl>();
        PreviouslyActiveEvent = false;
        TurnsTillDissapearLeft = 4;
        HasTriggered = false;
        HasSetUp = false;
    }
    public virtual void ObjectTrigger()
    {
        
    }
    public virtual void SetUp()
    {
        EventAcsess.QueEvent(gameObject, 0, Grid_Info.CharacterOn.name + " triggers " + this.GetType().Name , 6);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(Grid_Info == null)
        {
            Grid_Info = gameObject.GetComponent<GridControl>();
        }
        if(TurnsTillDissapearLeft <= 0)
        {
            gameObject.GetComponent<GridControl>().StatusIndex = 0;
            gameObject.GetComponent<ObstacleInsert>().WipeStatus();
        }
        if(Grid_Info.CharacterOn != null)
        {
            //Help debugging
            if (Grid_Info != null && TurnsTillDissapearLeft > 0 && HasSetUp == false && HasTriggered == false)
            {
                SetUp();
                HasSetUp = true;
            }
        }
        
        if (EventAcsess.active == false && PreviouslyActiveEvent == true && TurnsTillDissapearLeft > 0)
        {
            TurnsTillDissapearLeft -= 1;
            HasTriggered = false;
            HasSetUp = false;
        }
        PreviouslyActiveEvent = EventAcsess.active;
        if(EventAcsess.CheckQue(gameObject, 0) && HasTriggered == false)
        {
            ObjectTrigger();       
            HasTriggered = true;
        }
    }
}
