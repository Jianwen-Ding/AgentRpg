using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnFromPauseButton : MonoBehaviour
{
    public void Activate()
    {
        Time.timeScale = 1;
        Destroy(gameObject.transform.parent.parent.parent.gameObject);
        Camera.main.GetComponent<AudioSource>().UnPause();
    }
}
