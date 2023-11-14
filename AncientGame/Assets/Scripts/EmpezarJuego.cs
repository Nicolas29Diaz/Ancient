using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EmpezarJuego : MonoBehaviour
{
    public float tiempoDelVideo;
    public void Start()
    {
        Invoke("empezarJuego", tiempoDelVideo);
    }
    public void empezarJuego()
    {
        SceneManager.LoadSceneAsync(1);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadSceneAsync(1);
        }
    }


}
