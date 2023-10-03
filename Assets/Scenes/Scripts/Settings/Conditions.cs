using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Conditions : MonoBehaviour
{
    [SerializeField] Transform background;
    //[SerializeField] TMP_InputField sizeT;
    [SerializeField] Camera cam;
    [SerializeField] TextMeshProUGUI butt;
    [SerializeField] Slider slideSize;
    [SerializeField] Killer killManager;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;   //60fps
        slideSize.gameObject.SetActive(true);
        Time.timeScale = 0;
        killManager.updateSideKiller();
    }

    public void sizeConfirmed()     //change bckgr size and camera zoom accordingly
    {
        Vector3 newScale = background.localScale;

        switch (slideSize.value)
        {
            default: break;
            case 1:
                newScale = new Vector3(80, 80, 0);
                cam.orthographicSize = 45;
                break;
            case 2:
                newScale = new Vector3(120, 120, 0);
                cam.orthographicSize = 70;
                break;
            case 3:
                newScale = new Vector3(150, 150, 0);
                cam.orthographicSize = 90;
                break;
        }

        background.localScale = newScale;
        killManager.updateSideKiller();
    }


    public void resetScene()
    {
        if (butt.text == "RUN")     //after stopping it reloads scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
