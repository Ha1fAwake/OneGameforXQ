using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 祝薛芹生日快乐！—— 来自wxx的祝福
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{// 角色（罗小黑）控制器
    public float moveForce = 20f;
    public float maxSpeed = 5f;
    public float jumpForce = 0.1f;
    public float jumpIncrement = 1.5f;
    public float maxJumpHeight = 2f;

    public AudioClip damage;
    public AudioClip jump;

    [HideInInspector]
    public bool isDead = false;

    private MyScrollRect scrollRect;
    private MyJumpButton jumpButton;
    private Rigidbody2D rgb;
    private Vector3 startPosition;
    private bool isOnPlatform = false;
    private bool releaseWhenFloat = false;
    private float currentPositionY;    // 记录跳跃前的高度
    private bool isLevelup = false;

    private void Start()
    {
        scrollRect = GameObject.Find("VirtualJoystick").GetComponent<MyScrollRect>();
        jumpButton = GameObject.Find("StartNJump").GetComponent<MyJumpButton>();
        rgb = this.GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void Update()
    {
        if (!GameController.start)
        {
            rgb.bodyType = RigidbodyType2D.Static;
            return;
        }
        if (GameController.start && !isLevelup)
            rgb.bodyType = RigidbodyType2D.Dynamic;
        if (isLevelup)
        {
            rgb.bodyType = RigidbodyType2D.Static;
            return;
        }

        // 移动
        if (Mathf.Abs(scrollRect.output.x) > 0)
            if (Mathf.Abs(rgb.velocity.x) <= maxSpeed)
                rgb.AddForce(new Vector2(scrollRect.output.x, 0).normalized * moveForce);
        if (scrollRect.output.x < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        if(scrollRect.output.x>0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        if (scrollRect.output.x == 0)
            rgb.velocity = new Vector2(0, rgb.velocity.y);

        // 电脑端测试用的键盘移动输入
        if(Input.GetKey(KeyCode.A))
            if (Mathf.Abs(rgb.velocity.x) <= maxSpeed)
            {
                rgb.AddForce(new Vector2(-1f, 0) * moveForce * 5);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        if (Input.GetKey(KeyCode.D))
            if (Mathf.Abs(rgb.velocity.x) <= maxSpeed)
            {
                rgb.AddForce(new Vector2(1f, 0) * moveForce * 5);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

        // 跳跃
        if (jumpButton.isDown && !releaseWhenFloat)     // 当跳跃键按下且没有在半空中松开
            if (transform.position.y - currentPositionY <= maxJumpHeight)
                rgb.velocity = new Vector2(rgb.velocity.x, rgb.velocity.y + jumpIncrement);     // 用AddForce很难实现跳跃的迸发力

        if (!isOnPlatform)
            if (!jumpButton.isDown || transform.position.y - currentPositionY >= maxJumpHeight / 2)         // 若在半空中且松开按键
                releaseWhenFloat = true;    // 记录下来

        // 电脑端测试用的键盘跳跃输入
        if (Input.GetKey(KeyCode.W))     // 当跳跃键按下且没有在半空中松开
            if (transform.position.y - currentPositionY <= maxJumpHeight / 2)
                rgb.velocity = new Vector2(rgb.velocity.x, rgb.velocity.y + jumpIncrement);     // 用AddForce很难实现跳跃的迸发力

        //if (!isOnPlatform)
        //    if (Input.GetKeyUp(KeyCode.W))      // 若在半空中且松开按键
        //        releaseWhenFloat = true;        // 记录下来
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap" || collision.gameObject.tag == "Noface")
        {
            isDead = true;
            Invoke("Replay", 0.2f);
            AudioSource.PlayClipAtPoint(damage, transform.position);
        }

        if (collision.gameObject.tag == "Platform")
        {
            isOnPlatform = true;
            releaseWhenFloat = false;
            currentPositionY = transform.position.y;
        }

        if(collision.gameObject.tag=="Reward")
        {
            isLevelup = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isOnPlatform = false;
            if (jumpButton.isDown || Input.GetKeyDown(KeyCode.W))
                AudioSource.PlayClipAtPoint(jump, transform.position);
        }
    }

    private void Replay()
    {
        transform.position = startPosition;
        rgb.velocity = Vector2.zero;
        isDead = false;
    }
}
