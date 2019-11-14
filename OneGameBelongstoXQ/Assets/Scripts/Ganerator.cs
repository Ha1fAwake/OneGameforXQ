using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 祝薛芹生日快乐！—— 来自wxx的祝福
/// </summary>
public class Ganerator : MonoBehaviour
{// 在出现奖励前，触碰屏幕随机生成机关。一定触碰次数生成奖励和玩家
    public GameObject[] layouts;    // 触碰屏幕可以生成的物体，包括：玩家（罗小黑）、陷阱、移动平台、奖励
    public GameObject player;
    public GameObject reward;
    public int timeToGeneratePlayer;// 生成玩家的时机
    public int timeToGenerateReward;// 生成奖励的时机

    private int timer = 0;                  // 触屏次数
    private Vector3 generatePosition;       // 生成位置

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            timer++;
            generatePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            generatePosition.z = 0;
            GameObject toGenerate = layouts[Random.Range(0, layouts.Length)];
            if (timer == timeToGeneratePlayer)
                toGenerate = player;
            if (timer == timeToGenerateReward)
                toGenerate = reward;
            Instantiate(toGenerate, generatePosition, Quaternion.identity);
        }
    }
}
