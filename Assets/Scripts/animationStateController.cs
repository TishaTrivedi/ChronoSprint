using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    public Rigidbody rb; // Rigidbody component
    int IsJumpingHash;
    int isRollingHash;

    bool wasJumping;
    bool wasRolling;
    bool isChangingLane = false; // Flag to track if a lane change is in progress

    public float laneWidth = 2f; // Width of each lane
    public float currentXPosition = 0.0f; // Initial X position of the player
    public float currentLane = 0f; // Current lane (0 for middle, negative for left, positive for right)
    public float moveSpeed = 10f; // Speed of movement (units per second)
    public float forwardSpeed =10f; // Forward speed of the player
    
    public float speed=10f;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        IsJumpingHash = Animator.StringToHash("IsJumping");
        isRollingHash = Animator.StringToHash("isRolling");

        // Adjust the initial position based on the starting lane
        currentXPosition = currentLane * laneWidth;
        transform.position = new Vector3(currentXPosition, transform.position.y, transform.position.z);
    }

    void Update()
    {
     UnityEngine.Debug.Log(rb.position);
//        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;

// // Apply forward movement to the Rigidbody's position
//     rb.MovePosition(rb.position + forwardMove);


        // Check for jump input (using arrow keys)
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            wasJumping = true;
            animator.SetBool(IsJumpingHash, true);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) && wasJumping)
        {
            wasJumping = false;
            animator.SetBool(IsJumpingHash, false);
        }

        // Check for roll input (using arrow keys)
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            wasRolling = true;
            animator.SetBool(isRollingHash, true);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) && wasRolling)
        {
            wasRolling = false;
            animator.SetBool(isRollingHash, false);
        }

        // Check for movement input (using arrow keys) only if not already changing lanes
        if (!isChangingLane)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLane(-1); // Move left
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveLane(1); // Move right
            }
        }
    }
    void onCollisionEnter(Collision other)
         {
            if (other.gameObject.tag== "lethal")
            {
                Destroy(gameObject);
            }
         }

   void MoveLane(int direction)
{
    // Set the flag to indicate a lane change is in progress
    isChangingLane = true;

    // Calculate the target lane
    float targetLane = currentLane + direction;

    // Clamp the target lane to stay within the available lanes
    targetLane = Mathf.Clamp(targetLane, -1, 1);

    // Calculate the target position based on the lane width
    float targetX = targetLane * laneWidth;

    // Update the current lane
    currentLane = targetLane;

    // Clamp the target X position to stay within the screen boundaries
    targetX = Mathf.Clamp(targetX, -4.48f, 4.48f); // Adjust the range based on your screen size and player position

    // Update the current X position
    currentXPosition = targetX;

    // Move the player to the target position while maintaining the Z-axis position
    transform.position = new Vector3(currentXPosition, transform.position.y, transform.position.z);

    // Reset the flag to allow for another lane change
    isChangingLane = false;
}



    // private void FixedUpdate()
    // {
    //     // Move the player forward
    //     Vector3 forwardMove = transform.forward * forwardSpeed * Time.fixedDeltaTime;
    //     rb.MovePosition(rb.position + forwardMove);
    // }
}
