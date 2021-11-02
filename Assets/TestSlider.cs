using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TestSlider : MonoBehaviour
{
    public Image barImage;
    private float amount = 0.1f;
    private float fillAmount =0.05f;
    // Start is called before the first frame update
    private void Awake()
    {
        barImage = GetComponent<Image>();
    }

    private void Update()
    {
        barImage.fillAmount += Time.deltaTime*fillAmount;
        if (Input.GetKeyDown(KeyCode.E))
        {
            barImage.fillAmount -= amount;
        }
    }
    // Update is called once per frame

}


