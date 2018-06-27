using UnityEngine;
using System.Collections;

public class PlayerCollectiblesManager : MonoBehaviour {
    //Pieces of Papers
    public int totPiecesOfPaper=4;
    
    //Weapons parts collectibles
    public int totPiecesOfWeapons = 1;
    
    private int piecesCollected = 0;
    private int piecesOfWeaponCollected=0;
    // pieces of story to complete
    //poi da vedere se anche lui da fare public o no
    private bool researchCompleted = false;
    private bool puzzleCompleted = false;
    private bool keyfound=false;
    private bool prisonersfreed = false;

    public GameObject puzzle;


	public void CollectAPieceOfPaper()
    {
        piecesCollected++;
        if(piecesCollected==1)
            this.GetComponent<PlayerController>().NPC_Chankeli.GetComponent<ChankeliController>().SayPlotThings(3);
        if (piecesCollected == totPiecesOfPaper)
        {
            researchCompleted = true;      
            this.GetComponent<PlayerController>().NPC_Chankeli.GetComponent<ChankeliController>().SayPlotThings(6);
        }
    }

    public void CollectAPieceOfWeapon()
    {
        totPiecesOfWeapons++;
        if (piecesOfWeaponCollected == totPiecesOfWeapons)
        {
         // Do something. not relevant for the storyline/ prototype
           // Debug.Log("Hai trovato tutti i pezzi");
        }
    }

    public int StartThePuzzle()
    {   
        //researchCompleted=true;
        if (researchCompleted)
        {   
            // the puzzle
            puzzle.GetComponent<PuzzleManager>().ShowThePuzzle(this.GetComponent<PlayerBlaster>().active);

            // puzzleCompleted = true;
            if (puzzleCompleted) // already done
                return 0;
            else
                return 2;
        }
        else
            return 1; 
    }

    // It try to open the safe: the int is the index for the element to say in the interaction
    public int OpenTheSafe()
    {
        //All pieces collected... need to do the puzzle
        if (researchCompleted)
        {
            if (puzzleCompleted)
            {
                //puzzle risolto
                keyfound = true;
                this.GetComponent<PlayerController>().NPC_Chankeli.GetComponent<ChankeliController>().setIndex(8);
                return 0;
            }
            else
                return 1; //you have to solve the puzzle
        }
        else
            return 1;
    }

    // It tries to open the safe: the int is the index for the element to say in the interaction
    public int OpenTheCell()
    {
        bool alarm = GameObject.Find("Bandits").GetComponent<AlarmController>().GetAlarm(); // Da inserire il vero riferimento
        //Key aquired or not?
        //Debug.Log("prisoners: " + prisonersfreed);
        if (!prisonersfreed)
        {
            if (keyfound)
            {
                if (!alarm || GameObject.Find("Bandits").GetComponent<AlarmController>().allWaveDead)
                {
                    prisonersfreed = true;
                    return 0; //can free the prisoners
                }
                else
                {
                    this.GetComponent<PlayerController>().NPC_Chankeli.GetComponent<ChankeliController>().SayPlotThings(7);
                    return 2; //not all waves killed but alarm raised
                }
            }
            else
                return 1;
        }
        else
        {
            if(GameObject.Find("Bandits").GetComponent<AlarmController>().allWaveDead)
                return 3;
        }
        return 1;
    }

    public int EnterInTheCave()
    {
        //Key aquired or not?
        if (prisonersfreed)
        {
            //level finished
            GetComponent<PlayerDeathController>().LevelFinished();
            return 0; 
        }
        else
        {
            this.GetComponent<PlayerController>().NPC_Chankeli.GetComponent<ChankeliController>().SayPlotThings(8);
            return 1;
        }            
    }

    public void PuzzleSolved()
    {
        puzzleCompleted = true;
    }

    public bool GetPrisonerFreed()
    {
        return prisonersfreed;
    }


}

