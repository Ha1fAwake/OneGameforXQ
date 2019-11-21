using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;    // 慢1快2

    private Vector2 startPosition;
    private Vector2 endPosition;
    private bool onTheMove;

    private void Start()
    {
        startPosition = (Vector2)transform.position + new Vector2(-0.6f, 0);
        endPosition = (Vector2)transform.position + new Vector2(0.6f, 0);
    }

    private void FixedUpdate()
    {
        float step = speed * Time.deltaTime;

        if (onTheMove == false)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, endPosition, step);
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, startPosition, step);
        }

        if (this.transform.position.x == endPosition.x && this.transform.position.y == endPosition.y && onTheMove == false)
        {
            onTheMove = true;
        }
        else if (this.transform.position.x == startPosition.x && this.transform.position.y == startPosition.y && onTheMove == true)
        {
            onTheMove = false;
        }
    }
}
