using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class PlayerUI : MonoBehaviour
{
    [Header("Player Components")]
    public Image image;

    [Header("Child Text Objects")]
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI timeGameText;

    private string namePlayer;
    private float timeGame;
    private int idPlayer;

    private readonly List<Players> players = new();


    void Update()
    {
        PostPlayer();
        GetPlayer();
        UpdateScore();
    }

    /* void SetToUIPlayer()
    {
        string texto = "";
        foreach (Players jogador in players)
        {
            texto += string.Format("ID: {0}, Nome: {1}, Score: {2}\n", jogador.IdPlayer, jogador.NamePlayer, jogador.PlayerScore);
        }
        playerNameText.text = texto;
    } */

    public void OnIdPlayerChanged(int idPlayer)
    {
        this.idPlayer = idPlayer;
    }

    public void OnPlayerNameChanged(string namePlayer)
    {
        this.namePlayer = namePlayer;
    }

    public void OnTimeGameChanged(float timeGame)
    {
        float minutes = Mathf.FloorToInt(timeGame / 60);
        float seconts = Mathf.FloorToInt(timeGame % 60);

        timeGameText.text = string.Format("{0:00}:{1:00}", minutes, seconts);
    }

    //POST
    private void PostPlayer()
    {
        Players addPlayers = new()
        {
            IdPlayer = idPlayer,
            NamePlayer = namePlayer,
            PlayerScore = timeGame
        };

        players.Add(addPlayers);
    }

    //GET player to UI
    private void GetPlayer()
    {
        foreach (Players item in players)
        {
            playerNameText.text = item.NamePlayer;
        }
    }

    //UPDATE
    private void UpdateScore()
    {
        bool idExistPlayer = players.Any(player => player.IdPlayer == idPlayer);

        if (idExistPlayer)
        {
            Players playerUpdate = players.Find(updatePlayer => updatePlayer.IdPlayer == idPlayer);

            playerUpdate.PlayerScore = timeGame;
        }
    }

    //GET new score to UI
    private void GetNewScore()
    {
        foreach (Players item in players)
        {
            float minutes = Mathf.FloorToInt(item.PlayerScore / 60);
            float seconts = Mathf.FloorToInt(item.PlayerScore % 60);

            timeGameText.text = string.Format("{0:00}:{1:00}", minutes, seconts);
        }
    }
}

internal class Players
{
    public int IdPlayer { get; set; }
    public string NamePlayer { get; set; }
    public float PlayerScore { get; set; }
}