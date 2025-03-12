using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThrowableSpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime;
    [SerializeField] private RectTransform content;
    [SerializeField] private Button button;
    [SerializeField] private List<ThrowableScriptable> throwables = new List<ThrowableScriptable>();
    [SerializeField] private Button noFood;

    public Throwable currentThrowable { get; private set; }

    private Throwable lastThrowable;
    private bool isSpawning;

    void Start()
    {
        noFood.onClick.AddListener(() => { Destroy(currentThrowable.gameObject); lastThrowable = null; });
        foreach (ThrowableScriptable throwable in throwables)
        {
            Button buttonTemp = Instantiate(button, content);
            TextMeshProUGUI buttonText = buttonTemp.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = throwable.named;
            }
            if(throwable.uIImage != null)
            {
                buttonTemp.image.sprite = throwable.uIImage;
            }
            buttonTemp.onClick.AddListener(() => {lastThrowable = throwable.prefab; StartCoroutine(SetThrowable());});
        }
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
        if (lastThrowable == null) yield break;
        currentThrowable = Instantiate(lastThrowable, transform, false);

        isSpawning = false;
    }

    public void ReleaseThrowable()
    {
        currentThrowable.transform.parent = null;
        currentThrowable = null;
    }

}
