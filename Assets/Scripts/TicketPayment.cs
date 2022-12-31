using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TicketPayment : MonoBehaviour
{
    public DatabaseManager dm;
    public TMP_Text idText,originText,destinationText,paymentText,priceText;
    private string ticketID, origin, destination, payment;
    private int price;
    private int userID;

    // Start is called before the first frame update
    void Start()
    {
        userID = PlayerPrefs.GetInt("userID");

        ticketID = GenerateTicketID();
        idText.text = ticketID;
        
        
        
        
        origin  = PlayerPrefs.GetString("stationOrigin");
        originText.text = origin;
        
        destination = PlayerPrefs.GetString("stationDestination");
        destinationText.text = destination;
        
        payment = PlayerPrefs.GetString("paymentType");
        paymentText.text = payment;
        
        price = PlayerPrefs.GetInt("ticketPrice");
        priceText.text = "RP. "+ price.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ConfirmTicketPurchase()
    {
        int paymentId = PlayerPrefs.GetInt("paymentId");
        string date = GetCurrentDate();
        string time = GetCurrentTime();
        dm.AddTickets(ticketID, 1, userID, paymentId, date, time);
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
        string date = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy");
        return date;
    }

    string GetCurrentTime()
    {
        string time = System.DateTime.UtcNow.ToLocalTime().ToString("HH:mm");
        return time;
    }

    
}
