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
        public ReadBook(Book book)
        {
            
            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            ReceivedBook = book;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        [RelayCommand]
        public void Read()
        {
            synth.Speak(ReceivedBook.Content.ToString());
        }
        [RelayCommand]
        public void Pause()
        {
            synth.Pause();
        }

        [RelayCommand]
        public void Stop()
        {
            synth.Dispose();
        }

        // A vous de jouer maintenant
    }

    /* Cette classe sert juste a afficher des donnée de test dans le designer */
    class InDesignReadBook : ReadBook
    {
        public InDesignReadBook() : base(new Book())
        {
        }
    }
}
