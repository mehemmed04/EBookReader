using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EBookReader.Services
{
    public class TextToSpeechService
    {
        public static SpeechSynthesizer _ss { get; set; } = new SpeechSynthesizer();
        public static InstalledVoice SelecetedVoice { get; set; }
        public static void TextToSpeech(string text)
        {
            if (text != null && text != string.Empty)
            {
                _ss = new SpeechSynthesizer();
                if (SelecetedVoice != null)
                {
                    _ss.SelectVoice(SelecetedVoice.VoiceInfo.Name);
                }
                //_ss.AddLexicon(new Uri("C:\\Users\\Acer\\Desktop\\Blue.pls"), "application/pls+xml");
                _ss.Speak(text);
            }
        }
    }
}
