using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : MonoBehaviour
{
    //Works on still objects
    [SerializeField]
    Vector3 OriginalPosition;
    [SerializeField]
    Vector3 OriginalShakePosition;
    [SerializeField]
    bool ShakeStart;
    [SerializeField]
    float ShakeIntensity;
    [SerializeField]
    float ShakeTimeLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void StartShake(float Intensity, float Length)
    {
        if(ShakeStart == false)
        {
            OriginalPosition = gameObject.transform.position;
        }
        ShakeStart = true;
        ShakeIntensity = Intensity;
        ShakeTimeLeft = Length;
        
    }
    // Update is called once per frame
    void Update()
    {
        if(OriginalShakePosition != gameObject.transform.position && OriginalPosition != gameObject.transform.position)
        {
            OriginalPosition = gameObject.transform.position;
        }
        if(ShakeStart == true)
        {
            ShakeTimeLeft -= Time.deltaTime;
            gameObject.transform.position = new Vector3(OriginalPosition.x + Random.Range(-ShakeIntensity, ShakeIntensity), OriginalPosition.y + Random.Range(-ShakeIntensity, ShakeIntensity), OriginalPosition.z);
            OriginalShakePosition = gameObject.transform.position;
            if (ShakeTimeLeft < 0)
            {
                gameObject.transform.position = OriginalPosition;
                ShakeStart = false;
            }
            
        }
    }
}
