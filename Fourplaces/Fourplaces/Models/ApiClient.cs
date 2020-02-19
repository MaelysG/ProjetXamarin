using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Common.Api.Dtos;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using TD.Api.Dtos;

public class ApiClient
{
	private readonly HttpClient _client = new HttpClient();

	public async Task<HttpResponseMessage> Execute(HttpMethod method, string url, object data = null, string token = null)
	{
		HttpRequestMessage request = new HttpRequestMessage(method, url);

		if (data != null)
		{
			request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
		}

		if (token != null)
		{
			request.Headers.Add("Authorization", $"Bearer {token}");
		}

		return await _client.SendAsync(request);
	}

	public async Task<T> ReadFromResponse<T>(HttpResponseMessage response)
	{
		string result = await response.Content.ReadAsStringAsync();

		return JsonConvert.DeserializeObject<T>(result);
	}

	public async Task<ImageItem> PublishImage(MediaFile file)
	{
		HttpClient client = new HttpClient();
		var memoryStream = new MemoryStream();
		file.GetStream().CopyTo(memoryStream);
		file.Dispose();
		byte[] imageData = memoryStream.ToArray();

		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://td-api.julienmialon.com/images");
		request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "__access__token__");

		MultipartFormDataContent requestContent = new MultipartFormDataContent();

		var imageContent = new ByteArrayContent(imageData);
		imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

		// Le deuxième paramètre doit absolument être "file" ici sinon ça ne fonctionnera pas
		requestContent.Add(imageContent, "file", "file.jpg");

		request.Content = requestContent;

		HttpResponseMessage response = await client.SendAsync(request);

		string result = await response.Content.ReadAsStringAsync();
		if (response.IsSuccessStatusCode)
		{
			ApiClient apiClient = new ApiClient();
			Response<ImageItem> imageResponse = await apiClient.ReadFromResponse<Response<ImageItem>>(response);
			ImageItem item = imageResponse.Data;

			return item;
		}
		return new ImageItem();
	}
}