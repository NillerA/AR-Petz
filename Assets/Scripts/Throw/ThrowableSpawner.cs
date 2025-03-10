using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowableSpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime;

    [SerializeField] private Throwable test;
    [SerializeField] private List<Throwable> throwables = new List<Throwable>();
    [SerializeField] private Button testButton;
    [SerializeField] private Button foodButton;

    public Throwable currentThrowable { get; private set; }

    private ThrowSetting throwSetting;
    private bool isSpawning;

    void Start()
    {
        testButton.onClick.AddListener(() => { throwSetting = ThrowSetting.TEST; StartCoroutine(SetThrowable());});
        foodButton.onClick.AddListener(() => { throwSetting = ThrowSetting.FOOD; StartCoroutine(SetThrowable()); });
    }


    void Update()
    {
        if(currentThrowable == null && isSpawning == false)
        {
            StartCoroutine(SetThrowable());
        }
    }

    IEnumerator SetThrowable()
    {
        isSpawning = true;
        if(currentThrowable != null)
        {
            Destroy(currentThrowable.gameObject);
            currentThrowable = null;
        }
        else
        {
            yield return new WaitForSeconds(spawnTime);
        }

        switch (throwSetting)
            {
                case ThrowSetting.TEST:
                    currentThrowable = Instantiate(test, transform, false);
                    break;

                case ThrowSetting.FOOD:
                    currentThrowable = Instantiate(throwables[Random.Range(0, throwables.Count)], transform, false);
                    break;
            }
        isSpawning = false;
    }

    public void ReleaseThrowable()
    {
        currentThrowable.transform.parent = null;
        currentThrowable = null;
    }

}

public enum ThrowSetting
{
    TEST,
    FOOD,
}
