using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 祝薛芹生日快乐！—— 来自wxx的祝福
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpHeight;

    private MyScrollRect scrollRect;
    private MyJumpButton jumpButton;
    private Rigidbody2D rgb;
    private Vector3 startPosition;
    private bool isGround = false;
    private bool canJump = false;

    private void Start()
    {
        scrollRect = GameObject.Find("VirtualJoystick").GetComponent<MyScrollRect>();
        jumpButton = GameObject.Find("Start").GetComponent<MyJumpButton>();
        rgb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void Update()
    {
        if (Mathf.Abs(scrollRect.output.x) > 0)
            rgb.velocity = new Vector2(scrollRect.output.x, 0) * speed;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap")
            Invoke("Replay", 0.5f);
        if (collision.gameObject.tag == "Platform")
        {
            isGround = true;
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
            isGround = false;
    }

    private void Replay()
    {
        transform.position = startPosition;
    }

    //private bool CanJump()
    //{
    //    if (!isGround && !jumpButton.isDown)        // 如果在半空中且松开跳跃按键
    //        return false;
    //}
}
