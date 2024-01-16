 
using PrefetchVideo.Background;
using PrefetchVideo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Threading;
using VideoOS.Platform;
using VideoOS.Platform.Client;
using VideoOS.Platform.Data;
using VideoOS.Platform.UI;

namespace PrefetchVideo.Client
{
    /// <summary>
    /// Interaction logic for PrefetchVideoWorkSpaceViewItemWpfUserControl.xaml
    /// </summary>
    public partial class PrefetchVideoWorkSpaceViewItemWpfUserControl : ViewItemWpfUserControl
    {
        public Item selectedItem;
        public List<Item> camList=new List<Item>();

        public DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public DateTime BasTarih = DateTime.Now.AddDays(-1);
        public DateTime BitTarih = DateTime.Now;
        public int ToplamIslemSayisi = 0;
      
        public string BasSaat = DateTime.Now.ToString("HH");
        public string BasDakika = DateTime.Now.ToString("mm");

        public int ZamanTurID = 15;

        public SelectModel SeciliZaman = new SelectModel() { Name = "15 Dakika", Deger = 15, isSelected = true };

        public string TextBoxYazi;

        public int Yuzdelik = 0;

        public bool isIslemYapiliyor = false;

        public List<ServerInfoModel> ServerList = new List<ServerInfoModel>();

        public string ServiceUrlBase = "http://192.168.99.35/init_recall.php?";
 

        public List<SelectModel> ZamanModel = new List<SelectModel>();
        public PrefetchVideoWorkSpaceViewItemWpfUserControl()
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


            cmb_BasSaat.ItemsSource = Saatler;
            cmb_BasDakika.ItemsSource = Dakikalar;

            cmb_BasSaat.SelectedItem = DateTime.Now.ToString("HH");
            cmb_BasDakika.SelectedItem = DateTime.Now.ToString("mm");

            cmb_Zaman.SelectedValuePath = "Deger";
            cmb_Zaman.DisplayMemberPath = "Name";
       

            foreach (var item in camList)
            {
                lstBox_Cameras.Items.Add(item);
            }
            ZamanComboBoxAyarla();
            //dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            //dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dpcr_BasTarih.SelectedDate = DateTime.Now.AddDays(-1);
            
