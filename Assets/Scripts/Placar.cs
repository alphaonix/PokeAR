using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Placar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;
    void Start()
    {
        //GameController.print()
       // slider.minValue=
        slider.onValueChanged.AddListener((v) => 
        {
            sliderText.text = v.ToString("0");
        });
    }

    void Update()
    {

    }
}
