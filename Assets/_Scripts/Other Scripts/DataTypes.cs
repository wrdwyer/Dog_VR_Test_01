using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;

namespace Unity6Test
    {
    public class DataTypes : MonoBehaviour
        {
        private int age = 35;
        private float speed = 100f;
        private string name = "John";
      
        [TextArea(3, 10)]
        [SerializeField]
        private new string Notes = "Don't forget to buy milk. Don't forget to buy milk.Don't forget to buy milk.Don't forget to buy milk.Don't forget to buy milk.Don't forget to buy milk.Don't forget to buy milk.Don't forget to buy milk.";
        void Start()
            {
            Notes = ($"My age is {age}, I can run {speed}, and my name is {name}");
            }


       
        }
    }
