using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Move Player;
    public GameObject Platform;
    private float PlatformCount;
    float lastposx;
    public GameObject Coin;
    public Camera cam;
    private int NtEvl = 3;
    private float CheckInterval;
    List<GameObject> Platforms = new List<GameObject>();
    List<GameObject> Coins = new List<GameObject>();
    bool flip;
    bool attractor;
    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("Coin Attractor") == 1)
        {
            attractor = true;
        }
        for (int i = 0; i < 10; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        CheckInterval -= Time.deltaTime;
        if (CheckInterval < 0)
        {
            for (int i = 0; i < Platforms.Count; i++)
            {
                if (Player.transform.position.z - 20 > Platforms[i].transform.position.z)
                {
                    GameObject tmp = Platforms[i];
                    if (tmp.GetComponent<Platform>().IsSide)
                    {
                        Platforms.Remove(tmp);
                        Destroy(tmp);
                    }
                    else
                        Respawn(tmp);

                }
            }
            for (int i = 0; i < Coins.Count; i++)
            {
                if (Player.transform.position.z - 20 > Coins[i].transform.position.z)
                {
                    GameObject tmp = Coins[i];
                    Coins.Remove(tmp);
                    Destroy(tmp);
                    i--;
                }
            }
            CheckInterval = .1f;
        }
    }
    void SpawnPlatform()
    {
        Vector3 pos = new Vector3(lastposx + UnityEngine.Random.Range(3, 8), 0, PlatformCount * 10);
        if (UnityEngine.Random.Range(0, 2) == 0)
            pos = new Vector3(lastposx + UnityEngine.Random.Range(-8, -3), 0, PlatformCount * 10);
        lastposx = pos.x;
        if (PlatformCount < 4)
        {
            pos = new Vector3(0, 0, PlatformCount * 10);
            lastposx = 0;
        }
        int rndm = UnityEngine.Random.Range(0, 8);
        if (pos.x > 6)
        {
            GameObject plt = Instantiate(Platform, new Vector3(-pos.x, 0, PlatformCount * 10), Quaternion.Euler(90, 0, 0));
            Platforms.Add(plt);
            plt.GetComponent<Platform>().IsSide = true;
            plt.GetComponent<Platform>().Camm = cam;
            rndm = UnityEngine.Random.Range(0, 4);
            flip = true;
            if (UnityEngine.Random.Range(0, 6) == 1 && NtEvl < 0)
            {
                plt.GetComponent<Platform>().IsEvil = true;
                NtEvl = 3;
            }
        }
        else
        {
            flip = false;
        }
        if (rndm == 2 && PlatformCount > 2)
        {
            Coins.Add(Instantiate(Coin, new Vector3((flip) ? -pos.x : pos.x, 1, PlatformCount * 10), Quaternion.identity));
        }
        GameObject plt2 = Instantiate(Platform, pos, Quaternion.Euler(90, 0, 0));
        Platforms.Add(plt2);
        plt2.GetComponent<Platform>().Camm = cam;
        if (UnityEngine.Random.Range(0, 6) == 2 && NtEvl < 0 && PlatformCount > 3)
        {
            plt2.GetComponent<Platform>().IsEvil = true;
            NtEvl = 3;
        }
        NtEvl--;
        PlatformCount++;
    }
    void Respawn(GameObject Platform)
    {
        Vector3 pos = new Vector3(lastposx + UnityEngine.Random.Range(3, 8), 0, PlatformCount * 10);
        if (UnityEngine.Random.Range(0, 2) == 0)
            pos = new Vector3(lastposx + UnityEngine.Random.Range(-8, -3), 0, PlatformCount * 10);
        lastposx = pos.x;
        int rndm = UnityEngine.Random.Range(0, 8);
        if (pos.x > 6)
        {
            GameObject plt = Instantiate(Platform, new Vector3(-pos.x, 0, PlatformCount * 10), Quaternion.Euler(90, 0, 0));
            Platforms.Add(plt);
            plt.GetComponent<Platform>().IsSide = true;
            plt.GetComponent<Platform>().Camm = cam;
            rndm = UnityEngine.Random.Range(0, 4);
            flip = true;
            if (UnityEngine.Random.Range(0, 6) == 1 && NtEvl < 0)
            {
                plt.GetComponent<Platform>().IsEvil = true;
                NtEvl = 3;
            }
        }
        else
        {
            flip = false;
        }
        if (rndm == 2 && PlatformCount > 2)
        {
            GameObject coin = Instantiate(Coin, new Vector3((flip) ? -pos.x : pos.x, 1, PlatformCount * 10), Quaternion.identity);

            Coins.Add(coin);
            if (attractor)
            {
                coin.GetComponent<SphereCollider>().radius = coin.GetComponent<SphereCollider>().radius * 2f;
            }
        }
        //GameObject plt2 = Instantiate(Platform, pos, Quaternion.Euler(90, 0, 0));
        //Platforms.Add(plt2);
        //plt2.GetComponent<Platform>().Camm = cam;
        Platform.transform.position = pos;
        Platform.GetComponent<Animator>().Rebind();
        Platform.GetComponent<Platform>().IsEvil = false;
        if (UnityEngine.Random.Range(0, 6) == 2 && NtEvl < 0 && PlatformCount > 3)
        {
            Platform.GetComponent<Platform>().IsEvil = true;

            NtEvl = 3;
        }
        Platform.GetComponent<Platform>().initialize();
        NtEvl--;
        PlatformCount++;

    }
}
