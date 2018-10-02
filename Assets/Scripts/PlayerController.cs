
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float multiplier;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        Player p = GetComponent<Player>();

        if (Input.GetButton("Jump"))
        {
            if (p.onGround)
                p.Velocity += p.Jump;
        }

        p.Velocity += new Vector3(h*multiplier, 0f, 0f);
	}
}
