using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Cam : MonoBehaviour
{
    private Camera Cammer;
    public Shop shp;
    public GameObject[] PS;
    public bool IsSwitching;
    public Shader[] Trailvis;
    float tlt = 0f;
    private void Start()
    {
        Cammer = GetComponent<Camera>();
        PS = shp.Trail;
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 8, transform.position.z);
        tlt = Mathf.Lerp(tlt, -Input.GetAxis("Horizontal") * 12- CrossPlatformInputManager.GetAxis("Horizontal")*12, 2* Time.deltaTime);
        transform.localRotation =Quaternion.Euler( new Vector3(30, transform.localRotation.y, tlt));
        if (Input.GetKeyDown(KeyCode.L))
        {
            //ChangeBackground(false);
            changecol();
        }
        
       
    }
    public void changecol()
    {
        if (Cammer.backgroundColor.r < 0.5)
            StartCoroutine(Fade2());
        else
        {
            StartCoroutine(Fade());
        }
    }
    public void ChangeBackground(bool White)
    {
        Debug.Log("Change");
        StartCoroutine(Change(White));
    }
    IEnumerator Fade()
    {
        IsSwitching = true;
        for (float f = 1f; f >= 0; f -= 0.2f)
        {
            Color c =Cammer.backgroundColor;
            c.r=c.g=c.b = f;
            Cammer.backgroundColor = c;
            yield return new WaitForSeconds(.05f);

        }
        for (int i = 0; i < PS.Length; i++)
        {
            if (Cammer.backgroundColor.r < 0.5)
            {
                PS[i].GetComponent<Renderer>().material.shader = Trailvis[1];
            }
            else
            {
                PS[i].GetComponent<Renderer>().material.shader = Trailvis[0];
            }
        }
        IsSwitching = false;
    }
    IEnumerator Fade2()
    {
        IsSwitching = true;
        for (float f = 1f; f >= 0; f -= 0.2f)
        {
            Color c = Cammer.backgroundColor;
            c.r = c.g = c.b =1- f;
            Cammer.backgroundColor = c;
            yield return new WaitForSeconds(.05f);
        }
        for (int i = 0; i < PS.Length; i++)
        {
            if (Cammer.backgroundColor.r < 0.5)
            {
                PS[i].GetComponent<Renderer>().material.shader = Shader.Find("Mobile/Particles/Multiply");
            }
            else
            {
                PS[i].GetComponent<Renderer>().material.shader = Shader.Find("Mobile/Particles/Additive");
            }
        }
        IsSwitching = false;
    }

    private IEnumerator Change(bool White)
    {
        if (White)
        {
            Color newcol = new Color(Cammer.backgroundColor.r + Time.deltaTime, Cammer.backgroundColor.g + Time.deltaTime, Cammer.backgroundColor.b + Time.deltaTime);
            newcol.r = newcol.g = newcol.b = Mathf.Clamp(newcol.r, 0, 255);
            Cammer.backgroundColor = newcol;
            if (newcol.r < 254)
            {
                yield return new WaitForSeconds(.1f);
            }
        }
        else
        {
            Color newcol = new Color(Cammer.backgroundColor.r - Time.deltaTime*4, Cammer.backgroundColor.g - Time.deltaTime * 4, Cammer.backgroundColor.b - Time.deltaTime * 4);
            newcol.r = newcol.g = newcol.b = Mathf.Clamp(newcol.r, 0, 255);
            Cammer.backgroundColor = newcol;
            if (newcol.r > 1)
            {
                yield return new WaitForSeconds(.1f);
            }
        }
    }
}
