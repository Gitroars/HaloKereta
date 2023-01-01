using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TicketPayment : MonoBehaviour
{
    public DatabaseManager dm;
    private string ticketID, origin, destination, payment;
    private int price;
    private int userID,originId,destId,routeId;
    private bool roundTrip = false;

    // Start is called before the first frame update
    void Start()
    {
        userID = PlayerPrefs.GetInt("userId");

        ticketID = GenerateTicketID();
        



        originId = PlayerPrefs.GetInt("stationOriginId");
        destId = PlayerPrefs.GetInt("stationDestinationId");
        routeId = dm.GetRouteIdFromStations(originId, destId);
        origin  = PlayerPrefs.GetString("stationOriginName");
       
        
        destination = PlayerPrefs.GetString("stationDestName");
        
        
        payment = PlayerPrefs.GetString("paymentTypeName");
        
        
        price = PlayerPrefs.GetInt("ticketPrice");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ConfirmTicketPurchase()
    {
       
        string currentDate = GetCurrentDate();
        string currentTime = GetCurrentTime();
        int paymentId = PlayerPrefs.GetInt("paymentTypeId");
        
        dm.AddTickets( 1, userID, paymentId,routeId,currentDate,currentTime);
        print("Added New Ticket with the following credentials");
        print("Timestamp: "+ currentDate + currentTime);

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
        string date= System.DateTime.UtcNow.ToLocalTime().ToString("yyyy-M-d");
        return date;
    }

    string GetCurrentTime()
    {
        string time= System.DateTime.UtcNow.ToLocalTime().ToString("HH:mm:ss");
        time = time.Replace(".", ":");
        return time;
    }

    

    
}
