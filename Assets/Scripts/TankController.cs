using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class TankController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float rotationSpeed = 100f;

    [Header("Acceleration")]
    [SerializeField] private float acceleration = 3f;
    [SerializeField] private float deceleration = 5f;
    [SerializeField] private float turnAcceleration = 6f;

    private Rigidbody tankRigidbody;

    private float targetMoveInput;
    private float targetTurnInput;
    private float currentMoveInput;
    private float currentTurnInput;
  
    private void Awake()
    {
	    tankRigidbody = GetComponent<Rigidbody>();
    }
	private void Update()
	{
        targetMoveInput = Input.GetAxisRaw("Vertical");
        targetTurnInput = Input.GetAxisRaw("Horizontal");
	}
    private void FixedUpdate()
    {
      SmoothInput();
      MoveTank();
    }
    private void SmoothInput()
    {
        float moveChangeSpeed = targetMoveInput == 0f ? deceleration : acceleration;
        currentMoveInput = Mathf.MoveTowards(currentMoveInput, targetMoveInput, moveChangeSpeed * Time.fixedDeltaTime);
        currentTurnInput = Mathf.MoveTowards(currentTurnInput, targetTurnInput, turnAcceleration * Time.fixedDeltaTime);
    }

    private void MoveTank()
    {
        float turn = currentTurnInput * rotationSpeed * Time.fixedDeltaTime;
        Quaternion rotationDelta = Quaternion.Euler(0f, turn, 0f);
        Quaternion nextRotation = tankRigidbody.rotation * rotationDelta;
        
        Vector3 moveDirection = nextRotation * Vector3.forward;
        Vector3 nextPosition = tankRigidbody.position + moveDirection * currentMoveInput * moveSpeed * Time.fixedDeltaTime;
        tankRigidbody.MoveRotation(nextRotation);
        tankRigidbody.MovePosition(nextPosition);
    }
}
