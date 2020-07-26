using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolderScript : MonoBehaviour
{
    Transform player;
    Vector3 defaultPos;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerScript>().gameObject.transform;
        defaultPos = transform.position;
    }

    // Update is called once per frame
    /*void Update()
    {
        defaultPos.x = player.position.x;
        transform.position = defaultPos;
    }*/
}
