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
    public TextMeshProUGUI ItemCollected;

    [Header("Settings")]
    public KeyCode pickupKey = KeyCode.E;    
    public KeyCode dismissKey = KeyCode.Space;
    public float messageDuration = 3f;


    private bool isPlayerNearby = false;
    private bool isMessageVisible = false;

    public SpriteRenderer spriteRenderer;
    public Collider2D itemCollider;
    private static int totalCollected = 0;

    void Start()
    {
        pickupUI?.SetActive(false);
        itemMessageUI?.SetActive(false);

        spriteRenderer = spriteRenderer ?? GetComponent<SpriteRenderer>();
        itemCollider = itemCollider ?? GetComponent<Collider2D>();

        UpdateItemCollectedUI();
    }

   
    void Update()
    {
        if(messageDuration <= 2)
        {
            messageDuration = 3;
        }
        if(messageDuration >= 4)
        {
            messageDuration = 3;
        }
        if (isPlayerNearby && Input.GetKeyDown(pickupKey))
        {
            HandleItemPickup();
        }


        if (isMessageVisible && Input.GetKeyDown(dismissKey))
        {
            HideItemMessage();
        }
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
    private void HandleItemPickup()
    {
   
        pickupUI?.SetActive(false);

        if (itemMessageText != null)
        {
            itemMessageText.text = $"Picked up: {itemName}\n{description}";
        }

        itemMessageUI?.SetActive(true);
        isMessageVisible = true;


        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (itemCollider != null) itemCollider.enabled = false;



        Invoke(nameof(DestroyItem), messageDuration);

        totalCollected++;
        UpdateItemCollectedUI();


    }
    private void HideItemMessage()
    {
        itemMessageUI?.SetActive(false);
        isMessageVisible = false;
      


        DestroyItem();
       
    }
    private void DestroyItem()
    {
        itemMessageText.text = $"";
        itemMessageUI.SetActive(false);
        Destroy(gameObject);
    }
    private void UpdateItemCollectedUI()
    {
        if (ItemCollected != null)
        {
            ItemCollected.text = $"x{totalCollected}";
        }
    }
}
