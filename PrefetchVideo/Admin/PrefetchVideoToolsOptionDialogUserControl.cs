using VideoOS.Platform.Admin;

namespace PrefetchVideo.Admin
{
    public partial class PrefetchVideoToolsOptionDialogUserControl : ToolsOptionsDialogUserControl
    {
        public PrefetchVideoToolsOptionDialogUserControl()
        {
            InitializeComponent();
        }

        public override void Init()
        {
        }

        public override void Close()
        {
        }

        public string MyPropValue
        {
            set { textBoxPropValue.Text = value ?? ""; }
            get { return textBoxPropValue.Text; }
        }
    }
}
