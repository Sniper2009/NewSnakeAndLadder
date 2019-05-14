using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Localization/Persian/LobbyTexts")]
public class LobbyTexts : ScriptableObject {

    //lobbyplayer
    public string playerTeamNumber;
    public string join;
    public string waitingForOthers;
    public string ready;
    public string matchStarting;
    public string enterTeam;


    //leaderboard
    public string notAvailable;
}
