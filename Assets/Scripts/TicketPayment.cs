using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TicketPayment : MonoBehaviour
{
    public TMP_Text idText,dateText,originText,destinationText,paymentText,priceText;
    

    // Start is called before the first frame update
    void Start()
    {
        
        
        idText.text = GenerateTicketID();
        dateText.text = GetCurrentDate();
        originText.text = PlayerPrefs.GetString("stationOrigin");
        destinationText.text = PlayerPrefs.GetString("stationDestination");
        paymentText.text = PlayerPrefs.GetString("paymentType");
        priceText.text = "RP. "+PlayerPrefs.GetInt("ticketPrice").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Reference: https://www.youtube.com/watch?v=WaWI60hYOzo
    string GenerateTicketID() 
    {
        var allChars = "abcdefghijklmnopqrstuvwxyz0123456789";
        int length = 21;
        var randomChars = new char[length];
        for (int i = 0; i < length; i ++)
        {
            randomChars[i] = allChars[UnityEngine.Random.Range(0, allChars.Length)];
        }

        return new string(randomChars);
    }

    string GetCurrentDate()
    {
        string time = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy HH:mm");
        return time;
    }
}
