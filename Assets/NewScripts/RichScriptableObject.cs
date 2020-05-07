using UnityEngine;

[System.Serializable]
public class RichScriptableObject : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField]
    [TextArea]
    protected string developerDescription = "Please enter a description.";
#endif
}
