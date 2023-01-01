using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
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
        bool mobileNumberExist = dm.CheckUserNumber(mobileNumber);
        
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
        bool pinIsCorrect = dm.CheckUserPin(mobileNumber,pinNumber);
        if (pinIsCorrect)
        {
            int currentUserID = dm.GetUserID(mobileNumber);
            PlayerPrefs.SetInt("userId",currentUserID);
            SceneManager.LoadScene("(4) MenuPage");
        }
        else if(!pinIsCorrect)
        {
            warningPinText.SetActive(true);
        }
    }
    
    public void ToRegisterPage()
    {
        SceneManager.LoadScene("(3) RegistrationPage");
    }
}
