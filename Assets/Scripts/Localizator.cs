using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Localizator : MonoBehaviour
{
    [SerializeField]
    public string Key;

	void OnStart() {
		// GetComponent<TextMesh>()?.text = LocalizationService.Instance.GetString(Key)
	}

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMesh>().text = LocalizationService.Instance.GetString(Key);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
