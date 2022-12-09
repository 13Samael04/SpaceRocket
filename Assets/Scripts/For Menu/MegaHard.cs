using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaHard : MonoBehaviour
{

    public GameObject ButtonMegaHardActive, ButtonMegaHardDiactive;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("MegaHard") == "MegaHardActive")
        {
            print("Работает" + PlayerPrefs.GetString("MegaHard"));
            ButtonMegaHardActive.SetActive(false);
            ButtonMegaHardDiactive.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveHardAction(string Action)
    {
        PlayerPrefs.SetString("MegaHard", Action);
        ButtonMegaHardActive.SetActive(false);
        ButtonMegaHardDiactive.SetActive(true);
        print (PlayerPrefs.GetString("MegaHard"));
    }
    public void DiActiveHardAction(string Action)
    {
        PlayerPrefs.SetString("MegaHard", Action);
        ButtonMegaHardDiactive.SetActive(false);
        ButtonMegaHardActive.SetActive(true);
        print (PlayerPrefs.GetString("MegaHard"));
    }
}
