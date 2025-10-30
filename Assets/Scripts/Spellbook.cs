using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum Spell { Forget, Burly, Smiley, Rage, Teleport, Mustache, Dispel, Charm, Laser, Quit, Calibrate, None}
public enum Pose { Forward, UpHigh, UpCenter, Down, RightMain, RightOff, RightLow, RightCenter, LeftMain, LeftOff, LeftLow, LeftCenter}
public class Spellbook : MonoBehaviour
{
    public Color[] spellColor = new Color[11];
    public float holdTime;
    public GameManager gameManager;
    public Image bubble;
    public Image preview;
    public AnimationCurve curve;

    private bool canCast = true;
    private Spell selection = Spell.None;
    private Coroutine effects;
    private void Start()
    {
        bubble.rectTransform.localScale = Vector3.zero;
        preview.gameObject.SetActive(false);
    }
    public void SpellSelection()
    {
        selection = Spell.None;
        canCast = true;
        StartCoroutine(SelectSpell());
    }
    IEnumerator SelectSpell()
    {
        // Prompt player to pick a spell
        Spell tempSelection = Spell.None;
        float timer = 0f;
        while (selection == Spell.None)
        {
            if (Input.anyKey || Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    tempSelection = Spell.Forget;
                    timer = 0f;
                    StopCoroutine(effects);
                    effects = StartCoroutine(SpellPreview(tempSelection));
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    tempSelection = Spell.Burly;
                    timer = 0f;
                    StopCoroutine(effects);
                    effects = StartCoroutine(SpellPreview(tempSelection));
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    tempSelection = Spell.Smiley;
                    timer = 0f;
                    StopCoroutine(effects);
                    effects = StartCoroutine(SpellPreview(tempSelection));
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    tempSelection = Spell.Rage;
                    timer = 0f;
                    StopCoroutine(effects);
                    effects = StartCoroutine(SpellPreview(tempSelection));
                }
                if (Input.GetMouseButtonDown(0))
                {
                    tempSelection = Spell.Teleport;
                    timer = 0f;
                    StopCoroutine(effects);
                    effects = StartCoroutine(SpellPreview(tempSelection));
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    tempSelection = Spell.Mustache;
                    timer = 0f;
                    StopCoroutine(effects);
                    effects = StartCoroutine(SpellPreview(tempSelection));
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    tempSelection = Spell.Dispel;
                    timer = 0f;
                    StopCoroutine(effects);
                    effects = StartCoroutine(SpellPreview(tempSelection));
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    tempSelection = Spell.Charm;
                    timer = 0f;
                    StopCoroutine(effects);
                    effects = StartCoroutine(SpellPreview(tempSelection));
                }
                if (Input.GetMouseButtonDown(1))
                {
                    tempSelection = Spell.Quit;
                    timer = 0f;
                    StopCoroutine(effects);
                    effects = StartCoroutine(SpellPreview(tempSelection));
                }
                if (Input.GetKeyDown(KeyCode.G))
                {
                    tempSelection = Spell.Calibrate;
                    timer = 0f;
                    StopCoroutine(effects);
                    effects = StartCoroutine(SpellPreview(tempSelection));
                }

                timer += Time.deltaTime;
            }
            else
            {
                if (timer > 0f) timer -= Time.deltaTime;
            }

            timer = Mathf.Clamp(timer, 0f, holdTime);
            bubble.rectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timer / holdTime);

            if (timer >= holdTime)
            {
                selection = tempSelection;
                canCast = false;
                gameManager.SetSpell(selection);
            }
            yield return null;
        }
        bubble.rectTransform.localScale = Vector3.one;

    }
    IEnumerator SpellPreview(Spell spell)
    {
        preview.gameObject.SetActive(true);
        Color col = spellColor[(int)spell];
        col.a = 0;
        float t = 0f;
        while (canCast)
        {
            t += Time.deltaTime;
            if (t > 1f) t -= 1f;
            col.a = Mathf.Lerp(0.2f, 0.8f, curve.Evaluate(t));
            preview.color = col;
            yield return null;
        }

        // Triggers when the coroutine is allowed to end naturally
        while (t > 0f)
        {
            t -= Time.deltaTime;
            col.a = Mathf.Lerp(0, 1, t%1f);
        }
        preview.gameObject.SetActive(false);
    }
}
