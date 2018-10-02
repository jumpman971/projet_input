using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundCollide : MonoBehaviour {
    private void Update()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        Vector3[] dir = { Vector3.forward, Vector3.back, Vector3.down, Vector3.up };

        Player p = GetComponent<Player>();

        for (int i = 0; i < dir.Length; ++i)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(dir[i]), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(dir[i]) * hit.distance, Color.yellow);

                Vector3 matrix = new Vector3(p.Velocity.x*dir[i].z, p.Velocity.y * dir[i].y, 0);

                if (dir[i].y != 0)
                {
                    if (hit.distance <= matrix.y)
                    {
                        p.Velocity.y = 0;
                        if (i == 2)
                        {
                            p.onGround = true;
                        }
                    } else if (i == 2)
                    {
                        p.onGround = false;
                    }
                }
                if (dir[i].z != 0)
                { 
                    if (hit.distance <= matrix.x)
                    {
                        p.Velocity.x = 0;
                        p.Velocity.z = 0;
                    }
                }
            } else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(dir[i]) * 1000, Color.white);
            }
        }
    }
}
