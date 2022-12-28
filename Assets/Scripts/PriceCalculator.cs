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
        string origin = originDropdown.options[originDropdown.value].text;
        string destination = destinationDropdown.options[destinationDropdown.value].text;
        int newPrice = dm.GetRoutePrice(origin, destination);
        priceText.text = "BUY TICKETS - RP. "+ newPrice.ToString();

    }
}
