using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns_dotNET.Behavioral
{
    public class Unit
    {
        public event EventHandler<string> Attack;

        public string Name { get; private set; }

        public void DoAttack(string n, string info)
        {
            Name = n;
            Debug.Log($"Attack {info}");
            OnDoAttack(info);
        }

        private void OnDoAttack(string info)
        {
            if(Attack != null)
            {
                Attack(this, info);
            }
        }
    }

    public class UI
    {
        public void AfterAttack(object sender, string info)
        {
            Debug.Log($"UI Update {sender as Unit} with {info}");
        }
    }

    public class Log 
    {
        public void AfterAttack(object sender, string info)
        {
            Debug.Log($"Log Update {sender} with {info}");
        }
    }

    public class Observer : MonoBehaviour
    {
        [SerializeField] private Button runBtn;
        [SerializeField] private InputField infoIF;

        Log log = new Log();
        UI ui = new UI();
        Unit unit = new Unit();

        private void OnEnable()
        {
            runBtn.onClick.AddListener(Run);
        }

        private void Run()
        {
            unit.Attack += ui.AfterAttack;
            unit.Attack += log.AfterAttack;

            unit.DoAttack("Unit 1", infoIF.text);

            unit.Attack -= ui.AfterAttack;
            unit.Attack -= log.AfterAttack;
        }

        private void OnDisable()
        {
            runBtn.onClick.RemoveListener(Run);
        }
    }
}