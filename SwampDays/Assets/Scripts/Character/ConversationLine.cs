using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationLine
{
    public string line;
    public List<Response> responses;
    public IQuest questForLine;

    public ConversationLine(string lineText, List<Response> responseList)
    {
        line = lineText;
        responses = responseList;
    }
}
