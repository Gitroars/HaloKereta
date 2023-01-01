using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TicketScript : MonoBehaviour
{
    public DatabaseManager dm;




    private int _userId;

    private List<DatabaseManager.Ticket> _ongoingTicketList = new List<DatabaseManager.Ticket>();
    private List<DatabaseManager.Ticket> _historyTicketList = new List<DatabaseManager.Ticket>();
    private int ongoingTicketAmount = 0;
    private int currentPageIndex = 0;
    private int currentTicketId = 0;
    private int currentTicketStatusId = 0;
    public GameObject ticketTemplate;




    public TMP_Text stationsText, dateText,idText, priceText, walletText, tapText;
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

    public void OnTap()
    {
        dm.ChangeTicketStatus(currentTicketId,currentTicketStatusId +1);
        RetrieveOngoingTicket();
        LoadOngoingTickets();
    }

    void RetrieveOngoingTicket()
    {
        print("Retrieving ongoing tickets from userId " + _userId);
        _ongoingTicketList = dm.GetUserOngoingTickets(_userId);
        ongoingTicketAmount= _ongoingTicketList.Count;
        print("Found " + ongoingTicketAmount + " ongoing tickets");
    }

    


    
    

    void LoadOngoingTickets()
    {
        if (ongoingTicketAmount > 0)
        {
            currentTicketId = _ongoingTicketList[currentPageIndex].TicketId;
            int currentTicketRouteId = _ongoingTicketList[currentPageIndex].RouteId; 
            currentTicketStatusId = _ongoingTicketList[currentPageIndex].StatusId;
            print("ticket status" + currentTicketStatusId);
            Tuple<string, string> stationsTuple = dm.GetStationsFromRouteId(currentTicketRouteId);
            stationsText.text = stationsTuple.Item1 + " > " + stationsTuple.Item2;
            idText.text = "Ticket Number "+ currentTicketId.ToString();
            priceText.text = "Rp. " + dm.GetRoutePriceFromId(currentTicketRouteId).ToString();
            walletText.text = dm.GetPayTypeName(_ongoingTicketList[currentPageIndex].PaymentId);
            if (currentTicketStatusId == 1)
            {
                tapText.text = "TAP IN";
            }
            else if (currentTicketStatusId == 2)
            {
                tapText.text = "TAP OUT";
            }
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

        if (currentPageIndex == ongoingTicketAmount-1)
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
        if (currentPageIndex != _ongoingTicketList.Count)
        {
            currentPageIndex++;
            DecideButtonVisible();
            LoadOngoingTickets();
        }
        
    }
}
