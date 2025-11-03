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

    [Header("Settings")]
    public KeyCode pickupKey = KeyCode.E;    
    public KeyCode dismissKey = KeyCode.Space;


    private bool isPlayerNearby = false;
    private bool isMessageVisible = false;

    void Start()
    {
        pickupUI?.SetActive(false);
        itemMessageUI?.SetActive(false);
    }

   
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            pickupUI?.SetActive(true);
            isPlayerNearby = true;
        }
    }
   void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            pickupUI?.SetActive(false);
            isPlayerNearby = false;
        }
    }
}
