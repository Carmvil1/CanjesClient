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
using System.Configuration;

namespace cvlCanjes1._0
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }        
        private void frmLogin_Load(object sender, EventArgs e)
        {
            string storeidbdCanjes = ConfigurationManager.AppSettings["storeidbdCanjes"];
            string ipbdCanjes = ConfigurationManager.AppSettings["ipbdCanjes"];
            string userbdCanjes = ConfigurationManager.AppSettings["userbdCanjes"];
            string passbdCanjes = ConfigurationManager.AppSettings["passbdCanjes"];
            string namebdCanjes = ConfigurationManager.AppSettings["namebdCanjes"];
            string storeidbdSucursal = ConfigurationManager.AppSettings["storeidbdSucursal"];
            string ipbdSucursal = ConfigurationManager.AppSettings["ipbdSucursal"];
            string userbdSucursal = ConfigurationManager.AppSettings["userbdSucursal"];
            string passbdSucursal = ConfigurationManager.AppSettings["passbdSucursal"];
            string namebdSucursal = ConfigurationManager.AppSettings["namebdSucursal"];
            MessageBox.Show("storeidbdCanjes: " + storeidbdCanjes + "\nipbdCanjes: " + ipbdCanjes + "\nuserbdCanjes: " + userbdCanjes
                + "\npassbdCanjes: " + passbdCanjes + "\nnamebdCanjes: " + namebdCanjes + "\nstoreidbdSucursal: " + storeidbdSucursal +
                "\nipbdSucursal: " + ipbdSucursal + "\nuserbdSucursal: " + userbdSucursal +
                "\npassbdSucursal: " + passbdSucursal + "\nnamebdSucursal: " + namebdSucursal);


            label_getIP.Text = ipbdCanjes;
            label_getUser.Text = userbdCanjes;
            label_getPass.Text = passbdCanjes;
            label_getBD.Text = namebdCanjes;
            label_getNumSucursal.Text = storeidbdSucursal;
            label_getIPSC.Text = ipbdSucursal;
            label_getUserSC.Text = userbdSucursal;
            label_getPassSC.Text = passbdSucursal;
            label_getBDSC.Text = namebdSucursal;



            /*StreamReader Config1txt = new StreamReader("C:/Program Files (x86)/Carmvil/Canjes Client/Config/config1.txt");
            StreamReader Config2txt = new StreamReader("C:/Program Files (x86)/Carmvil/Canjes Client/Config/config2.txt");
            StreamReader Config3txt = new StreamReader("C:/Program Files (x86)/Carmvil/Canjes Client/Config/config3.txt");
            StreamReader Config4txt = new StreamReader("C:/Program Files (x86)/Carmvil/Canjes Client/Config/config4.txt");
            StreamReader Config5_0txt = new StreamReader("C:/Program Files (x86)/Carmvil/Canjes Client/Config/config5_0.txt");
            StreamReader Config5txt = new StreamReader("C:/Program Files (x86)/Carmvil/Canjes Client/Config/config5.txt");
            StreamReader Config6txt = new StreamReader("C:/Program Files (x86)/Carmvil/Canjes Client/Config/config6.txt");
            StreamReader Config7txt = new StreamReader("C:/Program Files (x86)/Carmvil/Canjes Client/Config/config7.txt");
            StreamReader Config8txt = new StreamReader("C:/Program Files (x86)/Carmvil/Canjes Client/Config/config8.txt");
            
            label_getIP.Text = Config1txt.ReadLine();
            Config1txt.Close();
            label_getUser.Text = Config2txt.ReadLine();
            Config2txt.Close();
            label_getPass.Text = Config3txt.ReadLine();
            Config3txt.Close();
            label_getBD.Text = Config4txt.ReadLine();
            Config4txt.Close();
            label_getNumSucursal.Text = Config5_0txt.ReadLine();
            Config5_0txt.Close();
            label_getIPSC.Text = Config5txt.ReadLine();
            Config5txt.Close();
            label_getUserSC.Text = Config6txt.ReadLine();
            Config6txt.Close();
            label_getPassSC.Text = Config7txt.ReadLine();
            Config7txt.Close();
            label_getBDSC.Text = Config8txt.ReadLine();
            Config8txt.Close();*/


            string numSucursal = label_getNumSucursal.Text;
            string ipCanjes = label_getIP.Text;
            string sa = label_getUser.Text;
            string Fs1200 = label_getPass.Text;
            string CanjesHQ = label_getBD.Text;
            try
            {
                SqlConnection conexionSC = new SqlConnection("Data source = " + ipCanjes + ", 1433; Initial Catalog = " + CanjesHQ + "; User Id = " + sa + "; Password = " + Fs1200 + "; Integrated Security=false");
                SqlCommand query = new SqlCommand("USE "+CanjesHQ+" Select nombre_Sucursal from Sucursal where num_Sucursal = "+numSucursal+";", conexionSC);
                conexionSC.Open();
                SqlDataReader reader = query.ExecuteReader();
                string recogerNombreSucursal = "No se ha establecido ninguna conexión con la sucursal";
                while (reader.Read())
                {
                    recogerNombreSucursal = reader["nombre_Sucursal"].ToString();
                }
                label_SucursalLinkeada.Text = recogerNombreSucursal;  
                conexionSC.Close();
                reader.Close();
                traerImagenGlobal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No hay conexión a la IP.\n\n" + ex);
            }
            if (label_SucursalLinkeada.Text.Length == 0)
            {
                label_SucursalLinkeada.Text = "Configure una conexión válida.";
            }
        }

        private void traerImagenGlobal()
        {
            string numSucursal = label_getNumSucursal.Text;
            string ipCanjes = label_getIP.Text;
            string sa = label_getUser.Text;
            string Fs1200 = label_getPass.Text;
            string CanjesHQ = label_getBD.Text;
            try
            {
                SqlConnection conexionCanjes = new SqlConnection("Data source = " + ipCanjes + ", 1433; Initial Catalog = " + CanjesHQ + "; User Id = " + sa + "; Password = " + Fs1200 + "; Integrated Security=false");
                SqlCommand traerImagen = new SqlCommand("Use " + CanjesHQ + " Select image_GF from GlobalFile where id_GF = '1'", conexionCanjes);
                SqlDataAdapter adapta = new SqlDataAdapter(traerImagen);
                DataSet datasetAdapta = new DataSet("GlobalFile");
                byte[] MiImagen = new byte[0];

                adapta.Fill(datasetAdapta, "GlobalFile");

                DataRow myRow = datasetAdapta.Tables["GlobalFile"].Rows[0];
                MiImagen = (byte[])myRow["image_GF"];
                MemoryStream ms = new MemoryStream(MiImagen);
                picture_ImagenServer.Image = Image.FromStream(ms);
                conexionCanjes.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar el buffer.\n\n" + ex);
            }
        }

        private void btn_Conexion_Click(object sender, EventArgs e)
        {
            Form formTemp = new Form();
            using (frmConexion formConexion = new frmConexion())
            {
                formTemp.StartPosition = FormStartPosition.Manual;
                formTemp.FormBorderStyle = FormBorderStyle.None;
                formTemp.Opacity = .70d;
                formTemp.BackColor = Color.Black;
                formTemp.WindowState = FormWindowState.Maximized;
                formTemp.TopMost = true;
                formTemp.Location = this.Location;
                formTemp.ShowInTaskbar = false;
                formTemp.Show();

                formConexion.Owner = formTemp;
                formConexion.ShowDialog();
                formTemp.Dispose();
            }
        }
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string ipCnx = label_getIPSC.Text;
            string bdCnx = label_getBDSC.Text;
            string userCnx = label_getUserSC.Text;
            string passCnx = label_getPassSC.Text;
            string usuarioSC = txbx_usuario.Text;
            string passwordSC = txbx_password.Text;
            try
            {
                SqlConnection conexionBD = new SqlConnection("Data source = " + ipCnx + ", 1433; Initial Catalog = " + bdCnx + "; User Id = " + userCnx + "; Password = " + passCnx + "; Integrated Security=false");
                SqlCommand query = new SqlCommand("Use "+bdCnx+" Select SecurityLevel, Name from Cashier where Number = '"+usuarioSC+"' and password = '"+passwordSC+"'", conexionBD);
                conexionBD.Open();
                SqlDataReader reader = query.ExecuteReader();
                bool founded = false;
                int securityLevel = 1000;
                string name = "";
                while (reader.Read())
                {
                    securityLevel = Convert.ToInt32(reader["SecurityLevel"].ToString());
                    name = reader["Name"].ToString();
                    founded = true;
                }
                conexionBD.Close();
                reader.Close();
                if (founded == true)
                {
                    if ((securityLevel != 0) && (securityLevel != 1) && (securityLevel != 2) && (securityLevel != 3))
                    {
                        MessageBox.Show("Necesita privilegios de Administrador para ingresar.");
                    }
                    else
                    {
                        frmMainMenu formPrincipal = new frmMainMenu();
                        formPrincipal.label_Name.Text = name;   
                        formPrincipal.Show();
                        this.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Porfavor verifique sus credenciales.");
                    txbx_password.Clear();
                    txbx_usuario.Focus();
                    txbx_usuario.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No hay conexión a la IP\n\n" + ex);
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_Maximizar_Click(object sender, EventArgs e)
        {            
            if (this.Height == 680)
            {
                this.Width = 1024;
                this.Height = 768;
                int x = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
                int y = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
                this.Location = new Point(x, y);
            }
            else if (this.Height == 768)
            {
                this.Width = 957;
                this.Height = 680;
                int x = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
                int y = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
                this.Location = new Point(x, y);
            }
        }

        private void btn_Minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

       

        private void txbx_usuario_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
                txbx_password.Focus();
                txbx_password.SelectAll();
            }
        }

        private void txbx_password_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == (Keys.Tab | Keys.Shift))
            {
                e.IsInputKey = true;
                txbx_usuario.Focus();
                txbx_usuario.SelectAll();
            }
        }

        private void txbx_usuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txbx_password.Focus();
                txbx_password.SelectAll();
            }
        }

        private void txbx_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnEntrar_Click(sender, e);
            }
        }
    }
}
