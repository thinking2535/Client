using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CIOS : CMobile
{
    public override string GetUpdateUrl()
    {
        return "https://itunes.apple.com/kr/app/apple-store/id1530777694";

        // rso todo update url
        // return "https://itunes.apple.com/app/apple-store/id1530777694";
    }
}
