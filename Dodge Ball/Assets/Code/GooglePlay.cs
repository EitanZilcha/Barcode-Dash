using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using UnityEngine.SocialPlatforms;

public class GooglePlay : MonoBehaviour
{
    public bool IsTop;
    public static ILeaderboard HighScoreLeaderboard;
    public Animator bonanim;
    public GameObject BestBoard;
    private void Awake()
    {
        PlayGamesPlatform.Activate();
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool sucess) =>
            {
                OnConnectionResponse(sucess);
            }
            );
        }
        else
        {
            StartCoroutine(bon());
            GetLBScore();
        }
        
    }
    
    private void OnConnectionResponse(bool sucess)
    {
        StartCoroutine(bon());
        GetLBScore();
        if (PlayerPrefs.GetInt("BonRon") == 0 && Social.localUser.id == "Wants0mem0re")
        {
            PlayerPrefs.SetInt("BonRon", 1);
            GetComponent<Manager>().AddCoins(1000);
        }
    }
    public void ShowLeaderboard()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_best_score);
    }
    public void ShowAchivements()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {

            PlayGamesPlatform.Instance.ShowAchievementsUI();


        }
    }
    
    public void UnlockAchivement(string id)
    {
        //PlayGamesPlatform.Instance.UnlockAchievement(id, (bool success) => {
        // handle success or failure
        //});
        IAchievement achievement = Social.CreateAchievement();
        achievement.id = id;
        achievement.percentCompleted = 100.0;
        achievement.ReportProgress(result => {
            if (result)
                Debug.Log("Successfully reported progress");
            else
                Debug.Log("Failed to report progress");
        });
    }
    
   
    public void IncrementAchivement(string id,int Amount)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(
        id, Amount, (bool success) => {
            // handle success or failure
        });
    }
    public void UpdateLeaderboardScore(int score, string ID)
    {
        Social.ReportScore(score, GPGSIds.leaderboard_best_score, (bool sucess) =>
        {

        });
    }
    public void GetLBScore()
    {
        PlayGamesPlatform.Instance.LoadScores(
            GPGSIds.leaderboard_best_score,
            LeaderboardStart.TopScores,
            3,
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.Daily,
            (data) =>
            {
                Debug.Log("Leaderboard data valid: " + data.Valid);
                //mStatus += "\n approx:" + data.ApproximateCount + " have " + data.Scores.Length;
                if (data.Scores[0].userID == Social.localUser.id)
                {
                    IsTop = true;
                    GetComponent<AchivementManager>().EarnAchievement("Best Out There");
                    CheckBestBonus();
                }
                else
                {
                    IsTop = false;
                }
            });
    }
    
    void CheckBestBonus()
    {
        if (PlayerPrefs.GetInt("WasFirst") == 0 && IsTop)
        {
            PlayerPrefs.SetInt("WasFirst", 1);
            GiveBestBonus();
        }
        else if (!IsTop)
        {
            PlayerPrefs.SetInt("WasFirst", 0);
        }
    }
    
    void GiveBestBonus()
    {
        BestBoard.GetComponent<Animator>().SetTrigger("Play");
        GetComponent<Manager>().AddCoins(50);
    }
    void encryptnames(string text)
    {
        string[] name = text.Split('|');
        foreach (string nm in name)
        {
            if (Social.localUser.userName == nm)
            {
                rewardbon();
            }
        }
    }
    void rewardbon()
    {
        if (PlayerPrefs.GetInt("GotBonus") == 0)
        {
            PlayerPrefs.SetInt("GotBonus", 1);

            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 500);
            bonanim.SetTrigger("Play");
        }
    }
    IEnumerator bon()
    {
        using (WWW www = new WWW("https://wwwtimehostezcomgettimephp.000webhostapp.com/Bonus.txt"))
        {
            yield return www;
            encryptnames(www.text);

        }
    }
    
}
