using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    private List<string> linesFormed;
    // Start is called before the first frame update
    void Start()
    {
        linesFormed = new List<string>();
    }

    public void addToList(string item)
    {
        linesFormed.Add(item);
    }

    /*
    public void getNumberOfLines(int startingNumber, int numberOfColumns, int mouseOverObjectNumber)
    {
        if ((startingNumber + numberOfColumns) == mouseOverObjectNumber || (startingNumber - numberOfColumns) == mouseOverObjectNumber)
        {
            int numberOfLines = 0;
            string toCheck = (startingNumber - 1).ToString() + startingNumber.ToString();
            if (linesFormed.Contains(toCheck))
                numberOfLines++;
            toCheck = (startingNumber - 1).ToString() + (startingNumber - 1 + numberOfColumns).ToString();
            if (linesFormed.Contains(toCheck))
                numberOfLines++;
            toCheck = (startingNumber - 1 + numberOfColumns).ToString() + (startingNumber + numberOfColumns).ToString();
            if (linesFormed.Contains(toCheck))
                numberOfLines++;
            if (numberOfLines == 3)
                Debug.Log("Daba Ban Gia Left");

            numberOfLines = 0;
            toCheck = startingNumber.ToString() + (startingNumber + 1).ToString();
            if (linesFormed.Contains(toCheck))
                numberOfLines++;
            toCheck = (startingNumber + 1).ToString() + (startingNumber + 1 + numberOfColumns).ToString();
            if (linesFormed.Contains(toCheck))
                numberOfLines++;
            toCheck = (startingNumber + numberOfColumns).ToString() + (startingNumber + +numberOfColumns + 1).ToString();
            if (linesFormed.Contains(toCheck))
                numberOfLines++;


            if (numberOfLines == 3)
                Debug.Log("Daba Ban Gia Right");
        }
        else
        {
            if ((startingNumber + 1) == mouseOverObjectNumber || (startingNumber - 1) == mouseOverObjectNumber)
            {
                int numberOfLines = 0;
                string toCheck = (startingNumber - numberOfColumns).ToString() + (startingNumber).ToString();
                if (linesFormed.Contains(toCheck))
                    numberOfLines++;
                toCheck = (startingNumber - numberOfColumns).ToString() + (startingNumber + 1 - numberOfColumns).ToString();
                if (linesFormed.Contains(toCheck))
                    numberOfLines++;
                toCheck = (startingNumber + 1 - numberOfColumns).ToString() + (startingNumber + 1).ToString();
                if (linesFormed.Contains(toCheck))
                    numberOfLines++;

                if (numberOfLines == 3)
                    Debug.Log("Daba Ban Gia Ooper");

                numberOfLines = 0;
                toCheck = (startingNumber).ToString() + (startingNumber + numberOfColumns).ToString();
                if (linesFormed.Contains(toCheck))
                    numberOfLines++;
                toCheck = (startingNumber + numberOfColumns).ToString() + (startingNumber + 1 + numberOfColumns).ToString();
                if (linesFormed.Contains(toCheck))
                    numberOfLines++;
                toCheck = (startingNumber + 1).ToString() + (startingNumber + numberOfColumns + 1).ToString();
                if (linesFormed.Contains(toCheck))
                    numberOfLines++;

                if (numberOfLines == 3)
                    Debug.Log("Daba Ban Gia Neechy");
            }
        }
    }
    */

    public void getNumberOfLines(int startingNumber, int numberOfColumns, int mouseOverObjectNumber)
    {
        int x, y;
        if (startingNumber < mouseOverObjectNumber)
        {
            x = startingNumber;
            y = mouseOverObjectNumber;
        }
        else
        {
            y = startingNumber;
            x = mouseOverObjectNumber;
        }
        int n = numberOfColumns;
        int numberOfLines = 0;
        if ((startingNumber + numberOfColumns) == mouseOverObjectNumber || (startingNumber - numberOfColumns) == mouseOverObjectNumber)
        {
            string querry = x.ToString() + (x + 1).ToString();
            if (linesFormed.Contains(querry))
                numberOfLines++;
            querry = (y).ToString() + (y + 1).ToString();
            if (linesFormed.Contains(querry))
                numberOfLines++;
            querry = (x+1).ToString() + (y + 1).ToString();
            if (linesFormed.Contains(querry))
                numberOfLines++;

            if(numberOfLines == 3)
                Debug.Log("Right");
            numberOfLines = 0;
            querry = (x - 1).ToString() + (x).ToString();
            if (linesFormed.Contains(querry))
                numberOfLines++;
            querry = (y-1).ToString() + (y).ToString();
            if (linesFormed.Contains(querry))
                numberOfLines++;
            querry = (x-1).ToString() + (y-1).ToString();
            if (linesFormed.Contains(querry))
                numberOfLines++;
            if(numberOfLines==3)
                Debug.Log("Left");
        }
        else
        {
            string querry = x.ToString() + (x + n).ToString();
            if (linesFormed.Contains(querry))
                numberOfLines++;
            querry = (x + n).ToString() + (y + n).ToString();
            if (linesFormed.Contains(querry))
                numberOfLines++;
            querry = (y).ToString() + (y + n).ToString();
            if (linesFormed.Contains(querry))
                numberOfLines++;

            if(numberOfLines==3)
                Debug.Log("DOWN");
            numberOfLines = 0;
            querry = (x-n).ToString() + (x).ToString();
            if (linesFormed.Contains(querry))
                numberOfLines++;
            querry = (x - n).ToString() + (y - n).ToString();
            if (linesFormed.Contains(querry))
                numberOfLines++;
            querry = (y-n).ToString() + (y).ToString();
            if (linesFormed.Contains(querry))
                numberOfLines++;

            if(numberOfLines==3)
                Debug.Log("UP");
        }
    }
}
