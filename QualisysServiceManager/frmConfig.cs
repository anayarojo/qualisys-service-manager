using QualisysServiceManager.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace QualisysServiceManager
{
    public partial class frmConfig : Form
    {
        private string mStrConfigPath;
        private XDocument mObjConfigs;

        public frmConfig(string pStrConfigPath)
        {
            InitializeComponent();

            mStrConfigPath = pStrConfigPath;
            mObjConfigs = GetConfigs(mStrConfigPath);
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            LoadAppSettings();
            LoadConnectionStrings();
        }

        private void LoadAppSettings()
        {
            int lIntIndex = 0;

            try
            {
                foreach (XElement lObjConfig in mObjConfigs.GetSettings())
                {
                    Label lObjLabel = new Label();
                    lObjLabel.Location = new Point(0, 47 * lIntIndex + 10);
                    lObjLabel.Height = 22;
                    lObjLabel.Width = 350;
                    lObjLabel.Text = lObjConfig.Attribute("key").Value;

                    TextBox lObjTextBox = new TextBox();
                    lObjTextBox.Location = new Point(3, 47 * lIntIndex + 32);
                    lObjTextBox.Height = 22;
                    lObjTextBox.Width = 350;
                    lObjTextBox.Margin = new Padding(0, 0, 0, 10);
                    lObjTextBox.Text = lObjConfig.Attribute("value").Value;

                    pnlAppSettingsContainer.Controls.Add(lObjLabel);
                    pnlAppSettingsContainer.Controls.Add(lObjTextBox);

                    lIntIndex++;
                }
            }
            catch
            {
                Label lblEmpty = new Label();
                lblEmpty.Dock = DockStyle.Fill;
                lblEmpty.AutoSize = false;
                lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
                lblEmpty.Text = "Sin configuraciones disponibles";
                pnlAppSettingsContainer.Controls.Add(lblEmpty);
            }
        }

        private void LoadConnectionStrings()
        {
            int lIntIndex = 0;

            try
            {
                foreach (XElement lObjConnectionString in mObjConfigs.GetConnectionStrings())
                {
                    // NAME

                    Label lblName = new Label();
                    lblName.Location = new Point(0, 180 * lIntIndex + 10);
                    lblName.Height = 20;
                    lblName.Width = 350;
                    lblName.Text = "Name";

                    TextBox txtName = new TextBox();
                    txtName.Location = new Point(3, 180 * lIntIndex + 30);
                    txtName.Height = 22;
                    txtName.Width = 350;
                    txtName.Margin = new Padding(0, 0, 0, 10);
                    txtName.Text = lObjConnectionString.Attribute("name").Value;

                    // CONNECTION STRING

                    Label lblConnectionString = new Label();
                    lblConnectionString.Location = new Point(0, 180 * lIntIndex + 60);
                    lblConnectionString.Height = 20;
                    lblConnectionString.Width = 350;
                    lblConnectionString.Text = "Connection String";

                    TextBox txtConnectionString = new TextBox();
                    txtConnectionString.Location = new Point(3, 180 * lIntIndex + 80);
                    txtConnectionString.Height = 50;
                    txtConnectionString.Width = 350;
                    txtConnectionString.Margin = new Padding(0, 0, 0, 10);
                    txtConnectionString.Multiline = true;
                    txtConnectionString.Text = lObjConnectionString.Attribute("connectionString").Value;

                    // PROVIDER

                    Label lblProvider = new Label();
                    lblProvider.Location = new Point(0, 180 * lIntIndex + 140);
                    lblProvider.Height = 20;
                    lblProvider.Width = 350;
                    lblProvider.Text = "Provider Name";

                    TextBox txtProvider = new TextBox();
                    txtProvider.Location = new Point(3, 180 * lIntIndex + 160);
                    txtProvider.Height = 22;
                    txtProvider.Width = 350;
                    txtProvider.Margin = new Padding(0, 0, 0, 10);
                    txtProvider.Text = lObjConnectionString.Attribute("providerName").Value;

                    // PANEL

                    pnlConnectionStringsContainer.Controls.Add(lblName);
                    pnlConnectionStringsContainer.Controls.Add(txtName);
                    pnlConnectionStringsContainer.Controls.Add(lblConnectionString);
                    pnlConnectionStringsContainer.Controls.Add(txtConnectionString);
                    pnlConnectionStringsContainer.Controls.Add(lblProvider);
                    pnlConnectionStringsContainer.Controls.Add(txtProvider);

                    lIntIndex++;
                }
            }
            catch
            {
                Label lblEmpty = new Label();
                lblEmpty.Dock = DockStyle.Fill;
                lblEmpty.AutoSize = false;
                lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
                lblEmpty.Text = "Sin configuraciones disponibles";
                pnlConnectionStringsContainer.Controls.Add(lblEmpty);
            }
        }

        private XDocument GetConfigs(string pStrConfigPath)
        {
            XDocument lObjResult = null;

            if (File.Exists(pStrConfigPath))
            {
                lObjResult = XDocument.Load(pStrConfigPath);
            }
            else
            {
                throw new Exception("No se encontró el documento XML.");
            }

            return lObjResult;
        }

        private void SetAppSettings()
        {
            List<Label> lLstObjLabels = pnlAppSettingsContainer.Controls.OfType<Label>().ToList();
            List<TextBox> lLstObjTextBoxes = pnlAppSettingsContainer.Controls.OfType<TextBox>().ToList();

            for (int i = 0; i < lLstObjLabels.Count; i++)
            {
                mObjConfigs.SetSetting(lLstObjLabels[i].Text, lLstObjTextBoxes[i].Text);
            }
        }

        private void SetConnectionStrings()
        {
            List<TextBox> lLstObjTextBoxes = pnlConnectionStringsContainer.Controls.OfType<TextBox>().ToList();
            List<XElement> lLstObjConnectionStrings = mObjConfigs.GetConnectionStrings().ToList();

            for (int i = 0; i < lLstObjConnectionStrings.Count; i++)
            {
                lLstObjConnectionStrings[i].Attribute("name").SetValue(lLstObjTextBoxes[i * 3].Text);
                lLstObjConnectionStrings[i].Attribute("connectionString").SetValue(lLstObjTextBoxes[i * 3 + 1].Text);
                lLstObjConnectionStrings[i].Attribute("providerName").SetValue(lLstObjTextBoxes[i * 3 + 2].Text);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SetAppSettings();
                SetConnectionStrings();
                mObjConfigs.Save(mStrConfigPath);

                MessageBox.Show("Los cambios se han guardado correctamente.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception lObjException)
            {
                MessageBox.Show(lObjException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
