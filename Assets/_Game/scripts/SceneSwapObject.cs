using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapObject : MonoBehaviour
{

    [SerializeField]
    private string sceneName;

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
