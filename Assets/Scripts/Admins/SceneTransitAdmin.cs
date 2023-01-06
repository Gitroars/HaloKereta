using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransitAdmin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLoginPage()
    {
        SceneManager.LoadScene("(2.0) LoginPageAdmin");
    }

    public void OpenRegistrationPage()
    {
        SceneManager.LoadScene("(3.0) RegistrationPageAdmin");
    }

    public void OpenMenuPage()
    {
        SceneManager.LoadScene("(4.0) MenuPageAdmin");
    }

    public void OpenUserAdminPage()
    {
        SceneManager.LoadScene("(5.0) UserPageAdmin");
    }
}
