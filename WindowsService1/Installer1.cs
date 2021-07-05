using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace WindowsService1
{
    [RunInstaller(true)]
    public partial class Installer1 : System.Configuration.Install.Installer
    {
        ServiceInstaller serviceInstaller = new ServiceInstaller();
        ServiceProcessInstaller serviceProcess = new ServiceProcessInstaller();
        public Installer1()
        {
            InitializeComponent();
            serviceProcess.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.ServiceName = "alarm хром";
            serviceInstaller.Description = "Fucking ";

            serviceInstaller.AfterInstall += ServiceInstaller_AfterInstall;
            serviceInstaller.AfterRollback += ServiceInstaller_AfterRollback;

            Installers.Add(serviceProcess);
            Installers.Add(serviceInstaller);
        }
        private void ServiceInstaller_AfterRollback(object sender, InstallEventArgs e)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(" работатю");
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private void ServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine(" не увидите");
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
