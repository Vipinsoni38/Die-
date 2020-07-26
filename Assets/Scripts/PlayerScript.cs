using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D player;
    Vector2 CurrPos;
    MainScene mainScene;
    float vel = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        mainScene = FindObjectOfType<MainScene>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            CurrPos = (Vector2)transform.position;
            Right();
        } else if (Input.GetKey(KeyCode.LeftArrow))
        {
            CurrPos = (Vector2)transform.position;
            Left();
        }
    }
    void jump()
    {
        player.velocity = (new Vector2(0, 14));

    }
    void Right()
    {
        CurrPos.x += vel;
        //player.transform.position = CurrPos;
        player.velocity = (new Vector2(vel, player.velocity.y));
    }
    void Left()
    {
        CurrPos.x -= vel;
        //player.transform.position = CurrPos;
        player.velocity = (new Vector2(-1 * vel, player.velocity.y));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("death"))
        {
            GameOver();
        }
    }
    void GameOver()
    {
        mainScene.GameOver();
    }
}
