using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform bow;

    Rigidbody2D rb;

    Vector2 touchPosition;
    Vector2 direction;

    [SerializeField] int speed = 30;

    public Touch touch;
    public int phase = 0;

    public string handLeftORRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        TakingInput();
    }

    private void FixedUpdate()
    {
        if (PlayerInput.instance.stamina >= 5)
        {
            HandMovement();
        }
    }

    void TakingInput()
    {
        if(Input.touchCount > 0)
        {
            touch = PlayerInput.instance.touch;
            phase = PlayerInput.instance.phase;
            touchPosition = PlayerInput.instance.touchPosition;
        }
    }

    void HandMovement()
    {
        if(handLeftORRight == "Left")
        {
            if(phase == 1 || phase == 2)
            {
                rb.MovePosition(bow.position);
            }
        }
        else if(handLeftORRight == "Right")
        {
            if(phase == 1 || phase == 2)
            {
                Vector2 handPosition = transform.position;
                direction = touchPosition - handPosition;
                float rotationZ = Mathf.Atan2(-1 * direction.x, direction.y) * Mathf.Rad2Deg;

                rb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ, speed * Time.fixedDeltaTime));
            }  
        }
        
    }
}
