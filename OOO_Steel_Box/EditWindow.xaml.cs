using Microsoft.Win32;
using OOO_Steel_Box.Classes;
using OOO_Steel_Box.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OOO_Steel_Box
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //Получение данных        
            string strNameBuild = tbName.Text; //Название игры
            string strProcessor = cbProcessor.Text;
            string strVideocard = cbVideocard.Text;
            string strMotherboard = cbMotherboard.Text;
            string strHardDrive = cbHardDrive.Text;
            string strSSD = cbSolidStateDriver.Text;
            string strPowerUnit = cbPowerUnit.Text;
            string strFrame = cbFrame.Text;
            string strCooler = cbColer.Text;
            string strPrice = tbPrice.Text;
            string strDiscount = tbDiscount.Text;
            string strDescription = tbDescription.Text;


            // Далее можно использовать selectedContent по своему усмотрению

            int selectedProcessor = 1 + cbProcessor.SelectedIndex;
            int selectedVideocard = 1 + cbVideocard.SelectedIndex;
            int selectedMotherboard = 1 + cbMotherboard.SelectedIndex;
            int selectedHardDrive = 1 + cbHardDrive.SelectedIndex;
            int selectedSSD = 1 + cbSolidStateDriver.SelectedIndex;
            int selectedPowerUnit = 1 + cbPowerUnit.SelectedIndex;
            int selectedFrame = 1 + cbFrame.SelectedIndex;
            int selectedColer = 1 + cbColer.SelectedIndex;


            //Изображение 
            // Получение текущего изображения из элемента Image
            string fileName;
            string fileName1 = null;

            //Исключение если пустые поля
            if (strNameBuild != "" && strProcessor != "" && strVideocard != "" && strMotherboard != "" && strHardDrive != "" 
                && strSSD != "" && strPowerUnit != "" && strFrame != "" && strCooler != "" && strPrice != "" && strDiscount != "")
            {
                var image = SelectedPhoto.Source as BitmapSource;


                fileName = null;
                // Проверка, что изображение не равно null
                if (image != null)
                {
                    // Генерация уникального имени файла
                    string uniqName = strNameBuild.Replace(" ", "");
                    fileName = $"{uniqName.ToString()}.jpg"; //название файла с фото

                    //Путь файла
                    string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Resources/");

                    // Полный путь к сохраняемому файлу
                    string savePath = System.IO.Path.Combine(folderPath, fileName);

                    // Создание кодировщика JPEG для сохранения изображения
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image));


                    // Сохранение файла на диск
                    using (var fileStream = new FileStream(savePath, FileMode.Create))
                    {
                        encoder.Save(fileStream);
                    }
                    if (fileName != null)
                    {
                        fileName1 = fileName;
                    }
                }
                else
                {
                    fileName1 = "NULL";
                }
                //Наш товар
                System.Windows.MessageBox.Show("Название игры:" + strNameBuild.ToString() + "\n" +
                                "Процессор: " + strProcessor.ToString() + "\n" +
                                "Видеокарта: " + strVideocard.ToString() + "\n" +
                                "Материнская плата: " + strMotherboard.ToString() + "\n" +
                                "Жесткий диск: " + strHardDrive.ToString() + "\n" +
                                "SSD: " + strSSD.ToString() + "\n" +
                                "Блок питания: " + strPowerUnit.ToString() + "\n" +
                                "Системный блок: " + strFrame.ToString() + "\n" +
                                "Кулер: " + strCooler.ToString() + "\n" +
                                "Цена: " + strPrice.ToString() + "\n" +
                                "Скидка: " + strDiscount.ToString() + "\n" +
                                "Изображение: " + fileName1.ToString() + "\n" +
                                "Описание: " + strDescription.ToString() + "\n"
                    );

                //Сохранение в базу данных
                //Создаём объект товара
                Model.PCBuilds builds = new Model.PCBuilds()
                {
                    //Запполняем поля
                    PCBuildName = strNameBuild,
                    ProcessorID = selectedProcessor,
                    VideocardID = selectedVideocard,
                    MotherboardID = selectedMotherboard,
                    HardDriveID = selectedHardDrive,
                    SolidStateDriveID = selectedSSD,
                    PowerUnitID = selectedPowerUnit,
                    FrameID = selectedFrame,
                    CoolerID = selectedColer,
                    PCBuildDescription = strDescription,
                    PCBuildPrice = int.Parse(strPrice),
                    PCBuildDiscount = int.Parse(strDiscount),
                    PCBuildImage = fileName1
                };

                // Добавление нового продукта в таблицу product
                Helper.DB.PCBuilds.Add(builds);
                try
                {
                    //Сохранение в базу
                    Helper.DB.SaveChanges();
                    System.Windows.MessageBox.Show("Сборка добавлена");
                    this.Close();
                }
                catch
                {
                    System.Windows.MessageBox.Show("Произошёл сбой при сохранении");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Вы не все заполнили");
            }

            Catalog catalog = new Catalog();
            this.Close();
            catalog.Show();
        }

        private void SelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg) | *.jpg";

            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.EndInit();
                SelectedPhoto.Source = bitmap;
            }
        }

        private void tbPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$"); // Регулярное выражение для проверки числа
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true; // Если ввод не является числом, то обработка события формируется и значит ввод числа недопустим
            }
        }

        private void tbDiscount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$"); // Регулярное выражение для проверки числа
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true; // Если ввод не является числом, то обработка события формируется и значит ввод числа недопустим
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Catalog catalog = new Catalog();
            this.Close();
            catalog.Show();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            List<Processors> processorList = new List<Processors>();
            processorList = Helper.DB.Processors.ToList();
            cbProcessor.ItemsSource = processorList;
            cbProcessor.DisplayMemberPath = "ProcessorName";
            cbProcessor.SelectedValuePath = "ProcessorID";
            cbProcessor.SelectedIndex = 0;
            cbProcessor.SelectedValue = 0;

            List<Videocards> videocard = new List<Videocards>();
            videocard = Helper.DB.Videocards.ToList();
            cbVideocard.ItemsSource = videocard;
            cbVideocard.DisplayMemberPath = "VideocardName";
            cbVideocard.SelectedValuePath = "VideocardID";
            cbVideocard.SelectedIndex = 0;
            cbVideocard.SelectedValue = 0;

            List<Coolers> coolers = new List<Coolers>();
            coolers = Helper.DB.Coolers.ToList();
            cbColer.ItemsSource = coolers;
            cbColer.DisplayMemberPath = "CoolerName";
            cbColer.SelectedValuePath = "CoolerID";
            cbColer.SelectedIndex = 0;
            cbColer.SelectedValue = 0;

            List<Frames> frames = new List<Frames>();
            frames = Helper.DB.Frames.ToList();
            cbFrame.ItemsSource = frames;
            cbFrame.DisplayMemberPath = "FrameName";
            cbFrame.SelectedValuePath = "FrameID";
            cbFrame.SelectedIndex = 0;
            cbFrame.SelectedValue = 0;

            List<HardDrives> hardDrives = new List<HardDrives>();
            hardDrives = Helper.DB.HardDrives.ToList();
            cbHardDrive.ItemsSource = hardDrives;
            cbHardDrive.DisplayMemberPath = "HardDriveName";
            cbHardDrive.SelectedValuePath = "HardDriveID";
            cbHardDrive.SelectedIndex = 0;
            cbHardDrive.SelectedValue = 0;

            List<Motherboards> motherboards = new List<Motherboards>();
            motherboards = Helper.DB.Motherboards.ToList();
            cbMotherboard.ItemsSource = motherboards;
            cbMotherboard.DisplayMemberPath = "MotherboardName";
            cbMotherboard.SelectedValuePath = "MotherboardID";
            cbMotherboard.SelectedIndex = 0;
            cbMotherboard.SelectedValue = 0;

            List<PowerUnits> powerUnits = new List<PowerUnits>();
            powerUnits = Helper.DB.PowerUnits.ToList();
            cbPowerUnit.ItemsSource = powerUnits;
            cbPowerUnit.DisplayMemberPath = "PowerUnitName";
            cbPowerUnit.SelectedValuePath = "PowerUnitID";
            cbPowerUnit.SelectedIndex = 0;
            cbPowerUnit.SelectedValue = 0;

            List<SolidStateDrivers> solidStateDrivers = new List<SolidStateDrivers>();
            solidStateDrivers = Helper.DB.SolidStateDrivers.ToList();
            cbSolidStateDriver.ItemsSource = solidStateDrivers;
            cbSolidStateDriver.DisplayMemberPath = "SolidStateDriveName";
            cbSolidStateDriver.SelectedValuePath = "SolidStateDriveID";
            cbSolidStateDriver.SelectedIndex = 0;
            cbSolidStateDriver.SelectedValue = 0;

        }
    }
}
