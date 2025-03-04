using UnityEngine;

public class ChooseAnimal : MonoBehaviour
{

    [SerializeField]
    private PlayerManager.aniamls animalChoice;

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayerManager.chosenAnimal = animalChoice;
        }   
    }
}
