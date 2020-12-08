using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomMenu : MonoBehaviour
{
    public void Random2x2()
    {
        SceneManager.LoadScene("RandomMatrix2x2");
    }
    public void Random3x3()
    {
        SceneManager.LoadScene("RandomMatrix3x3");
    }
    public void Random4x4()
    {
        SceneManager.LoadScene("RandomMatrix4x4");
    }
    public void Random5x5()
    {
        SceneManager.LoadScene("RandomMatrix5x5");
    }
}
