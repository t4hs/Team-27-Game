using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PlayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    Fighter figtherPlayer;

    private void Start()
    {
        figtherPlayer = playerPrefab.GetComponent<Fighter>();
    }

    private void Update()
    {

    }

    public string HandleWinner()
    {
        return null;
    }

    public void UpdatePlayerScore(int point)
    {

    }

    public void HandleCardSelection(Card card1, Card card2)
    {

    }

    public void SetTurn(Player player)
    {

    }

    public bool CheckDeth(Player player)
    {
        return false;
    }
}
