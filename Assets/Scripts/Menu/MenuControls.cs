﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour {

    private enum MenuState
    {
        Title, PlayGame, Highscore, Credits, Quit
    };

    [SerializeField]
    private GameObject[] _screens;
    [SerializeField]
    private InputField _VRNameInput;
    [SerializeField]
    private InputField _PCNameInput;

    private MenuState _currState;
    private MenuState _nextState;
    private MenuState _prevState;
    private bool _isTransitioning;
	// Use this for initialization
	void Start () {
	    foreach(var menu in _screens)
        {
            menu.SetActive(false);
        }
        _VRNameInput.onEndEdit.AddListener(RecordVRPlayerName);
        _PCNameInput.onEndEdit.AddListener(RecordPCPlayerName);
        _currState = MenuState.Title;
        _screens[(int)_currState].SetActive(true);
        _isTransitioning = false;
	}
	
	// Update is called once per frame
	void Update () {


        ApplyStateRules();

        
	}

    private void ApplyStateRules()
    {
        switch (_currState)
        {
            case MenuState.Title:
                break;
            case MenuState.PlayGame:
                break;
            case MenuState.Highscore:
                break;
            case MenuState.Credits:
                break;
            case MenuState.Quit:
                Application.Quit();
                break;
            default:
                break;
        }
        StartCoroutine("MenuTransition");
    }

    private IEnumerator MenuTransition()
    {
        if (_isTransitioning)
        {
            yield break;
        }
        _isTransitioning = true;
        if(_nextState == _currState)
        {
            _currState = _nextState;
            _nextState = _currState;
            _isTransitioning = false;
            yield break;
        }
        var transitionInPlace = false;
        while (!transitionInPlace)
        {
            //TODO: a blocking color panel
            //DEBUG:
            transitionInPlace = true;
            yield return new WaitForFixedUpdate();
        }
        transitionInPlace = false;
        //swap scenes
        _screens[(int)_currState].SetActive(false);
       // print((int)_nextState);
        _screens[(int)_nextState].SetActive(true);
        _prevState = _currState;
        _currState = _nextState;
        _nextState = _currState;
        print((int)_prevState);
        yield return new WaitForFixedUpdate();
        while (!transitionInPlace)
        {
            //TODO: a blocking color panel
            //DEBUG:
            transitionInPlace = true;
            yield return new WaitForFixedUpdate();
        }
        _isTransitioning = false;
    }


    public void OnPlayGame()
    {
        _nextState = MenuState.PlayGame;
    }


    public void OnHighScore()
    {
        _nextState = MenuState.Credits;
    }

    public void OnCredits()
    {
        _nextState = MenuState.Credits;
    }

    public void OnExit()
    {
        _nextState = MenuState.Quit;
    }

    public void OnBack()
    {
        _nextState = _prevState;
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene("Prepare");
    }

    public void RecordVRPlayerName(string arg0)
    {
        print(arg0);
        PlayerPrefs.SetString("VRName", arg0);
    }

    public void RecordPCPlayerName(string arg0)
    {
        print(arg0);
        PlayerPrefs.SetString("PCName", arg0);
    }
}
