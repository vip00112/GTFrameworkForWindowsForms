using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTFrameworkForWindowsForms
{
    #region app.manifest
    //<?xml version="1.0" encoding="utf-8"?>
    //<assembly manifestVersion="1.0" xmlns="urn:schemas-microsoft-com:asm.v1">
    //  <compatibility xmlns="urn:schemas-microsoft-com:compatibility.v1">
    //    <application>
    //      <!-- Windows Vista -->
    //      <supportedOS Id="{e2011457-1546-43c5-a5fe-008deee3d3f0}" />
    //
    //      <!-- Windows 7 -->
    //      <supportedOS Id="{35138b9a-5d96-4fbd-8e2d-a2440225f93a}" />
    //
    //      <!-- Windows 8 -->
    //      <supportedOS Id="{4a2f28e3-53b9-4441-ba9c-d69d4a4a6e38}" />
    //
    //      <!-- Windows 8.1 -->
    //      <supportedOS Id="{1f676c76-80e1-4239-95bb-83d0f6d0da78}" />
    //
    //      <!-- Windows 10 -->
    //      <supportedOS Id="{8e0f7a12-bfb3-4fe8-b9a5-48fd50a15a9a}" />
    //    </application>
    //  </compatibility>
    //</assembly>
    #endregion

    public static class WinAPI
    {
        #region Public Method
        public static void SetTitleBarTheme(IntPtr handle, bool isDarkMode)
        {
            if (IsWindows10OrGreater(17763))
            {
                int attr = WindowNative.DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                if (IsWindows10OrGreater(18985))
                {
                    attr = WindowNative.DWMWA_USE_IMMERSIVE_DARK_MODE;
                }

                int value = isDarkMode ? 1 : 0;
                WindowNative.DwmSetWindowAttribute(handle, attr, ref value, sizeof(int));
            }
        }

        public static void SetFormShadow(IntPtr handle, int margin = 1)
        {
            int attr = 2;
            int value = 2;
            WindowNative.DwmSetWindowAttribute(handle, attr, ref value, sizeof(int));

            var margins = new WindowNative.Margins()
            {
                bottomHeight = margin,
                leftWidth = margin,
                rightWidth = margin,
                topHeight = margin,
            };
            WindowNative.DwmExtendFrameIntoClientArea(handle, ref margins);
        }
        #endregion

        #region Private Method
        private static bool IsWindows10OrGreater(int build = -1)
        {
            var version = Environment.OSVersion.Version;
            return version.Major >= 10 && version.Build >= build;
        }
        #endregion
    }
}
