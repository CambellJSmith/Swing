using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target; // Reference to the target object

    void LateUpdate()
    {
        if (target != null)
        {
            // Copy the X and Y coordinates from the target object
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10f);

            // Update the camera position
            transform.position = targetPosition;
        }
    }
}
