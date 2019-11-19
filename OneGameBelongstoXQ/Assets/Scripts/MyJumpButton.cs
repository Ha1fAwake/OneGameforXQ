using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 祝薛芹生日快乐！—— 来自wxx的祝福
/// </summary>
public class MyJumpButton : Button
{
    public bool isDown = false;

    private void Update()
    {
        if (GameController.start)
            GetComponentInChildren<Text>().text = "Q";
        else
            GetComponentInChildren<Text>().text = "S";
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (GetComponentInChildren<Text>().text == "Q")
            isDown = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (GetComponentInChildren<Text>().text == "Q")
            isDown = false;
    }
}
