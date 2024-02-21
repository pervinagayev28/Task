using ChatAppService.Services;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using ThreadPractic.Models;
using Whatsapp.Commands;
using static System.Net.Mime.MediaTypeNames;

namespace ThreadPractic.ViewModels
{
    public class MainWindowViewModel : ServiceINotifyPropertyChanged
    {
        private string filePath;
        private long maximum;
        private long currentValue;
        private long precentage;
        private bool MultiOrSingle = false;
        private ObservableCollection<Car> cars;


        public ICommand StartCommand { get; set; }
        public ICommand OnOffCommand { get; set; }

        public ObservableCollection<Car> Cars { get => cars; set { cars = value; OnPropertyChanged(); } }

        public MainWindowViewModel()
        {
            Cars = new();
            //Cars = new()
            //{
            //    new(){ImagePath =@"\Images\download (1).jpeg" ,Model="Nissan",Vendor="NisaanaOxshar",Marka="Nissosh"},
            //    new(){ImagePath =@"\Images\download (1).jpeg" ,Model="Nissan",Vendor="NisaanaOxshar",Marka="Nissosh"},
            //    new(){ImagePath =@"\Images\download (1).jpeg" ,Model="Nissan",Vendor="NisaanaOxshar",Marka="Nissosh"},
            //    new(){ImagePath =@"\Images\download (1).jpeg" ,Model="Nissan",Vendor="NisaanaOxshar",Marka="Nissosh"},
            //    new(){ImagePath =@"\Images\download (1).jpeg" ,Model="Nissan",Vendor="NisaanaOxshar",Marka="Nissosh"},
            //    new(){ImagePath =@"\Images\download (1).jpeg" ,Model="Nissan",Vendor="NisaanaOxshar",Marka="Nissosh"},
            //    new(){ImagePath =@"\Images\download (1).jpeg" ,Model="Nissan",Vendor="NisaanaOxshar",Marka="Nissosh"},
            //};

            StartCommand = new Command(ExecuteStartCommand);
            OnOffCommand = new Command(ExecuteOnOffCommand);
        }

        private void ExecuteOnOffCommand(object obj)
        {
            if (((Button)obj)?.Content == "Multi")
            {
                ((Button)obj).Content = "Single";
                MultiOrSingle = false; ;
            }
            else
            {
                ((Button)obj).Content = "Multi";
                MultiOrSingle = true;
            }



        }

        private void ExecuteStartCommand(object obj)
        {
            if (MultiOrSingle)
            {
                Thread th = new(LoadSingle);
                th.Start();
            }
            else
            {
                //var dis = Dispatcher.CurrentDispatcher;
                Thread th = new(LoadSingle);
                th.Start();

                //dis.Invoke(LoadSingle);
                //new Thread(LoadSingle).Start();
            }
        }


        private void LoadSingle()
        {
            string[] files = Directory.GetFiles("..\\..\\..\\Jsons");
            List<Car> temps = new();
            foreach (string file in files)
                foreach (var car in (JsonSerializer.Deserialize<List<Car>>(File.ReadAllText(file))))
                {
                    temps.Add(car);
                }




            Cars = new(temps);
        }
        private void LoadMulti()
        {

        }


    }
}
