using MudBlazor;
using QRCoder;

namespace QRGenerator.Pages
{
    public partial class Index  //partial means that Index Class is part of index
    {
        MudForm urlSubmitForm;
        public string SubmittedUrl { get; set; }
        public string QRCodeText { get; set; }

        private async Task SubmitUrl()
        {
            await urlSubmitForm.Validate();
            if (urlSubmitForm.IsValid)
                GenerateQRCode();
        }

        protected void GenerateQRCode()
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qrGenerator.CreateQrCode(SubmittedUrl, QRCodeGenerator.ECCLevel.Q); // generating img
            BitmapByteQRCode qrCode = new BitmapByteQRCode(qRCodeData); // saving img into a bitmap
            byte[] qrCodeAsByteArr = qrCode.GetGraphic(30); // saving img into a byte array

            var ms = new MemoryStream(qrCodeAsByteArr);

            QRCodeText = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
            SubmittedUrl = String.Empty;
        }
    }
}
