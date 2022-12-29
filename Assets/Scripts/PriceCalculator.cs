using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PriceCalculator : MonoBehaviour
{
    public DatabaseManager dm;
    public TMP_Dropdown originDropdown,destinationDropdown;

    public TMP_Text priceText;

    private string stationOrigin, stationDestination;

    private int ticketPrice;
    // Start is called before the first frame update
    void Start()
    {
        
        CalculatePrice();
        originDropdown.onValueChanged.AddListener(delegate
        {
            CalculatePrice();
        });
        
        destinationDropdown.onValueChanged.AddListener(delegate
        {
            CalculatePrice();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        
        
    }

    void CalculatePrice()
    {
        stationOrigin = originDropdown.options[originDropdown.value].text;
        stationDestination = destinationDropdown.options[destinationDropdown.value].text;
        ticketPrice = dm.GetRoutePrice(stationOrigin, stationDestination);
        priceText.text = "BUY TICKETS - RP. "+ ticketPrice.ToString();

    }

    public void ContinuePurchase()
    {
        PlayerPrefs.SetString("stationOrigin",stationOrigin);
        PlayerPrefs.SetString("stationDestination",stationDestination);
        PlayerPrefs.SetInt("ticketPrice",ticketPrice);
        
    }

    
}
