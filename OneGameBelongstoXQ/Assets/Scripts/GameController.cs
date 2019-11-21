using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;
/// <summary>
/// 祝薛芹生日快乐！—— 来自wxx的祝福
/// </summary>
public class GameController : MonoBehaviour
{
    public static bool start = false;
    public static int trapNum;             // 陷阱数（在UI显示）
    public static int platformNum;         // 平台数（在Ui显示）

    public float jumpForce;
    public float walkForce;
    public AudioClip cantStart;
    public GameObject controlBoard;
    public GameObject itemsBoard;
    public GameObject descriptionPanel;

    public Text levelShow;
    public Text trapNumText;
    public Text platformText;

    public List<GameObject> rewardList = new List<GameObject>();
    public List<GameObject> slots = new List<GameObject>();

    private GameObject player;
    private GameObject reward;

    private string levelDatas_json;
    private List<LevelData> levelDatas_list = new List<LevelData>();

    private int currentLevelId;
    private bool isLevelUp = true;
    private bool hasAddedMovingPlat = false;

    // 重新加载场景会调用Awake和Start函数
    private void Awake()
    {// 游戏主要逻辑的初始化必须是Awake函数
        start = false;
        isLevelUp = true;
        levelDatas_list.Clear();
        levelDatas_json = File.ReadAllText(Application.dataPath + "/LevelDatas.json");
        levelDatas_list = JsonConvert.DeserializeObject<List<LevelData>>(levelDatas_json);
        UpdateLevelData();
    }

    private void Update()
    {
        if (isLevelUp)
            UpdateLevelData();

        GameObject reward = GameObject.FindGameObjectWithTag("Reward");
        if (reward != null && reward.GetComponent<LevelUp>().sendLevelUp)
            Invoke("LevelUp", 0.8f);
    }

    public void OpenControlBoard()
    {
        // 开关原理
        controlBoard.SetActive(!controlBoard.activeInHierarchy);

        if (itemsBoard.activeInHierarchy)   // 此键关闭物品栏
            itemsBoard.SetActive(false);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnReplay()
    {// 所有重置设置
        SceneManager.LoadScene(1);
        if (controlBoard.activeInHierarchy == true)
            controlBoard.SetActive(false);
    }

    public void OnStart()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        reward = GameObject.FindGameObjectWithTag("Reward");

        if (player == null || reward == null)
        {
            AudioSource.PlayClipAtPoint(cantStart, transform.position);
        }

        if (player != null && reward != null)
        {
            start = true;

            if (currentLevelId > 3 && !hasAddedMovingPlat)  // 关卡三以上，且尚未添加组件
            {
                GameObject[] platforms;
                platforms = GameObject.FindGameObjectsWithTag("Platform");
                foreach(GameObject platform in platforms)
                {
                    platform.AddComponent<MovingPlatform>();
                    if (currentLevelId == 4)
                        platform.GetComponent<MovingPlatform>().speed = 1f;
                    if (currentLevelId > 4)
                        platform.GetComponent<MovingPlatform>().speed = 2f;
                }
                hasAddedMovingPlat = true;
            }
        }
    }

    public void OnOpenItems()
    {
        if (!itemsBoard.activeInHierarchy)      // 此键打开物品栏
            itemsBoard.SetActive(true);
        if (descriptionPanel.activeInHierarchy)
            descriptionPanel.SetActive(false);

        switch (currentLevelId)
        {
            case 2:
                slots[0].GetComponent<Image>().sprite = rewardList[0].GetComponent<SpriteRenderer>().sprite;
                break;
            case 3:
                slots[0].GetComponent<Image>().sprite = rewardList[0].GetComponent<SpriteRenderer>().sprite;
                slots[1].GetComponent<Image>().sprite = rewardList[1].GetComponent<SpriteRenderer>().sprite;
                break;
            case 4:
                slots[0].GetComponent<Image>().sprite = rewardList[0].GetComponent<SpriteRenderer>().sprite;
                slots[1].GetComponent<Image>().sprite = rewardList[1].GetComponent<SpriteRenderer>().sprite;
                slots[2].GetComponent<Image>().sprite = rewardList[2].GetComponent<SpriteRenderer>().sprite;
                break;
            case 5:
                slots[0].GetComponent<Image>().sprite = rewardList[0].GetComponent<SpriteRenderer>().sprite;
                slots[1].GetComponent<Image>().sprite = rewardList[1].GetComponent<SpriteRenderer>().sprite;
                slots[2].GetComponent<Image>().sprite = rewardList[2].GetComponent<SpriteRenderer>().sprite;
                slots[3].GetComponent<Image>().sprite = rewardList[3].GetComponent<SpriteRenderer>().sprite;
                break;
            case 6:
                slots[0].GetComponent<Image>().sprite = rewardList[0].GetComponent<SpriteRenderer>().sprite;
                slots[1].GetComponent<Image>().sprite = rewardList[1].GetComponent<SpriteRenderer>().sprite;
                slots[2].GetComponent<Image>().sprite = rewardList[2].GetComponent<SpriteRenderer>().sprite;
                slots[3].GetComponent<Image>().sprite = rewardList[3].GetComponent<SpriteRenderer>().sprite;
                slots[4].GetComponent<Image>().sprite = rewardList[4].GetComponent<SpriteRenderer>().sprite;
                break;
        }
    }

    private void UpdateLevelData()
    {// 更新关卡信息
        LevelData levelData = levelDatas_list.Find(
            delegate (LevelData match)
            {
                // 修改关卡只需要修改第一个信息的CurrentLevel
                return match.LevelId == levelDatas_list[0].CurrentLevel;
            });
        levelShow.text = levelData.Level;
        trapNumText.text = levelData.TrapNum.ToString();
        platformText.text = levelData.Platform.ToString();
        trapNum = levelData.TrapNum;
        platformNum = levelData.Platform;
        currentLevelId = levelData.LevelId;
        // 更新关卡奖励
        GetComponent<Generator>().reward = rewardList[levelData.LevelId - 1];   // levelID比列表下标多1位
        isLevelUp = false;
        hasAddedMovingPlat = false;
    }

    private void LevelUp()
    {// 升级
        // 更新json文件
        levelDatas_list[0].CurrentLevel++;
        levelDatas_json = JsonConvert.SerializeObject(levelDatas_list, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/LevelDatas.json", levelDatas_json);
        // 重新加载场景
        SceneManager.LoadScene(1);
    }
}

public class LevelData
{
    public LevelData(string level, int levelId, int trapNum, int platform, int currentLevel)
    {
        Level = level;
        LevelId = levelId;
        TrapNum = trapNum;
        Platform = platform;
        CurrentLevel = currentLevel;
    }

    public string Level { get; set; }
    public int LevelId { get; set; }
    public int TrapNum { get; set; }
    public int Platform { get; set; }
    public int CurrentLevel { get; set; }

}