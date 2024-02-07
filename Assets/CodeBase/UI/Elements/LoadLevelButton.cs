﻿using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class LoadLevelButton : MonoBehaviour
    {
        public Button Button;
        public string LoadLevel;
        
        private GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;

        public void Init(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
        }

        private void Awake() => 
            Button.onClick.AddListener(StartLevel);

        private void StartLevel()
        {
            _sceneLoader.Load(LoadLevel, onLoaded: EnterLoadLevel);
        }
        
        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>(LoadLevel);
    }
}