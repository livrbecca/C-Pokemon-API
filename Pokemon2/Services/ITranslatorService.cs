using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon2.Services
{
    public interface ITranslatorService
    {
        Task<string> TranslateToYoda(string textToTranslate);
        Task<string> TranslateToShakespeare(string textToTranslate);

    }
}