            FormDuzenle();
            ServerListModelDoldur();
            SetUpApplicationEventListeners();



        }
        public void ServerListModelDoldur()
        {
            List<ServerInfoModel> mdl = new List<ServerInfoModel>() {
                  new ServerInfoModel(){ServerAdi="RCD1",   ServerIPSonHane="1",ServerDosyaID="4E449B89-4D0A-48D8-8FD3-A97238D25957" },
                  new ServerInfoModel(){ServerAdi="RCD2",   ServerIPSonHane="2",ServerDosyaID="4F476FAF-9D19-41CB-9F44-32891D89B2CD" },
                  new ServerInfoModel(){ServerAdi="RCD3",   ServerIPSonHane="3",ServerDosyaID="6F1932E9-913A-446A-8C36-C93A32FA4AAC" },
                  new ServerInfoModel(){ServerAdi="RCD4",   ServerIPSonHane="4",ServerDosyaID="4E711030-D3C1-4439-B0FC-99D3C8568D78" },
                  new ServerInfoModel(){ServerAdi="RCD5",   ServerIPSonHane="5",ServerDosyaID="5F75CEF7-36A1-4631-9603-323DA1C23FAC" },
                  new ServerInfoModel(){ServerAdi="RCD6",   ServerIPSonHane="6",ServerDosyaID="550A82AF-1AC4-4891-A40A-4EB35FE90626" },
                  new ServerInfoModel(){ServerAdi="RCD7",   ServerIPSonHane="7",ServerDosyaID="011B9D01-62BD-4809-A013-A0FDDB2E19C2" },
                  new ServerInfoModel(){ServerAdi="RCD8",   ServerIPSonHane="8",ServerDosyaID="FD9AA02C-6549-4623-97BF-E4D0B275AE07" },
                  new ServerInfoModel(){ServerAdi="RCD9",   ServerIPSonHane="9",ServerDosyaID="D2100953-6295-4D5E-9F53-C6918FF5B3DD" },
                  new ServerInfoModel(){ServerAdi="RCD10",  ServerIPSonHane="10",ServerDosyaID="DF2AE3EC-BA3C-45FE-9886-DF6CC91A031A" },
                  new ServerInfoModel(){ServerAdi="RCD11",  ServerIPSonHane="11",ServerDosyaID="999D9F06-7F55-469C-A44D-4025369F9AE4" },
                  new ServerInfoModel(){ServerAdi="RCD12",  ServerIPSonHane="12",ServerDosyaID="9271523D-2F5B-4987-AF53-8EF53D8EFE98" },
                  new ServerInfoModel(){ServerAdi="RCD13",  ServerIPSonHane="13",ServerDosyaID="D0C92CD9-8BCF-4FBD-B73E-5E1B976C653E" },
                  new ServerInfoModel(){ServerAdi="RCD14",  ServerIPSonHane="14",ServerDosyaID="861BD636-9B43-41FA-9D1B-AB34859C4298" },

                  new ServerInfoModel(){ServerAdi="solos",  ServerIPSonHane="111",ServerDosyaID="C941FA00-7DC9-4699-9A62-680CB9261FFE" },
                  new ServerInfoModel(){ServerAdi="mls",    ServerIPSonHane="mls",ServerDosyaID="C941FA00-7DC9-4699-9A62-680CB9261FFE" },
                  //new ServerInfoModel(){ServerAdi="mls",   ServerIPSonHane="mls",ServerDosyaID="BD95D9B7-1CEE-4195-8AD4-3F4B8280F500" },
                
           
            };
            //cmb_Zaman.DisplayMember = "Name";
            //cmb_Zaman.ValueMember = "Deger";
            ServerList = mdl;
       
        }
        public void ZamanComboBoxAyarla()
        {
            List<SelectModel> mdl = new List<SelectModel>() {
            new SelectModel(){Name="1 Dakika",Deger=1 },
            new SelectModel(){Name="2 Dakika",Deger=2 },
            new SelectModel(){Name="3 Dakika",Deger=3 },
            new SelectModel(){Name="5 Dakika",Deger=5 },
            new SelectModel(){Name="10 Dakika",Deger=10 },
            new SelectModel(){Name="15 Dakika",Deger=15,isSelected=true },
            new SelectModel(){Name="20 Dakika",Deger=20 },
            new SelectModel(){Name="30 Dakika",Deger=30 },

            new SelectModel(){Name="1 Saat",Deger=60 },
            new SelectModel(){Name="2 Saat",Deger=120 },
            new SelectModel(){Name="5 Saat",Deger=300 },
            new SelectModel(){Name="10 Saat",Deger=600 },
            new SelectModel(){Name="15 Saat",Deger=900 },
            new SelectModel(){Name="1 Gün",Deger=1440 },

            };
            //cmb_Zaman.DisplayMember = "Name";
            //cmb_Zaman.ValueMember = "Deger";
            foreach (var item in mdl)
            {
                cmb_Zaman.Items.Add(item);
            }
            cmb_Zaman.SelectedItem = mdl.FirstOrDefault(d => d.isSelected);

            ZamanModel = mdl;


        }
        public override void Init()
        {


            PrefetchVideoBackgroundPlugin.UserControl = this;
             
            //MessageBox.Show(PrefetchVideoBackgroundPlugin.TheInstance.Control1Ayarla());
        }
        public void FormDuzenle()
        {
            if (PrefetchVideoBackgroundPlugin.TheInstance != null)
            { 
                 camList= PrefetchVideoBackgroundPlugin.TheInstance.CamList;
                 ZamanTurID= PrefetchVideoBackgroundPlugin.TheInstance.ZamanTurID;

                dpcr_BasTarih.SelectedDate = PrefetchVideoBackgroundPlugin.TheInstance.BasTarih;


                cmb_BasSaat.SelectedItem = PrefetchVideoBackgroundPlugin.TheInstance.BasSaat;

                cmb_BasDakika.SelectedItem = PrefetchVideoBackgroundPlugin.TheInstance.BasDakika;

                cmb_Zaman.SelectedItem = ZamanModel.FirstOrDefault(d => d.Deger == ZamanTurID);
                BasSaat = PrefetchVideoBackgroundPlugin.TheInstance.BasSaat;
                BasDakika = PrefetchVideoBackgroundPlugin.TheInstance.BasDakika;

                isIslemYapiliyor = PrefetchVideoBackgroundPlugin.TheInstance.isIslemYapiliyor;
                lstBox_Cameras.Items.Clear();
                foreach (var item in camList)
                {
                    lstBox_Cameras.Items.Add(item);
                }
               

                TextBoxYazi += PrefetchVideoBackgroundPlugin.TheInstance.TextBoxYazi;
               
                   rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run(TextBoxYazi)));
                formEnableIslem(!isIslemYapiliyor);
            }



        }
        public override void Close()
        {
            //PrefetchVideoBackgroundPlugin.UserControl = null;
        }
    
   
        /// <summary>
        /// Do not show the sliding toolbar!
        /// </summary>
        public override bool ShowToolbar
        {
            get { return false; }
        }

        private void ViewItemWpfUserControl_ClickEvent(object sender, EventArgs e)
        {
            FireClickEvent();
        }

        private void ViewItemWpfUserControl_DoubleClickEvent(object sender, EventArgs e)
        {
            FireDoubleClickEvent();
        }

        private void btn_KameraSec_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ItemPickerForm picker = new ItemPickerForm();

            picker.KindFilter = Kind.Camera;
            picker.Init(Configuration.Instance.GetItems());

            if (picker.ShowDialog() == DialogResult.OK)
            {
                selectedItem = picker.SelectedItem;
                camList.Add(selectedItem);
                PrefetchVideoBackgroundPlugin.TheInstance.CamList = camList;
                lstBox_Cameras.Items.Add(selectedItem);

            }
        }

       
        private void btn_Getir_Click(object sender, System.Windows.RoutedEventArgs e)
        { 
            if (lstBox_Cameras.Items.Count!=0)
            {
              
                TextBoxYazi = "";
                 
                ToplamIslemSayisi = 0;
                
                formEnableIslem(false);
                rctxbx_Surec.Document.Blocks.Clear();
                TextBoxYazi += "Ýþlem Baslatýldý. Tarih : "+DateTime.Now.ToString("dd.MM.yyyy HH:mm")+"\n";
                
                rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run("Ýþlem Baslatýldý. Tarih : " + DateTime.Now.ToString("dd.MM.yyyy HH:mm") )));
                DateControl();

                rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run("Baþlangýç Tarihi :" + BasTarih.ToString("dd MMM yyyy HH:mm"))));
                rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run("Bitiþ Tarihi :" + BitTarih.ToString("dd MMM yyyy HH:mm"))));
                TextBoxYazi += "Baþlangýç Tarihi :" + BasTarih.ToString("dd MMM yyyy HH:mm") + "\n";
                TextBoxYazi += "Bitiþ Tarihi :" + BitTarih.ToString("dd MMM yyyy HH:mm") + "\n";

                TextBoxYazi += "Seçilen Süre :" + ZamanModel.FirstOrDefault(d => d.Deger == ZamanTurID).Name+"\n";
                 
                rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run("Video istekleri baþlatýldý.")));
                TextBoxYazi += "Video istekleri baþlatýldý.\n";

                string camNames = "";
           
                bool isFirst = false;
          
                List<ServerInfoModel> SvcmdList = new List<ServerInfoModel>();
                foreach (Item kamera in lstBox_Cameras.Items)
                {
                    if (isFirst)
                    {
                        camNames += ",";
                        
                    }
                    camNames += kamera.Name;

                    ServerInfoModel md = new ServerInfoModel();

                    md.CamId = kamera.FQID.ObjectId.ToString();

                    md.ServerIPSonHane = kamera.FQID.ServerId.Uri.Host.ToString().Replace("/","").Split('.').LastOrDefault();

                    md.ServerDosyaID = ServerList.FirstOrDefault(d => d.ServerIPSonHane == md.ServerIPSonHane).ServerDosyaID;
                    SvcmdList.Add(md);
                    isFirst = true;
                }


                List<ServerServiceModel> svcModelList = SvcmdList.GroupBy(d => d.ServerDosyaID).Select(f => new ServerServiceModel { ServerDosyaId = f.Key, CamIds = f.Select(l => l.CamId).ToList() }).ToList();

                string serverIds = "";
                bool isFirstse = false;
                foreach (ServerServiceModel item in svcModelList)
                {
                    if (isFirstse)
                    {
                        serverIds += ",";
                    }
                    serverIds += item.ServerDosyaId + "|";

                    serverIds += string.Join(":", item.CamIds);
                    isFirstse = true;
                }

                string svrv = string.Join(",", SvcmdList.Select(d => d.ServerIPSonHane));
                rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run("Seçilen serverlar :" + svrv)));

                TextBoxYazi += "Seçilen Kameralar :" + camNames+"\n";
              
                rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run("Seçilen Kameralar :" + camNames )));
                string sonuc = "";
                string ServiceUrl = "";
               

                

                long bastmstmp = ((DateTimeOffset)BasTarih.ToUniversalTime()).ToUnixTimeSeconds();
                long bittmstmp = ((DateTimeOffset)BitTarih.ToUniversalTime()).ToUnixTimeSeconds();
                   
                ServiceUrl = ServiceUrlBase + "server_ids="+ serverIds + "&start_time=" + bastmstmp + "&stop_time=" + bittmstmp;
             
                TextBoxYazi += "Baþlangýç Tarihi timestamp:" + bastmstmp + " \n";
                TextBoxYazi += "Bitiþ Tarihi timestamp:" + bittmstmp + " \n";
              
                bool isrecallinit = true;
                //TextBoxYazi += "Servis istek URL :" + ServiceUrl + " \n";
                //rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run("Servis istek URL :" + ServiceUrl)));
                bool islemyapma = false;
               
                while (isrecallinit)
                {
                    sonuc = ServisIstekYap(ServiceUrl);
                    string rtn = "{\"code\":\"1\"";
                    string rtn2 = "{\"code\":\"13\"";
                    if (sonuc.Contains(rtn))
                    {
                        isrecallinit = false;
                    }
                    else if (sonuc.Contains(rtn2))
                    {
                        formEnableIslem(true);
                        islemyapma = true;
                        isrecallinit = false;
                    }
                    else if (sonuc.Contains("sunucu baðlantý hatasý"))
                    {
                        formEnableIslem(true);
                        islemyapma = true;
                        isrecallinit = false;
                    }
                    TextBoxYazi += "Servis cevap :" + sonuc + " \n";
                    rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run("Servis cevap :" + sonuc)));
                    
                }
                rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run("Baþlangýç Tarihi timestamp:" + bastmstmp + " \n Bitiþ Tarihi timestamp:" + bittmstmp)));
                isIslemYapiliyor = true;
                if (!islemyapma)
                {
                    timer_Kur();


                    PrefetchVideoBackgroundPlugin.TheInstance.ServerList = ServerList;
                    PrefetchVideoBackgroundPlugin.TheInstance.BasTarih = BasTarih;

                    PrefetchVideoBackgroundPlugin.TheInstance.BitTarih = BitTarih;
                    PrefetchVideoBackgroundPlugin.TheInstance.isIslemYapiliyor = isIslemYapiliyor;

                    PrefetchVideoBackgroundPlugin.TheInstance.BasSaat = BasSaat;
                    PrefetchVideoBackgroundPlugin.TheInstance.BasDakika = BasDakika;

                    PrefetchVideoBackgroundPlugin.TheInstance.TextBoxYazi = TextBoxYazi;
                    PrefetchVideoBackgroundPlugin.TheInstance.ZamanTurID = ZamanTurID;
                    PrefetchVideoBackgroundPlugin.TheInstance.SeciliZaman = SeciliZaman;

                    PrefetchVideoBackgroundPlugin.TheInstance.CamList = camList;
                    PrefetchVideoBackgroundPlugin.TheInstance.StartJob();
                }
             


           
            }
            else
            {
                MessageBox.Show("Kamera seçiniz.");
            }
           
        }

          private void timer_Kur()
        {
          
            dispatcherTimer.Start();
        }

        //private void dispatcherTimer_Tick(object sender, EventArgs e)
        //{
        //    string sonuc=  ServisIstekYap(ServiceRecallUrlBase);
        //    TextBoxYazi += "Servis cevap:" + sonuc;
           
        //    rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run("Servis cevap:" + sonuc)));

        //    PrefetchVideoBackgroundPlugin.TheInstance.TextBoxYazi = TextBoxYazi;


        //    if (sonuc.Contains("3"))
        //    {
        //        dispatcherTimer.Stop();
        //        formEnableIslem(true);
        //    }
              


        //}
        public string ServisIstekYap(string url)
        {
            var request = WebRequest.Create(url);
            request.Timeout = 7000;

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if ((response != null) && (response.StatusCode == HttpStatusCode.OK))
                    {
                        string xmlOutput;
                        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                            xmlOutput = sr.ReadToEnd();

                        return xmlOutput;
                    }
                    else
                    {
                        return "sunucu baðlantý hatasý";
                    }
                }
            }
            catch (Exception ex)
            {

                return "sunucu baðlantý hatasý :"+ ex.Message;
            }

        }

        public void FormElementControl(bool isIslemTamam=false,string servisCevap="")
        {
             
            Action a = () =>
            { 
                this.dpcr_BasTarih.SelectedDate = BasTarih;

                this.cmb_BasSaat.SelectedItem = BasSaat;
                this.cmb_BasDakika.SelectedItem = BasDakika;
                this.cmb_Zaman.SelectedItem = ZamanModel.FirstOrDefault(d=>d.Deger==ZamanTurID);
                this.lstBox_Cameras.Items.Clear();

                this.rctxbx_Surec.ScrollToEnd();
                //this.lstBox_Cameras.ItemsSource = camList;
                foreach (var item in camList)
                {
                    this.lstBox_Cameras.Items.Add(item);
                }
                if (isIslemTamam)
                {
                    string text = "Video istekleri baþarý ile tamamlandý. Bitiþ Tarihi :"+DateTime.Now.ToString("dd.MM.yyyy HH:mm")+"\n";
                    TextBoxYazi += servisCevap+"\n"+ text;
                    //PrefetchVideoBackgroundPlugin.TheInstance.TextBoxYazi = TextBoxYazi;
                    this.rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run(text)));
                    this.formEnableIslem(true);
                    
                }
                else
                {
                    this.rctxbx_Surec.Document.Blocks.Clear();
                    this.rctxbx_Surec.Document.Blocks.Add(new Paragraph(new Run(TextBoxYazi)));

                    this.formEnableIslem(false);
                }

            };

            Dispatcher.Invoke(a);




        }
       
        public void formEnableIslem(bool sonuc)
        {
            btn_Getir.IsEnabled = sonuc;

            dpcr_BasTarih.IsEnabled = sonuc;
            this.lstBox_Cameras.IsEnabled = sonuc;
            rctxbx_Surec.IsEnabled = sonuc;
            btn_KameraSec.IsEnabled = sonuc;
            cmb_BasSaat.IsEnabled = sonuc;
            cmb_BasDakika.IsEnabled = sonuc;
            cmb_Zaman.IsEnabled = sonuc;
        }

        private void dpcr_BasTarih_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateControl();

        }

      

        public void DateControl()
        {
            //Action x = () =>
            //{
            //    MessageBox.Show(BasSaat + " " + BasDakika);

            //};
            //Dispatcher.Invoke(x);
            //Action y = () =>
            //{
            //    MessageBox.Show(BitSaat + " " + BitDakika);

            //};
            //Dispatcher.Invoke(y);
            if (dpcr_BasTarih.SelectedDate != null)
            {
                string trh = dpcr_BasTarih.SelectedDate.Value.Day + "." + dpcr_BasTarih.SelectedDate.Value.Month + "." + dpcr_BasTarih.SelectedDate.Value.Year + " " + BasSaat + ":" + BasDakika;
                BasTarih = Convert.ToDateTime(trh);
            }

            BitTarih = BasTarih.AddMinutes(ZamanTurID);

           

        }

        private void cmb_BasSaat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BasSaat = (sender as System.Windows.Controls.ComboBox).SelectedItem as string;
            PrefetchVideoBackgroundPlugin.TheInstance.BasSaat = BasSaat;
            //Action a = () =>
            //{
            //    MessageBox.Show(BasSaat);

            //};

            //Dispatcher.Invoke(a);
            DateControl();
        }

        private void cmb_BasDakika_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BasDakika = (sender as System.Windows.Controls.ComboBox).SelectedItem as string;
            PrefetchVideoBackgroundPlugin.TheInstance.BasDakika = BasDakika;
            //Action a = () =>
            //{
            //    MessageBox.Show(BasDakika);

            //};
            //Dispatcher.Invoke(a);
            DateControl();
        }
         
        private void cbm_ZamanSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectModel SecilisZaman = (sender as System.Windows.Controls.ComboBox).SelectedItem as SelectModel;


            ZamanTurID = SecilisZaman.Deger;
            SeciliZaman = SecilisZaman;
            //PrefetchVideoBackgroundPlugin.TheInstance.BitDakika = BitDakika;
            DateControl();
        }
        private void SetUpApplicationEventListeners()
        {

            dpcr_BasTarih.SelectedDateChanged += dpcr_BasTarih_SelectedDateChanged;



            cmb_BasSaat.SelectionChanged += cmb_BasSaat_SelectionChanged;
            cmb_BasDakika.SelectionChanged += cmb_BasDakika_SelectionChanged;

            cmb_Zaman.SelectionChanged += cbm_ZamanSelectionChanged;

        }


        private void lstBox_Cameras_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lstBox_Cameras.SelectedItem != null)
            {
                lstBox_Cameras.Items.Remove(lstBox_Cameras.SelectedItem);
                var cc = new List<Item>();
                foreach (Item item in lstBox_Cameras.Items)
                {
                    cc.Add(item);
                }
                camList = cc;
                PrefetchVideoBackgroundPlugin.TheInstance.CamList = cc;
            }
        }

        public class SelectModel
        {
            public string Name { get; set; }
            public int Deger { get; set; }
            public bool isSelected { get; set; }
        }

        public class ServiceMessageModel
        {
            public string code { get; set; }

            public string message { get; set; }

        }
        public class ServerInfoModel
        {
            public string ServerIPSonHane { get; set; }
            public string ServerAdi { get; set; }
            public string ServerDosyaID { get; set; }
            public string CamId { get; set; }
        }

        public class ServerServiceModel
        {
            public string ServerDosyaId { get; set; }

            public List<string> CamIds { get; set; }

        }
    }


}
