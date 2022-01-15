using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField]
    GameObject PauseMenuSlide;
    GameObject Object;
    public void Activate()
    {
        if (Object == null && Camera.main.GetComponent<MoveSystem>().GameHasEnded == false)
        {
            Time.timeScale = 0;
            Camera.main.GetComponent<AudioSource>().Pause();
            Object = Instantiate(PauseMenuSlide);
        }
       
    }
}
