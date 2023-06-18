using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebSwing : MonoBehaviour
{
    public string buildingTag = "Building";
    public float swingForce = 20f;
    public float rotationSpeed = 100f;
    public float maxSpeed = 9.9f;

    private GameObject targetBuilding;
    private bool isSwinging = false;
    private DistanceJoint2D distanceJoint;
    private Rigidbody2D rb;

    private void Start()
    {
        distanceJoint = GetComponent<DistanceJoint2D>();
        rb = GetComponent<Rigidbody2D>();

        // Break all existing distance joints
        BreakAllDistanceJoints();
    }

    private void Update()
    {
        if (transform.position.y <= -40)
        {
            return; // Disable finding buildings when player's Y position is -40 or less
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            // Find the nearest building
            GameObject[] buildings = GameObject.FindGameObjectsWithTag(buildingTag);
            float closestDistance = Mathf.Infinity;
            GameObject nearestBuilding = null;

            foreach (GameObject building in buildings)
            {
                float distance = Vector2.Distance(transform.position, building.transform.position);
                bool isValidBuilding = building.transform.position.x > transform.position.x && building.transform.position.y > transform.position.y;

                if (distance < closestDistance && isValidBuilding)
                {
                    closestDistance = distance;
                    nearestBuilding = building;
                }
            }

            if (nearestBuilding != null)
            {
                // Break all existing distance joints before creating a new one
                BreakAllDistanceJoints();

                // Point towards the target building
                isSwinging = true;
                targetBuilding = nearestBuilding;
                distanceJoint.connectedBody = targetBuilding.GetComponent<Rigidbody2D>();
                distanceJoint.connectedAnchor = Vector2.zero;
            }
        }

        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.JoystickButton0))
        {
            // Stop swinging and disconnect the current distance joint
            isSwinging = false;
            distanceJoint.connectedBody = null;

            rb.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (isSwinging && targetBuilding != null)
        {
            // Calculate direction towards the target building
            Vector2 direction = targetBuilding.transform.position - transform.position;
            direction.Normalize();

            // Apply force in the direction the player is facing
            rb.AddForce(direction * swingForce);

            // Limit the velocity magnitude to the maximum speed if it exceeds the limit
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }

            // Calculate the target rotation based on the direction
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void BreakAllDistanceJoints()
    {
        DistanceJoint2D[] allDistanceJoints = FindObjectsOfType<DistanceJoint2D>();
        foreach (DistanceJoint2D joint in allDistanceJoints)
        {
            // Disconnect the joint
            joint.connectedBody = null;

            // Destroy the joint component
            Destroy(joint);
        }
    }
}
