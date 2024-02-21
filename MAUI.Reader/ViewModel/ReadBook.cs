using System.ComponentModel;
using System.Speech.Synthesis;
using CommunityToolkit.Mvvm.Input;
using MAUI.Reader.Model;

namespace MAUI.Reader.ViewModel
{
    public partial class ReadBook : INotifyPropertyChanged
    {
        
        public Book ReceivedBook { get; set; }
        public SpeechSynthesizer synth { get; set; }
        public string Controller { get; set; }
        public ReadBook(Book book)
        {
            Controller = "read";
            ReceivedBook = book;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void Read()
        {
            
            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.SpeakCompleted += RestartReading;
            synth.SpeakAsync(ReceivedBook.Content.ToString());
        }
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

        public void OnSelectText(Entry entry, EventArgs e)
        {
            
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
