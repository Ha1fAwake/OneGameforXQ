using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoFaceMovement : MonoBehaviour
{
    private GameObject player;
    public float moveForce = 2f;

    private Rigidbody2D rgb;
    private Vector2 targetPos = new Vector2();
    private Vector3 startPos;

    private void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    private void Update()
    {
        if (!GameController.start)
        {
            rgb.bodyType = RigidbodyType2D.Static;
            return;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        targetPos = player.transform.position;
        rgb.bodyType = RigidbodyType2D.Dynamic;
        rgb.AddForce((targetPos - (Vector2)transform.position).normalized * moveForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            transform.position = startPos;
    }
}
