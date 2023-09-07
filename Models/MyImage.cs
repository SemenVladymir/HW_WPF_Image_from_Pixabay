using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HW_WPF_Image_from_Pixabay.Models
{
    public class MyImage
    {
        string name;
        string urlPhoto;
        int size;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Size
        {
            get { return size; }
            set { size = value; OnPropertyChanged("Size"); }
        }

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        public string UrlPhoto
        {
            get { return urlPhoto; }
            set { urlPhoto = value; OnPropertyChanged("UrlPhoto"); }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
