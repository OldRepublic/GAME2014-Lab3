using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public float VerticalSpeed;//how fast we scroll
    public float VerticalBoundary;

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }

    private void _Move()
    {
        transform.position -= new Vector3(0.0f, VerticalSpeed);
    }
    private void _CheckBounds()
    {//if background is lower than the bottom of screen reset
        if (transform.position.y <= -VerticalBoundary)
        {
            _Reset();
        }
    }

    private void _Reset()
    {
        transform.position = new Vector3(0.0f, VerticalBoundary);
    }
}
