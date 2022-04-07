using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.UI;

public class SubScripts : MonoBehaviour
{
    public static SubScripts instance;
    public Image backgroundColor;

    public GameObject textBox;
    StreamReader reader = null;
    string fileName;
    string tutorialFile;
    public int x = 0;
    //string[][] dialogueText;
    List<String>[] tutorialText = new List<String>[100];
    List<int>[] tutorialTimers = new List<int>[100];
    int[] tutorialStopCount = new int[10];
    List<String>[] dialogueText = new List<String>[100];
    List<int>[] timers = new List<int>[100];
    int[] stationCount = new int[10];

    public int lineCount;
    public int tutLineCount;
    private Color invisible = new Color(0,0,0,0);
    private Color active = new Color(0, 0, 0, 180);


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        backgroundColor.color = invisible;
        fileName = Path.Combine(Application.streamingAssetsPath, "dialogueText.txt");
        tutorialFile = Path.Combine(Application.streamingAssetsPath, "tutorialText.txt");
        //These need to be changed to a better option than hardcoding :)


        //Make sure everything that needs to be "empty" is
        textBox.GetComponent<Text>().text = "";
        int station = 0;
        int tempInt = 0;
        int count = 0;
        lineCount = 0;
        tutLineCount = 0;

        //get the number of lines in the text file
        try
        {
            reader = new StreamReader(fileName);
            while (reader.ReadLine() != null)
            {
                lineCount++;

            }
            reader.Close();
        }
        catch (Exception e)
        {
            Debug.Log("Cannot Read the File: " + e);
        }
        try
        {
            reader = new StreamReader(tutorialFile);
            while (reader.ReadLine() != null)
            {
                tutLineCount++;

            }
            reader.Close();
        }
        catch (Exception e)
        {
            Debug.Log("Cannot Read the File: " + e);
        }

        //loop through the file more slowly now and separate the information correctly
        try
        {

            reader = new StreamReader(fileName);
            for (int i = 0; i < lineCount; i++)
            {
                string[] temp = reader.ReadLine().Split('|');

                //add station number
                //count how many for station number
                //if next station number is different, add count to station's second spot
                int.TryParse(temp[0].Trim(), out tempInt);
                if (tempInt != station)
                {
                    stationCount[station] = count;
                    station = tempInt;
                    count = 0;
                }
                else
                {
                    count++;
                }


                //add dialogue to string array
                //play just as many lines as there are in the station
                if (dialogueText[station] == null)
                {
                    dialogueText[station] = new List<string>();
                }
                dialogueText[station].Add(temp[1].Trim());



                //add each strings time to the array
                //and loop through the number of stations to get it
                int.TryParse(temp[2].Trim(), out tempInt);

                if (timers[station] == null)
                {
                    timers[station] = new List<int>();
                }
                timers[station].Add(tempInt);


            }

        }
        catch (Exception e)
        {
            Debug.Log("Something went wrong with reading the file: " + e);

        }
        finally
        {
            reader.Close();
        }
        /////////////////////////////////////////////////////////////////////////////////////
        ////Tutorial Start//////////////////////
        /////////////////////////////////////////////////////////////////////////////////////
        try
        {
            tempInt = 0;
            station = 0;
            count = 0;
            reader = new StreamReader(tutorialFile);
            for (int i = 0; i < tutLineCount; i++)
            {
                string[] temp = reader.ReadLine().Split('|');

                //add station number
                //count how many for station number
                //if next station number is different, add count to station's second spot
                int.TryParse(temp[0].Trim(), out tempInt);
                if (tempInt != station)
                {
                    tutorialStopCount[station] = count;
                    station = tempInt;
                    count = 0;
                }
                else
                {
                    count++;
                }


                //add dialogue to string array
                //play just as many lines as there are in the station
                if (tutorialText[station] == null)
                {
                    tutorialText[station] = new List<string>();
                }
                tutorialText[station].Add(temp[1].Trim());



                //add each strings time to the array
                //and loop through the number of stations to get it
                int.TryParse(temp[2].Trim(), out tempInt);

                if (tutorialTimers[station] == null)
                {
                    tutorialTimers[station] = new List<int>();
                }
                tutorialTimers[station].Add(tempInt);


            }

        }
        catch (Exception e)
        {
            Debug.Log("Something went wrong with reading the file: " + e);

        }
        finally
        {
            reader.Close();
        }

    }


    public void Run(int x)
    {
        StopAllCoroutines();
        StartCoroutine(TextSequence(x));
    }
    public void tutorialRun(int x)
    {
        StopAllCoroutines();
        StartCoroutine(TutorialSequence(x));
    }

    //testing out the text showing up
    //This works and will be used as a basis for getting the file text to work
    IEnumerator TextSequence()
    {
        yield return new WaitForSeconds(1);
        textBox.GetComponent<Text>().text = "Do we really need a gate, Mom?";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<Text>().text = "It feels like it cuts us off from the world";
        yield return new WaitForSeconds(4);
        textBox.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(1);
        textBox.GetComponent<Text>().text = "The gate makes it our home, child";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<Text>().text = "It helps draw a line between our safe haven and the outside";
        yield return new WaitForSeconds(5);
        textBox.GetComponent<Text>().text = "";

    }

    //takes the int provided from the trigger box, looks in the file for those lines, and returns them correctly :)
    //theoretically
    IEnumerator TextSequence(int x)
    {
        //yield return new WaitForSeconds(1);
        //loop through the given station's line count, change the subtitles based on the necessary text and with the right time length
        //StationCount[0] = 0
        backgroundColor.color = active;
        for (int i = 0; i <= stationCount[x]; i++)
        {
            textBox.GetComponent<Text>().text = dialogueText[x][i];
            yield return new WaitForSeconds(timers[x][i]);
        }
        textBox.GetComponent<Text>().text = "";
        backgroundColor.color = invisible;
    }

    IEnumerator TutorialSequence(int x)
    {
        backgroundColor.color = active;
        for (int i = 0; i < tutorialStopCount[x]; i++)
        {
            textBox.GetComponent<Text>().text = tutorialText[x][i];
            yield return new WaitForSeconds(tutorialTimers[x][i]);

        }
        textBox.GetComponent<Text>().text = "";
        backgroundColor.color = invisible;
    }

}
