using QualisysExtensions.Controls;
using QualisysServiceManager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Management;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;

namespace QualisysServiceManager
{
    public partial class frmManager : Form
    {
        #region Attributes

        private ServiceController mObjServiceController;
        private List<ServiceModel> mLstObjServices;
        private Thread mObjThread;

        #endregion

        #region Constructor

        public frmManager()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        #endregion

        #region Events

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadServices();
        }

        private void frmManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            //FinalizeThread();
        }

        private void cboService_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboService.SelectedIndex > 0)
                {
                    mObjServiceController = new ServiceController();
                    mObjServiceController.ServiceName = GetSelectedServiceName();
                    UpdateButtons();
                }
                else
                {
                    DisableButtons();
                    SetInfo("Listo");
                }
            }
            catch (Exception lObjException)
            {
                MessageBox.Show(lObjException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            mObjThread = new Thread(StartService);
            mObjThread.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            mObjThread = new Thread(StopService);
            mObjThread.Start();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            ShowLogViewer(GetServicePath());
        }

        #endregion

        #region Methods

        private void LoadServices()
        {
            mLstObjServices = GetServicesList();

            var lLstObjServicesProjection = mLstObjServices.Select(x => new
            {
                Id = x.Index,
                Name = x.DisplayName
            })
            .ToList();

            cboService.LoadDataSource(ComboBoxExtension.GetComboItemListFromList(lLstObjServicesProjection, "Favor de seleccionar"));
            cboService.SelectedIndex = 1;
        }

        private void StartService()
        {
            DisableButtons();

            try
            {
                if (mObjServiceController.Status == ServiceControllerStatus.Stopped)
                {
                    SetInfo("Iniciando servicio...");

                    try
                    {
                        mObjServiceController.Start();
                        mObjServiceController.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));
                        SetInfo("Listo.");
                    }
                    catch (InvalidOperationException)
                    {
                        if (!IsAdministrator())
                        {
                            SetInfo("Favor de iniciar la aplicación como Administrador.");
                        }
                        else
                        {
                            SetInfo("No se pudo iniciar el servicio.");
                        }
                        Thread.Sleep(5000);
                    }
                }
            }
            catch (Exception lObjException)
            {
                MessageBox.Show(lObjException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateButtons();
            }
        }

        private void StopService()
        {
            DisableButtons();

            try
            {
                if (mObjServiceController.Status == ServiceControllerStatus.Running)
                {
                    SetInfo("Deteniendo servicio...");

                    try
                    {
                        mObjServiceController.Stop();
                        mObjServiceController.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 30));
                        SetInfo("Listo.");
                    }
                    catch (InvalidOperationException)
                    {
                        if (!IsAdministrator())
                        {
                            SetInfo("Favor de iniciar la aplicación como Administrador.");
                        }
                        else
                        {
                            SetInfo("No se pudo detener el servicio.");
                        }
                        Thread.Sleep(5000);
                    }
                }
            }
            catch (Exception lObjException)
            {
                MessageBox.Show(lObjException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateButtons();
            }
        }

        private void ShowLogViewer(string pStrServicePath)
        {
            try
            {
                frmLogViewer lFrmLogViewer = new frmLogViewer(pStrServicePath);
                lFrmLogViewer.ShowDialog(this);
            }
            catch (Exception lObjException)
            {
                MessageBox.Show(lObjException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetInfo(string pStrInformation)
        {
            lblStatus.Invoke((Action)delegate
            {
                lblStatus.Text = pStrInformation;
            });
        }

        private void UpdateButtons()
        {
            try
            {
                this.Invoke((Action)delegate
                {
                    DisableButtons();
                    EnableButtons();
                });
            }
            catch (Exception lObjException)
            {
                MessageBox.Show(lObjException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnableButtons()
        {
            try
            {
                this.Invoke((Action)delegate
                {
                    switch (mObjServiceController.Status)
                    {
                        case ServiceControllerStatus.Running:
                            btnStop.Enabled = true;
                            btnLog.Enabled = true;
                            SetInfo("Iniciado");
                            break;
                        case ServiceControllerStatus.Stopped:
                            btnStart.Enabled = true;
                            btnLog.Enabled = true;
                            SetInfo("Detenido");
                            break;
                        case ServiceControllerStatus.StartPending:
                            SetInfo("Iniciado...");
                            break;
                        case ServiceControllerStatus.StopPending:
                            SetInfo("Deteniendo...");
                            break;
                        case ServiceControllerStatus.Paused:
                            SetInfo("Pausado");
                            break;
                        case ServiceControllerStatus.PausePending:
                            SetInfo("Pausando...");
                            break;
                        case ServiceControllerStatus.ContinuePending:
                            SetInfo("Continuando...");
                            break;
                    }
                });
            }
            catch (Exception lObjException)
            {
                MessageBox.Show(lObjException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisableButtons()
        {
            this.Invoke((Action)delegate
            {
                btnStart.Enabled = false;
                btnLog.Enabled = false;
                btnStop.Enabled = false;
            });
        }

        private string GetConfigurationPath()
        {
            return string.Format("{0}.config", GetServicePath().Replace("\"", ""));
        }

        private string GetServicePath()
        {
            string ServiceName = mObjServiceController.ServiceName;

            using (ManagementObject lObjManagementObject = new ManagementObject(string.Format("Win32_Service.Name='{0}'", mObjServiceController.ServiceName)))
            {
                lObjManagementObject.Get();
                return lObjManagementObject["PathName"].ToString().Replace("\"", "");
            }
        }

        private bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator);
        }

        private List<ServiceModel> GetServicesList()
        {
            return ConfigurationManager.GetSection("Services") as List<ServiceModel>;
        }

        private int GetSelectedIndexService()
        {
            return (int)cboService.SelectedValue;
        }

        private string GetSelectedServiceName()
        {
            return mLstObjServices.Where(x => x.Index == GetSelectedIndexService()).Select(x => x.Name).FirstOrDefault();
        }

        private void FinalizeThread()
        {
            if (mObjThread != null)
            {
                mObjThread.Join(1000);
                if (mObjThread.IsAlive)
                {
                    mObjThread.Abort();
                }
                mObjThread = null;
            }
        }

        #endregion
    }
}
