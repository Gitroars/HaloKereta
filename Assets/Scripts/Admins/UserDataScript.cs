using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UserDataScript : MonoBehaviour
{
    public DatabaseManager dm;
    private List<DatabaseManager.AppUser> _userList = new List<DatabaseManager.AppUser>();

    private int userAmount = 0;

    private int currentPageIndex = 0;

    private int currentUserId = 0;

    public GameObject userTemplate;

    public TMP_Text nameText, ageText, genderText, emailText, numberText;
    public TMP_Dropdown yearDropdown, monthDropdown, dateDropdown, genderDropdown;
    public TMP_InputField newName, newEmail, newPhone,newPin;
    public GameObject prevOngoingButton, nextOngoingButton;

    private string currentUserName;
    private string currentUserEmail;
    private string currentUserPhoneNumber;
    private string currentUserBirthDate ;
    private string currentUserPin;
    private char currentUserSex;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SetDateValue();
        RetrieveUserList();
        LoadUserData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RetrieveUserList()
    {
        _userList = dm.GetUserList();
        userAmount = _userList.Count;
        print("Found " + userAmount + " users");
    }

    public void LoadUserData()
    {
        if (userAmount > 0)
        {
            currentUserId = _userList[currentPageIndex].UserId;
            currentUserName = _userList[currentPageIndex].FullName;
            currentUserSex = _userList[currentPageIndex].Sex;
            currentUserEmail = _userList[currentPageIndex].Email;
            currentUserPhoneNumber = _userList[currentPageIndex].MobileNumber;
            currentUserBirthDate = _userList[currentPageIndex].BirthDate;
            currentUserPin = _userList[currentPageIndex].Pin;
            nameText.text = currentUserName;
            genderText.text = currentUserSex.ToString();
            emailText.text = currentUserEmail;
            numberText.text = currentUserPhoneNumber;
            ageText.text = currentUserBirthDate;
            userTemplate.SetActive(true);
        }
        else
        {
            userTemplate.SetActive(false);
        }
    }
    
    void DecideButtonVisible()
    {
        if (currentPageIndex == 0)
        {
            prevOngoingButton.SetActive(false);
        }
        else
        { prevOngoingButton.SetActive(true);
        }

        if (currentPageIndex == userAmount-1)
        {
            nextOngoingButton.SetActive(false);
        }
        else
        {
            nextOngoingButton.SetActive(true);
        }
    }
    
    public void PreviousUserData()
    {
        if (currentPageIndex != 0)
        {
            currentPageIndex--;
            DecideButtonVisible();
            LoadUserData();
        }
        
    }
    public void NextUserData()
    {
        if (currentPageIndex != userAmount)
        {
            currentPageIndex++;
            DecideButtonVisible();
            LoadUserData();
        }
        
    }

    public void SaveNewUserData()
    {
        

        string newNameText = newName.text;
        if (IsNameValid(newNameText))
        {
            currentUserName = newNameText;
        }

        string newDate = GetDateOfBirth();

        char newSex = ConvertGender(genderDropdown.options[genderDropdown.value].text);
        

        string newEmailText = newEmail.text;
        if (newEmailText.Length>0)
        {
            currentUserEmail = newEmailText;
        }

        string newPhoneText = newPhone.text;
        if (IsNumberValid(newPhoneText))
        {
            currentUserPhoneNumber = newPhoneText;
        }
        string newPinText = newPin.text;

        if (newPinText.Length == 6)
        {
            currentUserPin = newPinText;
        }
        
        dm.SetUserData(currentUserId,currentUserName,newSex,currentUserEmail,currentUserPin,currentUserPhoneNumber,newDate);
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

