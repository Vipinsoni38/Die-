using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDelete : MonoBehaviour
{
    float DelTime = 1.5f;
    void Start()
    {
        Destroy(this.gameObject, DelTime);
    }
    
    void getDelTime(float t)
    {
        DelTime = t;
    }
}
