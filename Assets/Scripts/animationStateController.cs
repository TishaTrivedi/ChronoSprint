using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    Rigidbody rb; // Removed public access as it's assigned in Start() method
    int IsJumpingHash;
    int isRollingHash;

    bool wasJumping;
    bool wasRolling;
    bool isChangingLane = false;

    public float laneWidth = 2f;
    public float currentXPosition = 0.0f;
    public float currentLane = 0f;
    public float moveSpeed = 10f;
    public float forwardSpeed = 10f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        IsJumpingHash = Animator.StringToHash("IsJumping");
        isRollingHash = Animator.StringToHash("isRolling");

        currentXPosition = currentLane * laneWidth;
        transform.position = new Vector3(currentXPosition, transform.position.y, transform.position.z);
    }

    void Update()
    {
        MoveForward(); // Call method to move player forward

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

        if (!isChangingLane)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLane(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveLane(1);
            }
        }
    }

    void MoveForward()
    {
        // Move the player forward
        Vector3 forwardMove = transform.forward * forwardSpeed * Time.deltaTime;
        transform.position += forwardMove;
    }

    void MoveLane(int direction)
    {
        isChangingLane = true;

        float targetLane = currentLane + direction;
        targetLane = Mathf.Clamp(targetLane, -1, 1);
        float targetX = targetLane * laneWidth;
        currentLane = targetLane;

        targetX = Mathf.Clamp(targetX, -4.48f, 4.48f);
        currentXPosition = targetX;

        transform.position = new Vector3(currentXPosition, transform.position.y, transform.position.z);

        isChangingLane = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "lethal")
        {
            Destroy(gameObject);
        }
    }
}
