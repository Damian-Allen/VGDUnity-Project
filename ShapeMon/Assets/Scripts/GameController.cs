using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour {
    private static GameController _instance;

    public static GameController Instance { get { return _instance; } }


    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start adding from here

    public Sprite[] shapeMon;

    [Tooltip("% chance for an enconter")]
    public int encounterChance;
    public int i;
    public TextMeshProUGUI test;


    public void EncounterCheck() {

        if (encounterChance == 0) {
            encounterChance = 10;
        }
        
        i = Random.Range(1, 101);
        if (i <= encounterChance){
            SceneManager.LoadScene(1);
        }

    }

    public void Button() {
        SceneManager.LoadScene(0);
    }




}