using UnityEngine;
using UnityEngine.Events;

public class MopHead : MonoBehaviour
{
    //[SerializeField] private UnityEvent OnGrow;
    [SerializeField] private AudioClip _growSound;
    public void WidthUp(float widthScale)
    {
        var curLocalScale = this.transform.localScale;
        var localXUpdated = new Vector3(curLocalScale.x * widthScale, 
            curLocalScale.y, curLocalScale.z);
        this.transform.localScale =  localXUpdated;

        //this.OnGrow?.Invoke();
        Audio.Instance.PlaySfx(_growSound);
    }
}