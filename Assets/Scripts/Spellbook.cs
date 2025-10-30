using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum Spell { Forget, Burly, Smiley, Rage, Teleport, Mustache, Dispel, Charm, Laser, Quit, Calibrate, None}
public enum Pose { Forward, UpHigh, UpCenter, Down, RightMain, RightOff, RightLow, RightCenter, LeftMain, LeftOff, LeftLow, LeftCenter}
public class Spellbook : MonoBehaviour
{
    public Color[] spellColor = new Color[11];
    public float holdTime;
    public GameManager gameManager;

    private bool canCast = true;
    private Spell selection = Spell.None;
    private Coroutine effects;

    public void SpellSelection()
    {
        StartCoroutine(SelectSpell());
    }
    IEnumerator SelectSpell()
    {
        // Prompt player to pick a spell
        Spell tempSelection = Spell.None;
        float timer = 0f;
        while (selection == Spell.None)
        {
            if (Input.anyKey)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    tempSelection = Spell.Forget;
                    timer = 0f;
                    StopCoroutine(effects);
                    effects = StartCoroutine(SpellPreview(tempSelection));
                }
                if (Input.GetKeyDown(KeyCode.A))
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
                if (Input.GetKeyDown(KeyCode.D))
                {
                    tempSelection = Spell.Rage;
                    timer = 0f;
                    StopCoroutine(effects);
                    effects = StartCoroutine(SpellPreview(tempSelection));
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    tempSelection = Spell.Teleport;
                    timer = 0f;
                    StopCoroutine(effects);
                    effects = StartCoroutine(SpellPreview(tempSelection));
                }
                if (Input.GetKeyDown(KeyCode.G))
                {
                    tempSelection = Spell.Mustache;
                    timer = 0f;
                    StopCoroutine(effects);
                    effects = StartCoroutine(SpellPreview(tempSelection));
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
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
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    tempSelection = Spell.Quit;
                    timer = 0f;
                    StopCoroutine(effects);
                    effects = StartCoroutine(SpellPreview(tempSelection));
                }
                if (Input.GetKeyDown(KeyCode.Space))
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

            if (timer >= holdTime)
            {
                selection = tempSelection;
                canCast = false;
                gameManager.SetSpell(selection);
            }
            yield return null;
        }

    }
    IEnumerator SpellPreview(Spell spell)
    {
        while (canCast)
        {
            // Spell preview effects here
            yield return null;
        }
    }
    public void EndSpell()
    {
        selection = Spell.None;
        canCast = true;
    }
}
