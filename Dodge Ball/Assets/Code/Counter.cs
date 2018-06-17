using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    public TextMeshProUGUI[] Score;
    public TextMeshProUGUI[] Coins;
    int coins;
    private Manager manager;
    // Use this for initialization
    void Start()
    {
        Score[0].text = PlayerPrefs.GetInt("HighScore").ToString();
        Coins[0].text = PlayerPrefs.GetInt("Coins").ToString();
        manager = GetComponent<Manager>();
        Coins[1].text += " Coins";
    }

    // Update is called once per frame
    void Update()
    {
        int temp = (int)manager.TimePassed;
        if (manager.Started && !manager.Lost)
            Score[0].text = Score[1].text = Score[2].text = temp.ToString();
    }
    public void AddCoin()
    {
        GetComponent<AchivementManager>().EarnAchievement("Coin Collector");
        coins++;
        foreach (TextMeshProUGUI cn in Coins)
        {
            cn.text = coins.ToString();
        }
        Coins[1].text += " Coins";
    }
}
