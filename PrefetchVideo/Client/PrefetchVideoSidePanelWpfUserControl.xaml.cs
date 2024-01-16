using PrefetchVideo.Background;
using PrefetchVideo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Client;
using VideoOS.Platform.Data;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.UI;

namespace PrefetchVideo.Client
{
    public partial class PrefetchVideoSidePanelWpfUserControl : SidePanelWpfUserControl
    {
 

      
        private List<DateTime> BolunenZamanlar = new List<DateTime>();
        public DateTime BasTarih = DateTime.Now.AddDays(-1);
        public DateTime BitTarih = DateTime.Now;
        public int ToplamIslemSayisi = 0;
   
        public string BasSaat = "00";
        public string BasDakika = "00";
 

        public int ZamanTurID = 15;
        public SelectModel SeciliZaman = new SelectModel() { Name = "15 Dakika", Deger = 15, isSelected = true };
        public class SelectModel
        {
            public string Name { get; set; }
            public int Deger { get; set; }
            public bool isSelected { get; set; }
        }
        public string TextBoxYazi;
        public PrefetchVideoSidePanelWpfUserControl()
        {
            InitializeComponent();

            List<string> Saatler = new List<string>();
            List<string> Dakikalar = new List<string>();

            for (int i = 0; i < 24; i++)
            {

                string saat = i + "";
                if (i < 10)
                {
                    saat = "0" + i;
                }
                Saatler.Add(saat);
            }
            for (int i = 0; i < 60; i++)
            {
                string dakika = i + "";
                if (i < 10)
                {
                    dakika = "0" + i;
                }
                Dakikalar.Add(dakika);
            }
            int CurrentHour = DateTime.Now.AddDays(-1).Hour;
            int CurrentMunite = DateTime.Now.AddDays(-1).Minute;

            cmb_BasSaat.ItemsSource = Saatler;
            cmb_BasDakika.ItemsSource = Dakikalar;

            BasSaat = (CurrentHour + "");
            if (CurrentHour < 10)
                BasSaat = 0 + "" + CurrentHour;

            BasDakika = (CurrentMunite + "");
            if (CurrentMunite < 10)
                BasDakika = 0 + "" + CurrentMunite;

            cmb_BasSaat.SelectedItem = BasSaat;
            cmb_BasDakika.SelectedItem = BasDakika;


            cmb_BitSaat.ItemsSource = Saatler;
            cmb_BitDakika.ItemsSource = Dakikalar;
            DateTime bitTarih = DateTime.Now;
           


            dpcr_BasTarih.SelectedDate = DateTime.Now.AddDays(-1);
            dpcr_BitTarih.SelectedDate = DateTime.Now;


            SetUpApplicationEventListeners();
        }

        private void btn_KameraSec_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ItemPickerForm picker = new ItemPickerForm();

            picker.KindFilter = Kind.Camera;
            picker.Init(Configuration.Instance.GetItems());

            if (picker.ShowDialog() == DialogResult.OK)
            {
                //selectedItem = picker.SelectedItem;
                //lbl_SeciliKamera.Content = selectedItem.Name;


            }
        }

        private void btn_Getir_Click(object sender, System.Windows.RoutedEventArgs e)
        {
 

            //if (lstBox_Cameras.Items.Count != 0)
            //{
            //    TaskList = new List<TaskListModel>();
            //    ToplamIslemSayisi = 0;
            //    pgrb_Surec.Value = 0;
            //    formEnableIslem(false);
            //    rctxbx_Surec.Document.Blocks.Clear();
            //    TextBoxYazi += "Ýþlem Baslatýldý.\n";
            //    rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run("Ýþlem Baslatýldý.")));
            //    DateControl();

            //    rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run("Baþlangýç Tarihi :" + BasTarih.ToString("dd MMM yyyy HH:mm"))));
            //    rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run("Bitiþ Tarihi :" + BitTarih.ToString("dd MMM yyyy HH:mm"))));
            //    TextBoxYazi += "Baþlangýç Tarihi :" + BasTarih.ToString("dd MMM yyyy HH:mm") + "\n";
            //    TextBoxYazi += "Bitiþ Tarihi :" + BitTarih.ToString("dd MMM yyyy HH:mm") + "\n";

               
            //    rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run("Zamanlar bölündü.")));
            //    TextBoxYazi += "Zamanlar bölündü.\n";
             
            //    int Sayac = 0;
            //       pgrb_Surec.Value = Sayac;

                 
                
            //    rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run("Video istekleri baþlatýldý.")));
            //    TextBoxYazi += "Video istekleri baþlatýldý.\n";
                

           
                 
               
            //    PrefetchVideoBackgroundPlugin.TheInstance.BasTarih = BasTarih;
            //    PrefetchVideoBackgroundPlugin.TheInstance.BitTarih = BitTarih;
            //    PrefetchVideoBackgroundPlugin.TheInstance.BasSaat = BasSaat;
                
            //    PrefetchVideoBackgroundPlugin.TheInstance.ZamanTurID = ZamanTurID;
            //    PrefetchVideoBackgroundPlugin.TheInstance.SeciliZaman = SeciliZaman;
            //    PrefetchVideoBackgroundPlugin.TheInstance.TextBoxYazi = TextBoxYazi;

                
            //    //rvsrc.Close();
            //    //formEnableIslem(true);

            //}

            //else
            //{
              
            //        System.Windows.MessageBox.Show("Lutfen kamera seciniz.");
            //}
        }
      
