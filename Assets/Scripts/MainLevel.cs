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

    void Start()
    {
        SceneChanger.Instance.OnSceneLoadComplete.AddListener(() => StartLevel());
        door.OnTouchDoor.AddListener(() => EndLevel());
        
    }

    public void StartLevel()
    {
        if (GlobalData.CurrentLevel == 1)
        {
            player.SetInitValues();
            timer.StartTimer();
        }

        //Disable all elements
        foreach (GameObject obj in levelObjects)
        {
            obj.SetActive(false);
        }

        Debug.Log("Current level " + GlobalData.CurrentLevel);

        if (GlobalData.CurrentLevel != 0)
        {
            //Activate only level objects
            for (int i = 1; i < GlobalData.CurrentLevel; i++)
            {
                levelObjects[i - 1].SetActive(true);
            }
        }

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
                Debug.Log("ACABASTE EL JUEGOOO");
                timer.StopTimer();
                GlobalData.OnEndGame?.Invoke();
                LeanTween.delayedCall(this.gameObject, 0.1f, () =>
                {
                    player.CanPlay(false);
                    TransitionScreen.Instance.Out(null);
                });
                return;
            }

            LeanTween.delayedCall(this.gameObject, 0.1f, () =>
            {
                SceneChanger.Instance.LoadSceneAsync("Level");
            });
        });
    }
}
