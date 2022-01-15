using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    [SerializeField]
    bool Loads;
    [SerializeField]
    float time;
    [SerializeField]
    float prevTime;
    [SerializeField]
    float StartTime;
    [SerializeField]
    float EndTime;
    // Start is called before the first frame update
    void Start()
    {
        if(Loads == false)
        {
            DontDestroyOnLoad(gameObject);
        }
        
        if (GameObject.FindGameObjectsWithTag("MenuMusic").Length > 1)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
       if(gameObject.GetComponent<AudioSource>().time > EndTime)
        {
            gameObject.GetComponent<AudioSource>().time = StartTime;
        }
       if(time != prevTime)
        {
            gameObject.GetComponent<AudioSource>().time = time;
        }
        prevTime = time;
    }
    public static void DestroySelf()
    {
        Destroy(GameObject.FindGameObjectWithTag("MenuMusic"));
    }
}
