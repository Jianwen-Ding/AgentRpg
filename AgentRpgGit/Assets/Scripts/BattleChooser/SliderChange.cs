using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SliderChange : MonoBehaviour
{
    [SerializeField]
    float DestinationYPosition;
    [SerializeField]
    GameObject SliderObject;
    public int CurrentSlide;
    [SerializeField]
    float ChangeSpeed;
    [SerializeField]
    float XStart;
    [SerializeField]
    float YStart;
    [SerializeField]
    float YChange;
    [SerializeField]
    GameObject SlidePrefab;
    [SerializeField]
    string[] SliderText;
    [SerializeField]
    int[] SliderDifficultyRating;
    [SerializeField]
    bool[] SliderIsBoss;
    [SerializeField]
    GameObject[] SliderObjects;
    [SerializeField]
    bool WipeAllPlayerPrefs = false;
    // Start is called before the first frame update
    void Start()
    {
        print("Start: " + PlayerPrefs.GetInt("HasStartedBefore", -69) + "");
        if (PlayerPrefs.GetInt("HasStartedBefore", -69) == -69)
        {
            //If it has not started before
            PlayerPrefs.SetInt("HasStartedBefore", 1);
            for(int i = 0; i < SliderText.Length; i++)
            {
                //0 = HasNotFinished
                //1 = HasFinished
                PlayerPrefs.SetInt("HasFinished" + i, 0);
            }
        }
        
        SliderObjects = new GameObject[SliderText.Length];
        for (int i = 0; i < SliderText.Length; i++)
        {
            SliderObjects[i] = Instantiate(SlidePrefab, new Vector2(0, YChange * i), Quaternion.identity.normalized);
            SliderObjects[i].GetComponent<SliderModify>().HasCompletedLevel = (PlayerPrefs.GetInt("HasFinished" + i, 0) == 1);
            print(i + ": " + PlayerPrefs.GetInt("HasFinished" + i, 0) + "");
            SliderObjects[i].GetComponent<SliderModify>().IsSpecialBoss = SliderIsBoss[i];
            SliderObjects[i].GetComponent<SliderModify>().Difficulty = SliderDifficultyRating[i];
            SliderObjects[i].GetComponent<SliderModify>().Text = SliderText[i];
            SliderObjects[i].transform.SetParent(SliderObject.transform);
        }
        SliderObject.transform.position = new Vector3(XStart, YStart);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("CurrentWatching", CurrentSlide);
        if (WipeAllPlayerPrefs == true)
        {
            PlayerPrefs.DeleteAll();
        }
        DestinationYPosition = -YChange * CurrentSlide + YStart;
        if (Mathf.Abs(DestinationYPosition - SliderObject.transform.position.y) < 0.15)
        {
            SliderObjects[CurrentSlide].GetComponent<SliderModify>().IsSelected = true;
            if (Input.GetAxisRaw("Vertical") < 0 && CurrentSlide + 1 < SliderObjects.Length)
            {
                SliderObjects[CurrentSlide].GetComponent<SliderModify>().IsSelected = false;
                CurrentSlide++;
            }
            if (Input.GetAxisRaw("Vertical") > 0 && CurrentSlide > 0)
            {
                SliderObjects[CurrentSlide].GetComponent<SliderModify>().IsSelected = false;
                CurrentSlide--;
            }
        }
        else
        {
            if (Mathf.Abs(DestinationYPosition - SliderObject.transform.position.y) < 0.15)
            {
                DestinationYPosition = SliderObject.transform.position.y;
            }
            if (DestinationYPosition < SliderObject.transform.position.y)
            {
                SliderObject.transform.position = new Vector2(SliderObject.transform.position.x, SliderObject.transform.position.y - (Time.deltaTime * ChangeSpeed));
            }
            else
            {
                SliderObject.transform.position = new Vector2(SliderObject.transform.position.x, SliderObject.transform.position.y + (Time.deltaTime * ChangeSpeed));
            }
        }
       
    }
}
