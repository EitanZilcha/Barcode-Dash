using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using EveryplayMiniJSON;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public int Score;
    public int NumOfJumps;
    public int NumOfCoins;
    public int NumOfBads;
    [HideInInspector]
    public bool Started;
    [HideInInspector]
    public bool Lost;
    public Animator LosePanel;
    public GameObject PlayButton;
    public float TimePassed;
    public GameObject BestScore;
    public bool RecEnabled;
    public Button ShareEveryplay;
    public Animator ShareCanvas;
    public Toggle Togl;
    public AudioSource Music;
    public AudioSource LoseSound;
    public Image Vol;
    public Sprite UnMute;
    public Sprite Mute;
    public Animator[] Inst;
    public Cam cmra;
    float ttc = 30;
    float instdelay=1;
    void Start()
    {
        if (PlayerPrefs.GetInt("FirstTime2") == 0)
        {
            PlayerPrefs.SetInt("FirstTime2", 1);
            PlayerPrefs.SetInt("Blue Ball", 1);
            PlayerPrefs.SetString("LatestBall", "Blue Ball");
            PlayerPrefs.SetInt("Orange Trail", 1);
            PlayerPrefs.SetString("LatestTrail", "Orange Trail");
            PlayerPrefs.SetInt("Coins", 50);
            GetComponent<Counter>().Coins[0].text = PlayerPrefs.GetInt("Coins").ToString();
        }
        if (PlayerPrefs.GetInt("tuto3") == 0)
        {
            PlayerPrefs.SetInt("tuto3", 1);
            Inst[0].gameObject.SetActive(true);
        }
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Music.pitch = UnityEngine.Random.Range(1.2f, 1.5f);
        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            Vol.sprite = Mute;
            Music.enabled = false;
        }
    }

    public void Lose()
    {
        Lost = true;
        CheckAch();
        LoseSound.Play();
        LosePanel.SetTrigger("Play"); GetComponent<GooglePlay>().UpdateLeaderboardScore(Score, "PlayGamesPlatform.Instance.localUser.authenticated");
        GetComponent<Counter>().Coins[2].text = PlayerPrefs.GetInt("Coins").ToString();
        if (Score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", Score);
            BestScore.SetActive(true);
        }
        Music.Stop();
        //Do Last!!!!!!!!!!!!!!!!!!!!
        if (GetComponent<AdMobCode>().interstitial.IsLoaded())
            GetComponent<AdMobCode>().interstitial.Show();
        //GetComponent<AchivementTracker>().CheckAchivements(Score, NumOfJumps,NumOfCoins, GetComponent<GooglePlay>().IsTop);

        
    }
    // Update is called once per frame
    void Update()
    {
        if (Started)
        {
            TimePassed += Time.deltaTime;
            ttc -= Time.deltaTime;
            int temps = Score;
            Score = (int)TimePassed;
            if (Score != temps&&!Lost)
            {
                GetComponent<AchivementManager>().EarnAchievement("Survivor");
                GetComponent<AchivementManager>().EarnAchievement("Old Man");
                GetComponent<AchivementManager>().EarnAchievement("Loyal Man");
            }
            if (ttc < 0)
            {
                cmra.changecol();
                ttc = 30;
            }
        }
        instdelay -= Time.deltaTime;
        if (Input.GetMouseButton(0) &&instdelay<0|| Input.GetKey(KeyCode.Space))
        {
            instdelay = 0.5f;
            Inst[0].SetTrigger("Con");
            if(Inst[1].isActiveAndEnabled)
            Inst[1].SetTrigger("Con");
            if(Inst[0].isActiveAndEnabled)
            Inst[1].gameObject.SetActive(true);
        }
    }
    public void Play()
    {
        Started = true;
        PlayButton.GetComponent<Animator>().SetTrigger("Play");
        Music.Play();
        GetComponent<Counter>().Coins[0].text = "0";
        //GetComponent<AdMobCode>().bannerView.Hide();
    }
    public void Retry()
    {
        PlayerPrefs.SetInt("TuAD", PlayerPrefs.GetInt("TuAD") + 1);
        if (PlayerPrefs.GetInt("TuAD") % 10 == 0)
        {
            //GetComponent<SimpleAds>().ShowAd();
        }
        SceneManager.LoadScene(0);
    }
    public void More()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Eitan+Zilcha");
    }
    void CheckAch()
    {
        GetComponent<AchivementManager>().EarnAchievement("Beginner!");
        GetComponent<AchivementManager>().EarnAchievement("This Is Not Easy!");
        GetComponent<AchivementManager>().EarnAchievement("Seen The Worst");
        if (Score == 0)
        {
            GetComponent<AchivementManager>().EarnAchievement("At Least Its An Achievement");
        }
        if (Score >= 5)
        {
            GetComponent<AchivementManager>().EarnAchievement("Learn The Game");
        }
        if (Score >= 30)
        {
            GetComponent<AchivementManager>().EarnAchievement("Prince");
        }
        if (Score >= 60)
        {
            GetComponent<AchivementManager>().EarnAchievement("King");
        }
        if (Score >= 100)
        {
            GetComponent<AchivementManager>().EarnAchievement("Legend");
            GetComponent<AchivementManager>().EarnAchievement("Time Counter");
        }
        if (Score >= 130)
        {
            GetComponent<AchivementManager>().EarnAchievement("Master");
        }
        if (Score >= 60 && NumOfCoins == 0)
        {
            GetComponent<AchivementManager>().EarnAchievement("Coin Dodger");
        }
        if (NumOfCoins >= 20)
        {
            GetComponent<AchivementManager>().EarnAchievement("Rich & Fast");
        }
        if (NumOfBads == 0 && Score >= 80)
        {
            GetComponent<AchivementManager>().EarnAchievement("Instinct Master");
        }
    }
    public void OpenCanvas()
    {
        ShareCanvas.SetTrigger("Enter");
    }
    public void CloseCanvas()
    {
        ShareCanvas.SetTrigger("Back");
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("TuAD", 0);
    }
    public void ChangeMute()
    {
        //if(PlayerPrefs.GetInt("Mute")==)
        PlayerPrefs.SetInt("Mute", (PlayerPrefs.GetInt("Mute") == 1) ? 0 : 1);
        Vol.sprite = (PlayerPrefs.GetInt("Mute") == 1) ? Mute : UnMute;
    }
    public void AddCoins(int Amount)
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + Amount);
        if (!Started)
            GetComponent<Counter>().Coins[0].text = PlayerPrefs.GetInt("Coins").ToString();
    }
    
}
