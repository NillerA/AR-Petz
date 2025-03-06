using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowableController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] ThrowableSpawner throwableSpawner;
    [SerializeField] InputActionAsset playerInput;

    [SerializeField] float throwMaxForce;
    [SerializeField] float throwMinForce;

    private Throwable currentThrowable;
    private Vector3 spawnPosition;
    private InputAction touchPress;
    private InputAction touchPosition;

    private bool isDragging;
    private float touchSpeed;
    private Vector2 startTouchPos, endTouchPos;


    private void Awake()
    {
        if(cam == null)
        {
            cam = Camera.main;
        }

        if(playerInput == null)
        {
            playerInput = new InputActionAsset();
        }

        touchPress = playerInput.FindActionMap("MobileTouch").FindAction("Press");
        touchPosition = playerInput.FindActionMap("MobileTouch").FindAction("Position");
    }

    private void OnEnable()
    {
        touchPress.performed += OnPress;
        touchPress.canceled += OnRelease;
        touchPress.Enable();
        touchPosition.Enable();
    }

    private void OnDisable()
    {
        touchPress.performed -= OnPress;
        touchPress.canceled -= OnRelease;
        touchPress.Disable();
        touchPosition.Disable();
    }

    private void FixedUpdate()
    {
        currentThrowable = throwableSpawner.currentThrowable;
        if (!isDragging) return;

    }

    private void OnPress(InputAction.CallbackContext context)
    {
        Debug.Log("OnPress");
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            Debug.Log("OnPress if");
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Ray ray = cam.ScreenPointToRay(touchPosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == currentThrowable.gameObject)
            {
                Debug.Log("OnPress if if");
                isDragging = true;
                startTouchPos = touchPosition;
            }
        }
    }

    private void OnRelease(InputAction.CallbackContext context)
    {
        Debug.Log("OnRelease");
        if (!isDragging) return;

        Debug.Log("OnRelease if");

        if (Touchscreen.current != null)
        {
            isDragging = false;
            Debug.Log("OnRelease if if");
            endTouchPos = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector2 swipeDirection = endTouchPos - startTouchPos;
            ThrowBall(swipeDirection);
        }
    }

    private void ThrowBall(Vector2 swipeDirection)
    {
        Debug.Log("Throwing");
        throwableSpawner.ReleaseThrowable();
        currentThrowable.rb.constraints = RigidbodyConstraints.None;
        currentThrowable.rb.useGravity = true;
        Vector3 throwDirection = new Vector3(swipeDirection.x, Mathf.Abs(swipeDirection.y), 1f);
        currentThrowable.rb.linearVelocity = throwDirection.normalized * throwMaxForce;
        
    }

}
