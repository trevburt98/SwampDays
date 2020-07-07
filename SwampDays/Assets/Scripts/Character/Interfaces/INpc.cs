using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INpc : ICharacter<float>
{
    //The NPCs opinion of the player. -100 is extremely hostile, 0 is neutral, and 100 is big like
    int Opinion
    {
        get;
        set;
    }

    List<ConversationLine> ConversationLines
    {
        get;
    }

    int CurrentLinePtr
    {
        get;
        set;
    }

    void addConversationLine(ConversationLine newLine);

    int startConversation();

    void endConversation();

}
