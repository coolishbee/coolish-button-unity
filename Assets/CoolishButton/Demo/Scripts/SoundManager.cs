using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoolishUI
{
    public class SoundManager
    {
        static public void Play(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            Debug.Log("Use your own asset or sound manager.");           
        }
    }
}
