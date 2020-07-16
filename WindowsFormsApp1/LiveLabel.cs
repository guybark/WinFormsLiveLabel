using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Automation.Provider;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class LiveLabel : Label, IRawElementProviderSimple
    {
        // Override WndProc to provide our own IRawElementProviderSimple provider when queried by UIA.
        protected override void WndProc(ref Message m)
        {
            // Is UIA asking for a IRawElementProviderSimple provider?
            if ((m.Msg == NativeMethods.WM_GETOBJECT) && 
                (m.LParam == (IntPtr)NativeMethods.UiaRootObjectId))
            {
                // Return our custom implementation of IRawElementProviderSimple.
                m.Result = AutomationInteropProvider.ReturnRawElementProvider(
                    this.Handle,
                    m.WParam,
                    m.LParam,
                (IRawElementProviderSimple)this);

                Debug.WriteLine("LiveLabel: Return custom IRawElementProviderSimple");

                return;
            }

            base.WndProc(ref m);
        }

        // IRawElementProviderSimple implementation.
        ProviderOptions IRawElementProviderSimple.ProviderOptions
        {
            get
            {
                // Assume the UIA provider is always running in the server process.
                return ProviderOptions.ServerSideProvider | 
                       ProviderOptions.UseComThreading;
            }
        }

        IRawElementProviderSimple IRawElementProviderSimple.HostRawElementProvider
        {
            get
            {
                return AutomationInteropProvider.HostProviderFromHandle(this.Handle);
            }
        }

        public object GetPatternProvider(int patternId)
        {
            Debug.WriteLine("LiveLabel: GetPatternProvider " + patternId);

            // The WinForms Label control only supports the LegacyIAccessible pattern,
            // and this custom control gets that for free.
            return null;
        }

        public object GetPropertyValue(int propertyId)
        {
            Debug.WriteLine("LiveLabel: GetPropertyValue " + propertyId);

            // With the exception of the UIA_LiveSettingPropertyId handling below,
            // all properties returned here are done so in order to replicate the
            // UIA representation of the standard WinForms Label control.

            // Note that (with the exception of the LiveSetting property,) the only difference between
            // the UIA properties of the LiveLabel and the standard Label is the ProviderDescription.
            // The standard Label's property will include:
            // "Microsoft: MSAA Proxy (unmanaged:uiautomationcore.dll)",
            // whereas the LiveLabel's will include (something like):
            // "Unidentified Provider (managed:WinForms_LiveRegion.LiveLabel, WinForms_LiveRegion"

            switch (propertyId)
            {
                case NativeMethods.UIA_ControlTypePropertyId:
                {
                    return NativeMethods.UIA_TextControlTypeId;
                }
                case NativeMethods.UIA_AccessKeyPropertyId:
                {
                    // This assumes the control has no access key. If it does have an access key,
                    // look for an '&' in the control's text, and return a string of the form
                    // "Alt+<the access key character>". It's pretty unlikely that the control
                    // would have an access key, as if it did, the LiveRegion-related announcement
                    // might unexpectedly include "and" in it.
                    return "";
                }
                case NativeMethods.UIA_IsKeyboardFocusablePropertyId:
                {
                    return false;
                }
                case NativeMethods.UIA_IsPasswordPropertyId:
                {
                    return false;
                }
                case NativeMethods.UIA_IsOffscreenPropertyId:
                {
                    // Assume the control is always visible on the screen while it exists.
                    return false;
                }
                case NativeMethods.UIA_LiveSettingPropertyId:
                {
                    // Return whichever of Polite or Assertive is most appropriate for your scenario.
                    // Note that a value of zero, (for "Off"), could also be returned, but typically if
                    // an element is ever a LiveRegion, it's always either Assertive or Polite.
                    return NativeMethods.Assertive;
                }
                default:
                {
                    return null;
                }
            }
        }
    }

    public class NativeMethods
    {
        public const int WM_GETOBJECT = 0x003D;
        public const int UiaRootObjectId = -25;

        public const int UIA_LiveRegionChangedEventId = 20024;

        public const int UIA_ControlTypePropertyId = 30003;
        public const int UIA_AccessKeyPropertyId = 30007;
        public const int UIA_IsKeyboardFocusablePropertyId = 30009;
        public const int UIA_IsPasswordPropertyId = 30019;
        public const int UIA_IsOffscreenPropertyId = 30022;
        public const int UIA_LiveSettingPropertyId = 30135;

        public const int UIA_TextControlTypeId = 50020;

        public const int Polite = 1;
        public const int Assertive = 2;

        [DllImport("UIAutomationCore.dll")]
        public static extern int UiaRaiseAutomationEvent(
        IRawElementProviderSimple provider,
        int id);

        [DllImport("UIAutomationCore.dll")]
        public static extern bool UiaClientsAreListening();
    }
}
