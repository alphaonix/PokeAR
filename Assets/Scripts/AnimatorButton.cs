using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AnimatorButton : MonoBehaviour
{
    public VirtualButtonBehaviour vbBtn;
    public Animator jammoAni;
    
    void Start()
    {
        vbBtn.RegisterOnButtonPressed(OnButtonPressed);
        vbBtn.RegisterOnButtonReleased(OnButtonReleased);


    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        jammoAni.Play("a_Scared");
        Debug.Log("BTN Pressed Anim");
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        jammoAni.Play("a_Happy");
        Debug.Log("BTN Released Anim");
    }


    /*
     * 
     * 
     *     
    public VirtualButtonBehaviour vbBtnObj;
    public GameObject jax;
    public float scalingSpeed = 0.03f;
    public float rotationSpeed = 70.0f;
    public float translationSpeed = 5.0f;
    bool repeatRotateLeft = false;

    void Start()
    {
        vbBtnObj.RegisterOnButtonPressed(OnButtonPressed);
        jax.SetActive(false);
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        jax.SetActive(true);
        jax.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        Debug.Log("BTN Pressed");
    }

     * 
     * 
     * 
     */

}
