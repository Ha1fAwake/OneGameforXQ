using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    [HideInInspector]
    public bool sendLevelUp;

    private void Start()
    {
        sendLevelUp = false;
    }

    private void Update()
    {
        Debug.Log(sendLevelUp);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
            sendLevelUp = true;
    }
}
