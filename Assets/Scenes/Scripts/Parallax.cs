using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length; 
    private float startPos;
    private GameObject Cam;
    [SerializeField] private float parallaxEffect;
    //serializedfield is edited in unity game engine itself; naa sa inspector

    void Start()
    {
        Cam = GameObject.Find("Cinemachine Camera");
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x; //length of the bg sprite
    }

    void Update()
    {
        float temp = (Cam.transform.position.x * (1 - parallaxEffect) ); //current position sa moving bg
        float distance = (Cam.transform.position.x * parallaxEffect); //the speed on moving of bg
        transform.position = new Vector3(startPos + distance, transform.position.y , transform.position.z); //moving of the bg itself

        if(temp > startPos + length) //if ang bg kay padung na sa end (to the right)
        {
            startPos += length;
        }
        else if (temp < startPos - length) //to the left
        {
            startPos -= length;
        }

    }


}
