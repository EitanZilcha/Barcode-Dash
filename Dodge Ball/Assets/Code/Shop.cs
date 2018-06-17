using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Shop : MonoBehaviour
{
    public Renderer Player;
    public GameObject[] Trail;
    public Button[] BallColorBtn;
    public Button[] BallTrailBtn;
    public Color Blue;
    public Color Red;
    public Color Green;
    public Color Purple;

    // Use this for initialization
    void Start()
    {
        Initialize();
        //ChangeColor();
        //ChangeTrail();
    }
    void Initialize()
    {

        foreach (Button btn in BallColorBtn)
        {
            if (PlayerPrefs.GetInt(btn.transform.parent.name) == 1)
            {
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "In Use";
                btn.interactable = false;
            }
            else if(PlayerPrefs.GetInt(btn.transform.parent.name) == 2)
            {
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "USE";
                btn.interactable = true;
            }
            else
            {
                btn.GetComponentInChildren<TextMeshProUGUI>().text = btn.GetComponent<Price>().price.ToString() + " Coins";
                btn.interactable = true;
            }
            GetComponent<Counter>().Coins[2].text = PlayerPrefs.GetInt("Coins").ToString();
        }
        foreach (Button btn in BallTrailBtn)
        {
            if (PlayerPrefs.GetInt(btn.transform.parent.name) == 1)
            {
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "In Use";
                btn.interactable = false;
            }
            else if (PlayerPrefs.GetInt(btn.transform.parent.name) == 2)
            {
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "USE";
                btn.interactable = true;
            }
            else
            {
                btn.GetComponentInChildren<TextMeshProUGUI>().text = btn.GetComponent<Price>().price.ToString() + " Coins";
                btn.interactable = true;
            }
        }
    }
    void ChangeColor()
    {
        if (PlayerPrefs.GetInt("Blue Ball") == 1)
        {
            Player.material.color = Blue;
        }
        else if (PlayerPrefs.GetInt("Red Ball") == 1)
        {
            Player.material.color = Red;
        }
        else if (PlayerPrefs.GetInt("Green Ball") == 1)
        {
            Player.material.color = Green;
        }
        else if (PlayerPrefs.GetInt("Purple Ball") == 1)
        {
            Player.material.color = Purple;
        }
    }
    void ChangeTrail()
    {
        if (PlayerPrefs.GetInt("Orange Trail") == 1)
        {
            Trail[0].SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Red Trail") == 1)
        {
            Trail[1].SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Blue Trail") == 1)
        {
            Trail[2].SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Green Trail") == 1)
        {
            Trail[3].SetActive(true);
        }
    }
    public void BuyBallColor(string Name)
    {
        int price = GameObject.Find("Share/Panel/Balls&Trails/Viewport/Content/" + Name).GetComponentInChildren<Price>().price;
        bool bought = (PlayerPrefs.GetInt(Name) == 2) ? true : false;
        if (PlayerPrefs.GetInt("Coins") >= price||PlayerPrefs.GetInt(Name)==2)
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("LatestBall"), 2);
            PlayerPrefs.SetString("LatestBall", Name);
            PlayerPrefs.SetInt(Name, 1);
            if(!bought)
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - price);
            if (Name=="Purple Ball")
            {
                
                GetComponent<AchivementManager>().EarnAchievement("Fancy");
            }
        }
        Initialize();
    }
    public void BuyTrailColor(string Name)
    {
        int price = GameObject.Find("Share/Panel/Balls&Trails/Viewport/Content/" + Name).GetComponentInChildren<Price>().price;
        bool bought = (PlayerPrefs.GetInt(Name) == 2) ? true : false;
        if (PlayerPrefs.GetInt("Coins") >= price)
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("LatestTrail"), 2);
            PlayerPrefs.SetString("LatestTrail", Name);
            PlayerPrefs.SetInt(Name, 1);
            if(!bought)
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - price);
        }
        Initialize();
    }
}
