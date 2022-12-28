using System;
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
    public TMP_InputField phoneNumberField,nameField,ageField,emailField,createPinField,confirmPinField;
    public TMP_Dropdown genderDropdown;

    private string phoneNumber,name,email,gender;
    private char newGender;
    private int age;
    
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
        age = Convert.ToInt32(ageField.text);
        email = emailField.text;
        gender = genderDropdown.options[genderDropdown.value].text;
        newGender = ConvertGender(gender);
        

        bool nameValid = IsNameValid(name);
        bool ageValid = IsAgeValid(age);
        bool phoneNumberValid = IsNumberValid(phoneNumber);
        bool emailValid = IsEmailValid(email);
        bool numberExist = dm.CheckUserNumber(phoneNumber);
        bool emailExist = dm.CheckUserEmail(email);
        
        if(nameValid && ageValid && phoneNumberValid && emailValid && !numberExist && !emailExist)
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

    private char ConvertGender(string gender)
    {
        if (gender == "Male")
        {
            return 'm';
        }
        else if (gender == "Female")
        {
            return 'f';
        }

        return 'n';
    }

    private bool IsAgeValid(int age)
    {
        bool ageValid = false;
        if (age >= 0 && age <= 150)
        {
            ageValid = true;
        }

        return ageValid;
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
            dm.AddUser(phoneNumber,name,email,createPin,newGender,age);
            SceneManager.LoadScene("(1) StartPage");
        }
    }
}
