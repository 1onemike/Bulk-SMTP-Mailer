using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;

namespace Atomic_Mail_Suite
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()

        {
            
            Application.Run(new Form1());

          
        }

    }

}