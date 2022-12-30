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
    public TMP_InputField phoneNumberField,nameField,ageField,emailField,createPinField,confirmPinField;
    public TMP_Dropdown genderDropdown;

    private string phoneNumber,userName,email,gender;
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
        userName = nameField.text;
        age = Convert.ToInt32(ageField.text);
        email = emailField.text;
        gender = genderDropdown.options[genderDropdown.value].text;
        newGender = ConvertGender(gender);
        

        bool nameValid = IsNameValid(userName);
        bool ageValid = IsAgeValid(age);
        bool phoneNumberValid = IsNumberValid(phoneNumber);
        
        bool numberExist = dm.CheckUserNumber(phoneNumber);
        bool emailExist = dm.CheckUserEmail(email);
        
        print("Name valid:"+ nameValid);
        print("Age valid:"+ ageValid);
        print("Number valid:"+ phoneNumberValid);
        
        print("Number:" +  phoneNumber + numberExist);
        print("Email:" + email+ emailExist);
        
        if(nameValid && ageValid && phoneNumberValid  && !numberExist && !emailExist)
        {
            print("Proceeding to next phase of Registration...");
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
       
        if (age >= 0 && age <= 150)
        {
            return true;
        }

        return false;
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
            dm.AddUser(userName,email,phoneNumber,createPin,newGender,age);
            SceneManager.LoadScene("(1) StartPage");
        }
    }
}
