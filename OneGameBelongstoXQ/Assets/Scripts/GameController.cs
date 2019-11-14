using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 祝薛芹生日快乐！—— 来自wxx的祝福
/// </summary>
public class GameController : MonoBehaviour
{
    public float jumpForce;
    public float walkForce;
    public AudioClip cantStart;
    public GameObject controlBoard;
    public static bool start = false;

    private GameObject player;
    private GameObject reward;

    public void OpenControlBoard()
    {
        if (controlBoard.activeInHierarchy == false)
            controlBoard.SetActive(true);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnReplay()
    {
        SceneManager.LoadScene(0);
        if (controlBoard.activeInHierarchy == true)
            controlBoard.SetActive(false);
        start = false;
    }

    public void OnJump()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        reward = GameObject.FindGameObjectWithTag("Reward");

        if (player == null || reward == null)
        {
            //AudioSource.PlayClipAtPoint(cantStart, transform.position);
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
            {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce * 100));
            }
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
