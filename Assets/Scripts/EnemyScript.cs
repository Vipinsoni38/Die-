using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    float vel = 10f;
    Transform EnemyTransform;
    public GameObject particles;
    CameraScript cameraScript;
    CanvasScript canvasScript;

    // Start is called before the first frame update
    void Start()
    {
        EnemyTransform = this.transform;
        cameraScript = FindObjectOfType<CameraScript>();
        canvasScript = FindObjectOfType<CanvasScript>();
        StartCoroutine(cool());
    }

    // Update is called once per frame
    void Update()
    {
        //EnemyTransform.position = (Vector2)EnemyTransform.position - new Vector2(vel, 0);
        if(EnemyTransform.position.x < -10)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")){
            cameraScript.CameraShake(0.3f, 0.15f);
            PlayerHit();
            canvasScript.AddScore();
        }
    }


    void PlayerHit()
    {
        GameObject g = Instantiate(particles);
        g.transform.position = EnemyTransform.position;
        Destroy(this.gameObject);
    }
    IEnumerator cool()
    {
        yield return new WaitForSeconds(1);
        if (this.transform.position.x > 0)
        {
            vel *= -1;
        }
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(vel ,0);
    }
}
