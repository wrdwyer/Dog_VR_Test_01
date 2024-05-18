using UnityEngine;

namespace Unity6Test
{
    public class TestScript : MonoBehaviour
    {
        private int age = 35;
        private float speed = 100f;
        private string name = "John";
        void Start()
        {
        Debug.Log($"My ag is {age}, I can run {speed}, and my name is {name}"); 
        }

       
        void Update()
        {
        
        }
    }
}
