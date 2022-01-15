using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveDescriptor : MonoBehaviour
{
    public int MoveIndex;
    int LastIndex = -69;
    [SerializeField]
    GameObject TextDescriptor;
    [SerializeField]
    GameObject ImageDescriptor;
    [SerializeField]
    GameObject ButtonToReturn;
    [SerializeField]
    string[] TextSummaries;
    [SerializeField]
    Sprite[] ImageDiscription;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MoveIndex != LastIndex)
        {
            ButtonToReturn.GetComponent<SelectMove>().MoveIndex = MoveIndex;
            if(MoveIndex > TextSummaries.Length - 1)
            {
                TextDescriptor.GetComponent<TextMeshPro>().text = "Error- Text Summary not found";
            }
            else
            {
                TextDescriptor.GetComponent<TextMeshPro>().text = "" + TextSummaries[MoveIndex];
            }
            if (MoveIndex > ImageDiscription.Length - 1)
            {
                print("Error- Image Summary not found");
            }
            else
            {
                ImageDescriptor.GetComponent<SpriteRenderer>().sprite = ImageDiscription[MoveIndex];
            }
            
        }
       
        LastIndex = MoveIndex;
    }
}
