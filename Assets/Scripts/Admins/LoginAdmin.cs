using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginAdmin : MonoBehaviour
{
    public GameObject numberCanvas, pinCanvas;
    public TMP_InputField phoneNumberField,pinNumberField;
    public DatabaseManager dm;
    public GameObject warningPhoneText,warningPinText;

    private string mobileNumber, pinNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PhoneLogin()
    {
        mobileNumber = "62"+phoneNumberField.text;
        print("Current Number: "+mobileNumber);
        bool mobileNumberExist = dm.CheckAdminNumber(mobileNumber);
        
        if (mobileNumberExist)
        {
            print("Mobile Number Found");
            numberCanvas.SetActive(false);
            pinCanvas.SetActive(true);
        }
        else
        {
            warningPhoneText.SetActive(true);
        }
    }

    public void AttemptLogin()
    {
        pinNumber = pinNumberField.text;
        bool pinIsCorrect = dm.CheckAdminPassword(mobileNumber,pinNumber);
        if (pinIsCorrect)
        {
            int currentAdminID = dm.GetAdminID(mobileNumber);
            PlayerPrefs.SetInt("adminId",currentAdminID);
            SceneManager.LoadScene("(4) MenuPageAdmin");
        }
        else if(!pinIsCorrect)
        {
            warningPinText.SetActive(true);
        }
    }
    
    
}