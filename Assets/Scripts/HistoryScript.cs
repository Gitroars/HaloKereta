using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HistoryScript : MonoBehaviour
{
    public DatabaseManager dm;




    private int _userId;

   
    private List<DatabaseManager.Ticket> _historyTicketList = new List<DatabaseManager.Ticket>();
    private int ticketAmount = 0;
    private int currentPageIndex = 0;
    private int currentTicketId = 0;
    public GameObject ticketTemplate;




    public TMP_Text stationsText, dateText,idText, priceText, walletText;
    public GameObject prevOngoingButton, nextOngoingButton;


    // Start is called before the first frame update
    void Start()
    {
        _userId = PlayerPrefs.GetInt("userId");
        
        
        RetrieveOngoingTicket();
        LoadOngoingTickets();
        DecideButtonVisible();
    } 

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void RetrieveOngoingTicket()
    {
        dm.CheckTicketExpiry();
        print("Retrieving ongoing tickets from userId " + _userId);
        _historyTicketList = dm.GetUserHistoryTickets(_userId);
        ticketAmount= _historyTicketList.Count;
        print("Found " + ticketAmount + " ongoing tickets");
    }

    


    
    

    void LoadOngoingTickets()
    {
        if (ticketAmount > 0)
        {
            currentTicketId = _historyTicketList[currentPageIndex].TicketId;
            int currentTicketRouteId = _historyTicketList[currentPageIndex].RouteId;
            Tuple<string, string> stationsTuple = dm.GetStationsFromRouteId(currentTicketRouteId);
            stationsText.text = stationsTuple.Item1 + " > " + stationsTuple.Item2;
            idText.text = "Ticket Number "+ currentTicketId.ToString();
            priceText.text = "Rp. " + dm.GetRoutePriceFromId(currentTicketRouteId).ToString();
            walletText.text = dm.GetPayTypeName(_historyTicketList[currentPageIndex].PaymentId);
            string dateTime = _historyTicketList[currentPageIndex].Time;
            dateTime.Replace(".", ":");
            dateText.text = dateTime;
            ticketTemplate.SetActive(true);
        }
        else
        {
            ticketTemplate.SetActive(false);
        }
    }

    void DecideButtonVisible()
    {
        if (currentPageIndex == 0)
        {
            prevOngoingButton.SetActive(false);
        }
        else
        { prevOngoingButton.SetActive(true);
        }

        if (currentPageIndex == ticketAmount-1)
        {
            nextOngoingButton.SetActive(false);
        }
        else
        {
            nextOngoingButton.SetActive(true);
        }
    }

    public void PreviousOngoingTicket()
    {
        if (currentPageIndex != 0)
        {
            currentPageIndex--;
            DecideButtonVisible();
            LoadOngoingTickets();
        }
        
    }
    public void NextOngoingTicket()
    {
        if (currentPageIndex != _historyTicketList.Count)
        {
            currentPageIndex++;
            DecideButtonVisible();
            LoadOngoingTickets();
        }
        
    }
}
