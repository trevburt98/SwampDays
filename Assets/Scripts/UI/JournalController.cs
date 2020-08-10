using Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalController : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
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
    }
    public void PopulateJournal()
    {
        clearJournal();
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

    private void updateNavigation(bool menu)
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
}
