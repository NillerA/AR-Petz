using UnityEngine;

[CreateAssetMenu(fileName = "ThrowableScriptable", menuName = "Scriptable Objects/ThrowableScriptable")]
public class ThrowableScriptable : ScriptableObject
{
    public Throwable prefab;
    public string named;
    public int uiImage;
}
