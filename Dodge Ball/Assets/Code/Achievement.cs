using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Achievement {
    private string title;
    private string description;
    private bool unlocked;
    private int points;
    private Sprite sprite;
    private GameObject achievementref;
    private int CurrentProgression;
    private int MaxProgression;

    public string Title
    {
        get
        {
            return title;
        }

        set
        {
            title = value;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }

    public bool Unlocked
    {
        get
        {
            return unlocked;
        }

        set
        {
            unlocked = value;
        }
    }

    public int Points
    {
        get
        {
            return points;
        }

        set
        {
            points = value;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return sprite;
        }

        set
        {
            sprite = value;
        }
    }

    public GameObject Achievementref
    {
        get
        {
            return achievementref;
        }

        set
        {
            achievementref = value;
        }
    }

    
    public Achievement(string title,string description,int points,Sprite spr,GameObject achref,int maxprogression)
    {
        this.Title = title;
        this.Description = description;
        this.Unlocked = false;
        this.Points = points;
        this.Sprite = spr;
        this.Achievementref = achref;
        this.MaxProgression = maxprogression;
        LoadAchievement();
    }
	// Use this for initialization
	void Start () {
        //Achievement MyAchievement = new Achievement(this.title,this);
	}
	
	public bool EarnAchievement()
    {
        if (!Unlocked&&CheckProgress())
        {
            Unlocked = true;
            achievementref.transform.GetChild(3).GetChild(0).GetComponent<Image>().fillAmount = 1;
            
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + points);
            SaveAchievement(true);
            return true;
        }
        return false;
    }
   


    //SAVE
    public void SaveAchievement(bool value)
    {
        unlocked = value;
        //int tmpPoints = PlayerPrefs.GetInt("Points");
        //PlayerPrefs.SetInt("Points", tmpPoints += points);
        PlayerPrefs.SetInt("Progression " + title,CurrentProgression);

        PlayerPrefs.SetInt(title, value ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void Reset()
    {
        if(!unlocked)
        PlayerPrefs.SetInt("Progression " + title,0);

        //PlayerPrefs.SetInt(title, 0);
        PlayerPrefs.Save();
        LoadAchievement();
    }
    //LOAD
    public void LoadAchievement()
    {
        unlocked = PlayerPrefs.GetInt(title) == 1 ? true : false;
        CurrentProgression = PlayerPrefs.GetInt("Progression " + title);
        if (MaxProgression > 0)
            achievementref.transform.GetChild(3).GetChild(0).GetComponent<Image>().fillAmount = (float)CurrentProgression / MaxProgression;
        if (unlocked)
        achievementref.transform.GetChild(3).GetChild(0).GetComponent<Image>().fillAmount = 1;
    }
    public bool CheckProgress()
    {
        CurrentProgression ++;
        if (MaxProgression > 0)
        {
            achievementref.transform.GetChild(3).GetChild(0).GetComponent<Image>().fillAmount = (float)CurrentProgression / MaxProgression;

            achievementref.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = title + " [" + CurrentProgression + "/" + MaxProgression+"]";
        }
            SaveAchievement(false);
        if (MaxProgression == 0)
            return true;
        if (CurrentProgression >= MaxProgression)
        {
            return true;
        }
        return false;
    }

}
