using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private float timeToTransition = 2f;
    [SerializeField] private Image blocker;
    public void Play()
    {
        StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {

        float timer = 0f;
        
        while (timer < timeToTransition)
        {
            timer += Time.deltaTime;

            float lerpValue = timer / timeToTransition;
            blocker.color = Color.Lerp(Color.clear, Color.black, lerpValue);
            yield return null;
        }
        
        SceneManager.LoadScene("SampleScene");
    }
}
