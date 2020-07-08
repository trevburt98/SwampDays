using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Response
{
    public string response;
    public int nextLinePtr;
    public int questIndex = -1;

    public Response(string responseString, int next)
    {
        response = responseString;
        nextLinePtr = next;
    }

    public Response(string responseString, int next, int quest)
    {
        response = responseString;
        nextLinePtr = next;
        questIndex = quest;
    }
}
