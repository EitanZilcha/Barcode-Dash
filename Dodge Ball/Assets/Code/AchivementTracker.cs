using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementTracker : MonoBehaviour {
    private GooglePlay GP;
    private Manager manager;
	// Use this for initialization
	void Start () {
        GP = GetComponent<GooglePlay>();
        manager = GetComponent<Manager>();
    }
	public void CheckAchivements(int LastScore,int TimesJumped,int Coins,bool top)
    {
        /*
        GP.IncrementAchivement(GPGSIds.achievement_old_man, LastScore);
        GP.IncrementAchivement(GPGSIds.achievement_loyal_man, LastScore);
        if(TimesJumped>0)
        GP.IncrementAchivement(GPGSIds.achievement_jump_expert, TimesJumped);
        if(Coins>0)
        GP.IncrementAchivement(GPGSIds.achievement_coin_collector, Coins);
        GP.IncrementAchivement(GPGSIds.achievement_seen_the_worst, 1);

        if (GP.CheckAchivement(GPGSIds.achievement_begginer))
        GP.UnlockAchivement(GPGSIds.achievement_begginer);
        if (LastScore >= 5)//&&GP.CheckAchivement(GPGSIds.achievement_learn_the_basics))
        {
            GP.UnlockAchivement(GPGSIds.achievement_learn_the_basics);
            //manager.AddCoins(20);
        }
        
        //GP.IncrementAchivement(GPGSIds.achievement_hyper_hopper,TimesJumped);
        
        //GP.IncrementAchivement(GPGSIds.achievement_survivor, LastScore);
        */
        if (LastScore >= 30&&LastScore<60)// && GP.CheckAchivement(GPGSIds.achievement_prince))
        {
            GP.UnlockAchivement(GPGSIds.achievement_prince);
            //manager.AddCoins(30);
        }
        else if (LastScore >= 60 && LastScore < 100)// && GP.CheckAchivement(GPGSIds.achievement_king))
        {
            GP.UnlockAchivement(GPGSIds.achievement_king);
            //manager.AddCoins(60);
        }
        else if (LastScore >= 100 && LastScore < 130)// && GP.CheckAchivement(GPGSIds.achievement_legend))
        {
            GP.UnlockAchivement(GPGSIds.achievement_legend);
            //manager.AddCoins(100);
        }
        else if (LastScore >= 130)// && GP.CheckAchivement(GPGSIds.achievement_master))
        {
            GP.UnlockAchivement(GPGSIds.achievement_master);
            //manager.AddCoins(200);
        }
        if (TimesJumped >= 20&&TimesJumped<100)// && GP.CheckAchivement(GPGSIds.achievement_its_actually_easier))
        {
            GP.UnlockAchivement(GPGSIds.achievement_its_actually_easier);
            //manager.AddCoins(40);
        }
        else if (TimesJumped >= 100)// && GP.CheckAchivement(GPGSIds.achievement_master))
        {
            GP.UnlockAchivement(GPGSIds.achievement_king_of_jumps);
            //manager.AddCoins(200);
        }
        
        if (PlayerPrefs.GetInt("WasFirst") == 1)//&& GP.CheckAchivement(GPGSIds.achievement_best_out_there))
        {
            GP.UnlockAchivement(GPGSIds.achievement_best_out_there);
            //manager.AddCoins(100);
        }
        if (LastScore > 60)
        {
            if (LastScore >= 60 && TimesJumped == 0)// && GP.CheckAchivement(GPGSIds.achievement_keeping_it_safe))
            {
                GP.UnlockAchivement(GPGSIds.achievement_keeping_it_safe);
                //manager.AddCoins(50);
            }
            
            if (LastScore >= 60 && Coins == 0)// && GP.CheckAchivement(GPGSIds.achievement_coin_dodger))
            {
                GP.UnlockAchivement(GPGSIds.achievement_coin_dodger);
                //manager.AddCoins(50);
            }
        }
        else if (LastScore == 0)// && GP.CheckAchivement(GPGSIds.achievement_at_least_its_an_achivement))
            {
                GP.UnlockAchivement(GPGSIds.achievement_at_least_its_an_achivement);
                //manager.AddCoins(50);
            }
    }
}
