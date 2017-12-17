using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace RESTClient
{
    [Guid("D2A7ACC4-9F3E-4A89-8A1B-1A79271921BF")]
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IHoleLayout
    {
        [DispId(1)]
        Boolean CreateHoleLayoutImage([In, MarshalAs(UnmanagedType.BStr)] ref string xmlFile);
    }

    [Guid("A4EC2F30-F074-47A2-9F1D-CCA37BD7B74E")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("RESTClient.HoleLayoutPreview")]
    public partial class HoleLayoutPreview : IHoleLayout
    {
        public Boolean CreateHoleLayoutImage([In, MarshalAs(UnmanagedType.BStr)] ref string xmlFile)
        {
            MessageBox.Show(xmlFile);
            Bitmap bmp = CreateHoleImage(xmlFile, false, true);
            if (bmp == null)
                return false;

            String png = xmlFile;
            int idx = png.LastIndexOf(".");
            if (idx >= 0)
                png = png.Substring(0, idx);
            png += ".png";

            bmp.Save(png, ImageFormat.Png);
            return true;
        }
    }
}
