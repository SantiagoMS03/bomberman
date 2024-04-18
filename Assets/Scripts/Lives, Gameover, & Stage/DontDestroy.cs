using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public bool On = true;

    // Update is called once per frame
    void Update()
    {
        if (On)
        {
            DontDestroyOnLoad(gameObject);
          //  DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
            Destroy(GetComponent<DontDestroy>());
        }
    }

}
