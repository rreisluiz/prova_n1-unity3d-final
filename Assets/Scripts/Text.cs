using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Text : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        TMP_Text input = gameObject.GetComponent<TMP_Text>();
        input.text = player.GetComponent<Movement>().speedMultiplier.ToString();
    }
}
