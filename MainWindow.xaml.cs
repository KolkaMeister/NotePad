using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using NotePad.models;

namespace NotePad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TextBlock textBlock;
        public TextBox textBox;
        public DocumentModel Document =new DocumentModel();
        public FormatModel Format =new FormatModel();
        public MainWindow()
        {
            InitializeComponent();
           // ReadSavedFormat();
            InitFollowEvents();
        }
        private void InitFollowEvents()
        {
            Document.TextChanged += OnTextChanged;
            Document.PathChanged += OnPathChanged;
            Format.ColorChanged += OnColorChanged;
            Format.FontSizeChanged += OnSizeChanged;
            Format.FontFamilyChanged += OnFamilyChanged;
            OnPathChanged(Document.Path);
        }
        public void ReadSavedFormat()
        {
            var format = File.ReadAllText("Format");
            Format = JsonSerializer.Deserialize<FormatModel>(format);
            OnColorChanged(Format.Color);
            OnFamilyChanged(Format.FontFamily);
            OnSizeChanged(Format.FontSize);
        }
        public void OnColorChanged(Brush col)
        {
            _textBox.Foreground= col;
        }
        public void OnFamilyChanged(FontFamily fam)
        {
            _textBox.FontFamily = fam;
        }
        public void OnSizeChanged(double size)
        {
            _textBox.FontSize = size;
        }
        public void SetColor(object sender, RoutedEventArgs e)
        {
            Format.Color =(Brush) new BrushConverter().ConvertFromString((sender as MenuItem).Name);
        }
        public void SetFontSize(object sender, RoutedEventArgs e)
        {
            var val = (sender as MenuItem).Header;
            Format.FontSize = double.Parse(val.ToString());
        }
        public void SetFontFamily(object sender, RoutedEventArgs e)
        {
            Format.FontFamily = new FontFamily((sender as MenuItem).Header.ToString());
        }
        public void NewFile(object sender, RoutedEventArgs e)
        {
            Document.Text = string.Empty;
            Document.Path = string.Empty;
            Document.Name = string.Empty;
        }
        public void OnTextChanged(string value)
        {
            _textBox.Text = value;
        }
        public void OnPathChanged(string value)
        {
            _textBlock.Text = value;
            _saveMenuItem.IsEnabled = !Document.IsEmpty;
        }
        public void OnType(object sender, RoutedEventArgs e)
        {
            Document.Text = _textBox.Text;
        }
        public void SaveFile(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(Document.Path,Document.Text);
        }
        public void SaveAsFile(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File(*.txt)|*.txt";
            if (saveFileDialog.ShowDialog()==true)
            {
                FillDoc(saveFileDialog);
                File.WriteAllText(saveFileDialog.FileName, Document.Text);
            }
        }
        public void OpenFile(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog()==true ) 
            {
                FillDoc(openFileDialog);
                Document.Text=File.ReadAllText(openFileDialog.FileName);
            }
        }
        public void FillDoc<T>(T dialog) where T : FileDialog 
        {
            Document.Name = dialog.SafeFileName;
            Document.Path = dialog.FileName;

        }
        private void OnExit(object sender, EventArgs e)
        {
            File.WriteAllText("Format", JsonSerializer.Serialize(Format));
        }
    }
}
