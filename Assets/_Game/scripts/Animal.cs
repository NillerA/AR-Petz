using System.Collections;
using UnityEngine;

public class Animal : MonoBehaviour
{

    [SerializeField]
    private float _speed = 10;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public IEnumerator walkTo(Vector3 position)
    {
        while (Vector3.Distance(transform.position, position) > 0.1f) 
        { 
            transform.position = Vector3.LerpUnclamped(transform.position, position, _speed * Time.deltaTime);
            _animator.SetFloat("WalkSpeed",_speed * Time.deltaTime);
            transform.LookAt(position);
            yield return null;
        }
        _animator.SetFloat("WalkSpeed", 0);
    }
}
