using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class informationPanel : MonoBehaviour
{
    public GameObject panelInformation;
    public GameObject panelMenu;

    public bool showPanelInformation;
    public bool showPanelMenu;

    // Start is called before the first frame update
    void Start()
    {
        showPanelInformation = false;
        showPanelMenu = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void panelInfo()
    {
        if(showPanelInformation)
        {
            panelMenu.gameObject.SetActive(false);
            panelInformation.gameObject.SetActive(true);
            showPanelInformation = false;
        }
        else
        {
            panelMenu.gameObject.SetActive(true);
            panelInformation.gameObject.SetActive(false);
            showPanelInformation = true;
        }
    }
}
