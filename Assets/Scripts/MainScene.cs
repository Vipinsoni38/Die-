using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public GameObject enemy, particles, GameoverPannel, PauseMenu, BaseGround;
    GameObject player, temp;
    float time = 0;    
    bool Paused = false, gameOver = false;
    int x = 0;
    // Start is called before the first frame update
    void Start()
    {        
        Physics2D.gravity = new Vector2(0, -40);
        Application.targetFrameRate = 60;
        player = GameObject.FindGameObjectWithTag("Player");
        GameoverPannel.SetActive(false);
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            if (Paused)
            {
                Resume();
                Paused = false;
            }
            else
            {
                Pause();
                Paused = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(gameOver) Restart();
        }


        float targetaspect = 16.0f / 9.0f;
        float windowaspect = (float)Screen.width / (float)Screen.height;
        float scaleheight = windowaspect / targetaspect;
        Camera camera = FindObjectOfType<Camera>();
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }

        if (!player) return;
        if (player.transform.position.y > 6 * x)
        {
            x++;
            temp = Instantiate(BaseGround);
            temp.transform.position = new Vector2(Random.Range(-8,8), 6 * x);
        }
    }
    void RandomInstantiate()
    {
        GameObject g = Instantiate(enemy);
        g.transform.position = new Vector3(12, Random.Range(-1.5f, 5.35f));
    }

    public void GameOver()
    {
        GameObject g2 = Instantiate(particles);
        var main = g2.GetComponent<ParticleSystem>();
        main.startColor = Color.black;
        g2.transform.position = player.transform.position;
        Destroy(player.gameObject);
        GameoverPannel.SetActive(true);
        gameOver = true;
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }
}
