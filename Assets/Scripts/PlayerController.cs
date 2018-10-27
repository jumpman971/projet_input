
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float v;

    [SerializeField]
    private float multiplier;

    private int nbJump;
    private float jumpBoost = 1.2f;

    private int nbWallJump = 0;
    private float wallJumpStartTime = 0f;

    private float lastHValue;

    public bool inverseAxis = false;
    public bool stopInverseAxis = false;
    private bool stickDownLast;

    public float startDashTime;

    // Use this for initialization
    void Start () {
        lastHValue = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Player p = GetComponent<Player>();
        /*float h = 0;
        if (p.isWallJumping && (wallJumpStartTime + 1 < Time.time || p.onGround || (p.onWall && !p.onGround))) {
            p.isWallJumping = false;
        }

        if (!p.isWallJumping)
            h = Input.GetAxis("Horizontal") * (p.speed/4.0f);*/
        float ajustedSpeed = p.speed / 4.0f;

        float h = Input.GetAxis("Horizontal") * ajustedSpeed;

        v = Input.GetAxis("Vertical") * ajustedSpeed;
        //float trueH = Input.GetAxis("Horizontal");

        PlayerCollider c = GetComponent<PlayerCollider>();

        if (p.onGround) {
            nbJump = 0;
            nbWallJump = 0;
            if (p.isWallJumping) {
                p.isWallJumping = false;
                //inverseAxis = false;
                //stopInverseAxis = false;
            }
            p.hasDashed = false;
        }

        /*if (GetAxisDown("Horizontal") && p.isWallJumping && inverseAxis) {
            inverseAxis = false;
            Debug.Log("1");
        }*/

        /*if (trueH > 0 && lastHValue < 0)
            p.Velocity.x += p.backBoost;
        else if (trueH < 0 && lastHValue > 0)
            p.Velocity.x -= p.backBoost;*/

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
                    //Debug.Log("wall jump forward");
                    tmp.x = -tmp.x;
                }
                p.Velocity += tmp;
                p.isWallJumping = true;
                ++nbWallJump;
                p.onWall = false;
                //stopInverseAxis = false;
            } else if (nbJump < p.jumpCount)
            {
                if (nbJump > 0)
                    p.Velocity += p.Jump * jumpBoost;
                else
                    p.Velocity += p.Jump;
                nbJump++;
            }
        }

        /*if (h != 0) {
            if (!stickDownLast) {
                stopInverseAxis = true;
                Debug.Log("1");
            }

            stickDownLast = true;
        } else
            stickDownLast = false;*/

        if (p.onWall && !p.onGround) {

        }

        if (p.onBouncingPlate) {
            p.Velocity += p.bouncingJump;
            p.onBouncingPlate = false;
        }

        if (p.isWallJumping) {
            h = -h;
        }

        if (!(v < 0))
            v = 0;

        p.Velocity += new Vector3(h*multiplier, v, 0f);

        //lastHValue = trueH;
	}
}
