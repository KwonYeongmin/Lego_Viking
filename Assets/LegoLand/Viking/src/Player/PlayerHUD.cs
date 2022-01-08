using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Image HPImg;
   
    public Image AmmoImg;
    private Image[] AmmoImgs = new Image[20];
    private int DefaultAmmoNum;

    public Text speedTXT;
    private GameObject player;
    public GameObject pin;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        DefaultAmmoNum = player.GetComponent<Player>().defaultAmmo;
        for (int i = 0; i < DefaultAmmoNum; i++)
            AmmoImgs[i] = AmmoImg.transform.GetChild(i).gameObject.GetComponent<Image>();
    }

    void Update()
    {
        HPImg.fillAmount = (float)(player.GetComponent<Player>().HP) / (float)(player.GetComponent<Player>().DefaultHP);

        int ammoNum = player.GetComponent<Player>().ammo;

        if(ammoNum >= 0)
        {
            for (int i = 0; i < ammoNum; i++)
                AmmoImgs[i].enabled = true;

            for (int i = DefaultAmmoNum - 1; i > ammoNum - 1; i--)
                AmmoImgs[i].enabled = false;
        }

        //speedTXT.text ="speed : "+ player.GetComponent<Movement>().moveSpeed.ToString();
        //pinSpeedTXT.text = 5.ToString();
        //pin.GetComponent<Viking>().speed = float.Parse(pinSpeedTXT.text.ToString());
    }
}
