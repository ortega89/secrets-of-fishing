using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    private Vector2 speed = Vector2.zero;

    public float acceleration = 1;
    public float maxSpeed = 5;
    public float inertia = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");
        Debug.Log("dx = " + dx + ", dy = " + dy);
        speed *= Mathf.Pow(inertia, Time.fixedDeltaTime);
        speed += Vector2.ClampMagnitude(new Vector2(dx, dy), acceleration * Time.fixedDeltaTime);
        speed = Vector2.ClampMagnitude(speed, maxSpeed);
        transform.Translate(speed * Time.fixedDeltaTime);
    }
}
