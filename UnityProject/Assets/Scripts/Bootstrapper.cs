using UnityEngine;

namespace ToyGame
{
    public class Bootstrapper 
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Execute() => Object.Instantiate(Resources.Load("GameManager"));
        /*[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Execute() => Object.Instantiate(Resources.Load("GameManager"));*/
       /* [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Execute() => Object.Instantiate(Resources.Load("GameManager"));*/
    }
}
