using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public GameObject enemy, particles, GameoverPannel, PauseMenu, BaseGround, BlackHole, WonMenu;
    public GameObject MainCloud, BadGround, Missile, DoubleJump, Shield, JumpCloud, DynamicBase, PauseButton;
    SpriteRenderer playerSprite;
    public Transform water;
    GameObject player, temp;
    Color playerSpriteColor;
    float time = 0;
    public AudioSource Audio;
    public static AudioClip die, powerup, jump, win;
    bool Paused = false, gameOver = false, isWon = false, StoryMode = true;
    int x = -2;
    Vector3 scaleWater;

    // Start is called before the first frame update
    void Start()
    {
        die = Resources.Load<AudioClip>("music/die");
        powerup = Resources.Load<AudioClip>("music/power_up");
        jump = Resources.Load<AudioClip>("music/jump");
        win = Resources.Load<AudioClip>("music/win");
        //PlayerPrefs.SetInt("justCurrentLevel", -1);
        PauseButton.SetActive(false);
        Physics2D.gravity = new Vector2(0, 0);
        Application.targetFrameRate = 60;
        player = GameObject.FindGameObjectWithTag("Player");
        GameoverPannel.SetActive(false);
        PauseMenu.SetActive(false);
        WonMenu.SetActive(false);
        Time.timeScale = 1;
        playerSprite = player.GetComponent<SpriteRenderer>();
        playerSpriteColor = playerSprite.color;
        playerSpriteColor.a = 0;
        playerSprite.color = playerSpriteColor;
        scaleWater = water.transform.localScale;
        //Audio = FindObjectOfType<AudioSource>();
    }

    private void Update()
    {
        CorrectRatioForGame();
        if (StoryMode)
        {
            Story();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver && !isWon)
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
            if (gameOver) Restart();
        }
    }

    void CorrectRatioForGame()
    {
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
        SceneManager.LoadScene(1);
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
    void Story()
    {
        scaleWater.y -= 1f;
        playerSpriteColor.a += 0.005f;
        if (scaleWater.y <= 0 || Input.GetKeyDown(KeyCode.Space))
        {
            StoryMode = false;
            water.gameObject.SetActive(false);
            Time.timeScale = 1;
            playerSpriteColor.a = 1;
            playerSprite.color = playerSpriteColor;
            player.GetComponent<PlayerScript>().MakePlayerActive();
            Physics2D.gravity = new Vector2(0, -40);
            LoadLevel(1);
            return;
        }
        playerSprite.color = playerSpriteColor;
        water.localScale = scaleWater;
    }
    void LoadLevel(int level)
    {
        PauseButton.SetActive(true);
        TextAsset theList = Resources.Load("levels/level" + level) as TextAsset;
        print("levels/level" + level);
        string[] lines = theList.text.Split('\n');
        string[] words;
        GameObject g;
        foreach (string line in lines)
        {
            words = line.Split(' ');
            g = InstantiateGO(words[0]);
            g.transform.position = new Vector2(float.Parse(words[1]), float.Parse(words[2]));
            if (words.Length < 4)
            {
                continue;
            }
            g.transform.rotation = Quaternion.Euler(new Vector3(0, 0, float.Parse(words[3])) );
        }
        FindObjectOfType<CameraHolderScript>().calculateMaxHeight();
    }
    GameObject InstantiateGO(string v)
    {
        switch (v)
        {
            case "base": return Instantiate(BaseGround); 
            case "BlackHole": return Instantiate(BlackHole);
            case "MainCloud": return Instantiate(MainCloud);
            case "BadGround": return Instantiate(BadGround);
            case "Missile": return Instantiate(Missile);
            case "DoubleJump": return Instantiate(DoubleJump);
            case "Shield": return Instantiate(Shield); 
            case "JumpCloud": return Instantiate(JumpCloud);
            case "DynamicBase": return Instantiate(DynamicBase);
                
        }
        return null;
    }
    public void Won()
    {
        isWon = true;
        Destroy(player);
        WonMenu.SetActive(true);
    }
    public void nextLevel()
    {

    }


    public void Menu()
    {
        SceneManager.LoadScene(0);     
    }
    public void playSound(string s)
    {
        switch (s)
        {
            case "die": Audio.PlayOneShot(die, 1); break;
            case "jump": Audio.PlayOneShot(jump, 1); break;
            case "powerup": Audio.PlayOneShot(powerup, 1); break;
            case "win": Audio.PlayOneShot(win, 1); break;
            default: break;
        }
    }
}