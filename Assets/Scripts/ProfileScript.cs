using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileScript : MonoBehaviour
{
    public DatabaseManager dm;
    private int _userId;
    private DatabaseManager.AppUser currentUser = new DatabaseManager.AppUser();
    public TMP_Text nameText, ageText, genderText, emailText, numberText;
    public TMP_Dropdown yearDropdown, monthDropdown, dateDropdown, genderDropdown;
    public TMP_InputField newName, newEmail, newPhone,newPin;
    


    // Start is called before the first frame update
    void Start()
    {
        SetDateValue();
        _userId = PlayerPrefs.GetInt("userId");
        currentUser = dm.GetIndividualUserData(_userId);
        LoadUserData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadUserData()
    {
        nameText.text = currentUser.FullName;
        genderText.text = currentUser.Sex.ToString();
        emailText.text = currentUser.Email;
        numberText.text = currentUser.MobileNumber;
        ageText.text = currentUser.BirthDate;
    }
    
    public void SaveNewUserData()
    {
        

        string newNameText = newName.text;
        if (IsNameValid(newNameText))
        {
            currentUser.FullName= newNameText;
        }

        string newDate = GetDateOfBirth();

        char newSex = ConvertGender(genderDropdown.options[genderDropdown.value].text);
        

        string newEmailText = newEmail.text;
        if (newEmailText.Length>0)
        {
            currentUser.Email = newEmailText;
        }

        string newPhoneText = newPhone.text;
        if (IsNumberValid(newPhoneText))
        {
            currentUser.MobileNumber = newPhoneText;
        }
        string newPinText = newPin.text;

        if (newPinText.Length == 6)
        {
            currentUser.Pin = newPinText;
        }
        
        dm.SetUserData(currentUser.UserId,currentUser.FullName,newSex,currentUser.Email,currentUser.Pin,currentUser.MobileNumber,newDate);
        ageText.text = newDate;
        genderText.text = newSex.ToString();
        
    }

    #region DataCheck
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
        
        private string  GetDateOfBirth()
        {
        
            return yearDropdown.options[yearDropdown.value].text + "-"+ monthDropdown.options[monthDropdown.value].text + "-"+ dateDropdown.options[dateDropdown.value].text;
        }
        #endregion
}
