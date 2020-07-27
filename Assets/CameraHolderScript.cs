using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolderScript : MonoBehaviour
{
    Transform player;
    Vector3 defaultPos;
    float currentMax = 3, maxHeight = 200;    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerScript>().gameObject.transform;
        defaultPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!player)
        {
            return;
        }
        if(player.position.y > currentMax && player.position.y<maxHeight)
        {
            defaultPos.y = player.position.y - 3;
            transform.position = defaultPos;
            currentMax = player.position.y;
        }

    }
    public void calculateMaxHeight()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Win");
        maxHeight = g.transform.position.y-2;
    }
}
