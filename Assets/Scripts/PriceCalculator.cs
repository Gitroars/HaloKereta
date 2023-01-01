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
    public Button button;
    
    private string stationOrigin, stationDestination;

    private int ticketPrice;

    private int originId = 0, destId = 0;
    private int roundTrip = 0;

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
        button.interactable = true;
        warnText.SetActive(false);
    }

    public void InvalidDropdownChange()
    {
        button.interactable = false;
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
        
        ticketPrice = dm.GetRoutePrice(originId, destId);
        if (roundTrip!=0)
        {
            ticketPrice *= 2;
            PlayerPrefs.SetInt("roundTrip",1);
        }
        priceText.text = "BUY TICKET - RP. "+ ticketPrice.ToString();

    }

    public void ContinuePurchase()
    {
        if (originId != destId)
        {
            PlayerPrefs.SetString("stationOriginName", originDropdown.options[originDropdown.value].text);
            PlayerPrefs.SetString("stationDestName",destinationDropdown.options[destinationDropdown.value].text);       
            PlayerPrefs.SetInt("stationOriginId",originId);
            PlayerPrefs.SetInt("stationDestinationId",destId);
            PlayerPrefs.SetInt("ticketPrice",ticketPrice);
        }
        
        
        
    }

    private void SetCurrentOriginId()
    {
        originId = stationDict[originDropdown.options[originDropdown.value].text];
        destId = stationDict[destinationDropdown.options[destinationDropdown.value].text];
    }

    
}
