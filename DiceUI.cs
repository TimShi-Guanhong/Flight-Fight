using System.Collections;
using UnityEngine;
using TMPro;

public class DiceUI : MonoBehaviour
{
    [Header("References")]
    public TokenMover token;
    public TMP_Text resultText;

    [Header("Animation")]
    public float animDuration = 1.0f;   // 摇骰子动画总时长
    public float tick = 0.08f;          // 每次跳数字的间隔（越小越快）
    public bool playTickSound = false;  // 你以后想加音效可以用
    public AudioSource audioSource;     // 可选
    public AudioClip tickClip;          // 可选

    private bool rolling = false;

    public void Roll()
    {
        if (rolling) return;

        if (token == null || resultText == null)
        {
            Debug.LogError("DiceUI: token or resultText is not assigned.");
            return;
        }

        StartCoroutine(RollDiceAnimation());
    }

    private IEnumerator RollDiceAnimation()
    {
        rolling = true;

        // 最终点数（真正用于移动）
        int finalRoll = Random.Range(1, 7);

        // 动画：不断显示随机数字
        float t = 0f;
        while (t < animDuration)
        {
            int v = Random.Range(1, 7);
            resultText.text = "Roll: " + v;

            if (playTickSound && audioSource != null && tickClip != null)
                audioSource.PlayOneShot(tickClip);

            yield return new WaitForSeconds(tick);
            t += tick;
        }

        // 停在最终点数
        resultText.text = "Roll: " + finalRoll;

        // 让棋子移动
        token.MoveSteps(finalRoll);

        rolling = false;
    }
}