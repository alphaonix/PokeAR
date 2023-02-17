using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidden : MonoBehaviour
{
    public GameObject DontLook;

        void Start()
        {
            DontLook.SetActive(false);
            
        }
    
}
