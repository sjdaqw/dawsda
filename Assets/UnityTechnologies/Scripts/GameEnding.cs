using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    [Header("페이드 시간")]
    [SerializeField]
    private float fadeDuration = 1f;
    [Header("이미지 표시 시간")]
    [SerializeField]
    private float displayImageDuration = 1f;
    [Header("플레이어 게임오브젝트")]
    [SerializeField]
    private GameObject player;

    [Header("성공 캔버스그룹")]
    [SerializeField]
    private CanvasGroup exitBackgroundImageCanvasGroup;
    [Header("실패 캔버스그룹")]
    [SerializeField]
    private CanvasGroup caughtBackgroundImageCanvasGroup;

    private bool isPlayerExit = false;
    private bool isPlayerCaught = false;
    
    private float timer = 0f;

    private void OnTriggerEnter(Collider other)
    {
        // .Equals() : ==과 같음
        if(other.gameObject.Equals(player))
        {
            isPlayerExit = true;
        }
    }

    public void CaughtPlayer()
    {
        isPlayerCaught = true;
    }

    private void Update()
    {
        if(isPlayerExit == true)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false);
        }
        else if(isPlayerCaught == true)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true);
        }
    }

    // CanvasGroup : 성공/실패 이미지, doRestart : 재시작 여부
    private void EndLevel(CanvasGroup imageGroup, bool doRestart)
    {        
        // timer = timer + Time.deltaTime과 같음
        timer += Time.deltaTime;

        imageGroup.alpha = timer / fadeDuration;

        if(timer > fadeDuration + displayImageDuration)
        {
            if (doRestart == true)
            {
                SceneManager.LoadScene(0);
                return;
            }            

            // 게임 종료
            Application.Quit();
        }
    }
}
