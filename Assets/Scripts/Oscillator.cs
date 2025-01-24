using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    public float speed = 10f;

    public float distance = 40f;

    public Vector3 movementAxis = Vector3.up;

    private Vector3 startingPosition;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startingPosition + movementAxis.normalized * newPosition;
    }
}
