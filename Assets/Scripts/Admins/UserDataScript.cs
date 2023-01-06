using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UserDataScript : MonoBehaviour
{
    public DatabaseManager dm;
    private List<DatabaseManager.AppUser> _userList = new List<DatabaseManager.AppUser>();

    private int userAmount = 0;

    private int currentPageIndex = 0;

    private int currentUserId = 0;

    public GameObject userTemplate;

    public TMP_Text nameText, ageText, genderText, emailText, numberText;
    public GameObject prevOngoingButton, nextOngoingButton;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
            string currentUserName = _userList[currentPageIndex].FullName;
            char currentUserSex = _userList[currentPageIndex].Sex;
            string currentUserEmail = _userList[currentPageIndex].Email;
            string currentUserPhoneNumber = _userList[currentPageIndex].MobileNumber;
            nameText.text = currentUserName;
            genderText.text = currentUserSex.ToString();
            emailText.text = currentUserEmail;
            numberText.text = currentUserPhoneNumber;
            
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
}
