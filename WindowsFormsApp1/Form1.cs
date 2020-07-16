using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // For the LiveRegionChanged event to be effective later, the LiveLabel 
            // element needs to be in the Control view of the UIA tree, and for some 
            // versions of .NET Framework a Label that has no text set on it is not 
            // included in the UIA tree at all. Setting text on the label will result 
            // in the element being added to the tree as required, but if the event 
            // is raised immediately after doing that, the full required effect of 
            // setting the text has not yet occurred. The most straightforward way 
            // to account for that is to set some empty text on the Label before 
            // the string to be announced is set on it later.
            labelLiveStatus.Text = " ";
        }

        private void buttonShowLiveLabel_Click(object sender, EventArgs e)
        {
            labelLiveStatus.Text = "Status is just fine.";

            // If any UIA client app is running, (such as Narrator,) raise
            // an event to make the app aware of the change in status.
            if (NativeMethods.UiaClientsAreListening())
            {
                Debug.WriteLine("LiveLabel: Call UiaRaiseAutomationEvent");

                // Raise a LiveRegionChanged event from the UIA provider associated with the LiveLabel control.
                NativeMethods.UiaRaiseAutomationEvent(
                    labelLiveStatus, 
                    NativeMethods.UIA_LiveRegionChangedEventId);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
