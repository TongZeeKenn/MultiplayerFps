using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ScoreBoard : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform container;
    [SerializeField] GameObject ScoreBoardItemPrefab;

    Dictionary<Player, ScoreBoardItem> scoreBoardItems = new Dictionary<Player, ScoreBoardItem>();

    void Start()
    {
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            AddScoreBoardItem(player);
        }
    }

    public override void OnPlayerEnteredRoom(Player player)
    {
        AddScoreBoardItem(player);
    }

    public override void OnPlayerLeftRoom(Player player)
    {
        RemoveScoreBoardItem(player);
    }

    void AddScoreBoardItem(Player player)
    {
        ScoreBoardItem item = Instantiate(ScoreBoardItemPrefab, container).GetComponent<ScoreBoardItem>();
        item.Initialized(player);
        scoreBoardItems[player] = item;
    }

    void RemoveScoreBoardItem(Player player)
    {
        Destroy(scoreBoardItems[player].gameObject);
        scoreBoardItems.Remove(player);
    }
}
