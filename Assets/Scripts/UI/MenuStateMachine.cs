using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;
using System;
using UnityEngine.Events;

public class MenuStateMachine : MonoBehaviour
{
    public GameObject mainPanelGO;
    public GameObject HighScorePanelGO;
    public GameObject HighScoreChildPanelGO;
    public GameObject HighScoreMenuManager;

    public enum State { Main, Options, Play, HighScore, Exit};
    public enum Trigger { Options, HighScore, Main, Play, Exit};

    public UnityEvent loadHighScore;
    
    StateMachine<State, Trigger> _machine;

    void Start()
    {
        Debug.Log("Start Menu.");

        _machine = new StateMachine<State, Trigger>(State.Main);
		_machine.Configure(State.Main)
                .Permit(Trigger.Options, State.Options)
                .Permit(Trigger.HighScore, State.HighScore)
                .Permit(Trigger.Play, State.Play)
                .Permit(Trigger.Exit, State.Exit)
                .OnExit(() => ExitMain())
                .OnEntry(() => EnterMain());

		_machine.Configure(State.Options)
                .Permit(Trigger.Main, State.Main)
                .Permit(Trigger.Exit, State.Exit)
                .OnEntry(() => EnterOptions());

		_machine.Configure(State.HighScore)
                .Permit(Trigger.Main, State.Main)
                .Permit(Trigger.Exit, State.Exit)
                .OnExit(() => ExitHighScore())
                .OnEntry(() => EnterHighScore());

		_machine.Configure(State.Play)
                .Permit(Trigger.Main, State.Main)
                .OnEntry(() => EnterPlay());

		_machine.Configure(State.Exit)
                .OnEntry(() => ExitExit());
    }

    public void Fire(Trigger trigger)
    {
        Debug.Log("Fire:" + trigger);
        try
        {
            _machine.Fire(trigger);
            Debug.Log("Move to State:" + _machine.State);
        }
        catch (Exception e)
        {
            Debug.Log("Error during state transition.");
            Debug.Log(e.StackTrace);
        }
    }

    void EnterMain()
    {
        Debug.Log("ActivateMain.");

        mainPanelGO.SetActive(true);
    }

    void ExitMain()
    {
        Debug.Log("ExitMain.");

        mainPanelGO.SetActive(false);
    }

    void EnterOptions()
    {
        Debug.Log("ActivateOptions.");
    }

    void EnterHighScore()
    {
        Debug.Log("ActivateHighScore.");

        HighScorePanelGO.SetActive(true);

        HighScoreMenuManager hsmm = HighScoreMenuManager.GetComponent<HighScoreMenuManager>();
        hsmm.BuildHighScore();
    }

    void ExitHighScore()
    {
        Debug.Log("ExitHighScore.");

        HighScoreMenuManager hsmm = HighScoreMenuManager.GetComponent<HighScoreMenuManager>();
        hsmm.DestroyHighScore();

        HighScorePanelGO.SetActive(false);
    }

    void EnterPlay()
    {
        Debug.Log("ExitPlay.");
    }

    void ExitExit()
    {
        Debug.Log("ExitExit.");
    }

    public void HighScoreClick()
    {
        Fire(Trigger.HighScore);
    }

    public void MainClick()
    {
        Fire(Trigger.Main);
    }
}
