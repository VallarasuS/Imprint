namespace Imprint.IO.Runtime
{
	public interface IJSBinderCallback
	{
		void OnTagIdentified(string tagName, string className, string id, string xpath);

		void OnTagValueIdentified(string callerID, string content, string value, string image, string url, string header, Element type);

		void OnResponse(string callerID, string json);
	}
}