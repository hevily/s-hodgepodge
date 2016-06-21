using System.ComponentModel;
using System.Configuration.Install;

namespace ScheduledTask.Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}
