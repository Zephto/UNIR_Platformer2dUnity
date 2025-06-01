using UnityEngine;

public class HUD : MonoBehaviour
{
    public static HUD Instance { get; private set; }
    
    void Awake() {
		if(Instance != null){
			Debug.Log("Ya existe el hud we :v");
			Destroy(this.gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(this.gameObject);
	}
}
