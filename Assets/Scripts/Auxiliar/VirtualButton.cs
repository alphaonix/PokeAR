using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VirtualButton: MonoBehaviour
{
    public VirtualButtonBehaviour vbBtnObj;
    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        vbBtnObj.RegisterOnButtonPressed(OnButtonPressed);
        cube.SetActive(false);
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        cube.SetActive(true);
        Debug.Log("BTN Pressed");
    }




}
