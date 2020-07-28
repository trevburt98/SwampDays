using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuController : MonoBehaviour
{
    public GameObject inventoryCanvas;
    public GameObject equipmentCanvas;
    public GameObject journalCanvas;

    public Button inventoryButton;
    public Button equipmentButton;
    public Button journalButton;

    private int lastTab = 0;

    void Start()
    {
        inventoryButton.onClick.AddListener(switchToInventoryMenu);
        equipmentButton.onClick.AddListener(switchToEquipmentMenu);
        journalButton.onClick.AddListener(switchToJournalCanvas);
    }

    //TODO: on opening a page, we need to ensure that we're still calling the correct populate function
    public void openMenu()
    {
        switch (lastTab)
        {
            case 0:
                switchToInventoryMenu();
                break;
            case 1:
                switchToEquipmentMenu();
                break;
            case 2:
                switchToJournalCanvas();
                break;
            default:
                break;
        }
    }

    void switchToInventoryMenu()
    {
        inventoryCanvas.SetActive(true);

        equipmentCanvas.SetActive(false);

        journalCanvas.SetActive(false);

        lastTab = 0;
        inventoryCanvas.GetComponentInChildren<InventoryMenuController>().PopulateInventory();
    }

    void switchToEquipmentMenu()
    {
        inventoryCanvas.SetActive(false);

        equipmentCanvas.SetActive(true);

        journalCanvas.SetActive(false);

        lastTab = 1;
        equipmentCanvas.GetComponentInChildren<EquipmentMenuController>().PopulateEquipment();
    }

    void switchToJournalCanvas()
    {
        inventoryCanvas.SetActive(false);

        equipmentCanvas.SetActive(false);

        journalCanvas.SetActive(true);

        lastTab = 2;
        journalCanvas.GetComponentInChildren<JournalController>().PopulateJournal();
    }
}
