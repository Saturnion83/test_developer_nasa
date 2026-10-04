using System;
using System.Collections.Generic;
using System.Text;

namespace test_developer_nasa.CustomObject
{
    public class CustomTabControl : TabControl
    {
        protected override void WndProc(ref Message m)
        {
            // 0x1328 è il messaggio di sistema LCM_ADJUSTRECT
            // Intercettando questo messaggio quando non siamo in modalità Design,
            // diciamo a Windows di nascondere completamente la barra delle schede.
            if (m.Msg == 0x1328 && !DesignMode)
            {
                m.Result = (IntPtr)1;
                return;
            }
            base.WndProc(ref m);
        }
    }
}
