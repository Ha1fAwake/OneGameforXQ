using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 祝薛芹生日快乐！—— 来自wxx的祝福
/// </summary>
public class NoFaceMovement : MonoBehaviour
{// 无脸男的追逐AI
    private GameObject player;
    public float speed = 2f;

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
        FaceDirection();
        rgb.velocity = (targetPos - (Vector2)transform.position).normalized * speed;

        if (player.GetComponent<PlayerController>().isDead)
            transform.position = startPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            transform.position = startPos;
    }

    private void FaceDirection()
    {
        if ((targetPos - (Vector2)transform.position).x < 0)    // 左朝向
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        else
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
}
