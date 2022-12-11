using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Registration : MonoBehaviour
{
    private DatabaseManager dm;
    [Header("InputField Properties")]
    public InputField phoneNumberField,nameField,emailField,pinField;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SubmitRegistration()
    {
        string phone_number = phoneNumberField.text;
        string name = nameField.text;
        string email = emailField.text;
        string pin = pinField.text;
        dm.AddUser();
    }
}
