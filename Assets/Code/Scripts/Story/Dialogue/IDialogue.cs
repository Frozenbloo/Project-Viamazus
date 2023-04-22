public interface IDialogue
{
    public string[] dialogue { get; set; }
    public string npcName { get; set; }

    void OnDialogueEnd();
}
