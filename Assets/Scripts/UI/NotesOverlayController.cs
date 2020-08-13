using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesOverlayController : MonoBehaviour
{
    [SerializeField] private GameObject selectionMenuCanvas;
    [SerializeField] private GameObject selectionMenuContent;
    [SerializeField] private Button selectionMenuButton;
    [SerializeField] private JournalController journalController;
    [SerializeField] private GameObject listItemPrefab;
    [SerializeField] private Text TitleBox;
    [SerializeField] private Text ContentBox;
    private bool selectionMenuOpen = false;
    void Start()
    {
        selectionMenuButton.onClick.AddListener(delegate { toggleSelectionMenu(!selectionMenuOpen); });
    }

    private void toggleSelectionMenu(bool open)
    {
        selectionMenuOpen = open;
        selectionMenuCanvas.SetActive(open);
        if(open){
            populateList();
        }
        
    }

    public void populateList()
    {
        clearList();
        GameObject newObj;
        foreach (JournalNote note in journalController.NoteList)
        {
            newObj = (GameObject)Instantiate(listItemPrefab, selectionMenuContent.transform);
            newObj.GetComponentInChildren<Text>().text = note.Title;
            newObj.GetComponentInChildren<Button>().onClick.AddListener(delegate { LoadNoteInfo(note); });
        }
    }

    private void clearList()
    {
        foreach (Transform child in selectionMenuContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void LoadNoteInfo(JournalNote note)
    {
            TitleBox.text = note.Title;
            ContentBox.text = note.Content;
    }
}
