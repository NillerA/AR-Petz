using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class ARCursor : MonoBehaviour
{

    [SerializeField]
    private GameObject _cursorChildObject, _objectToPlace;
    [SerializeField]
    private ARRaycastManager _raycastManager;

    [SerializeField]
    private bool _useCursor = true;

    void Start()
    {
        _cursorChildObject.SetActive(_useCursor);
        EnhancedTouchSupport.Enable();
#if UNITY_EDITOR
        TouchSimulation.Enable();
#endif
    }

    void Update()
    {
        if (_useCursor)
        {
            UpdateCursor();

            if (Touch.activeTouches.Count > 0 && Touch.activeTouches[0].phase == TouchPhase.Ended)
            {
                Instantiate(_objectToPlace, transform.position, transform.rotation);
            }
        }
    }

    private void UpdateCursor()
    {
        Vector2 screenCursorPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        _raycastManager.Raycast(screenCursorPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }
}
