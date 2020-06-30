using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationController : MonoBehaviour
{
    [SerializeField] private Text currentConversationText;
    [SerializeField] private Text currentConversationPartnerName;

    [SerializeField] private GameObject responsePanel;

    [SerializeField] private GameObject responsePrefab;

    private INpc currentPartner;

    public void setCurrentText(string text)
    {
        currentConversationText.text = text;
    }

    public void setConversationPartner(INpc partner)
    {
        currentPartner = partner;
        currentConversationPartnerName.text = partner.Name;
        currentConversationText.text = partner.ConversationLines[partner.CurrentLinePtr].line;

        GameObject newButton;

        foreach(Response response in partner.ConversationLines[partner.CurrentLinePtr].responses)
        {
            newButton = (GameObject)Instantiate(responsePrefab, responsePanel.transform);
            newButton.GetComponentInChildren<Text>().text = response.response;
            newButton.GetComponentInChildren<Button>().onClick.AddListener(delegate { swapConversationPtr(response.nextLinePtr); });
        }

    }

    public void clearConversation()
    {
        currentPartner = null;
        currentConversationPartnerName.text = "";
        currentConversationText.text = "";
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
        }

        canvasGroup.interactable = toggle;
        canvasGroup.blocksRaycasts = toggle;
    }

    void swapConversationPtr(int newPtr)
    {
        
    }
}
