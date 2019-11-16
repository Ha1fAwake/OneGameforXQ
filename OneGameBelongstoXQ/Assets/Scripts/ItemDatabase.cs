using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
    private List<ItemData> itemDatabase = new List<ItemData>();

    private void Start()
    {
        string itemDatas = File.ReadAllText(Application.dataPath + "/ItemData.json");
        itemDatabase = JsonConvert.DeserializeObject<List<ItemData>>(itemDatas);
        for (int i= 0; i < itemDatabase.Count; i++)
        {
            Debug.Log(itemDatabase[i].Name);
        }
    }

}

public class ItemData
{
    public string Name { get; set; }
    public string Description { get; set; }

    public ItemData(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
