using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MissionManager : MonoBehaviour {
    public Button[] Mission;
	// Use this for initialization
	void Start () {
        Initialize();
	}
	void Initialize()
    {
        for (int i = 1; i < Mission.Length; i++)
        {
            Mission[i].interactable = (PlayerPrefs.GetInt(i.ToString()) == 0) ? true : false;
            if(PlayerPrefs.GetInt(i.ToString()) != 1)
            Mission[i].GetComponentInChildren<TextMeshProUGUI>().text = (PlayerPrefs.GetInt(i.ToString()) == 0) ? "50 Coins": "Complete";
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
