﻿using Character.PlayerCharacter;
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

    public void PopulateJournal()
    {
        GameObject newObj;

        foreach (IQuest quest in questManager.questList)
        {
            if(quest.Status > 1)
            {
                newObj = (GameObject)Instantiate(listItemPrefab, transform);
                newObj.GetComponentInChildren<Text>().text = quest.QuestName;
                newObj.GetComponentInChildren<Button>().onClick.AddListener(delegate { LoadQuestInfo(quest); });
            }
        }
    }

    void LoadQuestInfo(IQuest quest)
    {
        questName.text = quest.QuestName;
        questDescription.text = quest.QuestDescription;
    }
}