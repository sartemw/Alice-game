﻿using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Fish;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Services.FishCollectorService
{
    public class RepaintingService : IRepaintingService
    {
        private const string Initial = "Initial";
        private const string GameEnd = "GameEnd";
        public Material Colorless { get; set; }
        public Material Colored { get; set; }
        
        public List<GameObject> ColorlessObjs { get; set; }
        public List<GameObject> ColorledObjs { get; set;}
        public event Action PickUpFish;

        public void Init(Material colorless, Material colored)
        {
            Colored = colored;
            Colorless = colorless;
        }

        public void Restart()
        {
            CleanUp();

            var repaintableObjs = GameObject.FindObjectsOfType<Repaintable>();

            if (repaintableObjs.Length == 0 && IsInitialOrEndScene()) 
                Debug.LogError($"RepaintableObjs counts = {repaintableObjs.Length}, add \"Repaintable\" Component");

            foreach (Repaintable repaintable in repaintableObjs)
            {
                GameObject obj = repaintable.gameObject;
                repaintable.Painting(Colorless);
                ColorlessObjs.Add(obj);
            }
        }

        private static bool IsInitialOrEndScene() => 
            SceneManager.GetActiveScene().name == Initial 
            || SceneManager.GetActiveScene().name == GameEnd;

        public void AddFish(ColoredFish fish)
        {
            Painting(fish);
            PickUpFish?.Invoke();
        }

        public bool IsAllFishCollected()
        {
            if (ColorlessObjs.Count == 0)
                return true;
            
            return false;
        }

        private void Painting(ColoredFish fish)
        {
            foreach (GameObject colorlessObj in ColorlessObjs.ToList())
            {
                if (fish.FishColorType == colorlessObj.GetComponent<Repaintable>().ColorType
                    || fish.FishColorType == ColorType.Rainbow)
                {
                    colorlessObj.GetComponent<Repaintable>().Painting(Colored);
                    ColorledObjs.Add(colorlessObj);
                    ColorlessObjs.Remove(colorlessObj);
                }
            }
            fish.gameObject.SetActive(false);
        }

        private void CleanUp()
        {
            ColorlessObjs = new List<GameObject>();
            ColorledObjs = new List<GameObject>();
        }
    }
}