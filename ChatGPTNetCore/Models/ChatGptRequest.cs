using System.Text.Json.Serialization;

namespace ChatGPTNetCore.Models
{
    public class ChatGptRequest
    {
        public ChatGptRequest(string prompt)
        {
            Prompt = prompt;
            Temperature = 0.2m;
            Max_tokens = 100;
            Model = "text-davinci-003";
        }

        [JsonPropertyName("model")]
        public string Model { get; set; }
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }
        [JsonPropertyName("max_tokens")]
        public int Max_tokens { get; set; }

        [JsonPropertyName("temperature")]
        public decimal Temperature { get; set; }

    }
}
