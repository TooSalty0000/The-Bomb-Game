using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class CalculatorModule : Module
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    public TextMeshPro[] digits;
    public int problemDigit;
    private Vector3 originalDigits;
    private List<int> enteredDigits = new List<int>();
    [SerializeField]
    private List<Vector3> possibleProblems = new List<Vector3>();
    private int answer = 0;

    private void Start() {
        // 50% chance to get set the original digits from possbileProblems, 50% chance to get a random problem
        if ((int)Random.Range(0, 8) == 0) {
            int index = Random.Range(0, possibleProblems.Count);
            originalDigits = possibleProblems[index];
        } else {
            originalDigits.x = (int)Random.Range(0, 10);
            originalDigits.y = (int)Random.Range(0, 10);
            originalDigits.z = (int)Random.Range(0, 10);
        }
        setAnswer();
        displayProblem();
        // convert int array into a single number, index 0 being the one's digit
        problemDigit = (int)(originalDigits.z + (originalDigits.y * 10) + (originalDigits.x * 100));
    }

    // Update is called once per frame
    void Update()
    {
        // if enteredDigit is not null, display it in the correct digit text
        if (enteredDigits.Count >= 3) {
            checkAnswer();
            enteredDigits = new List<int>();
            displayProblem();
        }
        if (enteredDigits.Count != 0) {
            for (int i = 0; i < 3; i++)
            {
                if (i < enteredDigits.Count) {
                    digits[i].text = enteredDigits[enteredDigits.Count() - 1 - i].ToString();
                } else {
                    digits[i].text = "";
                }
            }
        }
    }

    private void displayProblem() {
        digits[0].text = originalDigits.z.ToString();
        digits[1].text = originalDigits.y.ToString();
        digits[2].text = originalDigits.x.ToString();
    }

    private void checkAnswer() {
        int eDigits = (enteredDigits[2] + (enteredDigits[1] * 10) + (enteredDigits[0] * 100));
        if (answer == eDigits) {
            solved();
        }
        else 
        {
            fail();
        }
    }

    private void setAnswer() {
        int[] oDigits = new int[] { (int)originalDigits.z, (int)originalDigits.y, (int)originalDigits.x };
        if (oDigits[0] == oDigits[2]) { // first and last number is same
            //ones * hundreds
            answer = oDigits[0] * oDigits[2];
        } else if (oDigits[1] >= 8) { // tens is greater than 5
            // flip the order
            answer = oDigits[2] + oDigits[1] * 10 + oDigits[0] * 100;
        } else if (oDigits.Sum() == 10) { // all digits add up to 10
            answer = 100;
        } else if (oDigits.All(x => x % 2 == 0)) {
            //ones * tens - hundreds
            answer = oDigits[0] * oDigits[1] - oDigits[2];
        } else if (oDigits.Count(x => x % 2 == 1) == 1) { // if one odd number
            //ones + tens + hundreds
            answer = oDigits[0] + oDigits[1] + oDigits[2];
        } else if (oDigits.Count(x => x % 2 == 1) == 2) { // if two odd numbers
            //100 * hundreds
            answer = 100 * oDigits[2];
        } else if (oDigits.All(x => x % 2 == 1)) { // if all odd numbers
            //ones + tens
            answer = 203;
        }
    }

    public void enterDigit(int value) {
        // if value is between -1 and 9
        if (value >= -1 && value <= 9) {
            animator.ResetTrigger(value.ToString());
            animator.SetTrigger(value.ToString());
        }
        if (value == -1) {
            // backspace
            if (enteredDigits.Count > 0) {
                enteredDigits.RemoveAt(0);
            }
        } else {
            enteredDigits.Add(value);
        }
        if (enteredDigits.Count == 0) {
            displayProblem();
        }
    }
}
