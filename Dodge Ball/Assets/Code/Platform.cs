using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool IsEvil;
    public bool IsSide;
    public Camera Camm;
    private Cam cmscrpt;
    private Renderer rnd;
    // Use this for initialization
    void Start()
    {
        rnd = GetComponent<Renderer>();
        cmscrpt = Camm.GetComponent<Cam>();
        initialize();
    }
    public void initialize()
    {
        if (!IsEvil)
            rnd.material.color = new Color((Camm.backgroundColor.g < 0.5f) ? Mathf.Clamp(Camm.backgroundColor.r - 1, 0, 1) : 1 - Camm.backgroundColor.r, (Camm.backgroundColor.g < 0.5f) ? Mathf.Clamp(1 - Camm.backgroundColor.g, 0, 1) : 1 - Camm.backgroundColor.g, (Camm.backgroundColor.g < 0.5f) ? Mathf.Clamp(1 - Camm.backgroundColor.g, 0, 1) : 1 - Camm.backgroundColor.g);
        if (IsEvil)
        {
            rnd.material.color = Color.red;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cmscrpt.IsSwitching && !IsEvil)
            rnd.material.color = new Color((Camm.backgroundColor.g < 0.5f) ? Mathf.Clamp(Camm.backgroundColor.r - 1, 0, 1) : 1 - Camm.backgroundColor.r, (Camm.backgroundColor.g < 0.5f) ? Mathf.Clamp(1 - Camm.backgroundColor.g, 0, 1) : 1 - Camm.backgroundColor.g, (Camm.backgroundColor.g < 0.5f) ? Mathf.Clamp(1 - Camm.backgroundColor.g, 0, 1) : 1 - Camm.backgroundColor.g);

    }
}
