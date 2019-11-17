using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 祝薛芹生日快乐！—— 来自wxx的祝福
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float moveForce = 2;
    public float maxSpeed = 0.5f;
    //public float scrollSensitive = 0.1f;
    public float jumpForce = 2;

    private MyScrollRect scrollRect;
    private MyJumpButton jumpButton;
    private Rigidbody2D rgb;
    private Vector3 startPosition;
    private bool isOnPlatform = false;

    private void Start()
    {
        scrollRect = GameObject.Find("VirtualJoystick").GetComponent<MyScrollRect>();
        jumpButton = GameObject.Find("Start").GetComponent<MyJumpButton>();
        rgb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void Update()
    {
        //if (Mathf.Abs(scrollRect.output.x) > 0)
        //{
        //    if(isOnPlatform)
        //        rgb.MovePosition((Vector2)transform.position + new Vector2(scrollRect.output.x, 0) * scrollSensitive);
        //    else
        //        rgb.MovePosition((Vector2)transform.position + new Vector2(scrollRect.output.x, -Mathf.Abs(scrollRect.output.x) * 0.6f) * scrollSensitive);
        //}
        if (Mathf.Abs(scrollRect.output.x) > 0)
            if (Mathf.Abs(rgb.velocity.x) <= maxSpeed)
                rgb.AddForce(new Vector2(scrollRect.output.x, 0).normalized * moveForce);
        if (scrollRect.output.x == 0)
            rgb.velocity = new Vector2(0, rgb.velocity.y);
        //else
        //    rgb.velocity = Vector2.zero;
        Debug.Log(new Vector2(scrollRect.output.x, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap")
            Invoke("Replay", 0.2f);
        if (collision.gameObject.tag == "Platform")
            isOnPlatform = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
            isOnPlatform = false;
    }

    private void Replay()
    {
        transform.position = startPosition;
        rgb.velocity = Vector2.zero;
    }
}
