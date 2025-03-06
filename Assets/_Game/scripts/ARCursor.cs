using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

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
    }

    void Update()
    {
        if (_useCursor)
        {
            UpdateCursor();

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
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
    }
}
