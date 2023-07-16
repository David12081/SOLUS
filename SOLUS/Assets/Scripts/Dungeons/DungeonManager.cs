using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    public int[] dungeons = new int[3];
    static System.Random ran = new System.Random();
    public int rnd;
    public int numberOfDungeons = 5;
    int count = 0;
   
   // Move the player to a random dungeon 
    public void MoveToDungeon(){
        Debug.Log("MoveToDungeon");
        GenerateRnd();

        // Only if the array is empty
        if(dungeons.Length == 0)
        {
            // Open the next dungeon
            SceneManager.LoadScene(rnd, LoadSceneMode.Single);

            // Add the id to the array
            dungeons[count] = rnd;
        }

        else
        {
            // Check if the build id is already used
            if (IsInArray())
            {
                GenerateRnd();
            }
            else
            {
                SceneManager.LoadScene(rnd, LoadSceneMode.Single);
                dungeons[count] = rnd;
            }
        }
        
    }

    // Check if the id is in the array
    bool IsInArray(){
        for(int i = 0; i < dungeons.Length; i++){
            if(rnd == dungeons[i]){
                return true;
            }
        }

        return false;
    }

    // Generate random numbers
    void GenerateRnd(){
        rnd = ran.Next(0, numberOfDungeons);
    }

    public int GetLevelNum(){
        return count;
    }

}