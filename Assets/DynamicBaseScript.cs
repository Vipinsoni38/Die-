using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBaseScript : MonoBehaviour
{
    bool colorNow = false;
    Color current;
    SpriteRenderer sprite;
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        sprite = transform.parent.gameObject.GetComponent<SpriteRenderer>();
        current = sprite.color;
        Invoke("StartColorChange", 1.5f);
        Player = GameObject.FindGameObjectWithTag("Player");
    }   

    void StartColorChange()
    {
        colorNow = !colorNow;
        if (colorNow)
        {
            sprite.color = Color.black;
        } else
        {
            sprite.color = current;
        }
        Invoke("StartColorChange", 1.5f);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player") && colorNow)
        {
            Player.GetComponent<PlayerScript>().GameOver();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && colorNow)
        {
            Player.GetComponent<PlayerScript>().GameOver();
        }
    }
}
