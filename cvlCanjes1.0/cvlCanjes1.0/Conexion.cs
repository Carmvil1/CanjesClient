using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Configuration;

namespace cvlCanjes1._0
{
    public partial class frmConexion : Form
    {
        
        public frmConexion()
        {
            InitializeComponent();
        }

        private void btn_ConexionHQ_Click(object sender, EventArgs e)
        {
            if (combo_BDHQ.Text == "")
            {
                MessageBox.Show("Necesitas escoger una Base de datos.");
            }
            else if (combo_BDHQ.Text != "CanjesHQ")
            {
                MessageBox.Show("No está escogiendo una BD válida");
            }
            else
            {
                string ipHQ = txbx_IPHQ.Text;
                string usuarioHQ = txbx_UsuarioHQ.Text;
                string passwordHQ = txbx_PasswordHQ.Text;
                string bdHQ = combo_BDHQ.Text;
                try
                {
                    SqlConnection conexionBD = new SqlConnection("Data source = " + ipHQ + ", 1433; Initial Catalog = " + bdHQ + "; User Id = " + usuarioHQ + "; Password = " + passwordHQ + "; Integrated Security=false");
                    conexionBD.Open();
                    label_IPHQ.Enabled = false;
                    txbx_IPHQ.Enabled = false;
                    label_UsuarioHQ.Enabled = false;
                    txbx_UsuarioHQ.Enabled = false;
                    label_PasswordHQ.Enabled = false;
                    txbx_PasswordHQ.Enabled = false;
                    label_BDHQ.Enabled = false;
                    combo_BDHQ.Enabled = false;
                    btn_ConexionHQ.Enabled = false;
                    conexionBD.Close();
                    grupo_ConexionSucursal.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No hay conexión a la IP\n\n" + ex);
                }
            }
        }

        private void combo_BDHQ_Click(object sender, EventArgs e)
        {
            if (txbx_IPHQ.Text == "" || txbx_UsuarioHQ.Text == "" || txbx_PasswordHQ.Text == "")
            {
                MessageBox.Show("Revisar todos los datos esten llenos");
            }
            else
            {
                combo_BDHQ.Items.Clear();
                string ipHQ = txbx_IPHQ.Text;
                string usuarioHQ = txbx_UsuarioHQ.Text;
                string passwordHQ = txbx_PasswordHQ.Text;
                try
                {
                    SqlConnection conexionMaster = new SqlConnection("Data source = " + ipHQ + ", 1433; Initial Catalog = master; User Id = " + usuarioHQ + "; Password = " + passwordHQ + "; Integrated Security=false");
                    SqlCommand query = new SqlCommand("Select name from sys.databases where name != 'master' and name != 'tempdb' and name != 'model' and name != 'msdb' and name != 'ReportServer' and name != 'ReportServerTempDB'", conexionMaster);
                    conexionMaster.Open();
                    SqlDataReader reader = query.ExecuteReader();
                    while (reader.Read())
                    {
                        combo_BDHQ.Items.Add(reader["name"].ToString());
                    }
                    conexionMaster.Close();
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No hay conexión a la IP\n\n" + ex);
                }
            }
        }

        private void combo_BDSC_Click(object sender, EventArgs e)
        {
            if (txbx_IPSC.Text == "" || txbx_UsuarioSC.Text == "" || txbx_PasswordSC.Text == "")
            {
                MessageBox.Show("Revisar todos los datos esten llenos.");
            }
            else
            {
                combo_BDSC.Items.Clear();
                string ipSC = txbx_IPSC.Text;
                string usuarioSC = txbx_UsuarioSC.Text;
                string passwordSC = txbx_PasswordSC.Text;
                try
                {
                    SqlConnection conexionMaster = new SqlConnection("Data source = " + ipSC + ", 1433; Initial Catalog = master; User Id = " + usuarioSC + "; Password = " + passwordSC + "; Integrated Security=false");
                    SqlCommand query = new SqlCommand("Select name from sys.databases where name != 'master' and name != 'tempdb' and name != 'model' and name != 'msdb' and name != 'ReportServer' and name != 'ReportServerTempDB'", conexionMaster);
                    conexionMaster.Open();
                    SqlDataReader reader = query.ExecuteReader();
                    while (reader.Read())
                    {
                        combo_BDSC.Items.Add(reader["name"].ToString());
                    }
                    conexionMaster.Close();
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No hay conexión a la IP\n\n" + ex);
                }
            }
        }

        public string recogerNum = "null";
        private void btn_ConexionSC_Click(object sender, EventArgs e)
        {
            if (combo_BDSC.Text == "")
            {
                MessageBox.Show("Necesitas escoger una Base de datos.");
            }
            else
            {
                string ipSC = txbx_IPSC.Text;
                string usuarioSC = txbx_UsuarioSC.Text;
                string passwordSC = txbx_PasswordSC.Text;
                string bdSC = combo_BDSC.Text;
                try
                {
                    SqlConnection conexionSC = new SqlConnection("Data source = " + ipSC + ", 1433; Initial Catalog = " + bdSC + "; User Id = " + usuarioSC + "; Password = " + passwordSC + "; Integrated Security=false");
                    SqlCommand query = new SqlCommand("USE " + bdSC + " Select StoreID from Configuration", conexionSC);
                    conexionSC.Open();
                    SqlDataReader reader = query.ExecuteReader();
                    while (reader.Read())
                    {
                        recogerNum = reader["StoreID"].ToString();
                    }
                    label_IPSC.Enabled = false;
                    txbx_IPSC.Enabled = false;
                    label_UsuarioSC.Enabled = false;
                    txbx_UsuarioSC.Enabled = false;
                    label_PasswordSC.Enabled = false;
                    txbx_PasswordSC.Enabled = false;
                    label_BDSC.Enabled = false;
                    combo_BDSC.Enabled = false;
                    btn_ConexionSC.Enabled = false;
                    conexionSC.Close();
                    reader.Close();
                    btnGuardar.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No hay conexión con una Base de Datos válida.\n\n" + ex);
                }
            }
        }

