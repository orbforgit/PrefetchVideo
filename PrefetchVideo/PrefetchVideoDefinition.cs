using PrefetchVideo.Admin;
using PrefetchVideo.Background;
using PrefetchVideo.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Admin;
using VideoOS.Platform.Background;
using VideoOS.Platform.Client;

namespace PrefetchVideo
{
    /// <summary>
    /// The PluginDefinition is the ‘entry’ point to any plugin.  
    /// This is the starting point for any plugin development and the class MUST be available for a plugin to be loaded.  
    /// Several PluginDefinitions are allowed to be available within one DLL.
    /// Here the references to all other plugin known objects and classes are defined.
    /// The class is an abstract class where all implemented methods and properties need to be declared with override.
    /// The class is constructed when the environment is loading the DLL.
    /// </summary>
    public class PrefetchVideoDefinition : PluginDefinition
    {
        private static System.Drawing.Image _treeNodeImage;
        private static System.Drawing.Image _topTreeNodeImage;

        internal static Guid PrefetchVideoPluginId = new Guid("80088f69-eb27-47f2-97d4-607f2eb1c999");
        internal static Guid PrefetchVideoKind = new Guid("a2789828-a045-47f1-a6d2-7bbb1ea62313");
        internal static Guid PrefetchVideoSidePanel = new Guid("c35aa673-b49b-4037-896f-486868b6c20c");
        internal static Guid PrefetchVideoViewItemPlugin = new Guid("e407da2f-50ae-4cae-aa88-1f01d9446785");
        internal static Guid PrefetchVideoSettingsPanel = new Guid("1f9932c4-38b5-4c21-a76c-04bbdf795624");
        internal static Guid PrefetchVideoBackgroundPlugin = new Guid("fef8bd82-9e9a-4529-8f9b-df4f180de58e");
        internal static Guid PrefetchVideoWorkSpacePluginId = new Guid("13e16668-daab-47aa-8a46-8054fc9a74cb");
        internal static Guid PrefetchVideoWorkSpaceViewItemPluginId = new Guid("adcdc77b-733a-4125-8584-1083c5dd7f58");
        internal static Guid PrefetchVideoTabPluginId = new Guid("822307eb-acdc-4703-99ce-fbd256d2869b");
        internal static Guid PrefetchVideoViewLayoutId = new Guid("88b1177e-0979-49bf-8c20-16cca2bd9710");
        // IMPORTANT! Due to shortcoming in Visual Studio template the below cannot be automatically replaced with proper unique GUIDs, so you will have to do it yourself
        internal static Guid PrefetchVideoWorkSpaceToolbarPluginId = new Guid("1ae5004c-884f-4959-9a07-ed3c27ed37b7");
        internal static Guid PrefetchVideoViewItemToolbarPluginId = new Guid("4a2dd82f-3a76-4464-8a5f-d19dc5df6303");
        internal static Guid PrefetchVideoToolsOptionDialogPluginId = new Guid("3820418a-f7a4-4456-8c29-32692d359da0");

        #region Private fields

        private UserControl _treeNodeInofUserControl;

        //
        // Note that all the plugin are constructed during application start, and the constructors
        // should only contain code that references their own dll, e.g. resource load.

        private List<BackgroundPlugin> _backgroundPlugins = new List<BackgroundPlugin>();
        private Collection<SettingsPanelPlugin> _settingsPanelPlugins = new Collection<SettingsPanelPlugin>();
        private List<ViewItemPlugin> _viewItemPlugins = new List<ViewItemPlugin>();
        private List<ItemNode> _itemNodes = new List<ItemNode>();
        private List<SidePanelPlugin> _sidePanelPlugins = new List<SidePanelPlugin>();
        private List<String> _messageIdStrings = new List<string>();
        private List<SecurityAction> _securityActions = new List<SecurityAction>();
        private List<WorkSpacePlugin> _workSpacePlugins = new List<WorkSpacePlugin>();
        private List<TabPlugin> _tabPlugins = new List<TabPlugin>();
        private List<ViewItemToolbarPlugin> _viewItemToolbarPlugins = new List<ViewItemToolbarPlugin>();
        private List<WorkSpaceToolbarPlugin> _workSpaceToolbarPlugins = new List<WorkSpaceToolbarPlugin>();
        private List<ViewLayout> _viewLayouts = new List<ViewLayout> { new PrefetchVideoViewLayout() };
        private List<ToolsOptionsDialogPlugin> _toolsOptionsDialogPlugins = new List<ToolsOptionsDialogPlugin>();

