using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;
/// <summary>
/// 祝薛芹生日快乐！—— 来自wxx的祝福
/// </summary>
public class ReactToStart : MonoBehaviour
{
    public Flowchart flowchart;
    public GameObject background;
    public Sprite gameplay;

    private void OnMouseDown()
    {
        if (!flowchart.HasExecutingBlocks())
            flowchart.ExecuteBlock("First");
    }

    public void ShowGameplay()
    {
        background.GetComponent<Image>().sprite = gameplay;
    }
}
