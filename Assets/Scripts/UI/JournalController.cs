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
    [SerializeField] private GameObject content;
    [SerializeField] private Button questButton;
    [SerializeField] private Button notesButton;
    [SerializeField] private Button addNoteButton;
    private List<JournalNote> noteList;
    private bool inNotesMenu = false;
    private int noteCount = 0;

    public void Start()
    {
        noteList = new List<JournalNote>();
        questButton.onClick.AddListener(delegate { updateNavigation(false); });
        notesButton.onClick.AddListener(delegate { updateNavigation(true); });
        addNoteButton.onClick.AddListener(delegate { createNote(); });
    }
    public void PopulateJournal()
    {
        clearJournal();
        eraseInfo();
        addNoteButton.gameObject.SetActive(inNotesMenu);
        if (!inNotesMenu)
        {
            GameObject newObj;
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
            GameObject newObj;
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
    }

    void eraseInfo(){
        questName.text = "";
        questDescription.text = "";
    }

    private void updateNavigation(bool menu)
    {
        inNotesMenu = menu;
        PopulateJournal();
    }

    private void createNote(){
        noteCount++;
        noteList.Add(new JournalNote(noteCount));
        PopulateJournal();
    }

    private void clearJournal(){
        foreach (Transform child in content.transform){
            GameObject.Destroy(child.gameObject);
        }
    }
}
