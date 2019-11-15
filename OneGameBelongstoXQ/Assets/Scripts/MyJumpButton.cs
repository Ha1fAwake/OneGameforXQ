using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        isDown = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        isDown = false;
    }
}
