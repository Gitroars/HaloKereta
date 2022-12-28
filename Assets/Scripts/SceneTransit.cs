using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenuPage()
    {
        SceneManager.LoadScene("(4) MenuPage");
    }

    public void OpenStationPage()
    {
        SceneManager.LoadScene("(5) StationPage");
    }

    public void OpenPaymentPage()
    {
        SceneManager.LoadScene("(6) PaymentPage");
    }

    public void OpenTicketPage()
    {
        SceneManager.LoadScene("(7) TicketPage");
    }

    public void OpenOngoingPage()
    {
        SceneManager.LoadScene("(8) OngoingPage");
    }

    public void OpenHistoryPage()
    {
        SceneManager.LoadScene("(9) HistoryPage");
    }
    
}
