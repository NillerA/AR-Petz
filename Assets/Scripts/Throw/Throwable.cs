using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Throwable : MonoBehaviour
{
    public Rigidbody rb;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.isKinematic = true;
        GameObject.FindWithTag("Animal").GetComponent<Animal>().walkTo(transform.position, true);
    }
}
