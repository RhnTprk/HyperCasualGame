using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentRanking : MonoBehaviour
{
    //Rank class for real-time ranking. Rank has 2 parameters.Player name and player's z position.
    public class Rank : IComparable
    {
        public int posZ;
        public string playerName;

        public Rank(int score, string playerName)
        {
            this.posZ = score;
            this.playerName = playerName;
        }
        //set parameter to int 
        public int CompareTo(object obj)
        {
            Rank otherScore = obj as Rank;

            if (otherScore != null)
            {
                return posZ.CompareTo(otherScore.posZ);
            }
            else
            {
                throw new ArgumentException("Object is not a Score");
            }
        }
        //return player name
        public string getString()
        {
            return String.Format("{0}", playerName);
        }
    }

    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<TextMeshProUGUI> textList;
    [SerializeField] private List<GameObject> playerList;
    private List<Rank> ranks = new List<Rank>();

    //add all players ranks 
    void Start()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            ranks.Add(new Rank(Convert.ToInt32(playerList[i].transform.position.z), playerList[i].name));
        }
    }

    // check all players ranks for leaderboard and set text to ranks
    void Update()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            ranks[i] = new Rank(Convert.ToInt32(playerList[i].transform.position.z), playerList[i].name);
        }

        ranks.Sort();
        ranks.Reverse();

        for (int i = 0; i < ranks.Count; i++)
        {
            textList[i].text = (i + 1) + "- " + ranks[i].getString();
        }        
    }

    public string FirstPlayer { get { return ranks[0].getString(); } }
}
