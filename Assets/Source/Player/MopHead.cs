using UnityEngine;
using UnityEngine.Events;

public class MopHead : MonoBehaviour
{
    [SerializeField] private UnityEvent OnGrow;
    public void WidthUp(float widthScale)
    {
        var curLocalScale = this.transform.localScale;
        var localXUpdated = new Vector3(curLocalScale.x * widthScale, 
            curLocalScale.y, curLocalScale.z);
        this.transform.localScale =  localXUpdated;

        this.OnGrow?.Invoke();
        
    }
}