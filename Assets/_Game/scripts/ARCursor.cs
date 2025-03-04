using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCursor : MonoBehaviour
{

    [SerializeField]
    GameObject CursorChildObject, ObjectToPlace;
    [SerializeField]
    ARRaycastManager raycastManager;

    [SerializeField]
    private bool useCursor = true;

    void Start()
    {
        CursorChildObject.SetActive(useCursor);
    }

    void Update()
    {
        if (useCursor)
        {
            UpdateCursor();
        }

        //do the placement thing
    }

    private void UpdateCursor()
    {
        Vector2 screenCursorPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCursorPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }
}
