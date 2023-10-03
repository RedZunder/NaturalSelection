using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Killer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI butt,perctText;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject blockVision;
    [SerializeField] Transform sideKiller;
    [SerializeField] Slider percentage;

    Transform backk;
    void Awake()
    {
        canvas.gameObject.SetActive(!blockVision.activeSelf);       //also in Conditions.cs
        backk = GameObject.FindGameObjectsWithTag("backgr")[0].transform;   //ONLY WORKS WITH ONE BACKGR
    }
    public void randomKill()
    {
        for (int i = Spawner.creatures.Count-1; i > 0; i--)
        {
            if (Random.Range(0, 1f) <= percentage.value / 100f)
            {
                GameObject cell = Spawner.creatures[i];
                killBitch(cell);
            }
        }
    }
    public void updateValue()
    {
        perctText.text = "" + percentage.value;
    }

    public void sideKill()
    {
        //sideKiller.gameObject.SetActive(true);

        for (int i = Spawner.creatures.Count - 1; i > 0; i--)
        {
            Transform cell = Spawner.creatures[i].transform;
            if ((cell.position.x < 2*sideKiller.position.x)&&(cell.position.x > sideKiller.position.x-sideKiller.localScale.x/2))
            {
                killBitch(cell.gameObject);
            }
        }
    }


    public static void killBitch(GameObject cell)
    {
        Destroy(cell);
        Spawner.creatures.Remove(cell);

    }
    public void updateSideKiller()
    {
        sideKiller.localScale = new Vector2(backk.localScale.x / 2, backk.localScale.y);
        sideKiller.position = new Vector2(sideKiller.localScale.x / 2, backk.position.y);
    }
}
