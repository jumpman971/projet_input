
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour { 
    private float multiplier = 0.2f;

    private int nbJump;
    private float jumpBoost = 1.2f;

    private int nbWallJump = 0;
    private float startedHForWallJump;

    private float rawH;
    private bool stickDownLast;
    private float startTimeHoldStick;
    private bool startHoldingStick = false;
    private float startTimeStopHoldStick;
    private bool stoppingHoldingStick = false;
    private float holdingH = 0;

    private float startDashTime;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Player p = GetComponent<Player>();
        PlayerCollider c = GetComponent<PlayerCollider>();

        float ajustedSpeed = p.speed / 4.0f;

        rawH = Input.GetAxisRaw("Horizontal");
        float h = Input.GetAxis("Horizontal") * ajustedSpeed;
        float v = Input.GetAxis("Vertical") * ajustedSpeed;

        if (p.onGround) {
            nbJump = 0;
            nbWallJump = 0;
            if (p.isWallJumping) {
                p.isWallJumping = false;
            }
            p.isSliding = false;
            p.hasDashed = false;
        } else if (!p.onWall)
            p.isSliding = false;

        if (Input.GetButtonDown("Dash") && !p.hasDashed && startDashTime + 1 < Time.time) {
            if (h > 0)
                p.Velocity.x += p.dashBoost;
            else
                p.Velocity.x -= p.dashBoost;
            startDashTime = Time.time;
            p.hasDashed = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (p.onWall && !p.onGround && nbWallJump < 2) //wall jump
            {
                Vector3 tmp = new Vector3(p.wallJump.x, p.wallJump.y);
                if (c.collideXDir == 1) {
                    tmp.x = -tmp.x;
                }
                p.Velocity += tmp;
                p.isWallJumping = true;
                ++nbWallJump;
                p.onWall = false;
                startedHForWallJump = rawH;
            } else if (nbJump < p.jumpCount)
            {
                if (nbJump > 0)
                    p.Velocity += p.Jump * jumpBoost;
                else
                    p.Velocity += p.Jump;
                nbJump++;
            }
        }

        if (p.onGround && stickDownLast && !p.hasBackBoost) {
            if (holdingH < 0 && rawH > 0) {
                p.Velocity.x += p.backBoost;
                p.hasBackBoost = true;
            } else if (holdingH > 0 && rawH < 0) {
                p.Velocity.x -= p.backBoost;
                p.hasBackBoost = true;
            }
        }

        if (rawH != 0) {
            if (!stickDownLast) {
                if (!startHoldingStick) {
                    startTimeHoldStick = Time.time;
                    startHoldingStick = true;
                    holdingH = rawH;
                } else if (holdingH == rawH && startTimeHoldStick + 0.1f < Time.time) {
                    stickDownLast = true;
                    startHoldingStick = false;
                }
            }
        } else if (stickDownLast) {
            if (!stoppingHoldingStick) {
                startTimeStopHoldStick = Time.time;
                stoppingHoldingStick = true;
            } else if (rawH == 0 && startTimeStopHoldStick + 0.1f < Time.time) {
                stickDownLast = false;
                startHoldingStick = false;
                stoppingHoldingStick = false;
                holdingH = 0;
                p.hasBackBoost = false;
            }
        }

        if (p.onWall && !p.onGround) {
            p.isSliding = true;
            p.Velocity.y += (-p.Gravity.y) + p.slidingValue;
        }

        if (p.onBouncingPlate) {
            p.Velocity += p.bouncingJump;
            p.onBouncingPlate = false;
        }

        if (p.isWallJumping && startedHForWallJump == rawH) {
            h = -h;
        } else {
            startedHForWallJump = 0;
        }

        if (v < 0)
            p.isTryingToGoDown = true;
        else
            p.isTryingToGoDown = false;

        p.Velocity += new Vector3(h*multiplier, 0f, 0f);
	}
}
