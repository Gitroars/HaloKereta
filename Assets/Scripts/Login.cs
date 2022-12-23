using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public TMP_InputField phoneNumberField;
    public DatabaseManager dm;
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
        string phoneNumber = phoneNumberField.text;
        dm.CheckUserNumber(phoneNumber);
    }

    public void AttemptLogin()
    {
        
    }
    
    public void ToRegisterPage()
    {
        SceneManager.LoadScene("(3) RegistrationPage 1");
    }
}
