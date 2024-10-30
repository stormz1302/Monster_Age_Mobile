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
    public Collider2D movementBounds;
    public Transform helicopterTransform; 
    public float maxDistanceFromHelicopter = 10f;
    public bool isDead = false;

    private void FixedUpdate()
    {
        if (!isDead)
        {
            direction = Vector2.up * variableJoystick.Vertical + Vector2.right * variableJoystick.Horizontal;


            rb.velocity = direction.normalized * speed;
            if (direction != Vector2.zero)
            {
                Vector3 localScale = transform.localScale;
                localScale.x = direction.x > 0 ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
                transform.localScale = localScale;
            }
            if (helicopterTransform != null)
            {
                LimitPlayerDistanceFromHelicopter();
            }
            ClampPlayerWithinBounds();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }


    }

    private void LimitPlayerDistanceFromHelicopter()
    {
        
        float distanceFromHelicopter = Vector2.Distance(transform.position, helicopterTransform.position);

        
        if (distanceFromHelicopter > maxDistanceFromHelicopter)
        {
            Vector2 directionToHelicopter = (helicopterTransform.position - transform.position).normalized;
            transform.position = (Vector2)helicopterTransform.position - directionToHelicopter * maxDistanceFromHelicopter;
        }
    }

    private void ClampPlayerWithinBounds()
    {
        
        Bounds bounds = movementBounds.bounds;

       
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(transform.position.x, bounds.min.x, bounds.max.x),
            Mathf.Clamp(transform.position.y, bounds.min.y, bounds.max.y),
            transform.position.z
        );

        
        transform.position = clampedPosition;
    }
}
