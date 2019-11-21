using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public List<GameObject> rewardList = new List<GameObject>();
    public GameObject finalImage;

    public void ShowCake()
    {
        rewardList[5].SetActive(true);
    }

    public void ShowOthers()
    {
        for(int i = 0; i < rewardList.Count - 1; i++)
        {
            rewardList[i].SetActive(true);
        }
    }

    public void ShowFinalImage()
    {
        finalImage.SetActive(true);
    }

    public void ExitGameFunction()
    {
        Application.Quit();
    }
}
