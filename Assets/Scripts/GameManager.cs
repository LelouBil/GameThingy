using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public BeatSynchronizer beatSynchronizer;

    public NoteCreator noteCreator;

    public ScoreManager scoreManager;

    public List<Musician> musicians;
    

    public bool started = false;
    
    // Start is called before the first frame update
    void Start()
    {
        noteCreator.scr = scoreManager;
    }

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
    }
}
