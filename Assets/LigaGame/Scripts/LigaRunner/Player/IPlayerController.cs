using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LigaGame
{
    public interface IPlayerController
    {
        void Jump();
        void Move(float direction);
        void Die();
        void Revive();
    }    
}
