using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualarray;
    private void Start()
    {
        player.Instance.OnSelectCounterChange += Player_OnSelectCounterChange;
    }

    private void Player_OnSelectCounterChange(object sender, player.OnSelectCounterChangeEventArgs e)
    {
        if(e.Selectcounter == baseCounter) {
            show();
        }
        else
        {
            hide(); 
        }
    }
    private void show()
    {
        foreach(GameObject visual in visualarray)
        {
            visual.SetActive(true);
        }
       
    }
    private void hide()
    {
        foreach (GameObject visual in visualarray)
        {
            visual.SetActive(false);
        }
    }
}
