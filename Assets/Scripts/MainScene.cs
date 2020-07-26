using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public GameObject enemy, particles, GameoverPannel, PauseMenu;
    GameObject player;
    float time = 0;
    public GameObject Ground, Top, Lscreen, Rscreen;
    bool Paused = false, gameOver = false;
    // Start is called before the first frame update
    void Start()
    {        
        Physics2D.gravity = new Vector2(0, -40);
        Application.targetFrameRate = 60;
        player = GameObject.FindGameObjectWithTag("Player");
        /*Top.transform.position = new Vector2(0, Screen.height/2);
        Lscreen.transform.position = new Vector2(-1 * Screen.width / 2, Screen.height/2);
        Rscreen.transform.position = new Vector2(Screen.width/2, Screen.height/2);
        Ground.transform.position = new Vector2(0, -1 * Screen.height / 2);*/
        //print(Camera.main.WorldToScreenPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0)));
        print(Screen.width + ":" + Screen.height);
        GameoverPannel.SetActive(false);
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        /*if(time > 4)
        {
            RandomInstantiate();
            time = 0;
        }
        time += Time.deltaTime;
        */



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
        if (Input.GetKeyDown(KeyCode.Space) && gameOver)
        {
            Restart();
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
