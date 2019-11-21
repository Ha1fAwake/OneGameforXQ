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

    private void OnMouseDown()
    {
        if (!flowchart.HasExecutingBlocks())
            flowchart.ExecuteBlock("First");
    }
}
