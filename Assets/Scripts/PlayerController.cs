using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
    public BulletManger bulletManager;

    private Rigidbody2D m_rigidBody;
    private Vector3 touchesEnd;
    public float horizontalBoundary;
    public float horizontalSpeed;
    public float MaxSpeed;
    public float horizontalLerp;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 touchesEnd = new Vector3();
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
        _FireBullet();
    }

    private void _FireBullet()
    {
        if(Time.frameCount % 20 == 0)//delays bullet firing every 40 frames
        {
            bulletManager.GetBullet(transform.position);
        }      
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

            touchesEnd = worldTouch;
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
        
        if(touchesEnd.x != 0.0f)
        {

           transform.position = new Vector2(Mathf.Lerp(transform.position.x, touchesEnd.x, horizontalLerp), transform.position.y);// Vector2.Lerp(transform.position, touchesEnd, 0.1f);
        }
        else
        {
            Vector2 newVelocity = m_rigidBody.velocity + new Vector2(direction * horizontalSpeed, 0.0f);
            m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, MaxSpeed);//will never be greater than max speed
            m_rigidBody.velocity *= 0.99f;
        }
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
