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
    public class WaterWellReportForm
    {
        #region Declaration
        int _totalColumn = 3;
        Document _document;
        Font _fontStyle;
        Font _fontStyleNotBold;
        PdfPTable _pdfTable = new PdfPTable(3);
        PdfPCell _pdfCell;
        MemoryStream _memoryStream = new MemoryStream();
        List<BoreStrata> _boreStrata;
        Models.Borehole _borehole;
        Physical _physical;
        PumpTest _pumpTest;
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
            _pdfCell = new PdfPCell(new Phrase("WATER-WELL COMPLETION FORM", _fontStyle));
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

            _pdfCell = new PdfPCell(ClientBoreInfo());
            _pdfCell.Colspan = 2;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(ClientBoreInfo1());
            //_pdfCell.Colspan = 2;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            _pdfCell = new PdfPCell(StrataTable());
            _pdfCell.Colspan = 2;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(BoreholeTable());
            //_pdfCell.Colspan = 2;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();


        }

        private PdfPTable ClientBoreInfo()
        {
            PdfPTable oPdfTable = new PdfPTable(2);
            oPdfTable.SetWidths(new float[] { 100f, 100f });

            _pdfCell = new PdfPCell(new Phrase("Borehole No:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.BoreholeNo, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Location/Area:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_survey.Village, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("District:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_survey.District.Name, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Region:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_survey.Region.Name, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Name of Applicant & Address:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_survey.Client.Name + " , " + _survey.Client.Address,_fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();


            return oPdfTable;

        }

        private PdfPTable ClientBoreInfo1()
        {
            PdfPTable oPdfTable = new PdfPTable(2);
            oPdfTable.SetWidths(new float[] { 100f, 100f });

            _pdfCell = new PdfPCell(new Phrase("Drilled By (Rig No./Type):", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_boreDrillMethod.Rig.RigType, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Coordinates:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("E" + _borehole.Eastings + "/N" + _borehole.Northings, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Start Date:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.StartDate.ToShortDateString(), _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);


            _pdfCell = new PdfPCell(new Phrase("End Date:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.EndDate.ToShortDateString(), _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();


            return oPdfTable;

        }

        private PdfPTable StrataTable()
        {

            PdfPTable oPdfTable = new PdfPTable(3);
            oPdfTable.SetWidths(new float[] { 40f, 40f, 70f });

            #region Table Header

            _fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
            _pdfCell = new PdfPCell(new Phrase("1. STRATA", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase("From(m)", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            oPdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase("To(m)", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            oPdfTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase("General Description", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();


            #endregion

            #region Table Body
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 0);
            foreach (var strata in _boreStrata)
            {
                _pdfCell = new PdfPCell(new Phrase(strata.RangeFrom, _fontStyleNotBold));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                oPdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(strata.RangeTo, _fontStyleNotBold));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                oPdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(strata.StrataName, _fontStyleNotBold));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                oPdfTable.AddCell(_pdfCell);
                oPdfTable.CompleteRow();
            }
            #endregion

            return oPdfTable;
        }

        private PdfPTable BoreholeTable()
        {

            PdfPTable oPdfTable = new PdfPTable(2);
            oPdfTable.SetWidths(new float[] { 100f, 100f });

            _fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
            _pdfCell = new PdfPCell(new Phrase("2. WATER", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.BorderColorLeft = BaseColor.WHITE;
            _pdfCell.BorderColorRight = BaseColor.WHITE;
            _pdfCell.BorderColorTop = BaseColor.WHITE;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Struck at: ", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.AquiferDepth, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("W.L. rose to: ", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.Eastings, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Yield tasted: ", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.Eastings, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Water quality taste: ", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.GravelType, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();


            _fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.Border = 0;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.BorderColorLeft = BaseColor.WHITE;
            _pdfCell.BorderColorRight = BaseColor.WHITE;
            _pdfCell.BorderColorTop = BaseColor.WHITE;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
            _pdfCell = new PdfPCell(new Phrase("3. DIAMETER DRILLED & DEPTH", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Diameter(m):", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.Diameter, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Finish Depth(m):", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.FinishDepth, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.Border = 0;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.BorderColorLeft = BaseColor.WHITE;
            _pdfCell.BorderColorRight = BaseColor.WHITE;
            _pdfCell.BorderColorTop = BaseColor.WHITE;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
            _pdfCell = new PdfPCell(new Phrase("4. CASING/SCREEN LEFT IN HOLE", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.BorderColorLeft = BaseColor.WHITE;
            _pdfCell.BorderColorRight = BaseColor.WHITE;
            _pdfCell.BorderColorTop = BaseColor.WHITE;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Type:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.CasingType, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Diameter(m):", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.CasingDiameter, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Length(m):", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.CasingHeight, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Type:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.ScreenType, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Diameter(m):", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.ScreenDiameter, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Length(m):", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.ScreenHeight, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Casing above G.L:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.ScreenHeight, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Top of Casing Secured:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.CasingTopper, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Bottom Casing Protected with:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.CasingBottom, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.Border = 0;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.BorderColorLeft = BaseColor.WHITE;
            _pdfCell.BorderColorRight = BaseColor.WHITE;
            _pdfCell.BorderColorTop = BaseColor.WHITE;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
            _pdfCell = new PdfPCell(new Phrase("5. FINISH OF SECTION UNCASED", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Hole uncased(m):", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.UncasedDepth, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Back-filled to(m):", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.BackFillHeight, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Filled with:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.BackFillMaterial, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Average size:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.BackFillAvgSize, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Other Method:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.BackFillMethod2, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.Border = 0;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.BorderColorLeft = BaseColor.WHITE;
            _pdfCell.BorderColorRight = BaseColor.WHITE;
            _pdfCell.BorderColorTop = BaseColor.WHITE;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
            _pdfCell = new PdfPCell(new Phrase("6. GRAVEL SCREEN", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Gravel Type:", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.GravelType, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Average Size(m):", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.GravelAvgSize, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Inserted From(m):", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.GravelAvgSize, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("To(m):", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_borehole.GravelAvgSize, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.Border = 0;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.BorderColorLeft = BaseColor.WHITE;
            _pdfCell.BorderColorRight = BaseColor.WHITE;
            _pdfCell.BorderColorTop = BaseColor.WHITE;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
            _pdfCell = new PdfPCell(new Phrase("7. DRILLING METHOD", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Air Rotary To(m):", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_drillingType.DrillMethod.Name, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Mud Rotary To(m):", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_drillingType.DrillMethod.Name, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Air Hummer To(m):", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_drillingType.DrillMethod.Name, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase("Cable-Tool To(m):", _fontStyle));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_drillingType.DrillMethod.Name, _fontStyleNotBold));
            _pdfCell.Border = 0;
            oPdfTable.AddCell(_pdfCell);
            oPdfTable.CompleteRow();



            return oPdfTable;

        }

        private void ReportFooter()
        {
            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfCell = new PdfPCell(new Phrase(" ", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfCell = new PdfPCell(new Phrase("........................", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfCell = new PdfPCell(new Phrase("Driller in charge signature:", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfCell = new PdfPCell(new Phrase("Satisfied this well has been completed in a manlike manner & drilling regulations have been complied with.", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfCell = new PdfPCell(new Phrase("........................", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfCell = new PdfPCell(new Phrase("CHIEF EXECUTIVE OFFICER:", _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
        }


    }
}