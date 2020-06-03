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

        StartCoroutine(StartSequence());
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


    public AudioClip crowd;

    public AudioSource sfx;

    public Animator crowdAnimator;

    public Animator yellow;
    public Animator red;
    public Animator green;
    public Animator blue;

    public IEnumerator StartSequence()
    {
        float speed = 1;
        sfx.PlayOneShot(crowd);
        while (speed > 0)
        {
            if (speed > 0.65) speed -= 0.04f;
            else speed -= 0.28f;
            crowdAnimator.speed = speed;
            yield return new WaitForSeconds(1);
        }
        crowdAnimator.SetTrigger(Start1);
        
        yield return new WaitForSeconds(0.5f);
        _clickspeed = 1/ (60f / beatSynchronizer.bpm);
        crowdAnimator.speed = _clickspeed;
        yellow.speed = _clickspeed;
        red.speed = _clickspeed;
        blue.speed = _clickspeed;
        green.speed = _clickspeed;
        yellow.SetTrigger(Jump);
        yield return null;
    }

    private int clickcount;

    public AudioClip click;
    
    
    public void Click()
    {
        clickcount++;
        sfx.PlayOneShot(click);
        switch (clickcount)
        {
            case 1:
                red.SetTrigger(Jump);
                break;
            case 2:
                blue.SetTrigger(Jump);
                break;
            case 3:
                green.SetTrigger(Jump);
                break;
            case 4:
                red.SetTrigger(Jump);
                blue.SetTrigger(Jump);
                green.SetTrigger(Jump);
                StartCoroutine(LaunchGame());
                
                return;
        }
    }

    public void EndAnim()
    {
        if (clickcount > 3)
        {
            
            
        }
    }

    private IEnumerator LaunchGame()
    {
        yield return new WaitForSeconds(1/_clickspeed);
        yellow.enabled = false;
        green.enabled = false;
        red.enabled = false;
        blue.enabled = false;
        StartGame();
    }

    public void StartGame()
    {
        started = true;
        Debug.Log("yay");
        beatSynchronizer.StartMusic();
        foreach (var musician in musicians)
        {
            musician.RunGame();
        }
    }

    private int pauseCounter = 3;
    private static readonly int Jump = Animator.StringToHash("Jump");
    private float _clickspeed;
    private static readonly int Start1 = Animator.StringToHash("Start");
    private static readonly int Combo = Animator.StringToHash("Combo");
    public AudioClip crowdShort;

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

    public int crowdcounter = 0;

    public void CheckCrowd()
    {
        crowdcounter++;
        if (crowdcounter >= 12)
        { 
            crowdcounter = 0;
            if (scoreManager.combo >= 5)
            {
                crowdAnimator.SetTrigger(Combo);
            }
            
        }
    }
}