        private void frmConexion_Load(object sender, EventArgs e)
        {

        }
        
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //string storeidbdCanjes = ConfigurationManager.AppSettings["storeidbdCanjes"];
            string ipbdCanjes = txbx_IPHQ.Text;
            string userbdCanjes = txbx_UsuarioHQ.Text;
            string passbdCanjes = txbx_PasswordHQ.Text;
            string namebdCanjes = combo_BDHQ.Text;
            string storeidbdSucursal = recogerNum;
            string ipbdSucursal = txbx_IPSC.Text;
            string userbdSucursal = txbx_UsuarioSC.Text;
            string passbdSucursal = txbx_PasswordSC.Text;
            string namebdSucursal = combo_BDSC.Text;

            XmlDocument appconfigxml = new XmlDocument();
            appconfigxml.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            foreach (XmlElement elementoxelemento in appconfigxml.DocumentElement)
            {
                if (elementoxelemento.Name.Equals("appSettings"))
                {
                    foreach (XmlNode keyxkey in elementoxelemento.ChildNodes)
                    {
                        if (keyxkey.Attributes[0].Value == "ipbdCanjes")
                        {
                            keyxkey.Attributes[1].Value = ipbdCanjes;
                        }
                        if (keyxkey.Attributes[0].Value == "userbdCanjes")
                        {
                            keyxkey.Attributes[1].Value = userbdCanjes;
                        }
                        if (keyxkey.Attributes[0].Value == "passbdCanjes")
                        {
                            keyxkey.Attributes[1].Value = passbdCanjes;
                        }
                        if (keyxkey.Attributes[0].Value == "namebdCanjes")
                        {
                            keyxkey.Attributes[1].Value = namebdCanjes;
                        }
                        if (keyxkey.Attributes[0].Value == "storeidbdSucursal")
                        {
                            keyxkey.Attributes[1].Value = storeidbdSucursal;
                        }
                        if (keyxkey.Attributes[0].Value == "ipbdSucursal")
                        {
                            keyxkey.Attributes[1].Value = ipbdSucursal;
                        }
                        if (keyxkey.Attributes[0].Value == "userbdSucursal")
                        {
                            keyxkey.Attributes[1].Value = userbdSucursal;
                        }
                        if (keyxkey.Attributes[0].Value == "passbdSucursal")
                        {
                            keyxkey.Attributes[1].Value = passbdSucursal;
                        }
                        if (keyxkey.Attributes[0].Value == "namebdSucursal")
                        {
                            keyxkey.Attributes[1].Value = namebdSucursal;
                        }
                    }
                }
            }
            appconfigxml.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            ConfigurationManager.RefreshSection("appSettings");







            /*
            TextWriter Config1txt = new StreamWriter("C:/Program Files (x86)/Carmvil/Canjes Client/Config/config1.txt");
            Config1txt.WriteLine(txbx_IPHQ.Text);
            TextWriter Config2txt = new StreamWriter("C:/Program Files (x86)/Carmvil/Canjes Client/Config/config2.txt");
            Config2txt.WriteLine(txbx_UsuarioHQ.Text);
            TextWriter Config3txt = new StreamWriter("C:/Program Files (x86)/Carmvil/Canjes Client/Config/config3.txt");
            Config3txt.WriteLine(txbx_PasswordHQ.Text);
            TextWriter Config4txt = new StreamWriter("C:/Program Files (x86)/Carmvil/Canjes Client/Config/config4.txt");
            Config4txt.WriteLine(combo_BDHQ.Text);
            Config1txt.Close();
            Config2txt.Close();
            Config3txt.Close();
            Config4txt.Close();
            TextWriter Config5_0txt = new StreamWriter("C:/Program Files (x86)/Carmvil/Canjes Client/Config/config5_0.txt");
            Config5_0txt.WriteLine(recogerNum);
            TextWriter Config5txt = new StreamWriter("C:/Program Files (x86)/Carmvil/Canjes Client/Config/config5.txt");
            Config5txt.WriteLine(txbx_IPSC.Text);
            TextWriter Config6txt = new StreamWriter("C:/Program Files (x86)/Carmvil/Canjes Client/Config/config6.txt");
            Config6txt.WriteLine(txbx_UsuarioSC.Text);
            TextWriter Config7txt = new StreamWriter("C:/Program Files (x86)/Carmvil/Canjes Client/Config/config7.txt");
            Config7txt.WriteLine(txbx_PasswordSC.Text);
            TextWriter Config8txt = new StreamWriter("C:/Program Files (x86)/Carmvil/Canjes Client/Config/config8.txt");
            Config8txt.WriteLine(combo_BDSC.Text);
            Config5_0txt.Close();
            Config5txt.Close();
            Config6txt.Close();
            Config7txt.Close();
            Config8txt.Close();*/
            MessageBox.Show("Se requiere reiniciar la aplicación para efectuar los cambios");
            Application.Restart();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
