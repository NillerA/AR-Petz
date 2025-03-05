using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChooseAnimal : MonoBehaviour, IPointerDownHandler
{

    [SerializeField]
    private PlayerManager.aniamls animalChoice;

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerManager.chosenAnimal = animalChoice;
    }
}