        public void FormElementControl()
        {

            Action a = () =>
            {
                
                 //pgrb_Surec.Value = Yuzdelik;
               
                dpcr_BasTarih.SelectedDate = BasTarih;
                dpcr_BitTarih.SelectedDate = BitTarih;
                cmb_BasSaat.SelectedItem = BasSaat;
                cmb_BasDakika.SelectedItem = BasDakika;
                

              
                //if (Yuzdelik == 100)
                //{
                //    string text = "Toplam iþlem sayýsý:" + ToplamIslemSayisi + " \n  Ýþlemler gerçekleþtirildi. Video istekleri baþarý ile tamamlandý.";
                //    rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run(text)));
                //    formEnableIslem(true);
                   
                //}
                //else
                //{
                //    rctxbx_Surec.Document.Blocks.Clear();
                //    rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run(TextBoxYazi)));
                //    formEnableIslem(false);
                //}

            };
            Dispatcher.Invoke(a);




        }
     
          public void formEnableIslem(bool sonuc)
        {
            btn_Getir.IsEnabled = sonuc;
            dpcr_BitTarih.IsEnabled = sonuc;
            dpcr_BasTarih.IsEnabled = sonuc;

            rctxbx_Surec.IsEnabled = sonuc;
            btn_KameraSec.IsEnabled = sonuc;
            cmb_BasSaat.IsEnabled = sonuc;
            cmb_BasDakika.IsEnabled = sonuc;
            cmb_BitSaat.IsEnabled = sonuc;
            cmb_BitDakika.IsEnabled = sonuc;
            txb_ParcaSayi.IsEnabled = sonuc;
            tbx_FrameSayi.IsEnabled = sonuc;
        }

        private void cmb_BasSaat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateControl();
        }

        private void cmb_BasDakika_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateControl();
        }

   
     
        public void DateControl()
        {


            if (dpcr_BasTarih.SelectedDate != null)
            {
                string trh = (dpcr_BasTarih.SelectedDate.Value.Day + "." + dpcr_BasTarih.SelectedDate.Value.Month + "." + dpcr_BasTarih.SelectedDate.Value.Year + " " + cmb_BasSaat.SelectedItem.ToString() + ":" + cmb_BasDakika.SelectedItem.ToString());
                BasTarih = Convert.ToDateTime(trh);
            }

            if (dpcr_BitTarih.SelectedDate != null)
            {

                string trh = (dpcr_BitTarih.SelectedDate.Value.Day + "." + dpcr_BitTarih.SelectedDate.Value.Month + "." + dpcr_BitTarih.SelectedDate.Value.Year + " " + cmb_BitSaat.SelectedItem.ToString() + ":" + cmb_BitDakika.SelectedItem.ToString());
                BitTarih = Convert.ToDateTime(trh);
            }
            if (BasTarih >= BitTarih)
            {
                DateTime trh = BitTarih.AddMinutes(-10);
                dpcr_BasTarih.SelectedDate = trh.Date;

                string Saat = (trh.Hour + "");
                if (trh.Hour < 10)
                    Saat = 0 + "" + trh.Hour;

                string Dakika = (trh.Minute + "");
                if (trh.Minute < 10)
                    Dakika = 0 + "" + trh.Minute;


                cmb_BasSaat.SelectedItem = Saat;
                cmb_BasDakika.SelectedItem = Dakika;
            }

        }
        private void dpcr_BasTarih_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateControl();

        }

        
        private void SetUpApplicationEventListeners()
        {

            dpcr_BasTarih.SelectedDateChanged += dpcr_BasTarih_SelectedDateChanged;

       

            cmb_BasSaat.SelectionChanged += cmb_BasSaat_SelectionChanged;
            cmb_BasDakika.SelectionChanged += cmb_BasDakika_SelectionChanged;
         

        }
        public override void Init()
        {
            //PrefetchVideoBackgroundPlugin.UserControl2 = this;
        }

        public override void Close()
        {
            //PrefetchVideoBackgroundPlugin.UserControl2 = null;
        }

     
        
    }
}
