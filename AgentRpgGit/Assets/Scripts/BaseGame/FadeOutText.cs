using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FadeOutText : MonoBehaviour
{
    [SerializeField]
    Vector2 MovementSpeed;
    [SerializeField]
    float Slowdown;
    [SerializeField]
    string Text;
    [SerializeField]
    Color TextColor;
    [SerializeField]
    float TimeTillDissapear;
    [SerializeField]
    float TimeLeftTillDissapear;
    [SerializeField]
    bool Initiate;
    // Start is called before the first frame update
    void Start()
    {
        TimeLeftTillDissapear = 0;
    }
    public void BeginInitiate(float TimeActive, string TextPush, Color PushColor, Vector2 PushDirection)
    {
        MovementSpeed = PushDirection;
        TimeTillDissapear = TimeActive;
        Text = TextPush;
        TextColor = PushColor;
        Initiate = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(Initiate == true)
        {
            MovementSpeed.x -= (Slowdown * MovementSpeed.x) * Time.deltaTime;
            MovementSpeed.y -= (Slowdown * MovementSpeed.y) * Time.deltaTime;
            gameObject.transform.position = new Vector3(MovementSpeed.x * Time.deltaTime + gameObject.transform.position.x, MovementSpeed.y * Time.deltaTime + gameObject.transform.position.y, 0);
            gameObject.GetComponent<TextMeshPro>().text = Text;
            gameObject.GetComponent<TextMeshPro>().color = TextColor;
            TimeLeftTillDissapear += Time.deltaTime;
            if (TimeLeftTillDissapear > TimeTillDissapear)
            {
                Destroy(gameObject);
            }
        }
    }
}
