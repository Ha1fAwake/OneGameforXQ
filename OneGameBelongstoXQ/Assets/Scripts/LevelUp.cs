using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 祝薛芹生日快乐！—— 来自wxx的祝福
/// </summary>
public class LevelUp : MonoBehaviour
{
    [HideInInspector]
    public bool sendLevelUp;

    public AudioClip reward;

    private void Start()
    {
        sendLevelUp = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sendLevelUp = true;
            Camera.main.GetComponent<AudioSource>().volume = 0;
            AudioSource.PlayClipAtPoint(reward, transform.position);
        }
    }
}
