using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Response
{
    public string response;
    public int nextLinePtr;

    public Response(string responseString, int next)
    {
        response = responseString;
        nextLinePtr = next;
    }
}
