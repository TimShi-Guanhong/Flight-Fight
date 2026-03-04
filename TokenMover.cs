using System.Collections;
using UnityEngine;

public class TokenMover : MonoBehaviour
{

    public BoardPath board;
    public float stepDelay = 0.2f;
    public float hoverY = 0.8f;

    public int currentIndex = 0;

    private bool moving = false;

    public void MoveSteps(int steps)
    {
        if (moving) return;
        StartCoroutine(MoveRoutine(steps));
    }

    IEnumerator MoveRoutine(int steps)
    {
        moving = true;

        for (int i = 0; i < steps; i++)
        {
            currentIndex++;

            if (currentIndex >= board.points.Length)
            {
                currentIndex = board.points.Length - 1;

                Debug.Log("You Win!");

                break;
            }

            Vector3 p = board.points[currentIndex].position;
            transform.position = new Vector3(p.x, hoverY, p.z);

            yield return new WaitForSeconds(stepDelay);
        }

        moving = false;
    }
}