using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObjects/DialogueData", order = 1)]
public class DialogueData : ScriptableObject
{
    public Chapters[] chapters;
}

[System.Serializable]
public class Dialogue
{
    [Tooltip("ĳ���� �̸�")]
    public string Name;

    [Tooltip("ĳ���� �̹���")]
    public Sprite Img;

    [Tooltip("��� ����")]
    [TextArea]
    public string[] contexts;
}

[System.Serializable]
public class Chapters
{
    public Dialogue[] dialogues;
}
