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
    
    
     

    public GameObject ticketTemplate;

    private GameObject t;
    // Start is called before the first frame update
    void Start()
    {
        _userId = PlayerPrefs.GetInt("userId");
        
        
        RetrieveOngoingTicket();
        RetrieveHistoryTickets();
        DisplayOngoingTickets();
    } 

    // Update is called once per frame
    void Update()
    {
        
    }

    void RetrieveOngoingTicket()
    {
        _ongoingTicketList = dm.GetUserOngoingTickets(_userId);
        
    }

    void RetrieveHistoryTickets()
    {
        _historyTicketList = dm.GetUserHistoryTickets(_userId);
    }


    
    

    void DisplayOngoingTickets()
    {
        
        int N = _ongoingTicketList.Count;
        for (int i = 0; i < N; i++)
        {
            t = Instantiate(ticketTemplate, ticketTemplate.transform);
            int routeId = _ongoingTicketList[i].RouteId;
            Tuple<string, string> stations = dm.GetStationsFromRouteId(routeId);
                
            string ticketId = _ongoingTicketList[i].TicketId;
            string date = _ongoingTicketList[i].Date;
            string time = _ongoingTicketList[i].Time;
            int routePrice = dm.GetRoutePriceFromId(routeId);
            int payId = _ongoingTicketList[i].PaymentId;
            int statusId = _ongoingTicketList[i].StatusId;
            t.transform.GetChild(0).GetComponent<Text>().text = stations.Item1 + " > " + stations.Item2;
            t.transform.GetChild(1).GetComponent<Text>().text = date + " - " + time;
            t.transform.GetChild(2).GetComponent<Text>().text = ticketId;
            t.transform.GetChild(3).GetComponent<Text>().text = routePrice.ToString();
            t.transform.GetChild(4).GetComponent<Text>().text = dm.GetPayTypeName(payId);
            // Button tapButton = t.transform.GetChild(5).GetComponent<Button>();
            // void onClick()
            // {
            //     dm.ChangeTicketStatus(ticketId,statusId+1);
            // }
            // tapButton.onClick.AddListener(onClick);
            
            string tapValue = "";
            switch (statusId)
            {
                case 1: tapValue = "TAP IN"; break;
                    case 2: tapValue = "TAP OUT"; break;
                        default: break;
                            
            }
            t.transform.GetChild(5).GetChild(0).GetComponent<Text>().text = tapValue;
        }
            
        
    }
    
}
