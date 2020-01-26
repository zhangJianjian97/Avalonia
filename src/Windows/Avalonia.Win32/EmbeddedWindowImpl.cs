﻿// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Avalonia.Platform;
using Avalonia.Win32.Interop;

namespace Avalonia.Win32
{
    class EmbeddedWindowImpl : WindowImpl, IEmbeddableWindowImpl
    {
        protected override IntPtr CreateWindowOverride(ushort atom)
        {
            var hWnd = UnmanagedMethods.CreateWindowEx(
                0,
                atom,
                null,
                (int)UnmanagedMethods.WindowStyles.WS_CHILD,
                0,
                0,
                640,
                480,
                OffscreenParentWindow.Handle,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero);
            return hWnd;
        }

        protected override IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            if (msg == (uint)UnmanagedMethods.WindowsMessage.WM_KILLFOCUS)
                LostFocus?.Invoke();
            return base.WndProc(hWnd, msg, wParam, lParam);
        }

        public event Action LostFocus;
    }
}
