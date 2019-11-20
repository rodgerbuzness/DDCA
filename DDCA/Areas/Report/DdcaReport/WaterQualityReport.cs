using DDCA.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DDCA.Areas.Report.DdcaReport
{
    public class WaterQualityReport
    {
        #region Declaration
        int _totalColumn = 5;
        Document _document;
        Font _fontStyle;
        Font _fontStyleNotBold;
        PdfPTable _pdfTable = new PdfPTable(3);
        PdfPCell _pdfCell;
        MemoryStream _memoryStream = new MemoryStream();
        List<BoreStrata> _boreStrata;
        Models.Borehole _borehole;
        Physical _physical;
        Chemical _chemical;
        PumpTest _pumpTest;
        LabAnalysis _labAnalysis;
        BoreDrillMethod _boreDrillMethod;
        DrillingType _drillingType;
        GeoSurvey _survey;
        #endregion

        public byte[] PrepareReport(int id)
        {
            _boreDrillMethod = Database.Session.Load<BoreDrillMethod>(id);
            _boreStrata = Database.Session.Query<BoreStrata>().Where(b => b.Borehole.Id == _boreDrillMethod.Borehole.Id).ToList<BoreStrata>();
            _borehole = Database.Session.Load<Models.Borehole>(_boreDrillMethod.Borehole.Id);
            _survey = Database.Session.Load<GeoSurvey>(_boreDrillMethod.GeoSurvey.Id);
            _drillingType = Database.Session.Query<DrillingType>().Where(b => b.BoreDrillMethod.Id == _boreDrillMethod.Id).SingleOrDefault<DrillingType>();
            _pumpTest = Database.Session.Query<PumpTest>().Where(b => b.Borehole.Id == _borehole.Id).SingleOrDefault<PumpTest>();
            _labAnalysis = Database.Session.Query<LabAnalysis>().Where(b => b.PumpTest.Id == _pumpTest.Id).SingleOrDefault<LabAnalysis>();
            _physical = Database.Session.Query<Physical>().Where(b => b.LabAnalysis.Id == _labAnalysis.Id).SingleOrDefault<Physical>();
            _chemical = Database.Session.Query<Chemical>().Where(b => b.LabAnalysis.Id == _labAnalysis.Id).SingleOrDefault<Chemical>();



            #region Report
            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(20f, 20f, 20f, 20f);
            _pdfTable.WidthPercentage = 100;
            _pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();
            _pdfTable.SetWidths(new float[] { 20f, 150f, 100f });
            #endregion

            this.ReportHeader();
            this.ReportBody();
            this.ReportFooter();
            //this.ReportFooter();
            _document.Add(_pdfTable);
            _document.Close();
            return _memoryStream.ToArray();
        }

        private void ReportHeader()
        {
            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfCell = new PdfPCell(new Phrase("DRILLING AND DAM CONSTRUCTION AGENCY", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfCell = new PdfPCell(new Phrase("WATER QUALITY REPORT", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();


        }

        private void ReportBody()
        {

            _fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
            _fontStyleNotBold = FontFactory.GetFont("Tahoma", 10f, 0);

            _pdfCell = new PdfPCell(LabInfo());
            _pdfCell.Colspan = 2;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(LabInfo2());
            _pdfCell.Colspan = 2;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            _pdfCell = new PdfPCell(ChemicalPysical());
            _pdfCell.Colspan = _totalColumn;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            _pdfCell = new PdfPCell(Utility());
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfCell = new PdfPCell(new Phrase("RECOMMENDATIONS: ", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 0);
            _pdfCell = new PdfPCell(new Phrase(_labAnalysis.Recommend, _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();


        }

        private PdfPTable LabInfo()
        {
            PdfPTable oPdfTable = new PdfPTable(2);
            oPdfTable.SetWidths(new float[] { 100f, 100f });

            _pdfCell = new PdfPCell(new Phrase("Client Name:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_survey.Client.Name, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Analysis Date:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_labAnalysis.AnalysisDate.ToShortDateString(), _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();


            _pdfCell = new PdfPCell(new Phrase("Purpose:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Domestic use", _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();


            return oPdfTable;

        }

        private PdfPTable LabInfo2()
        {
            PdfPTable oPdfTable = new PdfPTable(2);
            oPdfTable.SetWidths(new float[] { 100f, 100f });


            _pdfCell = new PdfPCell(new Phrase("Laboratory Name:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_labAnalysis.LabName, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Place:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("DDCA", _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Borehole No:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.BoreholeNo, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();


            return oPdfTable;

        }

        private PdfPTable ChemicalPysical()
        {

            PdfPTable oPdfTable = new PdfPTable(5);
            oPdfTable.SetWidths(new float[] { 70f, 40f, 40f, 40f, 40f });

            #region Table Header

            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase("Physical & Chemical Parameters", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            oPdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase("Concentration", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            oPdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase("Unit", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            oPdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase("Tanzania National Standard", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            oPdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase("Remarks", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow(); 


            #endregion

            #region Table Body
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 0);
            
                _pdfCell = new PdfPCell(new Phrase("Turbidity", _fontStyleNotBold));
               _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
               _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
               _pdfCell.BackgroundColor = BaseColor.WHITE;
               oPdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(_physical.Turbidity, _fontStyleNotBold));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
               _pdfCell.BackgroundColor = BaseColor.WHITE;
                oPdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase("NTU", _fontStyleNotBold));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("25", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("pH", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_physical.PH, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("6.5 - 9.2", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Color", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_physical.Colour, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mgPt/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("50", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Electrical Conductivity", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_physical.Conductivity, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("µS/cm", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("2000", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Total Dissolved Solids", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_physical.SettleableMatter, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mg/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("500 - 1500", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Odour", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_physical.Odour, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("TON", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("No Offensive", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Taste", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_physical.Taste, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("No Offensive", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Phenolphthalein Alkalinity", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_chemical.Phenophthalein, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mgCaCO3/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("n.m", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Total Alkalinity", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_chemical.Alkalinity, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mgCaCO3/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("n.m", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Carbonate Hardness", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_chemical.Carbonate, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mgCaCO3/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("n.m", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Non Carbornate Hardness", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_chemical.NonCarbonate, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mgCaCO3/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("n.m", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Total Hardness", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_chemical.Hardness, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mgCaCO3/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("600", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Calcium", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_chemical.Calcium, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mg/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("n.300", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Magnesium", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_chemical.Magnesium, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mg/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("100", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Manganese", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_chemical.Manganese, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mg/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("0.5", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Zinc", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_chemical.Zinc, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mg/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("15", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Iron", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_chemical.Iron, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mg/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("1.0", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Chloride", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_chemical.Chloride, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mg/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("800", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Sulphate", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_chemical.Sulphate, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mg/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("600", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Nitrate", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_chemical.NitrateNitrogen, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mg/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("75", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Sodium", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_chemical.Sodium, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mg/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("n.m", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Potassium", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_chemical.Potassium, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mg/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("n.m", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Orthophosphate", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_chemical.OrthoPhosphate, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mg/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("n.m", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Fluoride", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_chemical.Fluoride, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mg/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("4.0", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Ammonical Nitrogen", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_chemical.AmmonicalNitrogen, _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("mg/L", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("2.0", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyleNotBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            #endregion


            return oPdfTable;
        }

        private PdfPTable Utility()
        {
            PdfPTable oPdfTable = new PdfPTable(3);
            oPdfTable.SetWidths(new float[] { 50f, 50f, 50f });

            _pdfCell = new PdfPCell(new Phrase("N.M = Not Mentioned", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("N.A = Not Applicable", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("N.D = Not Determined", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            return oPdfTable;
        }

        private void ReportFooter()
        {
            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfCell = new PdfPCell(new Phrase("Signature: ", _fontStyle));
            _pdfCell.Colspan = 2;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfCell = new PdfPCell(new Phrase("........................", _fontStyle));
            _pdfCell.Colspan = 1;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfCell = new PdfPCell(new Phrase("Initials: ", _fontStyle));
            _pdfCell.Colspan = 2;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfCell = new PdfPCell(new Phrase("........................", _fontStyle));
            _pdfCell.Colspan = 1;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfCell = new PdfPCell(new Phrase("Position: ", _fontStyle));
            _pdfCell.Colspan = 2;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfCell = new PdfPCell(new Phrase("........................", _fontStyle));
            _pdfCell.Colspan = 1;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

        }

    }
}