        #endregion

        #region Initialization

        /// <summary>
        /// Load resources 
        /// </summary>
        static PrefetchVideoDefinition()
        {
            _treeNodeImage = Properties.Resources.DummyItem;
            _topTreeNodeImage = Properties.Resources.Server;
        }


        /// <summary>
        /// Get the icon for the plugin
        /// </summary>
        internal static Image TreeNodeImage
        {
            get { return _treeNodeImage; }
        }

        #endregion

        /// <summary>
        /// This method is called when the environment is up and running.
        /// Registration of Messages via RegisterReceiver can be done at this point.
        /// </summary>
        public override void Init()
        {
            // Populate all relevant lists with your plugins etc.
            _itemNodes.Add(new ItemNode(PrefetchVideoKind, Guid.Empty,
                                         "Tape Prefecth (Orbisis)", _treeNodeImage,
                                         "Tape Prefecth (Orbisis) s", _treeNodeImage,
                                         Category.Text, true,
                                         ItemsAllowed.Many,
                                         new PrefetchVideoItemManager(PrefetchVideoKind),
                                         null
                                         ));
            if (EnvironmentManager.Instance.EnvironmentType == EnvironmentType.SmartClient)
            {
                _workSpacePlugins.Add(new PrefetchVideoWorkSpacePlugin());
                //_sidePanelPlugins.Add(new PrefetchVideoSidePanelPlugin());
                _viewItemPlugins.Add(new PrefetchVideoViewItemPlugin());
                _viewItemPlugins.Add(new PrefetchVideoWorkSpaceViewItemPlugin());
                _viewItemToolbarPlugins.Add(new PrefetchVideoViewItemToolbarPlugin());
                _workSpaceToolbarPlugins.Add(new PrefetchVideoWorkSpaceToolbarPlugin());
                _settingsPanelPlugins.Add(new PrefetchVideoSettingsPanelPlugin());
            }
            if (EnvironmentManager.Instance.EnvironmentType == EnvironmentType.Administration)
            {
                _tabPlugins.Add(new PrefetchVideoTabPlugin());
                _toolsOptionsDialogPlugins.Add(new PrefetchVideoToolsOptionDialogPlugin());
            }

            _backgroundPlugins.Add(new PrefetchVideoBackgroundPlugin());
        }

        /// <summary>
        /// The main application is about to be in an undetermined state, either logging off or exiting.
        /// You can release resources at this point, it should match what you acquired during Init, so additional call to Init() will work.
        /// </summary>
        public override void Close()
        {
            _itemNodes.Clear();
            _sidePanelPlugins.Clear();
            _viewItemPlugins.Clear();
            _settingsPanelPlugins.Clear();
            _backgroundPlugins.Clear();
            _workSpacePlugins.Clear();
            _tabPlugins.Clear();
            _viewItemToolbarPlugins.Clear();
            _workSpaceToolbarPlugins.Clear();
            _toolsOptionsDialogPlugins.Clear();
        }

        /// <summary>
        /// Return any new messages that this plugin can use in SendMessage or PostMessage,
        /// or has a Receiver set up to listen for.
        /// The suggested format is: "YourCompany.Area.MessageId"
        /// </summary>
        public override List<string> PluginDefinedMessageIds
        {
            get
            {
                return _messageIdStrings;
            }
        }

        /// <summary>
        /// If authorization is to be used, add the SecurityActions the entire plugin 
        /// would like to be available.  E.g. Application level authorization.
        /// </summary>
        public override List<SecurityAction> SecurityActions
        {
            get
            {
                return _securityActions;
            }
            set
            {
            }
        }

        #region Identification Properties

        /// <summary>
        /// Gets the unique id identifying this plugin component
        /// </summary>
        public override Guid Id
        {
            get
            {
                return PrefetchVideoPluginId;
            }
        }

