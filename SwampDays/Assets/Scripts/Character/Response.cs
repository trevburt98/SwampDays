using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Response
{
    public string response;
    public int nextLinePtr;
    public IQuest quest;

    public Response(string responseString, int next)
    {
        response = responseString;
        nextLinePtr = next;
    }

    public Response(string responseString, int next, IQuest newQuest)
    {
        response = responseString;
        nextLinePtr = next;
        quest = newQuest;
    }
}
