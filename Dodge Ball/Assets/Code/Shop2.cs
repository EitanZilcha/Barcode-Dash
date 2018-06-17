using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop2 : MonoBehaviour {
    public GameObject ItemPrefab;
    public GameObject PowUpsSep;
    public GameObject TailSep;
    public Renderer Player;
    public Transform content;
    public Sprite[] Sprites;

    public Dictionary<string, Item> Items = new Dictionary<string, Item>();

    //Colors
    public Color Orange;
    public Color Purple;
    
    // Use this for initialization
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
    }
    void Start () {

        //PlayerPrefs.SetInt("Coins", 1000000);
        //PlayerPrefs.DeleteKey("Coin Attractor");
        CreateItems();
        
        if (!PlayerPrefs.HasKey("ShopFirst1"))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("LatestBall"), 0);
            PlayerPrefs.SetInt(PlayerPrefs.GetString("LatestTrail"), 0);
            PlayerPrefs.SetInt("ShopFirst1", 1);
            UseItem("Blue Ball");
            PlayerPrefs.SetInt("Blue Ball", 1);
            PlayerPrefs.SetString("Used Ball", "Blue Ball");
            Items["Blue Ball"].Tmref.GetComponent<ItmRef>().ButtonText.text = "In Use";
            UseItem("Orange Trail");
            PlayerPrefs.SetInt("Orange Trail", 1);
            PlayerPrefs.SetString("Used Trail", "Orange Trail");
            Items["Orange Trail"].Tmref.GetComponent<ItmRef>().ButtonText.text = "In Use";
            Debug.Log("Call");
        }
        Player.material.color = Items[PlayerPrefs.GetString("Used Ball")].Color;
        Player.transform.Find(PlayerPrefs.GetString("Used Trail")).gameObject.SetActive(true);
    }
    void CreateItems()
    {
        CreateItem(content, "Blue Ball", Sprites[0], Color.cyan, 100);
        CreateItem(content, "Gray Ball", Sprites[0], Color.gray, 100);
        CreateItem(content, "Orange Ball", Sprites[0], Orange,200);//200 / 255, 127 / 255, 80 / 255), 200);
        CreateItem(content, "Yellow Ball", Sprites[0], Color.yellow, 300);
        CreateItem(content, "Red Ball", Sprites[0], Color.red, 500);
        CreateItem(content, "Pink Ball", Sprites[0], Color.magenta, 700);
        CreateItem(content, "Green Ball", Sprites[0], Color.green, 1000);
        CreateItem(content, "Purple Ball", Sprites[0],Purple, 1400);

        GameObject TR = Instantiate(TailSep);
        TR.transform.SetParent(content);
        TR.transform.localScale = Vector3.one;

        CreateItem(content, "Orange Trail", Sprites[1], Orange, 100);
        CreateItem(content, "Red Trail", Sprites[1], Color.red, 200);//200 / 255, 127 / 255, 80 / 255), 200);
        CreateItem(content, "Blue Trail", Sprites[1], Color.cyan, 300);
        CreateItem(content, "Green Trail", Sprites[1], Color.green, 700);

        GameObject PU= Instantiate(PowUpsSep);
        PU.transform.SetParent(content);
        PU.transform.localScale = Vector3.one;
        CreateItem(content, "Coin Doubler", Sprites[2], Color.white, 1500);
        CreateItem(content, "Coin Attractor", Sprites[2], Color.white, 1000);
        CreateItem(content, "Sensitivity Boost", Sprites[3], Color.white, 900);
    }
    void CreateItem(Transform parent, string Title, Sprite img,Color color, int Price)
    {
        GameObject item = (GameObject)Instantiate(ItemPrefab);

        Item NewItem = new Item(Title,img,color ,Price,item);

        Items.Add(Title, NewItem);
        SetItemInfo(parent,Title, item);
    }
    void SetItemInfo(Transform Parent,string Title,GameObject ItemRef)
    {
        ItemRef.transform.SetParent(Parent);
        ItemRef.transform.localScale = Vector3.one;
        ItemRef.GetComponent<ItmRef>().Title.text = Title;
        ItemRef.GetComponent<ItmRef>().img.sprite = Items[Title].Img;
        ItemRef.GetComponent<ItmRef>().img.color = Items[Title].Color;
        //ItemRef.GetComponent<ItmRef>().btn.onClick.AddListener(UseItem);
        ItemRef.GetComponent<ItmRef>().btn.onClick.AddListener(delegate { UseItem(Title); });
        Items[Title].LoadItem();
    }
    public void UseItem(string nem)
    {
        Items[nem].Use();
        foreach(string tm in Items.Keys)
        {
            Items[tm].LoadItem();
        }
        GetComponent<Counter>().Coins[2].text = PlayerPrefs.GetInt("Coins").ToString();
        if(nem=="Purple Ball")
        {
            if(PlayerPrefs.GetInt(nem) == 1)
            GetComponent<AchivementManager>().EarnAchievement("Fancy");
        }
        if (nem == "Coin Attractor"|| nem == "Coin Doubler" || nem == "Sensitivity Boost")
        {
            Debug.Log("Lev1");
            Debug.Log(Items[nem].Buystate);
            if (PlayerPrefs.GetInt(nem) == 1 )
            {
                GetComponent<AchivementManager>().EarnAchievement("Power Consumer");
                Debug.Log("Lev2");
            }
        }
        Debug.Log(nem);
    }
}
