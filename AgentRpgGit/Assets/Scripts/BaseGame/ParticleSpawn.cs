using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawn : MonoBehaviour
{
    [SerializeField]
    float XMin;
    [SerializeField]
    float XMax;
    [SerializeField]
    float YMin;
    [SerializeField]
    float YMax;
    [SerializeField]
    int amount;
    [SerializeField]
    float timeSpawn;
    [SerializeField]
    float timeSpawnLeft = 0;
    [SerializeField]
    bool spawnsOverTime;
    [SerializeField]
    float XSpeedMin;
    [SerializeField]
    float XSpeedMax;
    [SerializeField]
    float YSpeedMin;
    [SerializeField]
    float YSpeedMax;
    [SerializeField]
    float RotationSpeedMin;
    [SerializeField]
    float RotationSpeedMax;
    //Only greyscale
    [SerializeField]
    float darkest;
    [SerializeField]
    float lightest;
    [SerializeField]
    bool nonWhite;
    [SerializeField]
    GameObject prefab;
    public void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject Object = Instantiate(prefab, new Vector3(Random.Range(XMin + gameObject.transform.position.x, XMax + gameObject.transform.position.x), Random.Range(YMin + gameObject.transform.position.y, YMax + gameObject.transform.position.y), 0), Quaternion.identity.normalized);
            if (Object.GetComponent<Rigidbody2D>() != null)
            {
                Object.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(XSpeedMin, XSpeedMax), Random.Range(YSpeedMin, YSpeedMax));
                Object.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(RotationSpeedMin, RotationSpeedMax);
            }
            if (Object.GetComponent<SpriteRenderer>() != null && nonWhite == false)
            {
                float colorSet = Random.Range(darkest, lightest);
                Color newColor = new Color(colorSet, colorSet, colorSet );
                Object.GetComponent<SpriteRenderer>().color = newColor; 
            }
            Object.transform.parent = gameObject.transform;
        }
    }
    public void Update()
    {
        if(spawnsOverTime == true)
        {
            if(timeSpawnLeft < 0)
            {
                for (int i = 0; i < amount; i++)
                {
                    GameObject Object = Instantiate(prefab, new Vector3(Random.Range(XMin + gameObject.transform.position.x, XMax + gameObject.transform.position.x), Random.Range(YMin + gameObject.transform.position.y, YMax + gameObject.transform.position.y), 0), Quaternion.identity.normalized);
                    if (Object.GetComponent<Rigidbody2D>() != null)
                    {
                        Object.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(XSpeedMin, XSpeedMax), Random.Range(YSpeedMin, YSpeedMax));
                        Object.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(RotationSpeedMin, RotationSpeedMax);
                    }
                    if (Object.GetComponent<SpriteRenderer>() != null && nonWhite == false)
                    {
                        float colorSet = Random.Range(darkest, lightest);
                        Color newColor = new Color(colorSet, colorSet, colorSet);
                        Object.GetComponent<SpriteRenderer>().color = newColor;
                    }
                    Object.transform.parent = gameObject.transform;
                }
                timeSpawnLeft = timeSpawn;
            }
            else
            {
                timeSpawnLeft -= Time.deltaTime;
            }
        }
    }
}
