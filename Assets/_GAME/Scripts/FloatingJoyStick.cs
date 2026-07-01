using UnityEngine;
using UnityEngine.EventSystems;
public class FloatingJoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public RectTransform bg;
    public RectTransform handle;
    private Vector2 inputVector;
    public float Horizontal => inputVector.x;
    public float Vertical => inputVector.y;
    void Start()
    {
        bg.gameObject.SetActive(false);
    }
    public void OnPointerDown(PointerEventData e)
    {
        bg.gameObject.SetActive(true);
        bg.position = e.position;
        handle.anchoredPosition = Vector2.zero;
        OnDrag(e);     
    }
    public void OnDrag(PointerEventData e)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bg,e.position, e.pressEventCamera, out pos))
        {
            pos.x = (pos.x/bg.sizeDelta.x);
            pos.y = (pos.y/bg.sizeDelta.y);
            inputVector = new Vector2(pos.x*2-1,pos.y*2-1);
            inputVector = (inputVector.magnitude>1.0f)?inputVector.normalized:inputVector;
            handle.anchoredPosition = new Vector2(inputVector.x*(bg.sizeDelta.x/2),inputVector.y*(bg.sizeDelta.y/2));
        }
    }
    public void OnPointerUp(PointerEventData e)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
        bg.gameObject.SetActive(false);
    }
}
