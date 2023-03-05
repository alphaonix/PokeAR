using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseFunc : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;

    public void hide()
    {
        object1.SetActive(false);
        object2.SetActive(false);
        object3.SetActive(false);
    }

    public void show()
    {
        object1.SetActive(true);
        object2.SetActive(true);
        object3.SetActive(true);
    }
}
