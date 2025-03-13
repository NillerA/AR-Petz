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

    private void OnCollisionEnter(Collision collision)
    {
        rb.isKinematic = true;
        StartCoroutine(PlayerManager.animal.GetComponent<Animal>().walkTo(transform.position, true, gameObject));
        //GameObject.FindWithTag("Animal").GetComponent<Animal>().walkTo(transform.position, true);
    }
}
