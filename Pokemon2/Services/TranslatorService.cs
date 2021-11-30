using Pokemon2.Models.ModelsForTranslator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokemon2.Services
{
    public class TranslatorService : ITranslatorService
    {


        public async Task<string> TranslateToShakespeare(string textToTranslate)
        {
            try
            {
                var translator = new HttpClient
                {
                    BaseAddress = new Uri("https://api.funtranslations.com")
                };

                var encodedText = textToTranslate.Replace("\n", "\\n").Replace("\r", "").Replace(" ", "%20");

                var result = await translator.GetAsync($"/translate/shakespeare.json?text={encodedText}");

                if (!result.IsSuccessStatusCode)
                {
                    return textToTranslate;
                }

                var jsonContent = await result.Content.ReadAsStringAsync();
                var translationResponse = JsonSerializer.Deserialize<TranslationResponse>(jsonContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return translationResponse.Contents.Translated.Replace("\\n", "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine((ex, $"Error translating text :\"{textToTranslate}\" to Shakespeare"));
                return textToTranslate;
            }
        }


        public async Task<string> TranslateToYoda(string textToTranslate)
        {
            try
            {
                var translator = new HttpClient
                {
                    BaseAddress = new Uri("https://api.funtranslations.com")
                };

                var encodedText = textToTranslate.Replace("\n", "\\n").Replace("\r", "").Replace(" ", "%20");

                var result = await translator.GetAsync($"/translate/yoda.json?text={encodedText}");

                if (!result.IsSuccessStatusCode)
                {
                    return textToTranslate;
                }

                var jsonContent = await result.Content.ReadAsStringAsync();
                var translationResponse = JsonSerializer.Deserialize<TranslationResponse>(jsonContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return translationResponse.Contents.Translated.Replace("\\n", "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine((ex, $"Error translating text :\"{textToTranslate}\" to Shakespeare"));
                return textToTranslate;
            }
        }
    }
}
