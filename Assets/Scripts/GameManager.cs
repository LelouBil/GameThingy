using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public BeatSynchronizer beatSynchronizer;

    public NoteCreator noteCreator;

    public ScoreManager scoreManager;

    public List<Musician> musicians;

    public GameObject pointObject;
    

    public bool started = false;

    public bool paused = false;


    public Text pauseText;

    public GameObject pauseTint;
    
    // Start is called before the first frame update
    void Start()
    {
        noteCreator.scr = scoreManager;
    }
    
    List<NoteMover> inactive = new List<NoteMover>();

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && started == false)
        {
            started = true;
            Debug.Log("yay");
            beatSynchronizer.StartMusic();
            foreach (var musician in musicians)
            {
                musician.RunGame();
            }
        }

        if (started && Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                pauseText.gameObject.SetActive(false);
                pauseTint.gameObject.SetActive(false);
                foreach (var o in inactive)
                {
                    if (o != null) o.move = true;
                }

                beatSynchronizer.Resume();
                paused = false;
            }
            else
            {
                beatSynchronizer.Pause();
                pauseText.gameObject.SetActive(true);
                pauseTint.gameObject.SetActive(true);
                for (int i = 0; i < pointObject.transform.childCount; i++)
                {
                    GameObject o = pointObject.transform.GetChild(i).gameObject;

                    if (o.GetComponent<NoteMover>() != null)
                    {
                        o.GetComponent<NoteMover>().move = false;
                    }
                
                    inactive.Add(o.GetComponent<NoteMover>());
                }

                paused = true;
            }
        }
    }
}
