using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchivementManager : MonoBehaviour {
    public GameObject AchivementPrefab;
    public GameObject VisualAchievement;
    public Transform Content;
    public Transform Vis;
    public Animator anm;
    public Sprite[] Icons;
    private int achivementsdone;
    public Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();
    // Use this for initialization
    void Start () {
       // PlayerPrefs.DeleteAll();
        CreateAchivement(Content,"Beginner!", "Play 1 Game", Icons[0], 50,0);
        CreateAchivement(Content, "Learn The Game", "Score 5 Or More", Icons[0], 20, 0);
        CreateAchivement(Content, "Survivor", "Survive A Total Of 30 Seconds", Icons[0], 50, 30);
        CreateAchivement(Content, "At Least Its An Achievement", "Score 0", Icons[0], 10, 0);
        CreateAchivement(Content, "Prince", "Score 30 Or More", Icons[0], 20, 0);
        CreateAchivement(Content, "King", "Score 60 Or More", Icons[0], 50, 0);
        CreateAchivement(Content, "Legend", "Score 100 Or More", Icons[0], 100, 0);
        CreateAchivement(Content, "This Is Not Easy!", "Fail 50 Times", Icons[0], 50, 50);
        CreateAchivement(Content, "Coin Collector", "Earn 1000 Coins", Icons[0], 100,1000);
        CreateAchivement(Content, "Master", "Score 130 Or More", Icons[0], 200, 0);
        CreateAchivement(Content, "Time Counter", "Score Above 100 3 Times", Icons[0], 100, 3);
        CreateAchivement(Content, "Seen The Worst", "Fail 200 Times", Icons[0], 100, 200);
        CreateAchivement(Content, "Best Out There", "Be 1st On Daily Leaderboard", Icons[0], 50, 0);
        CreateAchivement(Content, "Old Man", "Survive A Total Of One Hour", Icons[0], 300, 3600);
        CreateAchivement(Content, "Loyal Man", "Survive A Total Of Two Hours", Icons[0], 500, 7200);
        CreateAchivement(Content, "Rich & Fast", "Collect 20 coins In One Game", Icons[0], 100, 0);
        CreateAchivement(Content, "Fancy", "Buy The Purple Ball Color", Icons[0], 100, 0);
        CreateAchivement(Content, "Power Consumer", "Buy A Power Up", Icons[0], 200, 0);
        CreateAchivement(Content, "Pain In The Ball", "Land On a Red Platform 100 Times", Icons[0], 100, 100);
        CreateAchivement(Content, "Coin Dodger", "Collect 0 Coins in a 60 Seconds Game", Icons[0], 100, 0);
        CreateAchivement(Content, "Living Dangerously", "Land On a Red Platform 12 Times In One Game", Icons[0], 80, 0);
        CreateAchivement(Content, "Bird Soul", "Fly Above Empty Space For 20 Seconds", Icons[0], 100, 0);
        CreateAchivement(Content, "Instinct Master", "Land on 0 Red Platforms In A 80 Seconds Game", Icons[0], 200, 0);
        CreateAchivement(Content, "King Of The Dash", "Complete All Achivements", Icons[0], 700, NumOfAchivements());
        CheckAllAch();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            EarnAchievement("This Is Not Easy!");
        }
    }
    void CheckAllAch()
    {
        achivementsdone = 0;
        foreach (KeyValuePair<string, Achievement> entry in achievements)
        {
            if (entry.Value.Unlocked)
            {
                achivementsdone++;
                Debug.Log("Achivements Completed: " + achivementsdone);
            }
        }
        achievements["King Of The Dash"].Reset();
        for(int i = 0; i < achivementsdone; i++)
        {
            EarnAchievement("King Of The Dash");
        }
    }
    int NumOfAchivements()
    {
        return achievements.Count;
    }
    public void ShowAch()
    {
        anm.SetTrigger("Enter");
    }
    public void HideAch()
    {
        anm.SetTrigger("Back");
    }
    // Update is called once per frame
    void CreateAchivement (Transform parent, string Title, string Description, Sprite Icon, int Points,int Progress) {
        GameObject achievement = (GameObject)Instantiate(AchivementPrefab);

        Achievement NewAchievement = new Achievement(Title, Description, Points, Icon, achievement,Progress);

        achievements.Add(Title, NewAchievement);

        
        SetAchivementInfo(parent,achievement, Title,Progress);
	}
    void SetAchivementInfo(Transform parent,GameObject Achivement,string Title,int progression=0)
    {
        Achivement.transform.SetParent(parent);
        Achivement.transform.localScale = Vector3.one;
        Debug.Log(progression +" "+ PlayerPrefs.GetInt("Progression " + Title));
        
        string prog = progression > 0 ? " [" + PlayerPrefs.GetInt("Progression " + Title) + "/" + progression.ToString()+"]" : string.Empty;
        
        Achivement.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Title +prog;
        Achivement.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =achievements[Title]. Description;
        Achivement.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite=achievements[Title].Sprite;
        Achivement.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = achievements[Title].Points.ToString();
    }
    public void EarnAchievement(string title)
    {
        if (achievements[title].EarnAchievement())
        {
            //Do Thing
            
            GameObject achievement = (GameObject) Instantiate(VisualAchievement);
            SetAchivementInfo(Vis,achievement, title,0);
            HideAchievement(achievement);
            CheckAllAch();
        }
    }
    public void HideAchievement(GameObject achievement)
    {
        Destroy(achievement, 3);
    }
}
