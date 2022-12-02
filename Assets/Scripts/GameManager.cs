using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject _player;
    public Texture2D cursor;

    private void Awake()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        _player = GameObject.Find("Player");
    }

    void Start()
    {
        AsyncOperation op = OwnSceneManager.instance?.LoadAsyncSceneAdditive("GameUI");
        // op.completed += (AsyncOperation x) => {
        //     GameUiManager uiManager = GameObject.Find("GameUI")?.GetComponent<GameUiManager>();
        //     }
        //     // _deathCounterScript = GameObject.Find("InGameUI")?.GetComponent<InGameUiManager>()?.deathCounter;
        //     // Slider dashSlider = GameObject.Find("InGameUI")?.GetComponent<InGameUiManager>()?.dashSlider;
        //     // _player.GetComponent<PlayerManager>().SetDashSlider(dashSlider);

        //     // TimeCounter timeCounterScript = GameObject.Find("CounterPanel")?.GetComponent<TimeCounter>();
        // };
    }

}
