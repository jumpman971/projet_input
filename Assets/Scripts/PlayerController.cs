
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float multiplier;

    private int nbJump;
    private float jumpBoost = 1.2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        Player p = GetComponent<Player>();
        BoundCollide c = GetComponent<BoundCollide>();

        if (p.onGround)
            nbJump = 0;

        if (Input.GetButtonDown("Jump"))
        {
            if (p.onWall && !p.onGround)
            {
                Vector3 tmp = new Vector3(p.wallJump.x, p.wallJump.y);
                if (c.collideInForward)
                    tmp = new Vector3(-p.wallJump.x, p.wallJump.y);
                p.Velocity += tmp;
            } else if (nbJump < p.jumpCount)
            {
                if (nbJump > 0)
                    p.Velocity += p.Jump * jumpBoost;
                else
                    p.Velocity += p.Jump;
                nbJump++;
            }
        }

        p.Velocity += new Vector3(h*multiplier, 0f, 0f);
	}
}
