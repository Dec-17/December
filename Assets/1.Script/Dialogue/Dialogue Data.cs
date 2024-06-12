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
    [Tooltip("캐릭터 이름")]
    public string Name;

    [Tooltip("캐릭터 이미지")]
    public Sprite Img;

    [Tooltip("대사 내용")]
    [TextArea]
    public string[] contexts;
}

[System.Serializable]
public class Chapters
{
    public Dialogue[] dialogues;
}
