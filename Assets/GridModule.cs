using System.Linq;
using UnityEngine;

public class GridModule : Module
{
    [SerializeField]
    private Grids[] grids;

    [SerializeField]
    private GridAnswer[] problems;

    // Start is called before the first frame update
    void Start()
    {
        setProblem();
    }

    void setProblem() {
        int index = Random.Range(0, problems.Length);
        for (int i = 0; i < grids.Length; i++)
        {
            grids[i].X = problems[index].problem.rows[Mathf.FloorToInt(i / 5)].row[i % 5];
            grids[i].O = problems[index].solution.rows[Mathf.FloorToInt(i / 5)].row[i % 5];
            grids[i].showTextures();
        }
    }

    public void checkAnswer() {
        if (grids.All(x => x.O == x.activated))
        {
            solved();
        }
    }
}
