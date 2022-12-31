using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TicketPayType : MonoBehaviour
{
    public DatabaseManager dm;
    public TMP_Dropdown typeDropdown;

    private Dictionary<string, int> paymentTypeDict;

    private List<string> paymentTypeList = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        dm = GetComponent<DatabaseManager>();
        paymentTypeDict = dm.GetPayTypeDict();
        AddPaymentTypeList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddPaymentTypeList()
    {
        foreach (KeyValuePair<string,int> ele in paymentTypeDict)
        {
            paymentTypeList.Add(ele.Key);
        }
        typeDropdown.ClearOptions();
        
        foreach (string paymentType in paymentTypeList)
        {
            print(paymentType);
            typeDropdown.options.Add(new TMP_Dropdown.OptionData(){text = paymentType });
        }
        typeDropdown.RefreshShownValue();
    }

    public void ToPayment()
    {
        string paymentType = typeDropdown.options[typeDropdown.value].text;
        int paymentId = paymentTypeDict[typeDropdown.options[typeDropdown.value].text];
        PlayerPrefs.SetString("paymentTypeName",paymentType);
        PlayerPrefs.SetInt("paymentTypeId",paymentId);
    }

    
}
