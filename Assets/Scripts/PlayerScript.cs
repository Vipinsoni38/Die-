using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D player;
    Vector2 CurrPos;
    MainScene mainScene;
    bool inJump = false, playerActive = false, canJump = true;
    int MaxJump = 1, currJump = 0;
    float vel = 7;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        mainScene = FindObjectOfType<MainScene>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerActive)
        {
            return;
        }
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
        if (inJump)
        {
            if (player.velocity.y <= 0)
            {
                this.gameObject.layer = LayerMask.NameToLayer("Default");
                inJump = false;
            }
        }
    }
    void jump()
    {
        if(currJump >= MaxJump)
        {
            return;
        }
        this.gameObject.layer = LayerMask.NameToLayer("ExtraLayer");
        player.velocity = (new Vector2(0, 18));        
        inJump = true;
        currJump++;
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
        currJump = 0;
        if (collision.gameObject.tag.Equals("death"))
        {
            GameOver();
        }        
        if (collision.gameObject.tag.Equals("Electric"))
        {
            GameOver();
        }
        if (collision.gameObject.tag.Equals("JumpCloud"))
        {
            player.AddForce(new Vector2(0, 1700));
            inJump = true;
            currJump = MaxJump;
            this.gameObject.layer = LayerMask.NameToLayer("ExtraLayer");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {       
        if (other.gameObject.tag.Equals("DoubleJump"))
        {
            if(MaxJump < 2)
            {
                MaxJump = 2;
            }
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag.Equals("Win"))
        {
            mainScene.Won();
        }        
    }
    void GameOver()
    {
        mainScene.GameOver();
    }
    public void MakePlayerActive()
    {
        playerActive = true;
    }
}
