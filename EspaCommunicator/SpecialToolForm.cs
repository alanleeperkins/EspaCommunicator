using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Drawing;

namespace EspaCommunicator
{
    /// <summary>
    /// start this dialog as non-modal
    /// and the SpecialToolForm.Owner = <parentform>
    /// </summary>
    public class SpecialToolForm : Form
    {
        protected bool IsMouseInFormContent;
        protected bool IsMouseInFormFrame;
        protected bool IsFormVisited
        {
            get { return (IsMouseInFormContent || IsMouseInFormFrame); }
        }

        protected System.Windows.Forms.Timer CursorPositionTimer = null;

        [DllImport("user32")]
        protected static extern bool TrackMouseEvent(ref TRACKMOUSEEVENT lpEventTrack);

        protected struct TRACKMOUSEEVENT
        {
            public int cbSize;
            public uint dwFlags;
            public IntPtr hwndTrack;
            public uint dwHoverTime;
        }

        protected const int WM_MOUSEHOVER = 0x2a1;
        protected const int WM_MOUSELEAVE = 0x2a3;
        protected const int WM_MOUSEMOVE = 0x200;
        protected const uint TME_HOVER = 0x00000001;
        protected const uint TME_LEAVE = 0x00000002;
        protected const uint HOVER_DEFAULT = 0xFFFFFFFF;
        protected const int TME_NONCLIENT = 0x00000010;
        protected const int WM_LBUTTONUP = 0x202;
        protected const int WM_LBUTTONDOWN = 0x201;
        protected const int WM_NCMOUSEMOVE = 0x00A0;
        protected const int WM_NCMOUSEHOVER = 0x02A0;
        protected const int WM_NCMOUSELEAVE = 0x02A2;
        protected const int WM_CAPTURECHANGED = 0x0215;

        /// <summary>
        /// 
        /// </summary>
        public SpecialToolForm()
        {
            CursorPositionTimer = new System.Windows.Forms.Timer();
            CursorPositionTimer.Interval = 200;
            CursorPositionTimer.Tick += CursorPositionTimer_Tick;
            CursorPositionTimer.Start();

            this.FormClosed += SpecialToolForm_FormClosed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CursorPositionTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // update mouse position info
                Point pt = Cursor.Position;
                Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);

                //check if form is intercepting with the parent form
                if (Program.GlobalVars.FormMainRectangle.IntersectsWith(screenRectangle) == false)
                {
                    ShowFormVisitedState(false);
                }
                else
                {
                    if (screenRectangle.Contains(pt))
                    {
                        IsMouseInFormFrame = false;
                        IsMouseInFormContent = true;
                        ShowFormVisitedState();
                    }
                    else
                    {
                        IsMouseInFormContent = false;
                        ShowFormVisitedState();
                    }
                }
            }
            catch (Exception)
            {
                

            }
            finally
            {
                CursorPositionTimer.Start(); 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpecialToolForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CursorPositionTimer!=null)
            {
                CursorPositionTimer.Tick -= CursorPositionTimer_Tick;
                CursorPositionTimer.Stop();
                CursorPositionTimer.Dispose();
                CursorPositionTimer = null;             
            }
        }   
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0xA0) // WM_NCMOUSEMOVE
            {
                IsMouseInFormFrame = true;
                IsMouseInFormContent = false;

                TRACKMOUSEEVENT msevnt = new TRACKMOUSEEVENT();
                msevnt.cbSize = Marshal.SizeOf(msevnt);
                msevnt.dwFlags = TME_NONCLIENT | TME_LEAVE | TME_HOVER;
                msevnt.hwndTrack = this.Handle;
                TrackMouseEvent(ref msevnt);

            }
            else if (m.Msg == WM_NCMOUSEHOVER)
            {
                IsMouseInFormFrame = true;
            }
            else if (m.Msg == WM_NCMOUSELEAVE)
            {
                IsMouseInFormFrame = false;
            }
            else if (m.Msg == WM_CAPTURECHANGED)
            {
                IsMouseInFormFrame = false;
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// Function called by the Event for the EventCursorPosition-Timer
        /// </summary>
        protected void EventCursorPositionTimer()
        {
            // update mouse position info
            Point pt = Cursor.Position;
            Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);

            //check if form is intercepting with the parent form
            if (Program.GlobalVars.FormMainRectangle.IntersectsWith(screenRectangle) == false)
            {
                ShowFormVisitedState(false);
            }
            else
            {
                if (screenRectangle.Contains(pt))
                {
                    IsMouseInFormFrame = false;
                    IsMouseInFormContent = true;
                    ShowFormVisitedState();
                }
                else
                {
                    IsMouseInFormContent = false;
                    ShowFormVisitedState();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IsFormIntercepting"></param>
        protected void ShowFormVisitedState(bool IsFormIntercepting = true)
        {
            if (IsFormIntercepting)
            {
                if (IsFormVisited)
                {
                    Opacity = 1.0;
                }
                else
                {
                    Opacity = 0.85;
                }
            }
            else
            {
                Opacity = 1.0;
            }
        }
    }
}
