using Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationController : MonoBehaviour
{
    [SerializeField] private Text currentConversationText;
    [SerializeField] private Text currentConversationPartnerName;

    [SerializeField] private GameObject responsePanel;
    [SerializeField] private PlayerCharacter player;

    [SerializeField] private GameObject responsePrefab;

    private INpc currentPartner;

    public void setCurrentText(string text)
    {
        currentConversationText.text = text;
        setResponses();
    }

    public void setConversationPartner(INpc partner)
    {
        currentPartner = partner;
        currentConversationPartnerName.text = partner.Name;
        currentConversationText.text = partner.ConversationLines[partner.CurrentLinePtr].line;

        setResponses();
    }

    public void clearConversation()
    {
        currentPartner = null;
        currentConversationPartnerName.text = "";
        currentConversationText.text = "";
        clearResponses();
    }

    public void toggleConversationCanvas(bool toggle)
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        if(toggle)
        {
            canvasGroup.alpha = 1;
        } else
        {
            canvasGroup.alpha = 0;
            clearConversation();
        }

        canvasGroup.interactable = toggle;
        canvasGroup.blocksRaycasts = toggle;
    }

    void swapConversationPtr(int newPtr)
    {
        currentPartner.CurrentLinePtr = newPtr;
        setCurrentText(currentPartner.ConversationLines[currentPartner.CurrentLinePtr].line);
    }

    void swapPtrAndGiveQuest(int newPtr, IQuest quest)
    {
        swapConversationPtr(newPtr);
        player.questList.Add(quest);
    }

    void setResponses()
    {
        clearResponses();

        List<Response> responseList = currentPartner.ConversationLines[currentPartner.CurrentLinePtr].responses;

        GameObject newButton;

        //If there are responses
        if (responseList != null)
        {
            float numResponses = responseList.Count;
            float buttonSize = responsePanel.GetComponent<RectTransform>().sizeDelta.x / numResponses;
            Vector3 buttonPosition = responsePanel.transform.position;
            buttonPosition.x -= (responsePanel.GetComponent<RectTransform>().sizeDelta.x / 2) - (buttonSize / 2);

            foreach (Response response in responseList)
            {
                newButton = (GameObject)Instantiate(responsePrefab, responsePanel.transform);
                newButton.transform.position = buttonPosition;
                newButton.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonSize, responsePanel.GetComponent<RectTransform>().sizeDelta.y);
                newButton.GetComponentInChildren<Text>().text = response.response;
                if(response.quest != null)
                {
                    newButton.GetComponentInChildren<Button>().onClick.AddListener(delegate { swapPtrAndGiveQuest(response.nextLinePtr, response.quest); });
                } else
                {
                    newButton.GetComponentInChildren<Button>().onClick.AddListener(delegate { swapConversationPtr(response.nextLinePtr); });
                }
                buttonPosition.x += buttonSize;
            }
        } 
        //If no responses, load exit conversation button
        else
        {
            newButton = (GameObject)Instantiate(responsePrefab, responsePanel.transform);
            newButton.transform.position = responsePanel.transform.position;
            newButton.GetComponentInChildren<Text>().text = "Exit Conversation";
            newButton.GetComponentInChildren<Button>().onClick.AddListener(delegate { player.exitConversation(); });
        }
    }

    void clearResponses()
    {
        foreach (Transform child in responsePanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
