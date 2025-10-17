using UnityEngine;

public class GameManager : MonoBehaviour
{


    int PuzzleCounter = 0;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is create


    public void PuzzleCompleted(int count)
    {
        PuzzleCounter += count;
         Debug.Log("completed");
        if (PuzzleCounter == 3)
        {
             ChangeDialogueState change = new ChangeDialogueState(DialougeChange.TutorialPuzzle, 1);
             EventBus.Act(change);
        }
    }


    public void GameOver(string reason)
    {
        //playerStats.isGameOver = true;
        Debug.Log("Game Over: " + reason);
        // Load end screen or show UI
    }


}
