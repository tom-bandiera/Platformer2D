using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera cam;
    public Transform subject;

    Vector2 startPosition;
    float startZ;

    float distanceFromSubject  => transform.position.z - subject.position.z; 
    float clippingPlane => (cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));

    float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;
    Vector2 travel => (Vector2)cam.transform.position - startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startZ = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = startPosition + travel;
        Vector2 newPosition = startPosition + travel * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startZ);
    }
}
