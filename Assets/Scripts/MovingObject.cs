using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {
    [HideInInspector]
    public Vector3 Velocity;
    [HideInInspector]
    public Vector3 LastLocation;

    public Vector3 firstMovingBound;
    public Vector3 secondMovingBound;
    public float speed;

    public float timeBeforeNextMove;
    private float startTime;
    private int step;
    private bool isMoving = false;

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
        step = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && LastLocation == transform.position) {
            startTime = Time.time;
            isMoving = false;
        }

        if (!isMoving && startTime + timeBeforeNextMove < Time.time) {
            if (step == 0) {
                Velocity = firstMovingBound;
                ++step;
            } else {
                Velocity = secondMovingBound;
                step = 0;
            }
            isMoving = true;
        }

        transform.position = transform.position + Velocity;

        LastLocation = transform.position;

        Velocity /= 1.2f;
        //Velocity /= 5f;
        //Velocity += Gravity;
    }
}
