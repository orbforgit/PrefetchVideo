 
using PrefetchVideo.Client;
using PrefetchVideo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Xml;
using VideoOS.Platform;
using VideoOS.Platform.Background;
using VideoOS.Platform.Client;
using VideoOS.Platform.Data;
using static PrefetchVideo.Client.PrefetchVideoWorkSpaceViewItemWpfUserControl;

namespace PrefetchVideo.Background
{
    /// <summary>
    /// A background plugin will be started during application start and be running until the user logs off or application terminates.<br/>
    /// The Environment will call the methods Init() and Close() when the user login and logout, 
    /// so the background task can flush any cached information.<br/>
    /// The base class implementation of the LoadProperties can get a set of configuration, 
    /// e.g. the configuration saved by the Options Dialog in the Smart Client or a configuration set saved in one of the administrators.  
    /// Identification of which configuration to get is done via the GUID.<br/>
    /// The SaveProperties method can be used if updating of configuration is relevant.
    /// <br/>
    /// The configuration is stored on the server the application is logged into, and should be refreshed when the ApplicationLoggedOn method is called.
    /// Configuration can be user private or shared with all users.<br/>
    /// <br/>
    /// This plugin could be listening to the Message with MessageId == Server.ConfigurationChangedIndication to when when to reload its configuration.  
    /// This event is send by the environment within 60 second after the administrator has changed the configuration.
    /// </summary>
    public class PrefetchVideoBackgroundPlugin : BackgroundPlugin
    {

      public  DispatcherTimer dispatcherTimer = new  DispatcherTimer();


        internal static PrefetchVideoBackgroundPlugin TheInstance;
        internal static  PrefetchVideoWorkSpaceViewItemWpfUserControl UserControl { get; set; }
        //internal static  PrefetchVideoSidePanelWpfUserControl UserControl2 { get; set; }
        public List<Item> CamList = new List<Item>();
       
        public DateTime BasTarih = DateTime.Now.AddDays(-1);
        public DateTime BitTarih = DateTime.Now;
        public bool isIslemYapiliyor = false;
        public SelectModel SeciliZaman = new SelectModel() { Name = "15 Dakika", Deger = 15, isSelected = true };
        public string ServiceRecallUrlBase = "http://192.168.99.35/check_recall.php?";
        public List<ServerInfoModel> ServerList = new List<ServerInfoModel>();
        public int FrameSayisi = 10;

        public int ParcaSayisi = 20;

        public string BasSaat = DateTime.Now.ToString("HH");
        public string BasDakika = DateTime.Now.ToString("mm");

        public int ZamanTurID = 15;
 

        public string TextBoxYazi;


        public int Yuzdelik = 0;
        /// <summary>
        /// Gets the unique id identifying this plugin component
        /// </summary>
        public override Guid Id
        {
            get { return PrefetchVideoDefinition.PrefetchVideoBackgroundPlugin; }
        }

        /// <summary>
        /// The name of this background plugin
        /// </summary>
        public override String Name
        {
            get { return "Tape Prefecth (Orbisis) BackgroundPlugin"; }
        }
         
        public override void Init()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            TheInstance = this;
            

        }
        /// <summary>
        /// Called by the Environment when the user has logged in.
        /// </summary>
     
     
       
        public void StartJob()
        {
          
            dispatcherTimer.Start();
            //this._thread = new Thread(new ThreadStart(this.BackgroundTaskIslem));
            //this._thread.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            string sonuc =ServisIstekYap(ServiceRecallUrlBase);
            TextBoxYazi += "Service Sonuc :" + sonuc+"\n";
            bool isIslemTamam = false;
            Control1Ayarla(isIslemTamam, sonuc);
            string rtn = "{\"code\":\"3\"";

            if (sonuc.Contains(rtn))
            {
                isIslemTamam = true;
                isIslemYapiliyor = false;
                TextBoxYazi += "Video istekleri tamamlandý :" + sonuc + "\n";

                Control1Ayarla(isIslemTamam, sonuc);
                dispatcherTimer.Stop();
            }

        }
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

                return ex.Message;
            }

        }
        public void Control1Ayarla(bool isIslemTamam=false,string servissonuc="")
        {

            UserControl.BasTarih = BasTarih;
            UserControl.BitTarih = BitTarih;

            UserControl.BasSaat = BasSaat;
            UserControl.BasDakika = BasDakika;

            UserControl.ZamanTurID = ZamanTurID;

            UserControl.isIslemYapiliyor = isIslemYapiliyor;
            UserControl.TextBoxYazi = TextBoxYazi;
            UserControl.camList = CamList;
            UserControl.ZamanTurID = ZamanTurID;
            UserControl.SeciliZaman = SeciliZaman;

            UserControl.FormElementControl(isIslemTamam, servissonuc);

            //return "Geldi";
        }
        //public void Control2Ayarla()
        //{
        //    UserControl2.TaskList = TaskList;
        //    UserControl2.ToplamIslemSayisi = ToplamIslemSayisi;
        //    UserControl2.FrameSayisi = FrameSayisi;
        //    UserControl2.ParcaSayisi = ParcaSayisi;

        //    UserControl2.BasTarih = BasTarih;
        //    UserControl2.BitTarih = BitTarih;

        //    UserControl2.BasSaat = BasSaat;
        //    UserControl2.BasDakika = BasDakika;

        //    UserControl2.BitSaat = BitSaat;
        //    UserControl2.BitDakika = BitDakika;
        //    UserControl2.selectedItem = selectedItem;
        //    UserControl2.TextBoxYazi = TextBoxYazi;
        //    UserControl2.FormElementControl();
        //}
        /// <summary>
        /// Called by the Environment when the user log's out.
        /// You should close all remote sessions and flush cache information, as the
        /// user might logon to another server next time.
        /// </summary>
        public override void Close()
        {
             
            TheInstance = null;

        }

        /// <summary>
        /// Define in what Environments the current background task should be started.
        /// </summary>
        public override List<EnvironmentType> TargetEnvironments
        {
            get { return new List<EnvironmentType>() { EnvironmentType.SmartClient }; } // Default will run in the Event Server
        }


       
    }
}
