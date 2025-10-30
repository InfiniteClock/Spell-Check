using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum Pattern { spell1, spell2, spell3}
public class GameManager : MonoBehaviour
{
    public Image arrow;
    public Image instructionsPanel;
    public JoyconWand jWand;
    public float holdPoseTime;

    private Pattern target;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        arrow.gameObject.SetActive(false);
        StartCoroutine(SetPose());
    }
    private void SetInstructions(Direction dir)
    {
        switch (dir)
        {
            case Direction.forward:
                arrow.transform.eulerAngles = new Vector3(-110, 0, 0);
                break;
            case Direction.down:
                arrow.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case Direction.right:
                arrow.transform.eulerAngles = new Vector3(0, 0, 90);
                break;
            case Direction.up:
                arrow.transform.eulerAngles = new Vector3(0, 0, 180);
                break;
            case Direction.left:
                arrow.transform.eulerAngles = new Vector3(0, 0, 270);
                break;
            case Direction.back:
                arrow.transform.eulerAngles = new Vector3(70, 0, 0);
                break;
            default:
                break;
        }
        arrow.gameObject.SetActive(true);
    }

    IEnumerator SetPose()
    {
        Pattern[] patValues = (Pattern[])Enum.GetValues(typeof(Pattern));
        int index = UnityEngine.Random.Range(0, patValues.Length);
        target = patValues[index];

        arrow.transform.eulerAngles = new Vector3(-110, 0, 0);

        yield return new WaitForSeconds(2f);
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
    }
    IEnumerator HoldPose(Direction dir)
    {
        float timer = 0f;
        SetInstructions(dir);
        while (timer < holdPoseTime) {
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
    IEnumerator CompletePose()
    {
        arrow.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
    }
}
