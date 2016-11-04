using UnityEngine;
using System.Collections;

public class Window : MonoBehaviour 
{
	public virtual void Close()
	{   
		gameObject.SetActive (false);
	}
    public virtual void Close(bool allowScroll)
    {
        GameObject.FindObjectOfType<TouchHandler>().allowScroll = allowScroll;
		GameObject.FindObjectOfType<TouchHandler>().isGUI = !allowScroll;
        gameObject.SetActive(false);
    }

	public virtual void Open()
	{
		gameObject.SetActive (true);
	}
    public virtual void Open(bool allowScroll)
    {
        GameObject.FindObjectOfType<TouchHandler>().allowScroll = allowScroll;
		GameObject.FindObjectOfType<TouchHandler>().isGUI = !allowScroll;
        gameObject.SetActive(true);
    }
}
