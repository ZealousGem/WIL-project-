using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCDialogue", menuName = "Scriptable Objects/NPCDialogue")]
public class NPCDialogue : ScriptableObject
{
    public int CharacterId;

    public List<DialogueTree> dialogueNodes;

    public string Name;

    public int RepeatDialogueIndex;
}
