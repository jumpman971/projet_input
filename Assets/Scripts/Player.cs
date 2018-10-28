using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed;
    public float backBoost;
    public float dashBoost;
    public float slidingValue;

    [SerializeField]
    public Vector3 Velocity;

    public Vector3 LastLocation;

    [SerializeField]
    public Vector3 Gravity;

    [SerializeField]
    public Vector3 Jump;

    [SerializeField]
    public Vector3 wallJump;

    [SerializeField]
    public Vector3 bouncingJump;

    public int jumpCount;

    public bool onGround = false;
    public bool onWall = false;
    public bool onBouncingPlate = false;
    public bool isWallJumping = false;
    public bool hasDashed = false;
    public bool isSliding = false;
    public bool isTryingToGoDown = false;
    public bool hasBackBoost = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position + Velocity;

        LastLocation = transform.position;

        Velocity /= 1.2f;
        Velocity += Gravity;
    }
}
