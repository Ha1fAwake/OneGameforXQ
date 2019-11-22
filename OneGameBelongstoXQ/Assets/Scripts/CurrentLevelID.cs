using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLevelID : MonoBehaviour
{
    public int showId;
    public static int currentLevelId = 1;

    private void Awake()
    {
        showId = currentLevelId;

        GameObject[] ids = GameObject.FindGameObjectsWithTag("CurrentLevelID");

        if (ids.Length > 1)
            Destroy(ids[0]);

        DontDestroyOnLoad(gameObject);
    }
}
