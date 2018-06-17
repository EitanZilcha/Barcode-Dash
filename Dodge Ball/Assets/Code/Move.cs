using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Move : MonoBehaviour
{
    public float Speed;
    public float JumpSpeed;
    private Rigidbody RB;
    public Manager manager;
    public bool IsGrounded;
    public bool Test;
    private float JumpTimer;
    float unconttime;
    int badplat = 0;
    float AirTime;
    float Senboost = 1;
    bool doubler;
    // Use this for initialization
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        if (Application.platform == RuntimePlatform.Android)
        {
            Test = false;
        }
        else
        {
            Test = true;
        }
        if (PlayerPrefs.GetInt("Sensitivity Boost") == 1)
        {
            Senboost = 1.35f;

        }
        if (PlayerPrefs.GetInt("Coin Doubler") == 1)
        {
            doubler=true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
        {
            // Jump();
            JumpTimer = .2f;
        }
        unconttime -= Time.deltaTime;
        JumpTimer -= Time.deltaTime;
        if (transform.position.y < -2)
        {
            if (!manager.Lost)
            {
                manager.Lose();
                
            }
        }
    }
    void FixedUpdate()
    {
        float Haxis = (Test) ? Input.GetAxis("Horizontal") : CrossPlatformInputManager.GetAxis("Horizontal");
        if (unconttime>0)
        {
            Haxis = UnityEngine.Random.Range(-1.5f, 1.5f);
        }
        if (manager.Started)
        {
            RB.velocity = new Vector3(Senboost* Speed * Haxis, RB.velocity.y, 16 + manager.TimePassed / 3);
            if (!Physics.Raycast(transform.position,Vector3.down, Mathf.Infinity)&&!manager.Lost)
            {
                AirTime += Time.deltaTime;

                if (AirTime > 20)
                {
                    manager.GetComponent<AchivementManager>().EarnAchievement("Bird Soul");
                }
            }
        }
    }
    void Jump()
    {
        JumpTimer = 0;
        if (manager.Started&&IsGrounded)
        {
            RB.velocity = new Vector3(0, JumpSpeed, 0);
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        IsGrounded = true;
        
        if (JumpTimer>0)
        {
            Jump();
            manager.NumOfJumps++;
            Debug.Log(manager.NumOfJumps);
            
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.GetComponent<Platform>()!=null)
        if (collision.gameObject.GetComponent<Platform>().IsEvil)
        {
            if (!manager.Lost)
            {
                    //manager.Lose();

                    Handheld.Vibrate();
                    unconttime = 0.5f;
                    manager.GetComponent<AchivementManager>().EarnAchievement("Pain In The Ball");
                    badplat++;
                    manager.NumOfBads++;
                    if(badplat>=12)
                    manager.GetComponent<AchivementManager>().EarnAchievement("Living Dangerously");

            }
        }
        if(!manager.Lost)
        if (collision.gameObject.CompareTag("Coin"))
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Play");
            collision.enabled = false;

            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
                manager.GetComponent<Counter>().AddCoin();
                manager.NumOfCoins++;
                if (doubler)
                {
                    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
                    manager.GetComponent<Counter>().AddCoin();
                    manager.NumOfCoins++;
                }
            }
    }
}
