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
    [HideInInspector]
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

    [HideInInspector]
    public bool onGround = false;
    [HideInInspector]
    public bool onWall = false;
    [HideInInspector]
    public bool onBouncingPlate = false;
    [HideInInspector]
    public bool isWallJumping = false;
    [HideInInspector]
    public bool hasDashed = false;
    [HideInInspector]
    public bool isSliding = false;
    [HideInInspector]
    public bool isTryingToGoDown = false;
    [HideInInspector]
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
