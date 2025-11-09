using UnityEngine;
using UnityEngine.UI;
using Control;

public class RepairAntenna : MonoBehaviour
{
    [Header("Primary")]
    public float primaryAngle = 0f;
    public float primaryTargetAngle = 145f;
    public float primaryTolerance = 3f;
    public float primarySpeed = 140f;

    [Header("Secondary")]
    public float secondaryAngle = 0f;
    public float secondaryTargetAngle = 220f;
    public float secondaryTolerance = 3f;
    public float secondarySpeed = 40f;

    [Header("Input")]
    public float deadzone = 0.15f;

    [Header("Indicator")]
    public Image indicatorImage;
    public Color colorNone = Color.red;
    public Color colorOne = new Color(1f, 0.92f, 0.016f);
    public Color colorAll = Color.green;
    public GameObject primaryPointer;
    public GameObject secondPointer;

    Vector2 navigateInput;
    Vector2 lookInput;

    bool primaryLocked;
    bool secondaryLocked;
    bool bothLocked;

    void OnEnable()
    {
        var ih = PlayerInputHandler.Instance;
        if (ih != null)
        {
            ih.Navigate += OnNavigate;
            ih.Look += OnLook;
            ih.SwitchTo(ActionMap.UI);
            ih.Enable();
        }
        ApplyIndicator();
        ApplyPointers();
    }

    void OnDisable()
    {
        var ih = PlayerInputHandler.Instance;
        if (ih != null)
        {
            ih.Navigate -= OnNavigate;
            ih.Look -= OnLook;
            ih.SwitchTo(ActionMap.Player);
            ih.Enable();
        }
    }

    void Update()
    {
        float navX = Mathf.Abs(navigateInput.x) > deadzone ? navigateInput.x : 0f;
        float lookX = Mathf.Abs(lookInput.x) > deadzone ? lookInput.x : 0f;

        primaryAngle = Wrap360(primaryAngle + navX * primarySpeed * Time.deltaTime);
        secondaryAngle = Wrap360(secondaryAngle + lookX * secondarySpeed * Time.deltaTime);

        bool newPrimaryLocked = Mathf.Abs(Mathf.DeltaAngle(primaryAngle, primaryTargetAngle)) <= primaryTolerance;
        bool newSecondaryLocked = Mathf.Abs(Mathf.DeltaAngle(secondaryAngle, secondaryTargetAngle)) <= secondaryTolerance;

        primaryLocked = newPrimaryLocked;
        secondaryLocked = newSecondaryLocked;
        bothLocked = primaryLocked && secondaryLocked;

        ApplyIndicator();
        ApplyPointers();
    }

    void ApplyIndicator()
    {
        if (indicatorImage == null) return;
        if (bothLocked) indicatorImage.color = colorAll;
        else if (primaryLocked ^ secondaryLocked) indicatorImage.color = colorOne;
        else indicatorImage.color = colorNone;
    }

    void ApplyPointers()
    {
        if (primaryPointer != null)
            primaryPointer.GetComponent<RectTransform>().localEulerAngles = new Vector3(0f, 0f, -primaryAngle);
        if (secondPointer != null)
            secondPointer.GetComponent<RectTransform>().localEulerAngles = new Vector3(0f, 0f, -secondaryAngle);
    }

    void OnNavigate(Vector2 v) => navigateInput = v;
    void OnLook(Vector2 v) => lookInput = v;

    static float Wrap360(float angle)
    {
        angle %= 360f;
        if (angle < 0f) angle += 360f;
        return angle;
    }

    public void SetPrimaryTarget(float angle) => primaryTargetAngle = Wrap360(angle);
    public void SetSecondaryTarget(float angle) => secondaryTargetAngle = Wrap360(angle);
    public void RandomizeTargets()
    {
        primaryTargetAngle = Random.Range(0f, 360f);
        secondaryTargetAngle = Random.Range(0f, 360f);
    }

    public bool IsCompelete() 
    {
        if (primaryLocked && secondaryLocked) 
        {
            return true;
        }

        return false;
    }
}
