using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    int score = 0, health = 50;
    public TMPro.TextMeshProUGUI text;
    public GameObject healthBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddScore()
    {
        score++;
        text.SetText("" + score);
    }

    void UpdateScore()
    {
        healthBar.GetComponent<RectTransform>().right = new Vector3(0,0,0);
    }
}
