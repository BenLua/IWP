using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public TMP_Text LivesText;

    // Update is called once per frame
    void Update()
    {
        LivesText.text = PlayerStats.Lives.ToString();
    }
}
