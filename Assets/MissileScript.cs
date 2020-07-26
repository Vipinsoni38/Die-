using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    public GameObject particles;
    Transform player;
    Vector2 vel;
    Rigidbody2D missile;
    CameraScript cameraScript;
    CanvasScript canvasScript;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        missile = GetComponent<Rigidbody2D>();
        cameraScript = FindObjectOfType<CameraScript>();
        canvasScript= FindObjectOfType<CanvasScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            return;
        }
        vel = (Vector2)(player.position - transform.position);
        vel = vel.normalized;
        if(vel.x < 0)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 57.2957f * Mathf.Atan(vel.y / vel.x)));
        } else
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0,180 + 57.2957f * Mathf.Atan(vel.y / vel.x)));
        }
        
        missile.velocity = vel * 4f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            cameraScript.CameraShake(0.3f, 0.15f);
            PlayerHit();
            canvasScript.AddScore();

            Destroy(this.gameObject);
        }
    }
    void PlayerHit()
    {
        GameObject g = Instantiate(particles);
        g.transform.position = transform.position;
        Destroy(this.gameObject);
    }
}
