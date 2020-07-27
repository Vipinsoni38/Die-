using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    int score = 0;
    public TMPro.TextMeshProUGUI text;
    public GameObject healthBar;

    public void AddScore()
    {
        score++;
        text.SetText("" + score);
    }

}
