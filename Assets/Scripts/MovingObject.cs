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

    private Vector3 startingPos;
    private Vector3 destPos;
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
        //if (isMoving && LastLocation == transform.position) {
        if (isMoving && Vector2.Distance(startingPos + destPos, transform.position) <= 0.5f) {
            startTime = Time.time;
            isMoving = false;
        }

        if (!isMoving && startTime + timeBeforeNextMove < Time.time) {
            startingPos = transform.position;
            if (step == 0) {
                destPos = firstMovingBound;
                ++step;
            } else {
                destPos = secondMovingBound;
                step = 0;
            }
            isMoving = true;
        }

        if (isMoving) {
            if (Velocity.x < speed) {
                if (destPos.x > 0)
                    Velocity.x += 0.1f;
                else
                    Velocity.x -= 0.1f;
            }
        }

        transform.position = transform.position + (Velocity * speed);

        LastLocation = transform.position;

        Velocity /= 1.2f;
        //Velocity /= 5f;
        //Velocity += Gravity;
    }
}
