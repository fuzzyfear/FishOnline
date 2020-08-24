using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private GameManager gm;
    private PassingCheckpoints pc;
    public float speed;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Vector2 mousePos;

    private float halfSpeed;
    private float halfHalfSpeed;
    private float halfHalfPcSpeed;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        pc = GameObject.Find("PassingCheckpoints").GetComponent<PassingCheckpoints>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        direction = Vector2.zero;

        halfSpeed = speed / 2;
        halfHalfSpeed = halfSpeed / 2;
        halfHalfPcSpeed = pc.speed;
    }

    private void Update()
    {
        float moveVertical = Input.GetAxis("Vertical");
        moveVertical = Mathf.Clamp(moveVertical, 0, 1);
        direction = new Vector2(0, moveVertical);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(direction * speed * Time.deltaTime);

        Vector2 mouseDir = mousePos - rb.position;
        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            gm.enemyList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            speed = halfSpeed + (halfSpeed - (gm.numberOfEnemies - (gm.currentNumberOfEnemies - 1))
                * (halfHalfSpeed/gm.numberOfEnemies));
            pc.speed += (halfHalfPcSpeed / gm.numberOfEnemies);
        }
    }
}
