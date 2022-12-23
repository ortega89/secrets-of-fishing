using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopedMovement : MonoBehaviour
{
    public Vector2 delta;
    public float loopTime = 3;

    private Vector3 start;
    private Vector3 end;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        end = new Vector3(start.x + delta.x, start.y + delta.y, start.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time = (time + Time.fixedDeltaTime) % loopTime;
        transform.position = Vector3.Lerp(start, end, time / loopTime);
    }
}
