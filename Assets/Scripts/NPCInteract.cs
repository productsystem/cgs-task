using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    bool canInteract = false;
    public Dialogue dialogue;

    void Update()
    {
        if(canInteract && Input.GetKeyDown(KeyCode.E))
        {
            DialogueStart();
        }   
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canInteract = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canInteract = false;
        }
    }

    public void DialogueStart()
    {
        FindObjectOfType<DialogueManager>().SetCurrentDialogue(dialogue);
    }
}
