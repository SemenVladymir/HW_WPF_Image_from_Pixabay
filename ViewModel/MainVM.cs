using HW_WPF_Image_from_Pixabay.Models;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UsefulPlantsCatalog.Model;

namespace HW_WPF_Image_from_Pixabay.ViewModel
{
    public class MainVM
    {
        private ObservableCollection<MyImage> images;
        private MyImage selectedImage;
        private string searchText;
        private string folder;
        private int amount = 3;
        private bool _isChecked;
        private bool _allfiles;

        public ObservableCollection<MyImage> Images
        {
            get { return images; }
            set { images = value; OnPropertyChanged("Images"); }
        }

        public MyImage SelectedImage
        {
            get { return selectedImage; }
            set { selectedImage = value; OnPropertyChanged("SelectedImage"); }
        }

        public string SearchText
        {
            get
            {return searchText;}
            set { searchText = value; OnPropertyChanged("SearchText"); }
        }

        public string Folder
        {
            get
            { return folder; }
            set { folder = value; OnPropertyChanged("Folder"); }
        }

        public int Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged("Amount"); }
        }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        public bool AllFiles
        {
            get { return _allfiles; }
            set
            {
                _allfiles = value;
                OnPropertyChanged(nameof(AllFiles));
            }
        }

        private RelayCommand searching;
        public RelayCommand Searching
        {
            get
            {
                if (searching == null)
                    searching = new RelayCommand(obj => { SearchNLoadImages(); });

                return searching;
            }
        }

        private RelayCommand saveToFolder;
        public RelayCommand SaveToFolder
        {
            get
            {
                if (saveToFolder == null)
                    saveToFolder = new RelayCommand(obj => { SeveImagesToFolder(); });

                return saveToFolder;
            }
        }


        public MainVM()
        {
            images = new ObservableCollection<MyImage>();
            selectedImage = new MyImage();
        }

        private void SearchNLoadImages()
        {
            if (Amount!=null && Amount > 2)
            {
                var key = "39206396-a3d0261b98314ee7c13677bfd";
                var searchingText = searchText.Replace(' ', '+');

                var request = new GetRequest($"https://pixabay.com/api/?key={key}&q={searchingText}&image_type=photo&per_page={amount}");
                request.Start();
                var response = request.Response;
                if (response!= null && response.Length > 0)
                {
                var json = JObject.Parse(response);
                var newimages = json["hits"];
                WebClient client = new WebClient();
                Uri img;
                Uri img1;
                    Images.Clear();
                    foreach (var item in newimages)
                    {
                        img = new Uri(item["largeImageURL"].ToString());
                        img1 = new Uri(item["pageURL"].ToString());
                        string[] tmp = img1.Segments.ToArray();
                        Images.Add(new MyImage { Name = $"{tmp.Last().Substring(0, tmp.Last().Length - 1)}{Path.GetExtension(img.ToString())}", Size = Convert.ToInt32(item["imageSize"]), UrlPhoto = img.ToString() });
                    }
                    SelectedImage = images.First();
                }
                else
                    MessageBox.Show("Images with this text aren`t found!");
            }
            else
                MessageBox.Show("You need to input an amount of images!\nThe amount has to be 3 or more.");

        }

        private void SeveImagesToFolder()
        {
            if (!string.IsNullOrEmpty(Folder) && Directory.Exists(Folder))
            {
                if (AllFiles)
                {
                    if (Images != null && Images.Count > 0)
                    {
                        WebClient client = new WebClient();
                        Uri img;
                        foreach (var image in Images)
                        {
                            img = new Uri(image.UrlPhoto);
                            client.DownloadFile(img, $@"D:/Images/{image.Name}");
                        }
                        MessageBox.Show($"{Amount} files saved to the folder path {Folder}!");
                    }
                }
                else
                {
                    if (SelectedImage != null)
                    {
                        WebClient client = new WebClient();
                        Uri img;
                        img = new Uri(SelectedImage.UrlPhoto);
                        client.DownloadFile(img, $@"D:/Images/{SelectedImage.Name}");
                        MessageBox.Show($"File witn name {SelectedImage.Name} saved to the folder path {Folder}!");
                    }
                    else
                        MessageBox.Show("Select an image to save!");
                }
            }
            else
                MessageBox.Show("This folder doesn`t exist!");
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
