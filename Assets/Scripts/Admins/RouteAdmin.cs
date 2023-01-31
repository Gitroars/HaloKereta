using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public class RouteAdmin : MonoBehaviour
{
    public DatabaseManager dm;
    private Dictionary<string,int> stationDict = new Dictionary<string,int>();
    private List<string>  stationList = new List<string>();
    public TMP_Dropdown originDropdown,destinationDropdown;
    public TMP_InputField newTicketPriceField;

    public TMP_Text priceText;
    [FormerlySerializedAs("button")] public Button priceButton;
    
    private string stationOrigin, stationDestination;

    private int ticketRouteId,ticketPrice;

    private int originId = 0, destId = 0;
    

    public GameObject warnText;
    // Start is called before the first frame update
    void Start()
    {
        stationDict = dm.GetStationDict();
        
        AddStationList();
        
        
        originDropdown.onValueChanged.AddListener(delegate
        {
            
            
            SetCurrentOriginId();
            if (originId != destId)
            {
                ValidDropdownChange();
            }
            else
            {
                InvalidDropdownChange();
            }
        });
        
        destinationDropdown.onValueChanged.AddListener(delegate
        {
            
            SetCurrentOriginId();
            if (originId != destId)
            {
                ValidDropdownChange();
            }
            else
            {
                InvalidDropdownChange();
            }
        });
    }

    public void ValidDropdownChange()
    {
        CalculatePrice();
        priceButton.interactable = true;
        warnText.SetActive(false);
    }

    public void InvalidDropdownChange()
    {
        priceButton.interactable = false;
        warnText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        
        
    }

    void AddStationList()
    {
        //A string list from station dictionary
        foreach (KeyValuePair<string,int> ele in stationDict)
        {
            stationList.Add(ele.Key);
        }
        //Set dropdown values based on string list
        originDropdown.ClearOptions();
        destinationDropdown.ClearOptions();
        
        foreach (string station in stationList)
        {
            originDropdown.options.Add(new TMP_Dropdown.OptionData(){text = station});
            destinationDropdown.options.Add(new TMP_Dropdown.OptionData(){text = station});
        }
        
        
        originDropdown.RefreshShownValue();
        destinationDropdown.RefreshShownValue();
        
    }
    
    

    void CalculatePrice()
    {
        ticketRouteId = dm.GetRouteIdFromStations(originId, destId);
        ticketPrice = dm.GetRoutePriceFromId(ticketRouteId);
        priceText.text = "RP. "+ ticketPrice.ToString();

    }

    public void SetNewPrice()
    {
        int newTicketPrice = 0;
        if (newTicketPriceField.text != null)
        {
            newTicketPrice = Convert.ToInt32(newTicketPriceField.text);
        }
        
        if (newTicketPrice> 0)
        {
            dm.SetRoutePrice(ticketRouteId,newTicketPrice);
            priceText.text = newTicketPrice.ToString();
        }
        
    }

    

    private void SetCurrentOriginId()
    {
        originId = stationDict[originDropdown.options[originDropdown.value].text];
        destId = stationDict[destinationDropdown.options[destinationDropdown.value].text];
    }

    
}
