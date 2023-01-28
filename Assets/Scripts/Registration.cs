using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Registration : MonoBehaviour
{
    public DatabaseManager dm;
    public GameObject profileCanvas, pinCanvas;
    public TMP_InputField phoneNumberField,nameField,emailField,createPinField,confirmPinField;
    public TMP_Dropdown dateDropdown,monthDropdown,yearDropdown,genderDropdown;

    private string phoneNumber,userName,email,gender;
    private char newGender;
    private string date,month,year;
    
   
    
    // Start is called before the first frame update
    void Start()
    {
        SetDateValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void ContinueRegistration()
    {
        phoneNumber = "62"+phoneNumberField.text;
        userName = nameField.text;
        email = emailField.text;
        gender = genderDropdown.options[genderDropdown.value].text;
        newGender = ConvertGender(gender);
        

        bool nameValid = IsNameValid(userName);
        
        bool phoneNumberValid = IsNumberValid(phoneNumber);
        
        bool numberExist = dm.CheckUserNumber(phoneNumber);
        bool emailExist = dm.CheckUserEmail(email);
        
        print("Name valid:"+ nameValid);
        
        print("Number valid:"+ phoneNumberValid);
        
        print("Number:" +  phoneNumber + numberExist);
        print("Email:" + email+ emailExist);
        
        if(nameValid && phoneNumberValid  && !numberExist && !emailExist)
        {
            print("Proceeding to next phase of Registration...");
            profileCanvas.SetActive(false);
            pinCanvas.SetActive(true);
        }
        
        
    }

    private void SetDateValue()
    {
        dateDropdown.ClearOptions();
        for (int i = 1; i < 32; i++)
        {
            if (i < 10)
            {
                dateDropdown.options.Add(new TMP_Dropdown.OptionData(){text = "0"+i.ToString()});
            }
            else
            {
                dateDropdown.options.Add(new TMP_Dropdown.OptionData(){text = i.ToString()}); 
            }
        }
        dateDropdown.RefreshShownValue();
        
        monthDropdown.ClearOptions();
        for (int i = 1; i < 13; i++)
        {
            if (i < 10)
            {
                monthDropdown.options.Add(new TMP_Dropdown.OptionData(){text = "0"+i.ToString()});
            }
            else
            {
                monthDropdown.options.Add(new TMP_Dropdown.OptionData(){text = i.ToString()}); 
            }
            
        }
        monthDropdown.RefreshShownValue();
        
        yearDropdown.ClearOptions();
        for (int i = 1900; i < 2023; i++)
        {
            yearDropdown.options.Add(new TMP_Dropdown.OptionData(){text = i.ToString()});
        }
        yearDropdown.RefreshShownValue();
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

    private string  GetDateOfBirth()
    {
        
        return yearDropdown.options[yearDropdown.value].text + "-"+ monthDropdown.options[monthDropdown.value].text + "-"+ dateDropdown.options[dateDropdown.value].text;
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
    
    
            
    


    public void CompleteRegistration()
    {
        string createPin = createPinField.text;
        string confirmPin = confirmPinField.text;
        if (createPin.Length ==6 && createPin == confirmPin)
        {
            string dob = GetDateOfBirth();
            print(dob);
            dm.AddUser(userName,email,phoneNumber,createPin,newGender,dob);
            SceneManager.LoadScene("(4) MenuPage");
        }
    }
}