        /// <summary>
        /// This Guid can be defined on several different IPluginDefinitions with the same value,
        /// and will result in a combination of this top level ProductNode for several plugins.
        /// Set to Guid.Empty if no sharing is enabled.
        /// </summary>
        public override Guid SharedNodeId
        {
            get
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Define name of top level Tree node - e.g. A product name
        /// </summary>
        public override string Name
        {
            get { return "Tape Prefecth (Orbisis)"; }
        }

        /// <summary>
        /// Your company name
        /// </summary>
        public override string Manufacturer
        {
            get
            {
                return "Orbisis Bilisim";
            }
        }

        /// <summary>
        /// Version of this plugin.
        /// </summary>
        public override string VersionString
        {
            get
            {
                return "1.0.0.0";
            }
        }

        /// <summary>
        /// Icon to be used on top level - e.g. a product or company logo
        /// </summary>
        public override System.Drawing.Image Icon
        {
            get { return _topTreeNodeImage; }
        }

        #endregion


        #region Administration properties

        /// <summary>
        /// A list of server side configuration items in the administrator
        /// </summary>
        public override List<ItemNode> ItemNodes
        {
            get { return _itemNodes; }
        }

        /// <summary>
        /// An extension plug-in running in the Administrator to add a tab for built-in devices and hardware.
        /// </summary>
        public override ICollection<TabPlugin> TabPlugins
        {
            get { return _tabPlugins; }
        }

        /// <summary>
        /// An extension plug-in running in the Administrator to add more tabs to the Tools-Options dialog.
        /// </summary>
        public override List<ToolsOptionsDialogPlugin> ToolsOptionsDialogPlugins
        {
            get { return _toolsOptionsDialogPlugins; }
        }

        /// <summary>
        /// A user control to display when the administrator clicks on the top TreeNode
        /// </summary>
        public override UserControl GenerateUserControl()
        {
            _treeNodeInofUserControl = new HelpPage();
            return _treeNodeInofUserControl;
        }

        /// <summary>
        /// This property can be set to true, to be able to display your own help UserControl on the entire panel.
        /// When this is false - a standard top and left side is added by the system.
        /// </summary>
        public override bool UserControlFillEntirePanel
        {
            get { return false; }
        }
        #endregion

        #region Client related methods and properties

        /// <summary>
        /// A list of Client side definitions for Smart Client
        /// </summary>
        public override List<ViewItemPlugin> ViewItemPlugins
        {
            get { return _viewItemPlugins; }
        }

        /// <summary>
        /// An extension plug-in running in the Smart Client to add more choices on the Settings panel.
        /// Supported from Smart Client 2017 R1. For older versions use OptionsDialogPlugins instead.
        /// </summary>
        public override Collection<SettingsPanelPlugin> SettingsPanelPlugins
        {
            get { return _settingsPanelPlugins; }
        }

        /// <summary> 
        /// An extension plugin to add to the side panel of the Smart Client.
        /// </summary>
        public override List<SidePanelPlugin> SidePanelPlugins
        {
            get { return _sidePanelPlugins; }
        }

        /// <summary>
        /// Return the workspace plugins
        /// </summary>
        public override List<WorkSpacePlugin> WorkSpacePlugins
        {
            get { return _workSpacePlugins; }
        }

        /// <summary> 
        /// An extension plug-in to add to the view item toolbar in the Smart Client.
        /// </summary>
        public override List<ViewItemToolbarPlugin> ViewItemToolbarPlugins
        {
            get { return _viewItemToolbarPlugins; }
        }

        /// <summary> 
        /// An extension plug-in to add to the work space toolbar in the Smart Client.
        /// </summary>
        public override List<WorkSpaceToolbarPlugin> WorkSpaceToolbarPlugins
        {
            get { return _workSpaceToolbarPlugins; }
        }

        /// <summary>
        /// An extension plug-in running in the Smart Client to provide extra view layouts.
        /// </summary>
        public override List<ViewLayout> ViewLayouts
        {
            get { return _viewLayouts; }
        }

        #endregion


        /// <summary>
        /// Create and returns the background task.
        /// </summary>
        public override List<BackgroundPlugin> BackgroundPlugins
        {
            get { return _backgroundPlugins; }
        }

    }
}
