using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public float speed;
    public float radius;
    public float swapDirTime;
    private Vector2 direction;
    private Rigidbody2D rb;
    private Vector2 angle;
    private Vector2 prevLocation;
    private Vector2 newAngle;
    private Vector2 circlePos;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        prevLocation = transform.position;
        StartCoroutine((NewDir()));
    }

    IEnumerator NewDir()
    {
        yield return new WaitForSeconds(swapDirTime);
        NewDirection();
        StartCoroutine((NewDir()));
    }

    private void FixedUpdate()
    {
        angle = ((Vector2)transform.position - prevLocation).normalized;
        circlePos = angle + (Vector2)transform.position;
        //Debug.DrawLine(transform.position, newAngle, Color.red);
        rb.AddForce(direction * speed * Time.deltaTime);
        prevLocation = transform.position;
    }

    void NewDirection()
    {
        newAngle = Random.insideUnitCircle.normalized * radius + circlePos;
        direction = newAngle.normalized;
    }
}
