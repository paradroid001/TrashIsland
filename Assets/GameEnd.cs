using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameEnd : MonoBehaviour
{
    public GameObject endTitle;


    [YarnCommand("playAnim2")]
    public void CallAnimation(string animName)
    {
        Animator a = gameObject.GetComponent<Animator>();
        if (a != null)
        {
            a.Play(animName);
        }
        else
            Debug.Log("Error - No Animator");
    }
    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
