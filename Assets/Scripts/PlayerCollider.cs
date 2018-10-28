using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {

    [HideInInspector]
    public int collideXDir { get; private set; }

    private Vector3 cubeOffset;

    private static int V_FORWARD = 0;
    private static int V_BACKWARD = 1;
    private static int V_DOWN = 2;
    private static int V_UP = 3;

    private void Start()
    {
        collideXDir = 0;
        cubeOffset = new Vector2(0.5f, 0.5f);
    }

    private void Update()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer

        //Vector3 cubeWidth = new Vector3(1,1,1);
        Vector3[] dir = { Vector3.forward, Vector3.back, Vector3.down, Vector3.up };

        Player p = GetComponent<Player>();

        for (int i = 0; i < dir.Length; ++i)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(dir[i]), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(dir[i]) * hit.distance, Color.yellow);

                Vector3 matrix = new Vector3(p.Velocity.x*dir[i].z, p.Velocity.y * dir[i].y, 0);
                matrix += cubeOffset;

                if (dir[i].y != 0)
                {
                    if (hit.distance <= matrix.y)
                    {
                        bool isCrossablePlate = hit.collider.tag.Equals("CrossablePlate");
                         if (!(isCrossablePlate && i == V_UP) && !(isCrossablePlate && i == V_DOWN && p.isTryingToGoDown)) {
                            if (hit.collider.tag.Equals("BouncingPlate") && i == V_DOWN)
                                p.onBouncingPlate = true;
                            else {
                                p.Velocity.y = 0;
                            }

                            if (i == V_DOWN) {
                                p.onGround = true;
                                if (hit.collider.tag.Equals("MovingPlate")) {
                                    p.onMovingPlate = true;
                                    p.currMovingPlate = hit.collider.gameObject;
                                }
                            }
                        }
                    } else if (i == V_DOWN)
                    {
                        p.onGround = false;
                    }
                }
                if (dir[i].z != 0)
                {
                    if (hit.distance <= matrix.x) {
                        p.Velocity.x = 0;
                        p.Velocity.z = 0;
                        if (i == V_FORWARD || i == V_BACKWARD) {
                            if (i == V_FORWARD) 
                                collideXDir = 1;
                            else
                                collideXDir = -1;
                            p.onWall = true;
                        }
                    } else if (i == V_FORWARD || (i == V_BACKWARD && collideXDir != 1)) {
                        p.onWall = false;
                        collideXDir = 0;
                    }
                }
            } else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(dir[i]) * 1000, Color.white);
            }
        }
    }
}
