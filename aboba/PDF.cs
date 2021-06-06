using ServiceCenter.Views;
using PdfSharp.Drawing;
using PdfSharp.Pdf;


namespace ServiceCenter.aboba
{
    public class PDF : Builder
    {
        private readonly PdfDocument _doc;
        private readonly PdfPage _page;
        private readonly XGraphics _gfx;

        public PDF()
        {
            _doc = new PdfDocument();
            _page = _doc.AddPage();
            _gfx = XGraphics.FromPdfPage(_page);
        }

        public PDF SetTitle(string title)
        {
            _doc.Info.Title = title;
            return this;
        }

        public PDF DrawString(string text,XFont font, int x,int y)
        {
            _gfx.DrawString(text, font, XBrushes.Black,
                new XRect(x, y, _page.Width, y),
                XStringFormats.Center);
            return this;
        }

        public PDF DrawString(string text, XFont font, int x, int y, XStringFormat stringFormat)
        {
            _gfx.DrawString(text, font, XBrushes.Black,
                new XRect(x, y, _page.Width, y),
                stringFormat);
            return this;
        }
        public static PDF NewBuilder()
        {
            return new PDF();
        }

        public object Build()
        {
            return _doc;
        }



    }
}
