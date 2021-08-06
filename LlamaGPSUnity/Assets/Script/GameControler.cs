using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControler : MonoBehaviour
{
    public int countLlamas;
    public int totalLlamas = 10;
    public List<GameObject> listLlamas;
    public List<GameObject> listOriginalPosition;
    public GameObject llamaObj;
    public bool gameOver, starGame, plaing;
    public float timeRemainingInitial = 2; 
    private float timeRemaining; 
    public Text timeText, countLlamasText, timeTextGameOver;
    public GameObject pnStart, pnGameOver, pnPlay, pnWinner, pnLoser, pnAbout;

    // Start is called before the first frame update
    void Start()
    {
        starGame = true;
    }

    public void findedLlama(GameObject llamaFinded){
        listLlamas.Remove(llamaFinded);
        Destroy(llamaFinded);
        countLlamas ++;
    }

    // Update is called once per frame
    void Update()
    {
        if(starGame){
            whileStart();
        }else{
            if(plaing){
                whilePlay();
            }else{
                if(gameOver){
                    whileGameOver();
                }
            }
        }
        if (Input.GetKey(KeyCode.Space))
            Cursor.lockState = CursorLockMode.None;
    }

    private void whilePlay(){  
        pnStart.SetActive(false);
        pnGameOver.SetActive(false);
        pnPlay.SetActive(true);
        pnWinner.SetActive(false);
        pnLoser.SetActive(false);

        //Check game over
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
            ShowLlamasCount();
            if(countLlamas == totalLlamas){
                gameOver = true;
                plaing = false;
            }
        }
        else
        {
            Debug.Log("Time has run out!");
            plaing = false;
            gameOver = true;
            timeTextGameOver.text= "00:00";
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeTextGameOver.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void whileStart(){
        Cursor.lockState = CursorLockMode.None;
        if(!pnAbout.activeSelf)
            pnStart.SetActive(true);

        pnGameOver.SetActive(false);
        pnPlay.SetActive(false);
        pnWinner.SetActive(false);
        pnLoser.SetActive(false);
    }

    private void whileGameOver(){
        Cursor.lockState = CursorLockMode.None;
        pnStart.SetActive(false);
        pnGameOver.SetActive(true);
        pnPlay.SetActive(false);
        
        if (timeRemaining > 0)
        {
            //Winner
            pnWinner.SetActive(true);
        }else{
            //lose
            pnLoser.SetActive(true);
        }
    }

    public void newGame(){
        countLlamas = 0;
        timeRemaining = timeRemainingInitial * 60;

        Cursor.lockState = CursorLockMode.Locked;
       
        starGame = false;
        plaing = true;
        gameOver = false;

        //Create new llamas
        InstantiateLhamas();

    }

    public void mainMenu(){
        starGame = true;
        plaing = false;
        gameOver = false;
        pnStart.SetActive(true);
        pnAbout.SetActive(false);
        pnGameOver.SetActive(false);
        pnPlay.SetActive(false);
        pnWinner.SetActive(false);
        pnLoser.SetActive(false);
    }

    public void showAbout(){
        pnStart.SetActive(false);
        pnAbout.SetActive(true);
        pnGameOver.SetActive(false);
        pnPlay.SetActive(false);
        pnWinner.SetActive(false);
        pnLoser.SetActive(false);
    }

    public void openURL(string url){
        Application.OpenURL(url);
    }

    public void InstantiateLhamas(){
        int rand = 0;
        List<int> lastPosition = new List<int>();
        Vector3 variablePosition = new Vector3();
        foreach(GameObject llamaOld in listLlamas){
            Destroy(llamaOld);
        }

        for(int index = 0; index < totalLlamas; index ++){
            while (lastPosition.IndexOf(rand) >= 0)
            {
                rand = Random.Range(0, listOriginalPosition.Count - 1);   
            }
            variablePosition = listOriginalPosition[rand].transform.position;
            variablePosition.x = Random.Range(variablePosition.x -1.5f, variablePosition.x +1.5f);
            variablePosition.z = Random.Range(variablePosition.z -1.5f, variablePosition.z +1.5f);
            //listOriginalPosition[rand].GetComponent<Animator>().SetBool("show", true);
            GameObject newLlama = Instantiate(llamaObj, variablePosition, listOriginalPosition[rand].transform.rotation);
            newLlama.SetActive(true);
            listLlamas.Add(newLlama);
            lastPosition.Add(rand);
        }
    }

    public void ShowLlamasCount(){
        countLlamasText.text = "Missing llamas: " + countLlamas.ToString() + "/" + totalLlamas.ToString();
    }
}
