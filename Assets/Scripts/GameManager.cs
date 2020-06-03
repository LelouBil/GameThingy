using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public BeatSynchronizer beatSynchronizer;
    
    public BeatCounter beatCounter;

    public BeatCounter otherCounter;

    public NoteCreator noteCreator;

    public AudioSource pauseAudio;

    public ScoreManager scoreManager;

    public List<Musician> musicians;

    public GameObject pointObject;
    

    public bool started = false;

    public bool paused = false;

    public bool finished = false;


    public Text pauseText;

    public GameObject pauseTint;

    public Finisher finishTint;
    
    // Start is called before the first frame update
    void Start()
    {
        noteCreator.scr = scoreManager;
    }
    
    List<NoteMover> inactive = new List<NoteMover>();

    // Update is called once per frame
    void Update()
    {
        if (beatSynchronizer.audioSource.time >= beatSynchronizer.audioSource.clip.length || Input.GetKeyDown(KeyCode.Space))
        {
            beatSynchronizer.audioSource.Pause();
            finished = true;

            StartCoroutine(EndCredits());
        }
        
        
        if (Input.anyKeyDown && !started && !finished )
        {
            started = true;
            Debug.Log("yay");
            beatSynchronizer.StartMusic();
            foreach (var musician in musicians)
            {
                musician.RunGame();
            }
        }

        if (started && Input.GetKeyDown(KeyCode.Escape) && !finished)
        {
            if (paused)
            {
                pauseAudio.Stop();
                StartCoroutine(PauseWait());
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
                pauseAudio.Play();
                paused = true;
            }
        }
    }

    private int pauseCounter = 3;

    private IEnumerator PauseWait()
    {
        while (paused)
        {
            if (pauseCounter > 0)
            {
                pauseText.text = pauseCounter.ToString();
                yield return new WaitForSeconds(1);
                pauseCounter--;
            }
            else
            {
                pauseCounter = 3;
                pauseText.text = "pause";
                beatSynchronizer.Resume();
                paused = false;
                pauseText.gameObject.SetActive(false);
                pauseTint.gameObject.SetActive(false);
                foreach (var o in inactive)
                {
                    if (o != null) o.move = true;
                }

                beatCounter.dspDelay += (float) (AudioSettings.dspTime - beatSynchronizer.pauseTime);
                otherCounter.dspDelay += (float) (AudioSettings.dspTime - beatSynchronizer.pauseTime);
            }

            
        }
    }

    private IEnumerator EndCredits()
    {
        yield return new WaitForSeconds(5);

        finishTint.Finish(this);

    }
}
