using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("levels");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
