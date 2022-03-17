using System.Linq;
using UnityEngine;
using TMPro;

public class PasswordModule : Module
{
    
    private string[] possiblePasswords = new string[] {
        "BROWN",
        "MRSAW",
        "CODES",
        "ABOUT",
        "ARRAY",
        "SPELL",
        "POWER",
        "PROMS",
        "SPEED",
        "RUPEE",
        "ROUND",
        "WRITE",
        "LARGE",
        "LEARN",
        "WOULD",
        "WORLD",
        "RIGHT",
        "SLIME",
        "NEVER",
        "NOUNS"
    };

    private char[][] lettersGiven;
    private int[] selectedIndexes;

    private Transform[] letterCubes;
    private string answer = "";


    // Start is called before the first frame update
    void Start()
    {
        letterCubes = new Transform[] {
            transform.GetChild(2),
            transform.GetChild(3),
            transform.GetChild(4),
            transform.GetChild(5),
            transform.GetChild(6)
        };
        setPassword();
    }

    void setPassword() {
        int randomIndex = Random.Range(0, possiblePasswords.Length);
        string password = possiblePasswords[randomIndex];
        answer = password;
        lettersGiven = new char[password.Length][];
        for (int i = 0; i < password.Length; i++) {
            lettersGiven[i] = new char[4];
            lettersGiven[i][0] = password.ToCharArray()[i];
            lettersGiven[i][1] = (char)('A' + Random.Range(0, 23));
            lettersGiven[i][2] = (char)('A' + Random.Range(0, 23));
            lettersGiven[i][3] = (char)('A' + Random.Range(0, 23));
            letterCubes[i].GetChild(0).GetComponent<TextMeshPro>().text = lettersGiven[i][0].ToString();
            letterCubes[i].GetChild(1).GetComponent<TextMeshPro>().text = lettersGiven[i][1].ToString();
            letterCubes[i].GetChild(2).GetComponent<TextMeshPro>().text = lettersGiven[i][2].ToString();
            letterCubes[i].GetChild(3).GetComponent<TextMeshPro>().text = lettersGiven[i][3].ToString();
        }
        //set random selectedIndexes between 0 and 2
        selectedIndexes = new int[password.Length];
        for (int i = 0; i < password.Length; i++) {
            selectedIndexes[i] = Random.Range(0, 4);
            letterCubes[i].GetComponent<LetterCube>().index = selectedIndexes[i];
            letterCubes[i].GetComponent<LetterCube>().currentRotation = selectedIndexes[i] * 90;
        }
    }

    void checkAnswer() {
        string input = "";
        for (int i = 0; i < 5; i++) {
            input += lettersGiven[i][selectedIndexes[i]];
        }
        if (possiblePasswords.Contains(input)) {
            solved();
        } else {
            fail();
        }
    }

    public void addIndex(int index) {
        selectedIndexes[index] = (selectedIndexes[index] + 1) % 4;
    }

    public void submit() {
        checkAnswer();
    }
}
