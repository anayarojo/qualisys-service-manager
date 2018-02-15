using QualisysExtensions.Controls;
using QualisysExtensions.Date;
using QualisysServiceManager.Enums;
using QualisysServiceManager.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace QualisysServiceManager
{
    public partial class frmLogViewer : Form
    {
        #region Attributes

        private string mStrServicePath;
        private Thread mObjThread;

        #endregion

        #region Constructor

        public frmLogViewer(string pStrPathService)
        {
            InitializeComponent();
            mStrServicePath = pStrPathService;
        }

        #endregion

        #region Events

        private void frmLogViewer_Load(object sender, System.EventArgs e)
        {
            LoadDateFilters();
            LoadLogSizes();

            LoadLog
            (
                dtmDateFrom.Value,
                dtmDateTo.Value
            );
        }

        private void cboDateFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeDateFilter(sender as ComboBox);
        }

        private void cboLogSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLog(dtmDateFrom.Value, dtmDateTo.Value);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadLog(dtmDateFrom.Value, dtmDateTo.Value);
        }

        #endregion

        #region Methods

        private void LoadLog(DateTime pDtmFromDate, DateTime pDtmToDate)
        {
            btnSearch.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                dgvLog.DataSource = null;
                dgvLog.AutoGenerateColumns = false;
                dgvLog.DataSource = GetLogList
                (
                    pDtmFromDate,
                    pDtmToDate,
                    GetAllLogFiles(GetPathService())
                );
            }
            catch (Exception lObjException)
            {
                MessageBox.Show(lObjException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSearch.Enabled = true;
            }
        }

        private List<LogModel> GetLogList(DateTime pDtmFromDate, DateTime pDtmToDate, string[] pArrStrLogFiles)
        {
            List<LogModel> lLstObjLog = new List<LogModel>();

            foreach (string pStrLogFile in pArrStrLogFiles)
            {
                FileInfo lObjFile = new FileInfo(pStrLogFile);

                if ((pArrStrLogFiles.Length == 1) ||
                   (lObjFile.CreationTime >= pDtmFromDate.Date && lObjFile.CreationTime <= pDtmToDate.Date.AddDays(1)))
                {
                    lLstObjLog.AddRange(GetLogListByFile(lObjFile));
                }
            }

            return lLstObjLog.OrderByDescending(x => x.Date).ToList();
        }

        private List<LogModel> GetLogListByFile(FileInfo pObjFile)
        {
            List<LogModel> lLstObjLog = new List<LogModel>();
            string[] lArrStrLines = File.ReadAllLines(pObjFile.FullName);
            LogModel lObjLogModel = new LogModel();

            if (lArrStrLines.Length > 0)
            {
                int lIntMaxLogView = GetMaxLogView();
                int lIntCountLogView = lArrStrLines.Length > lIntMaxLogView ? lIntMaxLogView : lArrStrLines.Length;
                int lIntEndLogView = lArrStrLines.Length - lIntCountLogView;
                int lIntIndex = 1;

                for (int i = lArrStrLines.Length - 1; i >= lIntEndLogView; i--)
                {
                    DateTime? lDtmDate = GetDate(lArrStrLines[i]);

                    if (lDtmDate != null)
                    {
                        lObjLogModel = ParseToLogModel(lArrStrLines[i]);
                        lLstObjLog.Add(lObjLogModel);
                    }
                    else
                    {
                        lObjLogModel.Message += string.Format(" {0}", lArrStrLines[i]);
                    }

                    lIntIndex++;
                }
            }

            return lLstObjLog;
        }

        private LogModel ParseToLogModel(string pStrLine)
        {
            LogModel lObjResult = new LogModel();

            int lIntIndexOne = pStrLine.IndexOf("[");
            int lIntIndexTwo = pStrLine.IndexOf("]");

            int lIntLengthOne = Math.Abs(lIntIndexOne - lIntIndexTwo);
            int lIntLengthTwo = Math.Abs((pStrLine.Length) - (lIntIndexTwo + 2));

            lObjResult.Date = GetDate(pStrLine) ?? DateTime.MinValue;
            lObjResult.Type = pStrLine.Substring(lIntIndexOne + 1, lIntLengthOne - 1);
            lObjResult.Message = pStrLine.Substring(lIntIndexTwo + 2, lIntLengthTwo);

            return lObjResult;
        }

        private int GetMaxLogView()
        {
            return cboLogSize.SelectedIndex > 0 ? (int)cboLogSize.SelectedValue : 0;
        }

        private string GetStringDate(string pStrLine)
        {
            return string.Format("{0:19}", pStrLine).Substring(0, 19);
        }

        private DateTime? GetDate(string pStrLine)
        {
            try
            {
                return DateTime.ParseExact(GetStringDate(pStrLine), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        private string[] GetAllLogFiles(string pStrServicePath)
        {
            return Directory.GetFiles(pStrServicePath, "*.log", SearchOption.AllDirectories);
        }

        private string GetPathService()
        {
            return new FileInfo(mStrServicePath).Directory.FullName;
        }

        private void LoadLogSizes()
        {
            cboLogSize.LoadDataSource(ComboBoxExtension.GetComboItemListFromEnum<LogSizeEnum>("Seleccione"));
            cboLogSize.SelectedIndex = 1;
        }

        private void LoadDateFilters()
        {
            cboDateFilter.LoadDataSource(ComboBoxExtension.GetComboItemListFromEnum<DateFilterEnum>("Seleccione"));
            cboDateFilter.SelectedIndex = 1;
        }

        private void ChangeDateFilter(ComboBox pObjComboBox)
        {
            dtmDateFrom.Enabled = false;
            dtmDateTo.Enabled = false;
            dtmDateFrom.Value = DateTime.Today;
            dtmDateTo.Value = DateTime.Today;

            if (pObjComboBox.SelectedIndex > 0)
            {
                switch ((DateFilterEnum)pObjComboBox.SelectedValue)
                {
                    case DateFilterEnum.CURRENT_WEEK:
                        dtmDateFrom.Value = DateTime.Today.StartOfWeek(DayOfWeek.Monday);
                        dtmDateTo.Value = dtmDateFrom.Value.AddDays(6);
                        break;
                    case DateFilterEnum.CURRENT_MONTH:
                        dtmDateFrom.Value = new DateTime(dtmDateFrom.Value.Year, dtmDateFrom.Value.Month, 1);
                        dtmDateTo.Value = dtmDateFrom.Value.AddMonths(1).AddDays(-1);
                        break;
                    case DateFilterEnum.CURRENT_YEAR:
                        dtmDateFrom.Value = new DateTime(dtmDateFrom.Value.Year, 1, 1);
                        dtmDateTo.Value = dtmDateFrom.Value.AddYears(1).AddDays(-1);
                        break;
                    case DateFilterEnum.LAST_WEEK:
                        dtmDateFrom.Value = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 6);
                        dtmDateTo.Value = dtmDateFrom.Value.AddDays(6);
                        break;
                    case DateFilterEnum.LAST_MONTH:
                        dtmDateFrom.Value = DateTime.Today.AddMonths(-1);
                        dtmDateFrom.Value = new DateTime(dtmDateFrom.Value.Year, dtmDateFrom.Value.Month, 1);
                        dtmDateTo.Value = dtmDateFrom.Value.AddMonths(1).AddDays(-1);
                        break;
                    case DateFilterEnum.LAST_YEAR:
                        dtmDateFrom.Value = DateTime.Today.AddYears(-1);
                        dtmDateFrom.Value = new DateTime(dtmDateFrom.Value.Year, 1, 1);
                        dtmDateTo.Value = dtmDateFrom.Value.AddYears(1).AddDays(-1);
                        break;
                    case DateFilterEnum.RANGE:
                        dtmDateFrom.Enabled = true;
                        dtmDateTo.Enabled = true;
                        break;
                }
            }
        }

        #endregion
    }
}
