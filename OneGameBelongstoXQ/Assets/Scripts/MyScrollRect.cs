using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 祝薛芹生日快乐！—— 来自wxx的祝福
/// </summary>
public class MyScrollRect : ScrollRect
{
    public float limitRadius;      // 最大拖拽半径
    public Vector2 output;

    protected override void Start()
    {
        limitRadius = (transform as RectTransform).sizeDelta.x * 0.4f;
        output = new Vector2();
    }

    public override void OnDrag(PointerEventData eventData)
    {// 重写方法需要先输入override才能快捷找到
        base.OnDrag(eventData);

        Vector2 contentPos = content.anchoredPosition;
        if (contentPos.magnitude > limitRadius)
        {
            contentPos = contentPos.normalized * limitRadius;
            SetContentAnchoredPosition(contentPos);             // 既然有函数，那就不直接赋值了
        }
    }

    private void Update()
    {
        // 使用时记得*Time.deltaTime
        output = content.anchoredPosition / limitRadius;
        Debug.Log(output);
    }
}
