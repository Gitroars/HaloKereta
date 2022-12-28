using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Registration : MonoBehaviour
{
    public DatabaseManager dm;
    public GameObject profileCanvas, pinCanvas;
    public TMP_InputField phoneNumberField,nameField,emailField,createPinField,confirmPinField;
    private Dropdown genderDropdown;

    private string phoneNumber,name,email,gender, birthDate;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void ContinueRegistration()
    {
        phoneNumber = "62"+phoneNumberField.text;
        name = nameField.text;
        email = emailField.text;
        gender = genderDropdown.options[genderDropdown.value].text;
        

        bool nameValid = IsNameValid(name);
        bool phoneNumberValid = IsNumberValid(phoneNumber);
        bool emailValid = IsEmailValid(email);
        bool numberExist = dm.CheckUserNumber(phoneNumber);
        bool emailExist = dm.CheckUserEmail(email);
        
        if(nameValid && phoneNumberValid && emailValid && !numberExist && !emailExist)
        {
            profileCanvas.SetActive(false);
            pinCanvas.SetActive(true);
        }
        
        
    }

    private bool IsNameValid(string name)
    {
        bool nameValid;
        if (name.Length > 0 && name.Length < 60)
        {
            nameValid = true;
        }
        else
        {
            nameValid = false;
        }

        return nameValid;
    }
    private bool IsNumberValid(string phoneNumber)
    {
        bool numberValid;
        if (phoneNumber.Length >= 10)
        {
            numberValid = true;
        }
        else
        {
            numberValid = false;
        }
        return numberValid;
    }
    private bool IsEmailValid(string email)
    {
        var valid = true;
        try
        {
            var emailAddress = new MailAddress(email);
        }
        catch
        {
            valid = false;
        }
        return valid;
    }

    public void CompleteRegistration()
    {
        string createPin = createPinField.text;
        string confirmPin = confirmPinField.text;
        if (createPin == confirmPin)
        {
            dm.AddUser(phoneNumber,name,email,createPin);
            SceneManager.LoadScene("(1) HomePage");
        }
    }
}
