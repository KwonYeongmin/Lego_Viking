using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Image HPImg;
    public Image AmmoImg;
    public Text speedTXT;
    public Text pinSpeedTXT;
    private GameObject player;
    public GameObject pin;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        HPImg.fillAmount = (float)(player.GetComponent<Player>().HP) / (float)(player.GetComponent<Player>().DefaultHP);
      
        AmmoImg.fillAmount = (float)(player.GetComponent<Player>().ammo) / (float)(player.GetComponent<Player>().defaultAmmo);

        speedTXT.text ="speed : "+ player.GetComponent<Movement>().moveSpeed.ToString();
        pin.GetComponent<Viking>().speed = float.Parse(pinSpeedTXT.text.ToString());
    }
}
