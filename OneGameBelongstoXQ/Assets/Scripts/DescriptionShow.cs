using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DescriptionShow : MonoBehaviour, IPointerDownHandler
{
    public GameObject descriptionPanel;
    public Sprite note;

    public void OnPointerDown(PointerEventData eventData)
    {
        descriptionPanel.SetActive(true);

        if (gameObject.GetComponent<Image>().sprite == note)
            descriptionPanel.GetComponentInChildren<Text>().text =
                    "（空）";
        else
        {
            switch (gameObject.name)
            {
                case "Slot1":
                    descriptionPanel.GetComponentInChildren<Text>().text =
                        "刻着“21”字样的特殊硬币";
                    break;
                case "Slot2":
                    descriptionPanel.GetComponentInChildren<Text>().text =
                        "大白向你招手";
                    break;
                case "Slot3":
                    descriptionPanel.GetComponentInChildren<Text>().text =
                        "一枚被炮姐使用过的哔哩哔哩硬币";
                    break;
                case "Slot4":
                    descriptionPanel.GetComponentInChildren<Text>().text =
                        "肥胖软乎的神烦鸟";
                    break;
                case "Slot5":
                    descriptionPanel.GetComponentInChildren<Text>().text =
                        "可可爱爱的罗小黑";
                    break;
                case "Slot6":
                    descriptionPanel.GetComponentInChildren<Text>().text =
                        "祝薛芹21岁生日快乐！";
                    break;
            }
        }
    }
}
