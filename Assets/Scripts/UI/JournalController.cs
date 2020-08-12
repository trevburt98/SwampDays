using Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalController : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    [SerializeField] private NotesOverlayController notesOverlayController;
    [SerializeField] private GameObject listItemPrefab;
    [SerializeField] private Text questName;
    [SerializeField] private Text questDescription;
    [SerializeField] private InputField titleField;
    [SerializeField] private InputField contentField;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject editWindow;
    [SerializeField] private GameObject currentEntryWindow;
    [SerializeField] private Button questButton;
    [SerializeField] private Button notesButton;
    [SerializeField] private Button addNoteButton;
    [SerializeField] private Button editNoteButton;
    [SerializeField] private Button deleteNoteButton;
    [SerializeField] private Button cancelEditButton;
    [SerializeField] private Button saveEditButton;
    private List<JournalNote> noteList;
    public List<JournalNote> NoteList{
        get => noteList;
    }
    private bool inNotesMenu = false;
    private int noteCount = 0;
    private JournalNote openNote = null;

    public void Start()
    {
        editWindow.SetActive(false);
        noteList = new List<JournalNote>();
        questButton.onClick.AddListener(delegate { updateNavigation(false); });
        notesButton.onClick.AddListener(delegate { updateNavigation(true); });
        addNoteButton.onClick.AddListener(delegate { createNote(); });
        editNoteButton.onClick.AddListener(delegate { toggleEditWindow(true, openNote, true); });
        deleteNoteButton.onClick.AddListener(delegate { deleteNote(openNote); });
        cancelEditButton.onClick.AddListener(delegate { toggleEditWindow(false, openNote, false); });
        saveEditButton.onClick.AddListener(delegate { toggleEditWindow(false, openNote, true); });
        //TODO: Remove this call
        generateDebugNotes();
    }
    public void PopulateJournal()
    {
        clearJournal();
        notesOverlayController.populateList();
        addNoteButton.gameObject.SetActive(inNotesMenu);
        editNoteButton.gameObject.SetActive(inNotesMenu);
        deleteNoteButton.gameObject.SetActive(inNotesMenu);
        GameObject newObj;
        if (!inNotesMenu)
        {
            foreach (IQuest quest in questManager.questList)
            {
                if (quest.Status > 1)
                {
                    newObj = (GameObject)Instantiate(listItemPrefab, content.transform);
                    newObj.GetComponentInChildren<Text>().text = quest.QuestName;
                    newObj.GetComponentInChildren<Button>().onClick.AddListener(delegate { LoadQuestInfo(quest); });
                }
            }
        }
        else
        {
            foreach (JournalNote note in noteList)
            {
                newObj = (GameObject)Instantiate(listItemPrefab, content.transform);
                newObj.GetComponentInChildren<Text>().text = note.Title;
                newObj.GetComponentInChildren<Button>().onClick.AddListener(delegate { LoadNoteInfo(note); });
            }
        }
    }

    void LoadQuestInfo(IQuest quest)
    {
            questName.text = quest.QuestName;
            questDescription.text = quest.QuestDescription;
    }
    void LoadNoteInfo(JournalNote note)
    {
            questName.text = note.Title;
            questDescription.text = note.Content;
            openNote = note;
    }

    public void updateNavigation(bool menu)
    {
            inNotesMenu = menu;
            PopulateJournal();
    }

    private void createNote()
    {
            JournalNote newNote = new JournalNote(noteCount);
            openNote = newNote;
            toggleEditWindow(true, newNote, true);
    }

    private void clearJournal()
    {
        questName.text = "";
        questDescription.text = "";
        foreach (Transform child in content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    private void toggleEditWindow(bool open, JournalNote note, bool save)
    {
        editWindow.SetActive(open);
        currentEntryWindow.SetActive(!open);
        questButton.enabled = !open;
        notesButton.enabled = !open;
        addNoteButton.enabled = !open;
        toggleContentButtons(!open);
        if (open)
        {
            titleField.text = note.Title;
            contentField.text = note.Content;
        }
        else
        {
            if (save){
                note.Title = titleField.text;
                note.Content = contentField.text;
                if(!note.saved){
                    note.saved = true;
                    noteList.Add(note);
                    noteCount++;
                }
                
            }
            PopulateJournal();
        }
    }

    private void deleteNote(JournalNote note)
    {
            noteList.Remove(note);
            PopulateJournal();
    }

    private void toggleContentButtons(bool on){
        foreach(Transform child in content.transform){
            child.gameObject.GetComponentInChildren<Button>().enabled = on;
        }
    }

    private void generateDebugNotes(){
        noteCount = 25;
        JournalNote note = new JournalNote(0);
        note.Title = "This is a note";
        note.Content = "Notes can be used to record information";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(1);
        note.Title = "Note 1";
        note.Content = "A new Note";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(2);
        note.Title = "Note 2";
        note.Content = "Note strikes back";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(3);
        note.Title = "Note 3";
        note.Content = "Return of Note";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(4);
        note.Title = "Note 4";
        note.Content = "The Revenge";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(5);
        note.Title = "Note 5";
        note.Content = "Son of Note";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(6);
        note.Title = "Note 6";
        note.Content = "Ghost of Note";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(7);
        note.Title = "Note 7";
        note.Content = "Son of the Ghost of Note";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(8);
        note.Title = "Note 8";
        note.Content = "Bride of Note";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(9);
        note.Title = "Musical notes";
        note.Content = "D#, C, Bb";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(10);
        note.Title = "Pickup Line (pure gold)";
        note.Content = "Hey baby are you a note? Because I want to take you";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(11);
        note.Title = "The tale of Isabelle";
        note.Content = "Once upon a time there was a girl named Isabelle. She was not helpful in helping me come up with content to put in my example notes";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(12);
        note.Title = "Tea joke";
        note.Content = "Tea is prohibited in Spain. No té";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(13);
        note.Title = "Data Joke";
        note.Content = "I have 2000 pounds of data. It's an eton (that's the word note backwards";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(14);
        note.Title = "Anagrams";
        note.Content = "The letter in the word Note can be rearranged into toen, oten, and many more!";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(15);
        note.Title = "What's in a note?";
        note.Content = "That which we call a note by any other name would be just as annoying to write 25 of";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(16);
        note.Title = "Good games";
        note.Content = "Psychonauts, Read Only Memories, Swamp Days, Balloon in a Wasteland, The Beginner's Guide";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(17);
        note.Title = "I should write a novel";
        note.Content = "It would be easier than this.";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(18);
        note.Title = "Bad Games";
        note.Content = "Ooh you thought you were gonna get a hot take didn't you?";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(19);
        note.Title = "Good Songs";
        note.Content = "Andrew Huang's Get Away, Louie Zong's Thumbnail, The Orion Experience's Cult of Dionysis";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(20);
        note.Title = "Uses for my college degree";
        note.Content = "";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(21);
        note.Title = "Bad Songs";
        note.Content = "https://youtu.be/dQw4w9WgXcQ";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(22);
        note.Title = "Breakfast joke";
        note.Content = "What do you get when you make breakfast with notes? Notemeal! I'm near the end and struggling";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(23);
        note.Title = "My name";
        note.Content = "I can't remember it. All I know is note.";
        note.saved = true;
        noteList.Add(note);
        note = new JournalNote(24);
        note.Title = "Reminder for Trevor";
        note.Content = "Make these better later";
        note.saved = true;
        noteList.Add(note);
    }
}
