using System.Linq;
using AdvancedSceneManager.Models;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{

    public Text text;

    public Scene[] scenes;

    private void Update()
    {
        //Not very efficient, since evaluating Scene.state every frame is rather heavy,
        //but for the purposes of demonstation, its fine
        text.text = string.Join("\n", scenes.Select(s => $"{s.name}: {s.state}"));
    }

}
