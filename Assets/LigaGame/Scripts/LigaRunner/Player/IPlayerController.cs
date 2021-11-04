using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LigaGame.Player
{
    public interface IPlayerController
    {
        void Jump();
        void Move(float direction);
        void HealthChanged(float healthChange);
        void Died();
    }    
}
