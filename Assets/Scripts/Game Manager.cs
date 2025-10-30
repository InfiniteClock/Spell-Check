using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image instruction;
    public JoyconWand jWand;
    public Spellbook spellbook;
    public float holdPoseTime;
    public List<Sprite> poseInstructions;
    public List<string> questions;
    public List<Spell> answer0;
    public List<Spell> answer1;
    public List<Spell> answer2;
    public List<Spell> answer3;
    public List<Spell> answer4;

    private int questionIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instruction.gameObject.SetActive(false);
        StartCoroutine(CreateQuestion());
    }
    public void SetSpell(Spell spell)
    {
        StartCoroutine(PerformSpell(spell));
    }
    IEnumerator PerformSpell(Spell spell)
    {
        // Show point forward 

        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(HoldPose(Pose.Forward));
        jWand.Recalibrate();
        switch (spell)
        {
            case Spell.Forget:
                yield return StartCoroutine(HoldPose(Pose.UpHigh));
                yield return StartCoroutine(HoldPose(Pose.RightMain));
                yield return StartCoroutine(HoldPose(Pose.Down));
                yield return StartCoroutine(HoldPose(Pose.LeftOff));
                yield return StartCoroutine(HoldPose(Pose.UpHigh));
                yield return StartCoroutine(HoldPose(Pose.Forward));
                break;
            case Spell.Burly:
                yield return StartCoroutine(HoldPose(Pose.UpHigh));
                yield return StartCoroutine(HoldPose(Pose.LeftOff));
                yield return StartCoroutine(HoldPose(Pose.Down));
                break;
            case Spell.Smiley:
                yield return StartCoroutine(HoldPose(Pose.UpHigh));
                yield return StartCoroutine(HoldPose(Pose.RightMain));
                yield return StartCoroutine(HoldPose(Pose.LeftOff));
                break;
            case Spell.Rage:
                yield return StartCoroutine(HoldPose(Pose.UpCenter));
                yield return StartCoroutine(HoldPose(Pose.RightMain));
                yield return StartCoroutine(HoldPose(Pose.Down));
                yield return StartCoroutine(HoldPose(Pose.LeftOff));
                yield return StartCoroutine(HoldPose(Pose.UpCenter));
                yield return StartCoroutine(HoldPose(Pose.Down));
                yield return StartCoroutine(HoldPose(Pose.RightMain));
                yield return StartCoroutine(HoldPose(Pose.Down));
                break;
            case Spell.Teleport:
                yield return StartCoroutine(HoldPose(Pose.UpHigh));
                yield return StartCoroutine(HoldPose(Pose.RightMain));
                yield return StartCoroutine(HoldPose(Pose.Down));
                yield return StartCoroutine(HoldPose(Pose.LeftOff));
                yield return StartCoroutine(HoldPose(Pose.UpHigh));
                break;
            case Spell.Mustache:
                yield return StartCoroutine(HoldPose(Pose.UpCenter));
                yield return StartCoroutine(HoldPose(Pose.RightMain));
                yield return StartCoroutine(HoldPose(Pose.UpCenter));
                yield return StartCoroutine(HoldPose(Pose.LeftOff));
                yield return StartCoroutine(HoldPose(Pose.UpCenter));
                break;
            case Spell.Dispel:
                yield return StartCoroutine(HoldPose(Pose.UpHigh));
                yield return StartCoroutine(HoldPose(Pose.RightMain));
                yield return StartCoroutine(HoldPose(Pose.Down));
                yield return StartCoroutine(HoldPose(Pose.LeftOff));
                yield return StartCoroutine(HoldPose(Pose.UpHigh));
                yield return StartCoroutine(HoldPose(Pose.Forward));
                jWand.Recalibrate();
                yield return StartCoroutine(HoldPose(Pose.UpHigh));
                yield return StartCoroutine(HoldPose(Pose.RightMain));
                yield return StartCoroutine(HoldPose(Pose.Down));
                yield return StartCoroutine(HoldPose(Pose.LeftOff));
                yield return StartCoroutine(HoldPose(Pose.UpHigh));
                yield return StartCoroutine(HoldPose(Pose.Down));
                break;
            case Spell.Charm:
                yield return StartCoroutine(HoldPose(Pose.UpCenter));
                yield return StartCoroutine(HoldPose(Pose.RightMain));
                yield return StartCoroutine(HoldPose(Pose.Down));
                yield return StartCoroutine(HoldPose(Pose.LeftOff));
                yield return StartCoroutine(HoldPose(Pose.UpCenter));
                yield return StartCoroutine(HoldPose(Pose.Forward));
                jWand.Recalibrate();
                yield return StartCoroutine(HoldPose(Pose.UpCenter));
                yield return StartCoroutine(HoldPose(Pose.RightMain));
                yield return StartCoroutine(HoldPose(Pose.Down));
                yield return StartCoroutine(HoldPose(Pose.LeftOff));
                yield return StartCoroutine(HoldPose(Pose.UpCenter));
                yield return StartCoroutine(HoldPose(Pose.Forward));
                jWand.Recalibrate();
                yield return StartCoroutine(HoldPose(Pose.UpCenter));
                yield return StartCoroutine(HoldPose(Pose.RightMain));
                yield return StartCoroutine(HoldPose(Pose.Down));
                yield return StartCoroutine(HoldPose(Pose.LeftOff));
                yield return StartCoroutine(HoldPose(Pose.UpHigh));
                break;
            case Spell.Laser:
                yield return StartCoroutine(HoldPose(Pose.UpCenter));
                break;
            case Spell.Quit:
                yield return StartCoroutine(HoldPose(Pose.UpCenter));
                break;
            case Spell.Calibrate:
                yield return StartCoroutine(HoldPose(Pose.Forward));
                jWand.Recalibrate();
                break;
        }
        StartCoroutine(CastSpell(spell));
    }
    private void SetInstructions(Pose pose)
    {
        switch (pose)
        {
            case Pose.Forward:
                instruction.sprite = poseInstructions[0];
                instruction.rectTransform.localScale = new Vector3(-1, 1, 1);
                break;
            case Pose.UpHigh:
                instruction.sprite = poseInstructions[1];
                instruction.rectTransform.localScale = new Vector3(-1, 1, 1);
                break;
            case Pose.UpCenter:
                instruction.sprite = poseInstructions[2];
                break;
            case Pose.Down:
                instruction.sprite = poseInstructions[3];
                instruction.rectTransform.localScale = new Vector3(-1, 1, 1);
                break;
            case Pose.RightMain:
                instruction.sprite = poseInstructions[4];
                instruction.rectTransform.localScale = new Vector3(-1, 1, 1);
                break;
            case Pose.RightOff:
                instruction.sprite = poseInstructions[5];
                instruction.rectTransform.localScale = new Vector3(1, 1, 1);
                break;
            case Pose.RightLow:
                instruction.sprite = poseInstructions[7];
                instruction.rectTransform.localScale = new Vector3(1, 1, 1);
                break;
            case Pose.RightCenter:
                instruction.sprite = poseInstructions[6];
                instruction.rectTransform.localScale = new Vector3(-1, 1, 1);
                break;
            case Pose.LeftMain:
                instruction.sprite = poseInstructions[4];
                instruction.rectTransform.localScale = new Vector3(1, 1, 1);
                break;
            case Pose.LeftOff:
                instruction.sprite = poseInstructions[5];
                instruction.rectTransform.localScale = new Vector3(-1, 1, 1);
                break;
            case Pose.LeftLow:
                instruction.sprite = poseInstructions[7];
                instruction.rectTransform.localScale = new Vector3(1, 1, 1);
                break;
            case Pose.LeftCenter:
                instruction.sprite = poseInstructions[6];
                instruction.rectTransform.localScale = new Vector3(-1, 1, 1);
                break;
            default:
                break;
        }
    }
    IEnumerator HoldPose(Pose pose)
    {
        Direction dir = Direction.forward;
        switch (pose)
        {
            case Pose.Forward:
                dir = Direction.forward;
                break;
            case Pose.UpHigh:
                dir = Direction.up;
                break;
            case Pose.UpCenter:
                dir = Direction.up;
                break;
            case Pose.Down:
                dir = Direction.down;
                break;
            case Pose.RightMain:
                dir = Direction.right;
                break;
            case Pose.RightOff:
                dir = Direction.right;
                break;
            case Pose.RightLow:
                dir = Direction.right;
                break;
            case Pose.RightCenter:
                dir = Direction.right;
                break;
            case Pose.LeftMain:
                dir = Direction.left;
                break;
            case Pose.LeftOff:
                dir = Direction.left;
                break;
            case Pose.LeftLow:
                dir = Direction.left;
                break;
            case Pose.LeftCenter:
                dir = Direction.left;
                break;
        }

        float timer = 0f;
        SetInstructions(pose);
        while (timer < holdPoseTime)
        {
            if (jWand.GetDirection() == dir)
            {
                timer += Time.deltaTime;
            }
            else if (timer > 0f)
            {
                timer -= Time.deltaTime;
            }
            timer = Mathf.Clamp(timer, 0f, holdPoseTime);
            yield return null;
        }
        yield return StartCoroutine(CompletePose());
    }
    
    /*
    IEnumerator SetPose()
    {
        Spell[] patValues = (Spell[])Enum.GetValues(typeof(Spell));
        int index = UnityEngine.Random.Range(0, patValues.Length);
        target = patValues[index];
        
        arrow.transform.eulerAngles = new Vector3(-110, 0, 0);

        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(HoldPose(Direction.forward));
        jWand.Recalibrate();
        
        switch (target)
        {
            case Pattern.spell1:
                yield return StartCoroutine(HoldPose(Direction.up));
                break;
            case Pattern.spell2:
                yield return StartCoroutine(HoldPose(Direction.right));
                yield return StartCoroutine(HoldPose(Direction.left));
                break;
            case Pattern.spell3:
                yield return StartCoroutine(HoldPose(Direction.down));
                yield return StartCoroutine(HoldPose(Direction.up));
                yield return StartCoroutine(HoldPose(Direction.back));
                break;
            default:
                yield return null;
                break;
        }
        StartCoroutine(SetPose());
    }*/

    IEnumerator CompletePose()
    {
        instruction.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
    }
    IEnumerator CastSpell(Spell spell)
    {
        switch (spell)
        {
            case Spell.Forget:
                break;
            case Spell.Burly:
                break;
            case Spell.Smiley:
                break;
            case Spell.Rage:
                break;
            case Spell.Teleport:
                break;
            case Spell.Mustache:
                break;
            case Spell.Dispel:
                break;
            case Spell.Charm:
                break;
            case Spell.Laser:
                break;
            case Spell.Quit:
                break;
            case Spell.Calibrate:
                break;
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine(SolveQuestion(spell));
    }
    private IEnumerator CreateQuestion()
    {
        questionIndex = (int)Random.Range(0, questions.Count);

        // Send question string to UI
        yield return new WaitForSeconds(1f);
        // Set game state to await spell selection
        spellbook.SpellSelection();
    }
    private IEnumerator SolveQuestion(Spell spell)
    {
        bool success = false;

        if (!(spell == Spell.Forget || spell == Spell.Quit || spell == Spell.Calibrate))
        {
            switch (questionIndex)
            {
                case 0:
                    for (int i = 0; i < answer0.Count; i++)
                    {
                        if (spell == answer0[i])
                        {
                            success = true;
                            break;
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < answer1.Count; i++)
                    {
                        if (spell == answer1[i])
                        {
                            success = true;
                            break;
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < answer2.Count; i++)
                    {
                        if (spell == answer2[i])
                        {
                            success = true;
                            break;
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < answer3.Count; i++)
                    {
                        if (spell == answer3[i])
                        {
                            success = true;
                            break;
                        }
                    }
                    break;
                case 4:
                    for (int i = 0; i < answer4.Count; i++)
                    {
                        if (spell == answer4[i])
                        {
                            success = true;
                            break;
                        }
                    }
                    break;
            }
            if (success)
            {
                // cheer
            }
            else
            {
                // sad
            }
            yield return new WaitForSeconds(1f);
            CreateQuestion();
        }
        else if (spell != Spell.Quit) 
        {
            yield return new WaitForSeconds(1f);
            spellbook.SpellSelection();
        }
        else
        {
            Application.Quit();
        }

    }
}
