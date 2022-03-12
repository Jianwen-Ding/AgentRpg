using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialInteractions : MonoBehaviour
{
    MoveSystem Move;
    public CharacterBase baseC;
    public EventSystem eventC;
    bool hasSetDisplayBefore = true;
    // Start is called before the first frame update
    void Start()
    {
        baseC = gameObject.GetComponent<CharacterBase>();
        Move = Camera.main.gameObject.GetComponent<MoveSystem>();
        eventC = GameObject.Find("EventDisplayer").GetComponent<EventSystem>();
    }
    public virtual void Activate()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Move.IsDisplayingHappening == false && hasSetDisplayBefore == false)
        {
            hasSetDisplayBefore = true;
            Activate();
        }
        if(Move.IsDisplayingHappening == true)
        {
            hasSetDisplayBefore = false;
        }
    }
}
