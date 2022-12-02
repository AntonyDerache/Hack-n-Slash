using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject _player;
    public Texture2D cursor;

    private void Awake()
    {
        Vector2 cursorHotspot = new Vector2(cursor.width / 2, cursor.height / 2);
        Cursor.SetCursor(cursor, cursorHotspot, CursorMode.ForceSoftware);
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
