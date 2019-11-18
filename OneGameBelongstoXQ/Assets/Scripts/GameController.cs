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
    public GameObject itemsBoard;

    private GameObject player;
    private GameObject reward;

    private void Awake()
    {
        Time.timeScale = 0;
        start = false;
    }

    public void OpenControlBoard()
    {
        // 开关原理
        controlBoard.SetActive(!controlBoard.activeInHierarchy);

        if (itemsBoard.activeInHierarchy)   // 此键关闭物品栏
            itemsBoard.SetActive(false);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnReplay()
    {// 所有重置设置
        SceneManager.LoadScene(0);
        if (controlBoard.activeInHierarchy == true)
            controlBoard.SetActive(false);
        start = false;
    }

    public void OnStart()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        reward = GameObject.FindGameObjectWithTag("Reward");

        if (player == null || reward == null)
        {
            AudioSource.PlayClipAtPoint(cantStart, transform.position);
        }

        if (player != null && reward != null)
        {
            start = true;
            Time.timeScale = 1;
        }
    }

    public void OnOpenItems()
    {
        if (!itemsBoard.activeInHierarchy)      // 此键打开物品栏
            itemsBoard.SetActive(true);
    }
}
