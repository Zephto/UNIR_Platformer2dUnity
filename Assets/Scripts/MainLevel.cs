using System.Collections.Generic;
using UnityEngine;

public class MainLevel : MonoBehaviour
{
    // public static MainLevel Instance;

    [SerializeField] private Door door;
    [SerializeField] private Player player;
    [SerializeField] private Timer timer;

    // private SceneChanger sceneCharger;
    // private TransitionScreen transitionScreen;

    [Header("Level construction references")]
    [SerializeField] private List<GameObject> levelObjects;

    // void Awake() {
	// 	if(Instance != null){
	// 		Destroy(this.gameObject);
	// 		return;
	// 	}

	// 	Instance = this;
	// 	DontDestroyOnLoad(this);
	// }

    void Start()
    {
        SceneChanger.Instance.OnSceneLoadComplete.AddListener(() => StartLevel());
        door.OnTouchDoor.AddListener(() => EndLevel());
        if (GlobalData.CurrentLevel == 0)
        {
            timer.StartTimer();
            TransitionScreen.Instance.StartIn();
        }
        StartLevel();
    }

    private void StartLevel()
    {
        //Disable all elements
        foreach (GameObject obj in levelObjects)
        {
            obj.SetActive(false);
        }

        Debug.Log("Current level " + GlobalData.CurrentLevel);
        //Check if is level zero

        if (GlobalData.CurrentLevel == 0)
        {
            // TransitionScreen.Instance.Out(null);
            // return;
        }
        else
        {
            //Activate only level objects
            for (int i = 1; i < GlobalData.CurrentLevel; i++)
            {
                levelObjects[i - 1].SetActive(true);
            }
        }
        ;

        LeanTween.delayedCall(this.gameObject, 0.1f, () =>
        {
            TransitionScreen.Instance.Out(null);
        });
    }

    private void EndLevel()
    {
        player.gameObject.SetActive(false);
        TransitionScreen.Instance.In(()=>
        {
            GlobalData.CurrentLevel++;

            //Finish game
            if (GlobalData.CurrentLevel >= levelObjects.Count)
            {
                timer.StopTimer();
                return;
            }

            LeanTween.delayedCall(this.gameObject, 0.1f, () =>
            {
                SceneChanger.Instance.LoadSceneAsync("Level");
            });
        });

        // sceneCharger.LoadSceneAsync("Level");
    }
}
