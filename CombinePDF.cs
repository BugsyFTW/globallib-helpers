using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalLib.Helpers
{
    /// <summary>
    /// Utiliza a biblioteca PDFSharp para combinar varios PDFs um só
    /// </summary>
    public class CombinePDF
    {
        public List<Stream> PDFContentList { get; set; }

        public Stream FinalPDF { get; set; }

        public CombinePDF()
        {
            PDFContentList = new List<Stream>();
        }

        /// <summary>
        /// Adiciona PDF à lista
        /// </summary>
        /// <param name="pdfBytes">PDF lido em Array de Bytes</param>
        public void AddPDF(byte[] pdfBytes)
        {
            PDFContentList.Add(new MemoryStream(pdfBytes));
        }

        /// <summary>
        /// Adiciona PDF à Lista
        /// </summary>
        /// <param name="pdfStream">PDF lido em Stream</param>
        public void AddPDF(Stream pdfStream)
        {
            PDFContentList.Add(pdfStream);
        }

        /// <summary>
        /// Combina o PDF
        /// </summary>
        /// <returns></returns>
        public Stream Combine(string pdfTitle = "MyDF Title")
        {
            PdfDocument combinedPDF = new PdfDocument();
            combinedPDF.Info.Title = pdfTitle;

            foreach (Stream sPDFFile in PDFContentList)
            {
                PdfDocument docReportPages = PdfReader.Open(sPDFFile, PdfDocumentOpenMode.Import);

                int repPageCount = docReportPages.PageCount;
                for (int i = 0; i < repPageCount; i++)
                {
                    PdfPage page = docReportPages.Pages[i];
                    page = combinedPDF.AddPage(page);
                }

            }

            if (FinalPDF != null)
            {
                FinalPDF = null;
            }

            FinalPDF = new MemoryStream();

            combinedPDF.Save(FinalPDF, false);

            return FinalPDF;
        }
    }
}
