using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Animator animator;
    private Queue<string> sentences;
    public TextMeshProUGUI dialogText;
    private Dialogue dialogueCurrent;
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void SetCurrentDialogue(Dialogue dialogue)
    {
        dialogueCurrent = dialogue;
        animator.SetBool("dialogueOpen", true);
        StartDialogue();
    }

    public void StartDialogue()
    {
        sentences.Clear();
        foreach(string s in dialogueCurrent.sentences)
        {
            sentences.Enqueue(s);
        }
        DisplayNextSentence();        
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(typing(sentence));
    }

    IEnumerator typing(string sentence)
    {
        dialogText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("dialogueOpen", false);
        dialogueCurrent = null;
    }
}