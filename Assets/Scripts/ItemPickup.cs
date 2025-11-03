using UnityEngine;
using TMPro;
using System.Security.Cryptography.X509Certificates;

public class ItemPickup : MonoBehaviour
{
    [Header("Item")]
     public string itemName;

    [TextArea]
    public string description;

    [Header("UI")]
    public GameObject pickupUI;
    public GameObject itemMessageUI;
    public TextMeshProUGUI itemMessageText;




    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Test");
        }
    }
   void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("exit");
        }
    }
}
