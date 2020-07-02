﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleQuest : MonoBehaviour, IQuest
{
    private string _questId = "exQuest";
    public string QuestId
    {
        get => _questId;
    }

    private string _questName = "Example Quest";
    public string QuestName
    {
        get => _questName;
        set => _questName = value;
    }

    private string _questDescription = "Example implementation of a quest. Used to create a workflow for future equipments";
    public string QuestDescription
    {
        get => _questDescription;
        set => _questDescription = value;
    }

    private float _timeToComplete = 0;
    public float TimeToComplete
    {
        get => _timeToComplete;
        set => _timeToComplete = value;
    }

    private float _currentTime = 0;
    public float CurrentTime
    {
        get => _currentTime;
        set => _currentTime = value;
    }

    private bool _complete = false;
    public bool Complete
    {
        get => _complete;
        set => _complete = value;
    }

    private bool _failed = false;
    public bool Failed
    {
        get => _failed;
        set => _failed = value;
    }

    [SerializeField] private GameObject _questGiver;
    public INpc QuestGiver
    {
        get => _questGiver.GetComponent<INpc>();
    }

    [SerializeField] private GameObject _questReceiver;
    public INpc QuestReceiver
    {
        get => _questReceiver.GetComponent<INpc>();
        //set => _questReceiver.GetComponent<INpc>() = value;
    }

    [SerializeField] private GameObject target;

    void Update()
    {
        if(target == null)
        {
            completeQuest();
        }
    }

    public void completeQuest()
    {
        Complete = true;
        Debug.Log("Yay we did it");
    }

    public void failQuest()
    {
        Failed = true;
    }

    public void updateQuestDescription(string descriptionAddition)
    {
        QuestDescription += "\n" + descriptionAddition;
    }

    public void updateQuestTime(float changeInTime)
    {
        TimeToComplete += changeInTime;
    }
}
