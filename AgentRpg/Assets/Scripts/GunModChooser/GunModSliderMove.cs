using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GunModSliderMove : MonoBehaviour
{
    [SerializeField]
    public int CurrentY;
    [SerializeField]
    float Startx;
    [SerializeField]
    float Starty;
    [SerializeField]
    float Changey;
    [SerializeField]
    int MaxY;
    [SerializeField]
    GameObject PreFab;
    [SerializeField]
    GameObject[] SliderObjects;
    [SerializeField]
    GameObject GameObjectAttach;
    [SerializeField]
    float Speed;
    [SerializeField]
    GameObject GunSelectBut;
    // Start is called before the first frame update
    void Start()
    {
        
        SliderObjects = new GameObject[MaxY];
        CurrentY = PlayerPrefs.GetInt("IndexGunModRemember", 0);
        CharacterRememberance CharacterRemembered = GameObject.FindGameObjectWithTag("CharacterRemeberance").GetComponent<CharacterRememberance>();
        for (int i = 0; i < MaxY; i++)
        {
            SliderObjects[i] = Instantiate(PreFab, new Vector3(0, Changey * i, 0), Quaternion.identity.normalized);
            SliderObjects[i].transform.parent = GameObjectAttach.transform;
            SliderObjects[i].GetComponent<TextMeshPro>().text = CharacterRemembered.GunName[i];
        }
        GameObjectAttach.transform.position = new Vector3(Startx, Starty + CurrentY * Changey, 0);
    }

    // Update is called once per frame
    void Update()
    {
        GunSelectBut.GetComponent<GunModSelect>().CurrentIndex = CurrentY;
        float DestinationYPosition = -Changey * CurrentY + Starty;
        if (Mathf.Abs(DestinationYPosition - GameObjectAttach.transform.position.y) < 0.15)
        {
            if (Input.GetAxisRaw("Vertical") > 0 && CurrentY + 1 < SliderObjects.Length)
            {
                CurrentY++;
            }
            if (Input.GetAxisRaw("Vertical") < 0 && CurrentY > 0)
            {
                CurrentY--;
            }
        }
        else
        {
            if (Mathf.Abs(DestinationYPosition - GameObjectAttach.transform.position.y) < 0.15)
            {
                DestinationYPosition = GameObjectAttach.transform.position.y;
            }
            if (DestinationYPosition < GameObjectAttach.transform.position.y)
            {
                GameObjectAttach.transform.position = new Vector2(GameObjectAttach.transform.position.x, GameObjectAttach.transform.position.y - (Time.deltaTime * Speed));
            }
            else
            {
                GameObjectAttach.transform.position = new Vector2(GameObjectAttach.transform.position.x, GameObjectAttach.transform.position.y + (Time.deltaTime * Speed));
            }
        }
    }
}
