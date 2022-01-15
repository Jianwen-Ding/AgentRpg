using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    [SerializeField]
    bool Fadeout;
    SpriteRenderer sprite;
    [SerializeField]
    float ChangeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Fadeout == false)
        {
            if (sprite.color.a + (Time.deltaTime * ChangeSpeed) < 1)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + (Time.deltaTime * ChangeSpeed));
            }
            else
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
            }
        }
        else
        {
            if (sprite.color.a + (Time.deltaTime * ChangeSpeed) > 0)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - (Time.deltaTime * ChangeSpeed));
            }
            else
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
            }
        }
        
    }
}
