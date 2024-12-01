using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Beginning : MonoBehaviour {
    [SerializeField] private AudioClip[] dialogues;
    [SerializeField] private bool skipCutScene = false;
    [SerializeField] private GameObject canvasPanel;

    [SerializeField] private PlayerInput input;

    private Image image;
    private float screenUnfadeTime = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        image = canvasPanel.GetComponent<Image>();
        image.color = new Color(0f, 0f, 0f, 1f);
        canvasPanel.SetActive(true);
        screenUnfadeTime = dialogues[dialogues.Length-1].length + 0.5f;
        input.actions.Disable();
        if (skipCutScene || GameState.BeginningPlayed) {
            StartCoroutine(UnfadeScreen());
            return;
        }
        StartCoroutine(Sequence());
    }
    
    
    private IEnumerator Sequence() {
        GameState.BeginningPlayed = true;
        yield return new WaitForSecondsRealtime(2f);
        var count = 0;
        foreach (var clip in dialogues) {
            AudioManager.I.PlayDialogue(clip);
            if (count == dialogues.Length-1) {
                Debug.Log("Unfading");
                StartCoroutine(UnfadeScreen());
            }
            yield return new WaitForSecondsRealtime(clip.length + 0.5f);
            count++;
        }
    }

    private IEnumerator UnfadeScreen() {
        var timePassed = 0f;
        var progress = 0f;
        while (true) {
            timePassed += Time.deltaTime;
            progress = timePassed / screenUnfadeTime;
            image.color = new Color(0f, 0f, 0f, Math.Max(1 - progress, 0f));
            if (progress > 1f) {
                canvasPanel.SetActive(false);
                break;
            }
            yield return null;
        }
        input.actions.Enable();
    }
}
