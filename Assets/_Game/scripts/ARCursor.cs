using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARFoundation;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class ARCursor : MonoBehaviour
{

    [SerializeField]
    private GameObject _cursorChildObject;
    GameObject _objectToPlace;
    [SerializeField]
    private GameObject
        _cat,
        _dog,
        _mouse,
        _parrot,
        _turtoise,
        _rabbit,
        _goldfish;
    [SerializeField]
    private ARRaycastManager _raycastManager;

    [SerializeField]
    private bool _useCursor = true;

    private void Awake()
    {
        switch (PlayerManager.chosenAnimal)
        {
            case PlayerManager.aniamls.Cat:
                _objectToPlace = _cat;
                return;
            case PlayerManager.aniamls.Dog:
                _objectToPlace = _dog;
                return;
            case PlayerManager.aniamls.Mouse:
                _objectToPlace = _mouse;
                return;
            case PlayerManager.aniamls.Goldfish:
                _objectToPlace = _goldfish;
                return;
            case PlayerManager.aniamls.Turtoise:
                _objectToPlace = _turtoise;
                return;
            case PlayerManager.aniamls.Parrot:
                _objectToPlace = _parrot;
                return;
            case PlayerManager.aniamls.Rabbit:
                _objectToPlace = _rabbit;
                return;
        }
    }

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
