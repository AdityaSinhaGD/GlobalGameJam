using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float gravity = 18f;
    private Vector3 velocity;
    private float yaw;
    private float pitch;
    private float verticalVelocity;

    public float mouseSensitivity = 10;

    float smoothYaw;
    float smoothPitch;

    float yawSmoothV;
    float pitchSmoothV;

    float yawInput;
    float pitchInput;

    public Vector2 pitchMinMax = new Vector2(-40, 85);
    public float rotationSmoothTime = 0.1f;

    private CharacterController characterController;
    private ObjectInteractionController objectInteractionController;

    [SerializeField] private Camera fpsCamera;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        objectInteractionController = GetComponent<ObjectInteractionController>();
        fpsCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.state == GameManager.GameState.running)
        {
            if (objectInteractionController.objectBeingExamined != null)
            {
                ExamineObject(objectInteractionController.objectBeingExamined);
            }
            else
            {
                ProcessPlayerMovement();
                ProcessPlayerLook();
            }
            
        }
        
    }

    private void ProcessPlayerMovement()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Vector3 inputDir = new Vector3(input.x, 0, input.y).normalized;
        Vector3 worldInputDir = transform.TransformDirection(inputDir);

        velocity = movementSpeed * worldInputDir;

        verticalVelocity -= gravity * Time.deltaTime;
        velocity = new Vector3(velocity.x, verticalVelocity, velocity.z);

        characterController.Move(velocity * Time.deltaTime);
    }

    private void ProcessPlayerLook()
    {
        float mX = Input.GetAxisRaw("Mouse X");
        float mY = Input.GetAxisRaw("Mouse Y");

        // Verrrrrry gross hack to stop camera swinging down at start
        float mMag = Mathf.Sqrt(mX * mX + mY * mY);
        if (mMag > 5)
        {
            mX = 0;
            mY = 0;
        }

        yaw += mX * mouseSensitivity;
        pitch -= mY * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
        smoothPitch = Mathf.SmoothDampAngle(smoothPitch, pitch, ref pitchSmoothV, rotationSmoothTime);
        smoothYaw = Mathf.SmoothDampAngle(smoothYaw, yaw, ref yawSmoothV, rotationSmoothTime);
        transform.eulerAngles = Vector3.up * smoothYaw;
        fpsCamera.transform.localEulerAngles = Vector3.right * smoothPitch;
    }

    public void ExamineObject(GameObject obj)
    {
        yawInput = Input.GetAxisRaw("Mouse X") * 1.5f;
        pitchInput = Input.GetAxisRaw("Mouse Y") * 1.5f;

        if (Mathf.Abs(pitchInput) > Mathf.Abs(yawInput))
            obj.transform.Rotate(transform.right, pitchInput, Space.World);

        else
            obj.transform.Rotate(transform.up, -yawInput, Space.World);
    }
}
