using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float jumpForce;
    public float walkForce;
    public AudioClip cantStart;

    private GameObject player;
    private GameObject reward;
    private bool start = false;

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnJump()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        reward = GameObject.FindGameObjectWithTag("Reward");

        if (player == null && reward == null)
        {
            AudioSource.PlayClipAtPoint(cantStart, transform.position);
        }

        if (player!= null && reward != null)
        {
            start = true;
        }

        if (start)
        {
            if (player.GetComponent<Rigidbody2D>() == null)
            {
                player.AddComponent<Rigidbody2D>();
                start = true;
            }
            else
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }
    }

    public void OnLeft()
    {
        if (start)
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-walkForce, 0));
    }
    
    public void OnRight()
    {
        if (start)
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(walkForce, 0));
    }
}
