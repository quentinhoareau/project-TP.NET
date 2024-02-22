using System.ComponentModel;
using System.Speech.Synthesis;
using CommunityToolkit.Mvvm.Input;
using MAUI.Reader.Model;

namespace MAUI.Reader.ViewModel
{
    public partial class ReadBook: INotifyPropertyChanged
    {
        

        public int CursorPosition
        {
            get;
            set;
        }


        public int SelectedTextLength
        {
            get;
            set;
        }


        public Book ReceivedBook { get; set; }
        public SpeechSynthesizer synth { get; set; }
        public string Controller { get; set; }
        public ReadBook(Book book)
        {
            Controller = "read";
            ReceivedBook = book;
            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
        }
        [RelayCommand]
        public void Read()
        {
            string textToSpeech = ReceivedBook.Content.ToString();
            string selectedText = ReceivedBook.Content[this.CursorPosition..(this.CursorPosition + this.SelectedTextLength)];
            Console.WriteLine(selectedText);
            if (selectedText != "")
            {
                textToSpeech = selectedText;
            }
            synth.SpeakCompleted += RestartReading;
            synth.SpeakAsync(textToSpeech);
        }
        [RelayCommand]
        public void Pause()
        {
            synth.Pause();
        }

        [RelayCommand]
        public void Stop()
        {
            synth.SpeakAsyncCancelAll();
            Controller = "read";
        }
        [RelayCommand]
        public void Resume()
        {
            synth.Resume();
        }
        
        [RelayCommand]
        public void PauseOrResumeOrRead()
        {
            switch (Controller)
            {
                case "resume":
                    Resume();
                    Controller = "pause";
                    break;
                case "pause":
                    Pause();
                    Controller = "resume";
                    break;
                default:
                    Read();
                    Controller = "pause";
                    break;
            }
            
            
        }

        private void RestartReading(Object sender, SpeakCompletedEventArgs eventArgs)
        {
            Controller = "read";
        }
    }

    /* Cette classe sert juste a afficher des donnée de test dans le designer */
    class InDesignReadBook : ReadBook
    {
        public InDesignReadBook() : base(new Book())
        {
        }
    }

}
