using UnityEngine;

public class TouchPlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;

    private Vector3 velocity;
    public float gravity = -9.81f;

    public bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float jumpHeight = 2f;

    private int leftFingerID, rightFingerID;
    private float halfScreen;
    private Vector2 moveInput;
    private Vector2 moveTouchStartPosition;

    private Vector2 lookInput;
    [SerializeField]private float cameraSensibility;
    private float cameraPitch;

    void Start()
    {
        leftFingerID = -1;
        rightFingerID = -1;
        halfScreen = Screen.width / 2f;
    }

    void Update()
    {
        GetTouchInput();

        if(leftFingerID != -1){
            Move();
        }
    }

    private void GetTouchInput()
    {
        for(int i = 0; i < Input.touchCount;i++){
            Touch t = Input.GetTouch(i);
            if(t.phase == TouchPhase.Began){
                if(t.position.x < halfScreen && leftFingerID == -1){
                    leftFingerID = t.fingerId;
                    moveTouchStartPosition = t.position;
                }else if(t.position.x > halfScreen && rightFingerID == -1){
                    rightFingerID = t.fingerId;
                }
            }
            if(t.phase == TouchPhase.Canceled){

            }
            if(t.phase == TouchPhase.Moved){
                if(leftFingerID == t.fingerId){
                    moveInput = t.position - moveTouchStartPosition;
                }else if(rightFingerID == t.fingerId){

                }
            }
            if(t.phase == TouchPhase.Stationary){

            }
            if(t.phase == TouchPhase.Ended){
                if(leftFingerID == t.fingerId){
                    leftFingerID = -1;
                }
            }
        }
    }

    private void Move(){
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = transform.right * moveInput.normalized.x + transform.forward * moveInput.normalized.y;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
