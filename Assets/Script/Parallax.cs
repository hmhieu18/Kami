using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
   private float lengthX, startposX, lengthY, startposY;
    public float parallaxFactorX;
    public float parallaxFactorY;
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        startposX = transform.position.x;
        startposY = transform.position.y;

        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
      float tempX     = cam.transform.position.x * (1 - parallaxFactorX);
     float distanceX = cam.transform.position.x * parallaxFactorX;

     float distanceY = cam.transform.position.y * parallaxFactorY;

     Vector3 newPosition = new Vector3(startposX + distanceX, startposY+distanceY, transform.position.z);

     transform.position = newPosition;

     if (tempX > startposX + (lengthX / 2))    startposX += lengthX;
     else if (tempX < startposX - (lengthX / 2)) startposX -= lengthX;
    }
}
