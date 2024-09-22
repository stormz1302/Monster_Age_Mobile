using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed; 
    public VariableJoystick variableJoystick; 
    public Rigidbody2D rb;
    public Vector2 direction;
    
    private void FixedUpdate()
    {
        
        direction = Vector2.up * variableJoystick.Vertical + Vector2.right * variableJoystick.Horizontal;

        
        rb.velocity = direction.normalized * speed;
        if (direction != Vector2.zero)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = direction.x > 0 ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
    }
}
