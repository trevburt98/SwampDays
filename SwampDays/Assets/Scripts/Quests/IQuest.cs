using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuest
{
    string QuestId
    {
        get;
    }

    string QuestName
    {
        get;
        set;
    }

    string QuestDescription
    {
        get;
        set;
    }

    float TimeToComplete
    {
        get;
        set;
    }

    float CurrentTime
    {
        get;
        set;
    }

    bool Complete
    {
        get;
        set;
    }

    bool Failed
    {
        get;
        set;
    }

    INpc QuestGiver
    {
        get;
    }

    INpc QuestReceiver
    {
        get;
        //set;
    }

    void completeQuest();

    void failQuest();

    void updateQuestDescription(string descriptionAddition);

    void updateQuestTime(float changeInTime);
}
