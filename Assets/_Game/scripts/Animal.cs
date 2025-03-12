using System;
using System.Collections;
using UnityEngine;

public class Animal : MonoBehaviour
{

    [SerializeField]
    private float _speed = 10, _hungerConsubtionRate = 0.2f, _HappinessConsubtionRate = 5;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(HungerConsubtion());
        StartCoroutine(HappinessConsubtion());
    }

    public IEnumerator walkTo(Vector3 position, bool eat)
    {
        while (Vector3.Distance(transform.position, position) > 0.1f) 
        { 
            transform.position = Vector3.LerpUnclamped(transform.position, position, _speed * Time.deltaTime);
            _animator.SetFloat("WalkSpeed",_speed * Time.deltaTime);
            transform.LookAt(position);
            yield return null;
        }
        _animator.SetFloat("WalkSpeed", 0);
        if (eat) 
        {
            _animator.SetTrigger("Eat");
        }
    }

    public IEnumerator HungerConsubtion()
    {
        while (PlayerManager.animalInfo.Hunger > 0) 
        {
            yield return new WaitForSeconds(_hungerConsubtionRate);
            PlayerManager.animalInfo.Hunger--;
        }
        Debug.Log("DeadAnimal");
        _animator.SetTrigger("Die");
    }

    public IEnumerator HappinessConsubtion()
    {
        while (PlayerManager.animalInfo.Hapiness > 0)
        {
            yield return new WaitForSeconds(_HappinessConsubtionRate);
            PlayerManager.animalInfo.Hapiness--;
        }
    }
}
