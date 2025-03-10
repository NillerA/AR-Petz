using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
public class ThrowableController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] ThrowableSpawner throwableSpawner;
    [SerializeField] InputActionAsset playerInput;

    [SerializeField] float throwMaxForce = 3f;
    [SerializeField] float throwMinForce = 1f;

    private Throwable currentThrowable;
    private InputAction touchPress;
    private InputAction touchPosition;
    private float startTime;
    private bool isDragging;
    private float touchSpeed;
    private Vector2 startTouchPos, endTouchPos;


    private void Awake()
    {
        EnhancedTouchSupport.Enable();//enables the touch input to be read in the touch variable
#if UNITY_EDITOR
        TouchSimulation.Enable();//simulates touch in the editor
#endif

        if (cam == null)
        {
            cam = Camera.main;
        }

        if(playerInput == null)
        {
            playerInput = new InputActionAsset();
        }

        touchPosition = playerInput.FindActionMap("MobileTouch").FindAction("Position");
    }

    private void OnEnable()
    {
        touchPosition.Enable();
    }

    private void OnDisable()
    {
        touchPosition.Disable();
    }

    void Update()
    {
        if (Touch.activeTouches.Count > 0)
        {
            var touch = Touch.activeTouches[0];
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            if (touch.phase == TouchPhase.Began)
            {
                if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
                {
                    Ray ray = cam.ScreenPointToRay(touchPosition);
                    if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == currentThrowable.gameObject)
                    {
                        isDragging = true;
                        startTouchPos = touchPosition;
                        startTime = Time.time;
                    }
                }
            }


            else if (touch.phase == TouchPhase.Moved)
            {
                if (isDragging)
                {
                    Ray ray = cam.ScreenPointToRay(touchPosition);
                    if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == currentThrowable.gameObject)
                    {
                        currentThrowable.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
                    }
                }
            }
            else if (touch.phase == TouchPhase.Stationary)
            {
                startTouchPos = touchPosition;
            }


            else if (touch.phase == TouchPhase.Ended)
            {
                if (!isDragging) return;
                isDragging = false;
                endTouchPos = touchPosition;
                float endTime = Time.time;

                float distance = Vector2.Distance(startTouchPos, endTouchPos);
                float timeTaken = endTime - startTime;
                float speed = distance / timeTaken;
                if (speed < 800f)
                {
                    Destroy(throwableSpawner.currentThrowable.gameObject);
                    return;
                }
                float force = Mathf.Lerp(throwMinForce, throwMaxForce, Mathf.InverseLerp(800f, 5500f, speed));
                Vector2 swipeDirection = (endTouchPos - startTouchPos).normalized;
                if (swipeDirection.y < 0) return;
                ThrowBall(swipeDirection, force);
            }
        }
    }

    private void FixedUpdate()
        {
            currentThrowable = throwableSpawner.currentThrowable;
        }

    private void ThrowBall(Vector2 swipeDirection, float force)
    {
        throwableSpawner.ReleaseThrowable();
        currentThrowable.rb.constraints = RigidbodyConstraints.None;
        currentThrowable.rb.useGravity = true;
        Vector3 throwDirection = (cam.transform.rotation * (Vector3)swipeDirection).normalized;
        currentThrowable.rb.linearVelocity = (new Vector3(throwDirection.x, throwDirection.y, throwDirection.z) + cam.transform.forward).normalized * force;
        Debug.Log(currentThrowable.rb.linearVelocity);
    }

}
