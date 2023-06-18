using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskEnd : MonoBehaviour
{
    public float scaleSpeed = 10f;
    private bool isScaling = false;
    private bool isInTrigger = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            isScaling = true;
            isInTrigger = false;
        }

        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.JoystickButton0))
        {
            isScaling = false;
            transform.localScale = new Vector3(transform.localScale.x, 0f, transform.localScale.z);
        }

        if (isScaling)
        {
            if (!isInTrigger)
            {
                float newYScale = transform.localScale.y + scaleSpeed * Time.deltaTime;
                transform.localScale = new Vector3(transform.localScale.x, newYScale, transform.localScale.z);
            }
            else
            {
                float newYScale = transform.localScale.y - scaleSpeed * Time.deltaTime; // Decrease scale when in trigger
                newYScale = Mathf.Clamp(newYScale, 0f, transform.localScale.y); // Ensure the scale doesn't go below 0
                transform.localScale = new Vector3(transform.localScale.x, newYScale, transform.localScale.z);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mask"))
        {
            isInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Mask"))
        {
            isInTrigger = false;
        }
    }
}
