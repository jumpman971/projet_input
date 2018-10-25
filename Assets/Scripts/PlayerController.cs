
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

	// Use this for initialization
	void Start () {
		
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

        v = Input.GetAxis("Vertical") * ajustedSpeed;
        float h = Input.GetAxis("Horizontal") * ajustedSpeed;
        PlayerCollider c = GetComponent<PlayerCollider>();

        if (p.onGround) {
            nbJump = 0;
            nbWallJump = 0;
            if (p.isWallJumping)
                p.isWallJumping = false;
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
            } else if (nbJump < p.jumpCount)
            {
                if (nbJump > 0)
                    p.Velocity += p.Jump * jumpBoost;
                else
                    p.Velocity += p.Jump;
                nbJump++;
            }
        }

        if (p.isWallJumping)
            h = -h;

        if (!(v < 0))
            v = 0;

        p.Velocity += new Vector3(h*multiplier, 0, 0f);
	}
}
