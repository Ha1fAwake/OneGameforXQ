using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 祝薛芹生日快乐！—— 来自wxx的祝福
/// </summary>
public class Generator : MonoBehaviour
{// 在出现奖励前，触碰屏幕随机生成机关。一定触碰次数生成奖励和玩家
    public int minCount = 2;            // 生成玩家和奖励的最小时机
    public int maxCount = 8;            // 生成玩家和奖励的最大时机

    public GameObject[] layouts;        // 陷阱或移动平台
    public GameObject player;           // 玩家
    [HideInInspector]
    public GameObject reward;           // 奖励（由GameController根据关卡数决定奖励的内容）
    // 用CompositeCollider2D不能实现需要的效果
    public Collider2D boardA;           // 可生成物体的水平范围
    public Collider2D boardB;           // 可生成物体的垂直范围
    public GameObject controlBoard;     // 控制面板（显示时不能生成物体）
    public GameObject itemsBoard;       // 物品面板（显示时不能生成物体）
    public Text trapNumText;            // 陷阱剩余数量
    public Text platformText;           // 平台剩余数量

    private int timer = 0;                  // 触屏次数
    private int timeToGeneratePlayer = 2;   // 生成玩家的时机
    private int timeToGenerateReward = 2;   // 生成奖励的时机
    private int trapCounter = 0;            // 陷阱计数器
    private int platformCounter = 0;        // 平台计数器
    private Vector3 generatePosition;       // 生成位置
    private Bounds boundsA;                 // 范围边界
    private Bounds boundsB;
    private bool isInsideBoundA;            // 判断是否在范围内触碰屏幕
    private bool isInsideBoundB;
    private bool isInsideOthers;

    private void Start()
    {
        while (timeToGeneratePlayer == timeToGenerateReward)
        {
            timeToGeneratePlayer = Random.Range(minCount, maxCount + 1);
            timeToGenerateReward = Random.Range(minCount, maxCount + 1);
        }

        boundsA = boardA.bounds;
        boundsB = boardB.bounds;

        trapNumText.text = (GameController.trapNum - trapCounter).ToString();
        platformText.text = (GameController.platformNum - platformCounter).ToString();
    }

    private void Update()
    {
        // 按键 且 游戏未开始 且 没有出现面板
        if (Input.GetMouseButtonDown(0) && !GameController.start && !controlBoard.activeInHierarchy && !itemsBoard.activeInHierarchy)
        {
            // 确定范围边界
            generatePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            generatePosition.z = 0;
            isInsideBoundA = boundsA.Contains(generatePosition);
            isInsideBoundB = boundsB.Contains(generatePosition);

            // 确定不在已放置物体的范围内
            Collider2D[] colls = Physics2D.OverlapPointAll(generatePosition);
            isInsideOthers = false;
            foreach (Collider2D coll in colls)
            {
                if (coll.name == "Unplaceable")
                    isInsideOthers = true;
            }

            //Collider2D coll = Physics2D.OverlapPoint(generatePosition);
            //if (coll.name == "Unplaceable")
            //    isInsideOthers = true;
            //else
            //    isInsideOthers = false;

            // 若在生成范围内，可以生成
            if (isInsideBoundA || isInsideBoundB)
                if(!isInsideOthers)
                    Generate();
        }
    }

    private void Generate()
    {
        timer++;        // 生成次数+1
        GameObject toGenerate = null;
        if (trapCounter < GameController.trapNum || platformCounter < GameController.platformNum)
            toGenerate = layouts[Random.Range(0, layouts.Length)];
        if (trapCounter == GameController.trapNum)
            toGenerate = layouts[1];
        if (platformCounter == GameController.platformNum)
            toGenerate = layouts[0];
        if (timer == timeToGeneratePlayer)
            toGenerate = player;
        if (timer == timeToGenerateReward)
            toGenerate = reward;
        if (trapCounter >= GameController.trapNum && platformCounter >= GameController.platformNum)
            return;     // 不能继续生成
        Instantiate(toGenerate, generatePosition, Quaternion.identity);

        // 在UI显示剩余陷阱和平台的数量
        if (toGenerate == layouts[0])
        {
            trapCounter++;
            trapNumText.text = (GameController.trapNum - trapCounter).ToString();
        }
        if (toGenerate == layouts[1])
        {
            platformCounter++;
            platformText.text = (GameController.platformNum - platformCounter).ToString();
        }
    }

    //private bool InsideOtherObject(RaycastHit hit)
    //{
    //    if (hit.transform != null)
    //    {
    //        if (hit.transform.tag == "Platform" || hit.transform.tag == "Trap")
    //        {
    //            Debug.Log("Inside");
    //            return true;
    //        }
    //        else
    //            return false;
    //    }
    //    else
    //        return false;
    //}
}
