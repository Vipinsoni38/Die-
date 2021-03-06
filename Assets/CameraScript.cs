﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    
    public IEnumerator CamShake(float duration, float mag)
    {        
        float currentTime = 0;
        float x, y;
        while(currentTime < duration)
        {
            x = Random.Range(-1f, 1f)* mag;
            y = Random.Range(-1f, 1f)* mag;
            this.transform.localPosition = new Vector3(x, y);
            currentTime += Time.deltaTime;
            yield return null;
        }
        this.transform.localPosition = new Vector3(0, 0);
    }
    public void CameraShake(float duration, float mag)
    {
        StartCoroutine(CamShake(duration, mag));
    }
}
