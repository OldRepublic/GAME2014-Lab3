using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigidBody;
    public float horizontalBoundary;
    public float horizontalSpeed;
    public float MaxSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }

    private void _Move()
    {
        float direction = 0.0f;
        //simple touch input
        //touch input support
        foreach (var touch in Input.touches)
        {
            var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);
            if (worldTouch.x > transform.position.x)
            {
                //direction is positive
                direction = 1.0f;
            }

            if (worldTouch.x < transform.position.x)
            {
                //direction is negative
                direction = -1.0f;
            }
        }

        //keyboard input 
        if(Input.GetAxis("Horizontal") >= 0.1f)
        {
            //direction is positive
            direction = 1.0f;
        }

        if (Input.GetAxis("Horizontal") <= -0.1f) 
        {
            //direction is negative
            direction = -1.0f;
        }
        Vector2 newVelocity = m_rigidBody.velocity + new Vector2(direction * horizontalSpeed, 0.0f);
        m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, MaxSpeed);//will never be greater than max speed
        m_rigidBody.velocity *= 0.99f;
    }
    private void _CheckBounds()
    {//check right and left bounds
        if(transform.position.x >= horizontalBoundary)//check right bounds
        {
            transform.position = new Vector3(horizontalBoundary, transform.position.y, 0.0f);
        }


        if (transform.position.x <= -horizontalBoundary)// check left bounds
        {
            transform.position = new Vector3(-horizontalBoundary, transform.position.y, 0.0f);
        }

    }
}
