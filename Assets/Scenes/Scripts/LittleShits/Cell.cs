using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]Transform backg;

    [SerializeField] int neuronNumbers, nNodes;
    string[] sensors = { "px", "py", "rn", "tm", "bx" };
    string[] nodes = { "n0", "n1", "n2" };
    string[] actions = { "mn", "ms", "me", "mw", "dm" };

    [SerializeField] float[] weights0;
    /**/
    [SerializeField] float[] ns;
    /**/
    [SerializeField] float[] weights1;
    /**/
    [SerializeField] float[] outputs;

    [SerializeField] public string[] genome;    //CONTAINING GENS OF LONGITUDE 3*CONEXIONS
    float speedx = 0, speedy = 0;
    public bool child = false;


    // Start is called before the first frame update
    void Awake()
    {
        genome = new string[neuronNumbers];
        weights0 = new float[neuronNumbers];     //[neuron]
        weights1 = new float[neuronNumbers];     //[neuron]
        outputs = new float[neuronNumbers];
        ns = new float[nNodes];
        backg = GameObject.FindGameObjectsWithTag("backgr")[0].transform;   //ONLY WORKS WITH ONE BACKGR

    }
    void Start()
    {
        if (!child)
        {
            encodeGen(genome);
        }

        decodeGen(genome);

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < backg.localScale.x / 2 - 1 && transform.position.x > -backg.localScale.x / 2 + 1)
            transform.Translate(speedx * Time.deltaTime, 0, 0);

        if (transform.position.y < backg.localScale.y / 2 - 1 && transform.position.y > -backg.localScale.y / 2 + 1)
            transform.Translate(0, speedy * Time.deltaTime, 0);

    }


    public void encodeGen(string[] genome)
    {
        for (int i = 0; i < genome.Length; i++)     //number of neurons
        {
            genome[i] += sensors[Random.Range(0, sensors.Length)];
            genome[i] += nodes[Random.Range(0, nodes.Length)];
            genome[i] += actions[Random.Range(0, actions.Length)];
        }

    }
    public void decodeGen(string[] gen)
    {
        genome = gen;
        for (int i = 0; i < gen.Length; i++)
        {
            //Debug.Log(gameObject.name+" "+gen[0]);
            makeFirstConexions(gen[i], i);

        }

        for (int i = 0; i < gen.Length; i++)
        {
            calculateNodes(gen[i], i);

            for (int k = 0; k < nNodes; k++)
                ns[k] = (float)System.Math.Tanh(ns[k]);
        }

        for (int i = 0; i < gen.Length; i++)         //once per gen word
        {
            ConexionsOutputs(gen[i], i);
            getOutputs(gen[i], i);
            //Debug.Log("weights1[" + i + "]: " + weights1[i]);

        }
    }

    void makeFirstConexions(string gen, int j)      // gen = gen sequence       j = number of gen sequence
    {

        string x = "";
        x += gen[0];
        x += gen[1];

        switch (x)       //INPUTS
        {
            default:
                weights0[j] = 0;
                break;

            case "px":
                weights0[j] = transform.position.x;
                break;

            case "py":
                weights0[j] = transform.position.y;
                break;

            case "rn":
                weights0[j] += Random.Range(-4.0f, 4.0f);
                break;

            case "tm":
                weights0[j] = Time.frameCount;
                break;

            case "bx":
                weights0[j] += (backg.position - transform.position).normalized.x;
                break;
        }

        //Debug.Log("weights0[" + j + "]: " + weights0[j]);

    }
    void calculateNodes(string gen, int j)
    {
        string x = "";
        x += gen[3];

        switch (x)       //INPUTS
        {
            default: break;

            case "0":
                ns[int.Parse(x)] += weights0[j];
                GetComponent<SpriteRenderer>().color += new Color(10, 0, 0);
                break;

            case "1":
                GetComponent<SpriteRenderer>().color += new Color(0, 10, 0);
                ns[int.Parse(x)] += 2 * weights0[j];
                break;

            case "2":
                GetComponent<SpriteRenderer>().color += new Color(0, 0, 10);
                ns[int.Parse(x)] += ((-1) ^ j) * weights0[j];
                break;
        }
        // Debug.Log("ns[n" + x + "]: " + ns[int.Parse(x)]);

    }

    void getOutputs(string gen, int j)
    {
        string x = "";       //node

        x += gen[4];
        x += gen[5];

        if (outputs[j] > 0.5f)
            switch (x)       //INPUTS
            {
                default:
                    outputs[j] = 0;
                    break;
                case "dm":
                    GetComponent<SpriteRenderer>().color += new Color(0, 0, -10);
                    speedx = 0;
                    speedy = 0;
                    break;

                case "mn":
                    speedy = 10;
                    GetComponent<SpriteRenderer>().color += new Color(-10, 0, 0);

                    break;

                case "ms":
                    speedy = -10;
                    GetComponent<SpriteRenderer>().color += new Color(10, 0, 0);

                    break;

                case "me":
                    speedx = 10;
                    GetComponent<SpriteRenderer>().color += new Color(0, -10, 0);

                    break;

                case "mw":
                    speedx = -10;
                    GetComponent<SpriteRenderer>().color += new Color(0, 10, 0);

                    break;
            }


    }
    void ConexionsOutputs(string gen, int j)
    {
        string x = "";
        x += gen[3];        //node

        weights1[j] += Random.Range(-4.0f, 4.0f);
        outputs[j] = (float)System.Math.Tanh(weights1[j] * ns[int.Parse(x)]);

    }


}
