using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackGround : MonoBehaviour
{

    private GameObject cam;
    [SerializeField] private float parallaxEffect;
    private float xPosition;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");

        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {

        float distanceToMove = cam.transform.position.x * parallaxEffect; 

        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);
        


    }
}
