using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Registration : MonoBehaviour
{
    public DatabaseManager dm;
    public GameObject profileCanvas, pinCanvas;
    public TMP_InputField phoneNumberField,nameField,emailField,createPinField,confirmPinField;

    private string phone_number,name,email;
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
        phone_number = phoneNumberField.text;
        name = nameField.text;
        email = emailField.text;
        profileCanvas.SetActive(false);
        pinCanvas.SetActive(true);
    }

    // public void CompleteRegistration()
    // {
    //     string createPin = createPinField.text;
    //     string confirmPin = confirmPinField.text;
    //     if (createPin == confirmPin)
    //     {
    //         dm.AddUser(phone_number,name,email,createPin);
    //         SceneManager.LoadScene("HomePage");
    //     }
    // }
}
