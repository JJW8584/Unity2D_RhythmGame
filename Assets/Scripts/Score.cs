using System.Collections;
using TMPro;
using UnityEngine;

//코드작성 : 지재원
public class Score : MonoBehaviour
{
    public TextMeshProUGUI perfectCount;
    public TextMeshProUGUI goodCount;
    public TextMeshProUGUI badCount;
    public TextMeshProUGUI missCount;
    public TextMeshProUGUI score;
    public TextMeshProUGUI Rank;

    private float maxTextDelay;
    private float curTextDelay;
    private float maxScoreDelay;
    private float curScoreDelay;

    private void Awake()
    {
        maxTextDelay = 0.05f;
        curTextDelay = 0f;
        maxScoreDelay = 1f;
        curScoreDelay = 0f;
        Rank.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
    }
    private void Start()
    {
        perfectCount.text = string.Format("{0:n0}", GameManager.instance.perfectCnt);
        goodCount.text = string.Format("{0:n0}", GameManager.instance.goodCnt);
        badCount.text = string.Format("{0:n0}", GameManager.instance.badCnt);
        missCount.text = string.Format("{0:n0}", GameManager.instance.missCnt);
        if (GameManager.instance.score >= GameManager.instance.maxScore * 95 / 10)
            Rank.text = "SS";
        else if(GameManager.instance.score >= GameManager.instance.maxScore * 9)
            Rank.text = "S";
        else if(GameManager.instance.score >= GameManager.instance.maxScore * 8)
            Rank.text = "A";
        else if(GameManager.instance.score >= GameManager.instance.maxScore * 7)
            Rank.text = "B";
        else if(GameManager.instance.score >= GameManager.instance.maxScore * 6)
            Rank.text = "C";
        else if(GameManager.instance.score >= GameManager.instance.maxScore * 5)
            Rank.text = "D";
        else
            Rank.text = "F";

        StartCoroutine(ScoreText());
    }
    IEnumerator ScoreText()
    {
        while(curScoreDelay < maxScoreDelay)
        {
            curScoreDelay += Time.deltaTime;
            curTextDelay += Time.deltaTime;
            if (curTextDelay > maxTextDelay)
            {
                curTextDelay = 0f;

                score.text = string.Format("{0:D7}", Random.Range(1231231, 9879879));
            }
            yield return null;
        }
        score.text = string.Format("{0:D7}", GameManager.instance.score);

        Transform rankTrans = Rank.GetComponent<Transform>();
        float _time = 0;
        while (true)
        {
            _time += Time.deltaTime;
            if (_time > 1f)
                break;
            rankTrans.localScale = new Vector3(Mathf.Lerp(0f, 1f, _time), Mathf.Lerp(0f, 1f, _time), 0);
            yield return null;
        }
    }
}
