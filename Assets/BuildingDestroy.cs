using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDestroy : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Get the left edge position of the camera in world coordinates
        float leftEdge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, mainCamera.nearClipPlane)).x;

        if (transform.position.x < leftEdge - 10f)
        {
            Destroy(gameObject);
        }
    }
}
