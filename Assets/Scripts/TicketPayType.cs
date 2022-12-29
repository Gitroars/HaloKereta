using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TicketPayType : MonoBehaviour
{
    public TMP_Dropdown typeDropdown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToPayment()
    {
        string paymentType = typeDropdown.options[typeDropdown.value].text;
        PlayerPrefs.SetString("paymentType",paymentType);
    }
}
