using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float PlayerMoveSpeed = 5f;

    public LayerMask Grass;

    public bool IsPlayerMoving;

    private Vector2 Movement;
    public Rigidbody2D PlayerRB;

    private Animator GeorgeAnimator;



    public float encounterCooldownTime = 2f; //devlog 2 
    private float encounterCooldown = 5f; //devlog 2


    private void Awake()
    {
        GeorgeAnimator = GetComponent<Animator>();
    }




    void Update()
    {
        if (encounterCooldown > 0)
            encounterCooldown -= Time.deltaTime;


        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");


        // remove diagonal movement
        if (Movement.y != 0)
        {
            Movement.x = 0;
        }


        // George Walk Animation
        if (Movement != Vector2.zero)
        {

                GeorgeAnimator.SetFloat("MoveX", Movement.x);
                GeorgeAnimator.SetFloat("MoveY", Movement.y);
                IsPlayerMoving = true;

                 ResetIdle();
        }
        else
        {
            // George stopped moving
            IsPlayerMoving = false;

            // set idle direction depending on last movement
            if (GeorgeAnimator.GetFloat("MoveX") < 0)
            {
                GeorgeAnimator.SetBool("LeftIdle", true);
            }
            else if (GeorgeAnimator.GetFloat("MoveX") > 0)
            {
                GeorgeAnimator.SetBool("RightIdle", true);
            }
            else if (GeorgeAnimator.GetFloat("MoveY") > 0)
            {
                GeorgeAnimator.SetBool("UpIdle", true);
            }
            else if (GeorgeAnimator.GetFloat("MoveY") < 0)
            {
                GeorgeAnimator.SetBool("DownIdle", true);
            }
        }

        EnemyEncounter();
    }
     void FixedUpdate()
    {
        // George Movement
        if (Movement != Vector2.zero)
        {
            PlayerRB.MovePosition(PlayerRB.position + Movement * PlayerMoveSpeed * Time.fixedDeltaTime);
           
        }

    }
    public void ResetIdle()
    {

        GeorgeAnimator.SetBool("LeftIdle", false);
        GeorgeAnimator.SetBool("RightIdle", false);
         GeorgeAnimator.SetBool("UpIdle", false);
        GeorgeAnimator.SetBool("DownIdle", false);
    }

    //devlog 2
    private void EnemyEncounter()
    {
        if (encounterCooldown <= 0 && IsPlayerMoving &&
              Physics2D.OverlapCircle(transform.position, 0.2f, Grass) != null)
        {
            if (Random.Range(1, 101) <= 1) 
            {
                encounterCooldown = encounterCooldownTime;
                StartEncounter();
            }
        }
    }
    private void StartEncounter()
    {
        
        SceneManager.LoadScene("EncounterScene");
    }
}
