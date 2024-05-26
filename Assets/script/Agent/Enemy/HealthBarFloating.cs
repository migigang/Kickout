using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarFloating : MonoBehaviour
{

    [SerializeField] 
    private Slider slider;
    [SerializeField]
    private Color Low;
    [SerializeField]
    private Color High;

    [SerializeField]
    private Camera camera;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offset;

    public void UpdateHealthBar(float currentValue, float maxValue){
        slider.value = currentValue / maxValue;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, slider.normalizedValue);
    }



    // Update is called once per frame
    void Update()
    {
       transform.rotation = camera.transform.rotation;
       transform.position = target.position + offset;
    }
}
