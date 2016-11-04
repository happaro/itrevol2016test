using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;

/*
 * https://github.com/ChrisMaire/unity-native-sharing 
 */

public class NativeShare : MonoBehaviour 
{
	//const string ScreenshotName = "screenshot.png";
	private bool isProcess = false;
	
	public IEnumerator ShareScreen(string text)
	{
		isProcess = true;
		yield return new WaitForEndOfFrame();
		Texture2D screenTexture = new Texture2D(Screen.width, Screen.height,TextureFormat.RGB24,true);
		screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
		screenTexture.Apply();
		//-------------------------------------------------------------------------------- PHOTO
		byte[] dataToSave = screenTexture.EncodeToPNG();
		string destination = Path.Combine(Application.persistentDataPath,System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
		File.WriteAllBytes(destination, dataToSave);
		
		Share(text, destination, "");
		isProcess = false;
		yield return new WaitForEndOfFrame();
	}
	
	public void ShareScreenshotWithText(string text)
	{
		if (!isProcess)
			StartCoroutine(ShareScreen(text));
		
		/*string screenShotPath = Application.persistentDataPath + "/" + ScreenshotName;
        Application.CaptureScreenshot(ScreenshotName);

        Share(text,screenShotPath,"");*/
	}
	
	public void Share(string shareText, string imagePath, string url, string subject = "")
	{
		#if UNITY_ANDROID
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
		
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + imagePath);
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
		intentObject.Call<AndroidJavaObject>("setType", "image/png");
		
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareText);
		
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		
		AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, subject);
		currentActivity.Call("startActivity", jChooser);
		#elif UNITY_IOS
		CallSocialShareAdvanced(shareText, subject, url, imagePath);
		#else
		Debug.Log("No sharing set up for this platform.");
		#endif
	}
	
	#if UNITY_IOS
	public struct ConfigStruct
	{
		public string title;
		public string message;
	}
	
	[DllImport ("__Internal")] private static extern void showAlertMessage(ref ConfigStruct conf);
	
	public struct SocialSharingStruct
	{
		public string text;
		public string url;
		public string image;
		public string subject;
	}
	
	[DllImport ("__Internal")] private static extern void showSocialSharing(ref SocialSharingStruct conf);
	
	public static void CallSocialShare(string title, string message)
	{
		ConfigStruct conf = new ConfigStruct();
		conf.title  = title;
		conf.message = message;
		showAlertMessage(ref conf);
	}
	
	public static void CallSocialShareAdvanced(string defaultTxt, string subject, string url, string img)
	{
		SocialSharingStruct conf = new SocialSharingStruct();
		conf.text = defaultTxt; 
		conf.url = url;
		conf.image = img;
		conf.subject = subject;
		
		showSocialSharing(ref conf);
	}
	#endif
}
