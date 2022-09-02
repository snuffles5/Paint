using System;
using System.Windows.Forms;

/************** TODO List in general *********************
**  Serializable / Deserialize and implement it.
**  ---regions
**  comments where needed
**  ---Rhombus class 
**  
**  features:
**  ---freefigure
**  Background Color (canvas)
**
**
*********************************************************/


namespace OOPproject
{
    static class Program
    {
        /// <summary>
        ///  
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
