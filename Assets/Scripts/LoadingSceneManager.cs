using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//코드 작성 : 지재원
public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    [SerializeField] Image progressBar;
    [SerializeField] GameObject[] tipSet;
    private int tipIndex;

    private void Start()
    {
        StartCoroutine(LoadScene());
        tipIndex = Random.Range(0, tipSet.Length); //랜덤한 팁 선택
        tipSet[tipIndex].SetActive(true); //팁 출력
    }

    public static void LoadScene(string sceneName) //다음 로딩할 씬 받아옴
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene"); //로딩씬 실행
    }

    IEnumerator LoadScene() //오버로딩
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false; //0.9에서 중지
        float timer = 0.0f;
        while (!op.isDone) //로딩이 완료될 때까지
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f) //로딩바 채우기
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer); //보간으로 자연스럽게 채움
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else //로딩이 완료되면 씬 전환
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer); //끝까지 채움
                if (progressBar.fillAmount == 1.0f) //로딩바가 다 찼을 때
                {
                    tipSet[tipIndex].SetActive(false); //팁 비활성화
                    op.allowSceneActivation = true; //다음 씬으로 넘어감
                    yield break;
                }
            }
        }
    }
}
