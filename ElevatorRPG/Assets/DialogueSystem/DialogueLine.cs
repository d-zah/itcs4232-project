using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public Speaker speaker;
    [TextArea] public string dialogue;
    [SerializeField] public bool isMale;
    [SerializeField] public bool isElevator;
}
