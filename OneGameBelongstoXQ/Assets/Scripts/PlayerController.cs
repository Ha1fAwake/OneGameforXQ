using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 祝薛芹生日快乐！—— 来自wxx的祝福
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float moveForce = 20f;
    public float maxSpeed = 5f;
    public float jumpForce = 0.1f;
    public float jumpIncrement = 1.5f;
    public float maxJumpHeight = 2f;

    public AudioClip damage;
    public AudioClip jump;

    private MyScrollRect scrollRect;
    private MyJumpButton jumpButton;
    private Rigidbody2D rgb;
    private Vector3 startPosition;
    private bool isOnPlatform = false;
    private bool releaseWhenFloat = false;
    private float currentPositionY;    // 记录跳跃前的高度

    private void Start()
    {
        scrollRect = GameObject.Find("VirtualJoystick").GetComponent<MyScrollRect>();
        jumpButton = GameObject.Find("Start").GetComponent<MyJumpButton>();
        rgb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void Update()
    {
        if (!GameController.start)
        {
            rgb.bodyType = RigidbodyType2D.Static;
            return;
        }
        rgb.bodyType = RigidbodyType2D.Dynamic;

        // 移动
        if (Mathf.Abs(scrollRect.output.x) > 0)
            if (Mathf.Abs(rgb.velocity.x) <= maxSpeed)
                rgb.AddForce(new Vector2(scrollRect.output.x, 0).normalized * moveForce);
        if (scrollRect.output.x == 0)
            rgb.velocity = new Vector2(0, rgb.velocity.y);

        // 跳跃
        if (jumpButton.isDown && !releaseWhenFloat)     // 当跳跃键按下且没有在半空中松开
            if (transform.position.y - currentPositionY <= maxJumpHeight)
            {
                rgb.velocity = new Vector2(rgb.velocity.x, rgb.velocity.y + jumpIncrement);     // 用AddForce很难实现跳跃的迸发力
            }

        if (!isOnPlatform)
            if (!jumpButton.isDown)         // 若在半空中且松开按键
                releaseWhenFloat = true;    // 记录下来
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            Invoke("Replay", 0.2f);
            AudioSource.PlayClipAtPoint(damage, transform.position);
        }

        if (collision.gameObject.tag == "Platform")
        {
            isOnPlatform = true;
            releaseWhenFloat = false;
            currentPositionY = transform.position.y;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isOnPlatform = false;
            if (jumpButton.isDown)
                AudioSource.PlayClipAtPoint(jump, transform.position);
        }
    }

    private void Replay()
    {
        transform.position = startPosition;
        rgb.velocity = Vector2.zero;
    }
}
