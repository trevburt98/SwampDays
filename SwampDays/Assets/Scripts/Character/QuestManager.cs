using Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameObject questObj;
    [SerializeField] private PlayerCharacter player;

    public IQuest[] questList;

    void Start()
    {
        questList = gameObject.GetComponents<IQuest>();
    }

    public void changeQuestStatus(int index, int newStatus)
    {
        questList[index].Status = newStatus;
    }

    public void turnInQuest(int index)
    {
        questList[index].turnInQuest(player);
    }
}
