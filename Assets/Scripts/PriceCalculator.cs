using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PriceCalculator : MonoBehaviour
{
    public DatabaseManager dm;
    private Dictionary<string,int> stationDict = new Dictionary<string,int>();
    private List<string>  stationList = new List<string>();
    public TMP_Dropdown originDropdown,destinationDropdown;

    public TMP_Text priceText;
    
    private string stationOrigin, stationDestination;

    private int ticketPrice;

    private int originId = 0, destId = 0;

    private bool firstClick = false;
    // Start is called before the first frame update
    void Start()
    {
        stationDict = dm.GetStationDict();
        
        AddStationList();
        
        
        originDropdown.onValueChanged.AddListener(delegate
        {
            UpdateDestinationList();
            
            destinationDropdown.interactable = true;
            if (originId != 0 && destId != 0)
            {
                CalculatePrice();
            }
        });
        
        destinationDropdown.onValueChanged.AddListener(delegate
        {
            
            UpdateOriginList();
            CalculatePrice();
        });
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
        originDropdown.options.Add(new TMP_Dropdown.OptionData(){text = ""});
        foreach (string station in stationList)
        {
            originDropdown.options.Add(new TMP_Dropdown.OptionData(){text = station});
        }
        originDropdown.RefreshShownValue();
        
    }
    void UpdateOriginList()
    {
        
        List<string> tempList = stationList;
        
        string currentVal = destinationDropdown.options[destinationDropdown.value].text;
        
        originDropdown.ClearOptions();
        
        tempList.Remove(currentVal);
        
        foreach (string station in tempList)
        {
            originDropdown.options.Add(new TMP_Dropdown.OptionData(){text = station});
        }
        originDropdown.RefreshShownValue();
        
    }
    void UpdateDestinationList()
    {
        
        List<string> tempList = stationList;
        
        string currentVal = originDropdown.options[originDropdown.value].text;
        
            
        
        destinationDropdown.ClearOptions();
        tempList.Remove(currentVal);
        foreach (string item in tempList)
        {
            print(item);
        }
        if (!firstClick)
        {
            destinationDropdown.options.Add(new TMP_Dropdown.OptionData(){text = ""});
            firstClick = false;
        }
        
        foreach (string station in tempList)
        {
            destinationDropdown.options.Add(new TMP_Dropdown.OptionData(){text = station});
        }
        destinationDropdown.RefreshShownValue();
    }

    void CalculatePrice()
    {
        SetCurrentOriginId();
        ticketPrice = dm.GetRoutePrice(originId, destId);
        priceText.text = "BUY TICKET - RP. "+ ticketPrice.ToString();

    }

    public void ContinuePurchase()
    {
        
;       PlayerPrefs.SetInt("stationOrigins",originId);
        PlayerPrefs.SetInt("stationDestination",destId);
        PlayerPrefs.SetInt("ticketPrice",ticketPrice);
        
    }

    private void SetCurrentOriginId()
    {
        originId = stationDict[originDropdown.options[originDropdown.value].text];
        destId = stationDict[destinationDropdown.options[destinationDropdown.value].text];
    }

    
}
