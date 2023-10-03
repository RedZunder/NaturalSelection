using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    [Header("CANVAS")]
    [SerializeField] Canvas canvas;
    [SerializeField] Canvas evolveCanvas;
    [SerializeField] Canvas killerCanvas;
    
    [Header("OTHERS")]
    [SerializeField] TMPro.TextMeshProUGUI shitsNumber;
    [SerializeField] GameObject blockVision;
    [SerializeField] Killer killManager;
    
    [Header("BUTTONS")]
    [SerializeField] TextMeshProUGUI butt;

    Spawner spawner;


    // Start is called before the first frame update
    void Start()
    {
        killerCanvas.gameObject.SetActive(false);
        blockVision.SetActive(true);
        spawner = FindObjectOfType<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        shitsNumber.text = "" + Spawner.creatures.Count;
    }
    public void startButton()
    {
        Time.timeScale = 1;
        daFade();
        killManager.updateSideKiller();

    }
    void daFade()       //make UI shit invisible
    {
        blockVision.gameObject.SetActive(!blockVision.activeSelf);
        evolveCanvas.gameObject.SetActive(!blockVision.activeSelf);

        if (blockVision.gameObject.activeSelf)
        {
            butt.text = "RUN";
            canvas.gameObject.SetActive(true);
        }
        else
        {
            butt.text = "RESET";
            canvas.gameObject.SetActive(false);
            spawner.spawnThem();
        }
    }

    public void showKillerMenu()
    {
        killerCanvas.gameObject.SetActive(!killerCanvas.gameObject.activeSelf);
    }



}
