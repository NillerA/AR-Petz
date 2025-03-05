using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneSwapObject : MonoBehaviour, IPointerDownHandler
{

    [SerializeField]
    private string sceneName;

    public void OnPointerDown(PointerEventData eventData)
    {
        SceneManager.LoadScene(sceneName);
    }
}
