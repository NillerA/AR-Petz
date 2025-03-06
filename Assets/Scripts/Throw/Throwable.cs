using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Throwable : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] private float maxMovespeed;
    [SerializeField] private float accMovespeed;


    private float currMovespeed;



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

}
