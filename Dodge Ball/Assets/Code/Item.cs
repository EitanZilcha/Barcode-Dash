using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item {

    private string title;
    private int buystate;
    private int price;
    private Sprite img;
    private Color color;
    private GameObject tmref;
    public Item(string title,Sprite img,Color clr,int price,GameObject ItmRef)
    {
        this.Title = title;
        this.Img = img;
        this.color = clr;
        this.tmref = ItmRef;
        this.price = price;
        LoadItem();
    }
    public void SaveItem(int value)
    {
        buystate = value;
        PlayerPrefs.SetInt(title,value);
        PlayerPrefs.Save();
    }
    public void Use()
    {
        if (buystate != 1)
        {
            bool bought = (buystate == 2) ? true : false;
            if (PlayerPrefs.GetInt("Coins") >= price||bought)
            {
                //END TITLE WITH TYPE WORD!!!!!!!!!!!!!!!!!!!!!!
                PlayerPrefs.SetInt(PlayerPrefs.GetString("Used " + title.Split(' ')[1]), 2);
                PlayerPrefs.SetString("Used " + title.Split(' ')[1], title);
                if (!bought)
                {
                    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - price);
                }
                tmref.GetComponent<ItmRef>().ButtonText.text = "Use";
                SaveItem(1);
                LoadItem();
                Debug.Log("Used " + title);
            }
        }
    }
    //LOAD
    public void LoadItem()
    {
        buystate = PlayerPrefs.GetInt(title);
        bool inter = true;
        if (buystate == 0)
            tmref.GetComponent<ItmRef>().ButtonText.text = price + " Coins";
        else if (buystate == 1)
        {
            tmref.GetComponent<ItmRef>().ButtonText.text = "In Use";
            inter = false;
        }
        else
            tmref.GetComponent<ItmRef>().ButtonText.text = "Use";

        tmref.GetComponent<ItmRef>().ButtonText.transform.parent.GetComponent<Button>().interactable = inter;
    }
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

    public int Buystate
    {
        get
        {
            return buystate;
        }

        set
        {
            buystate = value;
        }
    }

    public int Price
    {
        get
        {
            return price;
        }

        set
        {
            price = value;
        }
    }

    public Sprite Img
    {
        get
        {
            return img;
        }

        set
        {
            img = value;
        }
    }

    public GameObject Tmref
    {
        get
        {
            return tmref;
        }

        set
        {
            tmref = value;
        }
    }

    public Color Color
    {
        get
        {
            return color;
        }

        set
        {
            color = value;
        }
    }
}
