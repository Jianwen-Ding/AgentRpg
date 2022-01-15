using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SliderModify : MonoBehaviour
{
    Color OriginalColor;
    public bool HasCompletedLevel;
    public bool IsSpecialBoss;
    public int Difficulty;
    public string Text;
    [SerializeField]
    public GameObject TextObject;
    [SerializeField]
    public GameObject HasCompletedBar;
    [SerializeField]
    public GameObject IsSpecialBossBar;
    [SerializeField]
    public GameObject DifficultyBar1;
    [SerializeField]
    public GameObject DifficultyBar2;
    [SerializeField]
    public GameObject DifficultyBar3;
    [SerializeField]
    public GameObject DifficultyBar4;
    [SerializeField]
    public GameObject DifficultyBar5;
    [SerializeField]
    public bool IsSelected;
    // Start is called before the first frame update
    void Start()
    {
        TextObject.GetComponent<TextMeshPro>().text = Text;
        OriginalColor = gameObject.GetComponent<SpriteRenderer>().color;
        if (HasCompletedLevel == false)
        {
            HasCompletedBar.GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (IsSpecialBoss == false)
        {
            IsSpecialBossBar.GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (Difficulty < 1)
        {
            DifficultyBar1.GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (Difficulty < 2)
        {
            DifficultyBar2.GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (Difficulty < 3)
        {
            DifficultyBar3.GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (Difficulty < 4)
        {
            DifficultyBar4.GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (Difficulty < 5)
        {
            DifficultyBar5.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    void Update()
    {
        if(IsSelected == true)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = OriginalColor;
        }
    }
}
