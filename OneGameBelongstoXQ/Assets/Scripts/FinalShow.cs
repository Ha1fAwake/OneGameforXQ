using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 祝薛芹生日快乐！—— 来自wxx的祝福
/// </summary>
public class FinalShow : MonoBehaviour
{// 最后的展示
    public AudioClip gameClear;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Camera.main.GetComponent<AudioSource>().volume = 0;
            AudioSource.PlayClipAtPoint(gameClear, transform.position);
            Invoke("LoadLastScene", 6.5f);
        }
    }

    private void LoadLastScene()
    {
        SceneManager.LoadScene(2);
    }
}
