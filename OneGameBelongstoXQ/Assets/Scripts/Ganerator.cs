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
    public int minCount;
    public int maxCount;
    // 用CompositeCollider2D不能实现需要的效果
    public Collider2D boardA;       // 可生成物体的水平范围
    public Collider2D boardB;       // 可生成物体的垂直范围

    private int timer = 0;                  // 触屏次数
    private int timeToGeneratePlayer = 2;   // 生成玩家的时机
    private int timeToGenerateReward = 2;   // 生成奖励的时机
    private Vector3 generatePosition;       // 生成位置
    private Bounds boundsA;                 // 范围边界
    private Bounds boundsB;
    private bool isInsideBoundA;            // 判断是否在范围内触碰屏幕
    private bool isInsideBoundB;

    private void Start()
    {
        while (timeToGeneratePlayer == timeToGenerateReward)
        {
            timeToGeneratePlayer = Random.Range(minCount, maxCount + 1);
            timeToGenerateReward = Random.Range(minCount, maxCount + 1);
        }
        boundsA = boardA.bounds;
        boundsB = boardB.bounds;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameController.start)
        {
            // 确定范围边界
            generatePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            generatePosition.z = 0;
            isInsideBoundA = boundsA.Contains(generatePosition);
            isInsideBoundB = boundsB.Contains(generatePosition);

            // 确定物体外部
            //RaycastHit hit;
            //Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);

            if (isInsideBoundA || isInsideBoundB)
                //if(!InsideOtherObject(hit))
                    Generate();
        }
    }

    private void Generate()
    {
        timer++;

        GameObject toGenerate = layouts[Random.Range(0, layouts.Length)];
        if (timer == timeToGeneratePlayer)
            toGenerate = player;
        if (timer == timeToGenerateReward)
            toGenerate = reward;
        Instantiate(toGenerate, generatePosition, Quaternion.identity);
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
