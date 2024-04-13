using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int level = 1;
    public static int spacePressedCounter = 0; 
    public TextMeshProUGUI textLevel;
    public TextMeshProUGUI textSpacePressed; 

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        level = 1;
    }
    private void Update()
    {
        textLevel.text = level.ToString();
        textSpacePressed.text = spacePressedCounter.ToString(); ; 
    }
}
