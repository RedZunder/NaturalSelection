using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Repopulate : MonoBehaviour
{
    [SerializeField] Transform background;
    [SerializeField] TextMeshProUGUI genN;
    [SerializeField] GameObject prefab;
    [SerializeField] TMPro.TextMeshProUGUI shitsNumber;

    int generation = 1;
    string[] aux;

    // Start is called before the first frame update
    void Start()
    {
        genN.text = "" + generation;
    }


    public void rePopulate()
    {
        List<GameObject> gensPool = new List<GameObject>();

        //SAVE ALL GENOMES AND EMPTY CREATURES ARRAY
        for (int i = Spawner.creatures.Count - 1; i >= 0; i--)
        {
            GameObject parent = Spawner.creatures[Random.Range(0, Spawner.creatures.Count)];
            saveGens(parent, ref gensPool);
            Killer.killBitch(parent);
        }

        //REPOPULATE FROM THE GENS POOL
        while (Spawner.creatures.Count < Spawner.cellsNumber)
        {
            Cell parentGen = gensPool[Random.Range(0, gensPool.Count)].GetComponent<Cell>();
            createCell(parentGen);
        }
        generation++;
        genN.text = "" + generation;    //text Generations

    }
    void saveGens(GameObject dad, ref List<GameObject> cellGen)
    {
        cellGen.Add(dad.gameObject);
    }
    void createCell(Cell dad)
    {

        int d = ((int)background.localScale.x - 10) / 2;
        Vector2 initialPos = new Vector2((int)Random.Range(-d, d), (int)Random.Range(-d, d));
        GameObject x = Instantiate(prefab, initialPos, Quaternion.identity);

        x.GetComponent<Cell>().genome = dad.genome;

        x.GetComponent<Cell>().child = true;
        x.name = x.GetComponent<Cell>().genome[0] + "  " + generation;
        Spawner.creatures.Add(x);

        x.GetComponent<Cell>().decodeGen(dad.genome);

    }

}
