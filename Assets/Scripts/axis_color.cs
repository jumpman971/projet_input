using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axis_color : MonoBehaviour {
    GameObject joystick;
    GameObject boutonA;
    GameObject boutonB;
    GameObject boutonX;
    GameObject boutonY;


	// Use this for initialization
	void Start () {
        joystick = GameObject.Find("Joystick");
        boutonA = GameObject.Find("Bouton A");
        boutonB = GameObject.Find("Bouton B");
        boutonX = GameObject.Find("Bouton X");
        boutonY = GameObject.Find("Bouton Y");
    }
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        joystick.transform.position = joystick.transform.position + new Vector3(h, v, 0.0f);

        if (Input.GetButton("Button A"))
            boutonA.GetComponent<Renderer>().material.color = Color.green;
        else if (Input.GetButton("Button B"))
            boutonB.GetComponent<Renderer>().material.color = Color.red;
        else if (Input.GetButton("Button X"))
            boutonX.GetComponent<Renderer>().material.color = Color.blue;
        else if (Input.GetButton("Button Y"))
            boutonY.GetComponent<Renderer>().material.color = Color.yellow;
        else
        {
            boutonA.GetComponent<Renderer>().material.color = Color.white;
            boutonB.GetComponent<Renderer>().material.color = Color.white;
            boutonX.GetComponent<Renderer>().material.color = Color.white;
            boutonY.GetComponent<Renderer>().material.color = Color.white;
        }
            
    }
}
