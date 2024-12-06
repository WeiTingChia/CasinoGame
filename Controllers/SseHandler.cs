

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class SseController : ControllerBase
{
  [HttpGet]
  public async Task GetAsync()
  {
    Response.Headers.Add("Content-Type", "text/event-stream");
    Response.Headers.Add("Cache-Control", "no-cache");
    Response.Headers.Add("Connection", "keep-alive");

    int count = 0;
    while (count < 10)
    {
      await Response.WriteAsync($"data: {count}\n\n");
      count++;
      await Task.Delay(1000);
    }

    await Response.WriteAsync("data: This is the end!\n\n");
    await Response.CompleteAsync();
  }
}