using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        currentWarStatus = startingWarStatus;
    }

    public int enemySuspicionThreshold = 15;
    public int warSuccessThreshold = 10;
    public int startingWarStatus = 10;

    public int currentWarStatus;
    public string currentSummary;

    public void UpdateGameStatus(int value, string effect)
    {
        currentWarStatus += value;
        currentSummary += effect + " ";
    }

    // Start is called before the first frame update
    void Start()
    {
        currentWarStatus = startingWarStatus;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
