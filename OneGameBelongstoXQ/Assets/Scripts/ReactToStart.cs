using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
/// <summary>
/// 祝薛芹生日快乐！—— 来自wxx的祝福
/// </summary>
public class ReactToStart : MonoBehaviour
{
    public Flowchart flowchart;

    private Bounds bounds;
    private Vector2 touchPos;

    private void Start()
    {
        bounds = this.GetComponent<CircleCollider2D>().bounds;
    }

    void Update()
    {
        touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && bounds.Contains(touchPos))
        {
            flowchart.ExecuteBlock("First");
        }
    }
}
