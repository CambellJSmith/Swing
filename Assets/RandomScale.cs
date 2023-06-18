using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScale : MonoBehaviour
{
    public float minScale = 1f;
    public float maxScale = 5f;

    void Start()
    {
        // Generate a random scale value between minScale and maxScale
        float randomScale = Random.Range(minScale, maxScale);

        // Apply the random scale to the x-axis
        transform.localScale = new Vector3(randomScale, transform.localScale.y, transform.localScale.z);
    }
}
