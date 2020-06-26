using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuController : MonoBehaviour
{
    public CanvasGroup inventoryCanvas;
    public CanvasGroup equipmentCanvas;
    public CanvasGroup journalCanvas;

    public Button inventoryButton;
    public Button equipmentButton;
    public Button journalButton;

    void Start()
    {
        inventoryButton.onClick.AddListener(switchToInventoryMenu);
        equipmentButton.onClick.AddListener(switchToEquipmentMenu);
        journalButton.onClick.AddListener(switchToJournalCanvas);
    }

    //TODO: on opening a page, we need to ensure that we're still calling the correct populate function
    public void openMenu(int pageToOpen)
    {
        switch (pageToOpen)
        {
            case 1:
                switchToInventoryMenu();
                break;
            case 2:
                switchToEquipmentMenu();
                break;
            case 3:
                switchToJournalCanvas();
                break;
            default:
                break;
        }
    }

    void switchToInventoryMenu()
    {
        inventoryCanvas.alpha = 1;
        inventoryCanvas.interactable = true;
        inventoryCanvas.blocksRaycasts = true;

        equipmentCanvas.alpha = 0;
        equipmentCanvas.interactable = false;
        equipmentCanvas.blocksRaycasts = false;

        journalCanvas.alpha = 0;
        journalCanvas.interactable = false;
        journalCanvas.blocksRaycasts = false;

        inventoryCanvas.GetComponentInChildren<InventoryMenuController>().PopulateInventory();
    }

    void switchToEquipmentMenu()
    {
        inventoryCanvas.alpha = 0;
        inventoryCanvas.interactable = false;
        inventoryCanvas.blocksRaycasts = false;

        equipmentCanvas.alpha = 1;
        equipmentCanvas.interactable = true;
        equipmentCanvas.blocksRaycasts = true;

        journalCanvas.alpha = 0;
        journalCanvas.interactable = false;
        journalCanvas.blocksRaycasts = false;

        equipmentCanvas.GetComponentInChildren<EquipmentMenuController>().PopulateEquipment();
    }

    void switchToJournalCanvas()
    {
        inventoryCanvas.alpha = 0;
        inventoryCanvas.interactable = false;
        inventoryCanvas.blocksRaycasts = false;

        equipmentCanvas.alpha = 0;
        equipmentCanvas.interactable = false;
        equipmentCanvas.blocksRaycasts = false;

        journalCanvas.alpha = 1;
        journalCanvas.interactable = true;
        journalCanvas.blocksRaycasts = true;
    }
}
