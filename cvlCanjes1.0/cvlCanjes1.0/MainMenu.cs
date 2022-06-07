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
using System.Data.OleDb;
using System.Configuration;

namespace cvlCanjes1._0
{
    public partial class frmMainMenu : Form
    {
        public frmMainMenu()
        {
            InitializeComponent();
        }

        public bool counter;
        public int emptycounter;
        private void frmMainMenu_Load(object sender, EventArgs e)
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
                SqlCommand query = new SqlCommand("USE " + CanjesHQ + " Select nombre_Sucursal, telefono_Sucursal from Sucursal where num_Sucursal = " + numSucursal + ";", conexionSC);
                conexionSC.Open();
                SqlDataReader reader = query.ExecuteReader();
                string recogerNombreSucursal = "No se ha establecido ninguna conexión con la sucursal";
                string recogerCelularSucursal = "No se ha establecido ninguna conexión con la sucursal";
                while (reader.Read())
                {
                    recogerNombreSucursal = reader["nombre_Sucursal"].ToString();
                    recogerCelularSucursal = reader["telefono_Sucursal"].ToString();
                }
                label_SucursalLinkeada.Text = recogerNombreSucursal;
                label_celSucursal.Text = recogerCelularSucursal;
                conexionSC.Close();
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No hay conexión a la IP. \n\n" + ex);
            }
            counter = false;
            emptycounter = 0;
            //btn_Actualizar_Click(sender, e);
        }

        private void limpiarGrupoEditor()
        {
            grupo_Editor.Enabled = false;
            label_descargue.Text = "";
            label_producto.Text = "";
            label_nombreAuditor.Text = "";
            label_estadoAuditor.Text = "";
            label_fechaAuditado.Text = "";
            txbx_autPlataforma.Text = "";
            txbx_factura1.Text = "";
            txbx_factura2.Text = "";
            txbx_factura3.Text = "";
            txbx_factura4.Text = "";
            txbx_justificante.Text = "";
            label_estadoSucursal.Text = "";
            counter = true;
        }

        public DataTable dtTemporal = new DataTable();
        private void cargarDGV()
        {
            int numSucursal = Convert.ToInt32(label_getNumSucursal.Text);
            string ipCanjes = label_getIP.Text;
            string bdCanjes = label_getBD.Text;
            string userCanjes = label_getUser.Text;
            string passCanjes = label_getPass.Text;
            SqlConnection conexionHQ = new SqlConnection("Data source = " + ipCanjes + ", 1433; Initial Catalog = " + bdCanjes + "; User Id = " + userCanjes + "; Password = " + passCanjes + "; Integrated Security=false");
            conexionHQ.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter("USE "+bdCanjes+" Select vES.descripcion_EstadoSucursal AS 'ESTADO', vCanje.descargue_Canje AS 'DESCARGUE', vCanje.fechaDescargue_Canje AS 'FECHA DE DESCARGUE', vCanje.producto_Canje AS 'PRODUCTO', vCanje.cantidad_Canje AS 'CANTIDAD', vCanje.laboratorio_Canje AS 'LABORATORIO', vCanje.vendedor_Canje AS 'VENDEDOR', vCanje.nombreCliente_Canje AS 'CLIENTE', vCanje.ingresadopor_Canje AS 'INGRESADO POR', vCanje.lastUpdate_Canje AS 'ULTIMA ACTUALIZACION', vCanje.id_Canje AS 'IDENTIFICADOR' from Canje vCanje FULL JOIN EstadoSucursal vES ON vCanje.id_EstadoSucursal = vES.id_EstadoSucursal where id_Sucursal = '" + numSucursal + "' and fechaDescargue_Canje >= DATEADD(day,-10,GETDATE()) and fechaDescargue_Canje <= getdate() ORDER BY fechaDescargue_Canje DESC", conexionHQ);
            adaptador.Fill(dtTemporal);
            datagrid_Canjes.DataSource = dtTemporal;
            limpiarGrupoEditor();
        }

        private void datagrid_Canjes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            emptycounter = 0;
            label_idCanje.Text = datagrid_Canjes.SelectedCells[10].Value.ToString();
            label_descargue.Text = datagrid_Canjes.SelectedCells[1].Value.ToString();
            label_producto.Text = datagrid_Canjes.SelectedCells[3].Value.ToString();

            int get_idCanje = Convert.ToInt32(label_idCanje.Text);
            string ipCanjes = label_getIP.Text;
            string bdCanjes = label_getBD.Text;
            string userCanjes = label_getUser.Text;
            string passCanjes = label_getPass.Text;

            try
            {
                SqlConnection conexionBDCanjes = new SqlConnection("Data source = " + ipCanjes + ", 1433; Initial Catalog = " + bdCanjes + "; User Id = " + userCanjes + "; Password = " + passCanjes + "; Integrated Security=false");
                conexionBDCanjes.Open();
                SqlCommand traerDatos = new SqlCommand("USE " + bdCanjes + " Select C.id_EstadoAuditor AS IDESTADOAUDITOR, E.descripcion_EstadoAuditor AS ESTADOAUDITOR, C.fechaAuditado_Canje AS FECHAAUDITADO, S.id_EstadoSucursal AS IDESTADOSUCURSAL ,S.descripcion_EstadoSucursal AS ESTADOSUCURSAL, C.autPlataforma_Canje AS AUTORIZADO,  C.factura1_Canje AS FACTURA1,  C.factura2_Canje AS FACTURA2,  C.factura3_Canje AS FACTURA3,  C.factura4_Canje AS FACTURA4,  C.id_Auditor AS IDAUDITOR, A.nombres_Auditor AS NOMBREAUDITOR, C.justificante_Canje AS JUSTIFICANTE, C.ingresadopor_Canje AS INGRESADOPOR FROM Canje C FULL JOIN Auditor A ON C.id_Auditor = A.id_Auditor FULL JOIN EstadoAuditor E ON C.id_EstadoAuditor = E.id_EstadoAuditor FULL JOIN EstadoSucursal S ON C.id_EstadoSucursal = S.id_EstadoSucursal WHERE C.id_Canje = '" + get_idCanje + "'", conexionBDCanjes);
                SqlDataReader readerDatos = traerDatos.ExecuteReader();

                int idEstadoAuditor;
                string EstadoAuditor;
                string stringFecha;
                DateTime fechaAuditadoCanje;
                int idEstadoSucursal;
                string EstadoSucursal;
                string autoPlataforma;
                int fact1;
                int fact2;
                int fact3;
                int fact4;
                int idAuditor;
                string nombresAuditor;
                string justificante;
                string ingresador;

                while (readerDatos.Read())
                {
                    idEstadoAuditor = Convert.ToInt32(readerDatos["IDESTADOAUDITOR"]);
                    EstadoAuditor = readerDatos["ESTADOAUDITOR"].ToString();
                    stringFecha = readerDatos["FECHAAUDITADO"].ToString();
                    fechaAuditadoCanje = Convert.ToDateTime(stringFecha);
                    idEstadoSucursal = Convert.ToInt32(readerDatos["IDESTADOSUCURSAL"]);
                    if (idEstadoSucursal == 4)
                    {
                        grupo_Editor.Enabled = false;
                    }
                    if (idEstadoSucursal != 4)
                    {
                        grupo_Editor.Enabled = true;
                    }
                    EstadoSucursal = readerDatos["ESTADOSUCURSAL"].ToString();
                    autoPlataforma = readerDatos["AUTORIZADO"].ToString();
                    fact1 = Convert.ToInt32(readerDatos["FACTURA1"]);
                    fact2 = Convert.ToInt32(readerDatos["FACTURA2"]);
                    fact3 = Convert.ToInt32(readerDatos["FACTURA3"]);
                    fact4 = Convert.ToInt32(readerDatos["FACTURA4"]);
                    idAuditor = Convert.ToInt32(readerDatos["IDAUDITOR"]);
                    nombresAuditor = readerDatos["NOMBREAUDITOR"].ToString();
                    justificante = readerDatos["JUSTIFICANTE"].ToString();
                    ingresador = readerDatos["INGRESADOPOR"].ToString();

                    label_idAuditor.Text = idAuditor.ToString();
                    label_nombreAuditor.Text = nombresAuditor.ToString();
                    label_idEstadoAuditor.Text = idEstadoAuditor.ToString();
                    label_estadoAuditor.Text = EstadoAuditor.ToString();
                    label_fechaAuditado.Text = fechaAuditadoCanje.ToString();
                    if (label_fechaAuditado.Text == "01/01/2000 0:00:00")
                    {
                        label_fechaAuditado.Text = "SIN AUDITAR";
                    }
                    label_idEstadoSucursal.Text = idEstadoSucursal.ToString();
                    label_estadoSucursal.Text = EstadoSucursal.ToString();
                    txbx_autPlataforma.Text = autoPlataforma.ToString();
                    if (txbx_autPlataforma.Text == "0")
                    {
                        txbx_autPlataforma.Clear();
                    }
                    txbx_factura1.Text = fact1.ToString();
                    if (txbx_factura1.Text == "0")
                    {
                        txbx_factura1.Clear();
                    }
                    txbx_factura2.Text = fact2.ToString();
                    if (txbx_factura2.Text == "0")
                    {
                        txbx_factura2.Clear();
                    }
                    txbx_factura3.Text = fact3.ToString();
                    if (txbx_factura3.Text == "0")
                    {
                        txbx_factura3.Clear();
                    }
                    txbx_factura4.Text = fact4.ToString();
                    if (txbx_factura4.Text == "0")
                    {
                        txbx_factura4.Clear();
                    }
                    txbx_justificante.Text = justificante.ToString();
                    if (txbx_justificante.Text == "0")
                    {
                        txbx_justificante.Clear();
                    }
                    label_ingresadoPor.Text = ingresador.ToString();
                }
                conexionBDCanjes.Close();
                btn_changeEstSucursal.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No hay conexión a la IP. \n\n" + ex);
            }
        }

        private void combo_estadoSucursal_Click(object sender, EventArgs e)
        {
            llenarComboEnvio();
        }
        private void llenarComboEnvio()
        {
            combo_estadoSucursal.Items.Clear();
            int get_idCanje = Convert.ToInt32(label_idCanje.Text);
            string ipCanjes = label_getIP.Text;
            string bdCanjes = label_getBD.Text;
            string userCanjes = label_getUser.Text;
            string passCanjes = label_getPass.Text;

            try
            {
                SqlConnection conexionCanjes = new SqlConnection("Data source = " + ipCanjes + ", 1433; Initial Catalog = " + bdCanjes + "; User Id = " + userCanjes + "; Password = " + passCanjes + "; Integrated Security=false");
                SqlCommand query = new SqlCommand("Use "+bdCanjes+" Select descripcion_EstadoSucursal from EstadoSucursal", conexionCanjes);
                conexionCanjes.Open();
                SqlDataReader reader = query.ExecuteReader();
                while (reader.Read())
                {
                    combo_estadoSucursal.Items.Add(reader["descripcion_EstadoSucursal"].ToString());
                }
                conexionCanjes.Close();
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No hay conexión a la IP. \n\n" + ex);
            }
        }
        
        public void validarVacios()
        {
            emptycounter = 0;
            if (txbx_justificante.Text.Length == 0)
            {
                emptycounter = emptycounter + 1;
                labelatrk_Justificante.Visible = true;
                txbx_justificante.Focus();
            }
            if (txbx_factura4.Text.Length == 0)
            {
                emptycounter = emptycounter + 1;
                labelatrk_Factura4.Visible = true;
                txbx_factura4.Focus();
            }
            if (txbx_factura3.Text.Length == 0)
            {
                emptycounter = emptycounter + 1;
                labelatrk_Factura3.Visible = true;
                txbx_factura3.Focus();
            }
            if (txbx_factura2.Text.Length == 0)
            {
                emptycounter = emptycounter + 1;
                labelatrk_Factura2.Visible = true;
                txbx_factura2.Focus();
            }
            if (txbx_factura1.Text.Length == 0)
            {
                emptycounter = emptycounter + 1;
                labelatrk_Factura1.Visible = true;
                txbx_factura1.Focus();
            }
            if (txbx_autPlataforma.Text.Length == 0)
            {
                emptycounter = emptycounter + 1;
                labelatrk_AutPlataforma.Visible = true;
                txbx_autPlataforma.Focus();
            }       
        }
        private void grabarDatos(string ipCanjes, string bdCanjes, string userCanjes, string passCanjes, int get_idCanje)
        {
            if (combo_estadoSucursal.Text == "ENVIADO")
            {
                validarVacios();
                if (emptycounter == 0)
                {
                    try
                    {
                        string autoPlataforma = txbx_autPlataforma.Text;
                        int fact1 = Convert.ToInt32(txbx_factura1.Text);
                        int fact2 = Convert.ToInt32(txbx_factura2.Text);
                        int fact3 = Convert.ToInt32(txbx_factura3.Text);
                        int fact4 = Convert.ToInt32(txbx_factura4.Text);
                        string justificante = txbx_justificante.Text;
                        int id_estSucursal = Convert.ToInt32(label_idEstadoAuditor.Text);
                        string ingresador = label_Name.Text;
                        DateTime lastupdate2 = DateTime.Now;

                        SqlConnection updateCanje = new SqlConnection("Data source = " + ipCanjes + ", 1433; Initial Catalog = " + bdCanjes + "; User Id = " + userCanjes + "; Password = " + passCanjes + "; Integrated Security=false");
                        SqlCommand query = new SqlCommand("USE "+bdCanjes+" UPDATE Canje SET autPlataforma_Canje = '" + autoPlataforma + "', factura1_Canje = '" + fact1 + "', factura2_Canje = '" + fact2 + "', factura3_Canje = '" + fact3 + "', factura4_Canje = '" + fact4 + "', justificante_Canje = '" + justificante + "', ingresadopor_Canje = '"+ingresador+ "', lastUpdate_Canje = '"+lastupdate2+"', id_EstadoSucursal = '4' WHERE id_Canje = '" + get_idCanje + "'", updateCanje);
                        updateCanje.Open();
                        SqlDataReader reader = query.ExecuteReader();
                        updateCanje.Close();
                        reader.Close();

                        grupo_Editor.Enabled = false;
                        int numSucursal = Convert.ToInt32(label_getNumSucursal.Text);
                        SqlConnection conexionHQ = new SqlConnection("Data source = " + ipCanjes + ", 1433; Initial Catalog = " + bdCanjes + "; User Id = " + userCanjes + "; Password = " + passCanjes + "; Integrated Security=false");
                        conexionHQ.Open();
                        SqlDataAdapter adaptador = new SqlDataAdapter("USE "+bdCanjes+" Select vES.descripcion_EstadoSucursal as 'ESTADO', vCanje.descargue_Canje as 'DESCARGUE', vCanje.fechaDescargue_Canje AS 'FECHA DE DESCARGUE', vCanje.producto_Canje 'PRODUCTO', vCanje.cantidad_Canje 'CANTIDAD', vCanje.laboratorio_Canje 'LABORATORIO', vCanje.vendedor_Canje as 'VENDEDOR', vCanje.nombreCliente_Canje 'CLIENTE', vCanje.ingresadopor_Canje AS 'INGRESADO POR', vCanje.lastUpdate_Canje AS 'ULTIMA ACTUALIZACION', vCanje.id_Canje AS 'IDENTIFICADOR' from Canje vCanje FULL JOIN EstadoSucursal vES ON vCanje.id_EstadoSucursal = vES.id_EstadoSucursal where id_Sucursal = '" + numSucursal + "' and fechaDescargue_Canje >= DATEADD(day,-10,GETDATE()) and fechaDescargue_Canje <= getdate() ORDER BY fechaDescargue_Canje DESC", conexionHQ);
                        DataTable dtTemporal = new DataTable();
                        adaptador.Fill(dtTemporal);
                        datagrid_Canjes.DataSource = dtTemporal;
                        MessageBox.Show("Canje grabado correctamente.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No hay conexión a la IP. \n\n" + ex);
                    }
                }
            }
            if (combo_estadoSucursal.Text == "SIN ENVIAR")
            {
                txbx_justificante.Focus();
            }
        }

        private void combo_estadoSucursal_SelectedValueChanged(object sender, EventArgs e)
        {
            label_estadoSucursal.Text = combo_estadoSucursal.Text;
            int get_idCanje = Convert.ToInt32(label_idCanje.Text);
            string ipCanjes = label_getIP.Text;
            string bdCanjes = label_getBD.Text;
            string userCanjes = label_getUser.Text;
            string passCanjes = label_getPass.Text;
            string descCombo = combo_estadoSucursal.Text;
            try
            {
                SqlConnection conexionCanjes = new SqlConnection("Data source = " + ipCanjes + ", 1433; Initial Catalog = " + bdCanjes + "; User Id = " + userCanjes + "; Password = " + passCanjes + "; Integrated Security=false");
                SqlCommand query = new SqlCommand("USE "+bdCanjes+" Select id_EstadoSucursal from EstadoSucursal where descripcion_EstadoSucursal = '"+descCombo+"'", conexionCanjes);
                conexionCanjes.Open();
                SqlDataReader reader = query.ExecuteReader();
                while (reader.Read())
                {
                    label_idEstadoSucursal.Text = reader["id_EstadoSucursal"].ToString();
                }
                conexionCanjes.Close();
                reader.Close();
                combo_estadoSucursal.Visible = false;
                btn_changeEstSucursal.Visible = true;
                grabarDatos(ipCanjes, bdCanjes, userCanjes, passCanjes, get_idCanje);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No hay conexión a la IP. \n\n" + ex);
            }
        }

        private void btn_changeEstSucursal_Click(object sender, EventArgs e)
        {
            btn_changeEstSucursal.Visible = false;
            combo_estadoSucursal.Visible = true;
            llenarComboEnvio();
            combo_estadoSucursal.DroppedDown = true;
        }

        private void btn_Actualizar_Click(object sender, EventArgs e)
        {
            int numSucursal = Convert.ToInt32(label_getNumSucursal.Text);
            string ip = label_getIPSC.Text;
            string bd = label_getBDSC.Text;
            string usuario = label_getUserSC.Text;
            string pass = label_getPassSC.Text;
            string ipCanjes = label_getIP.Text;
            string bdCanjes = label_getBD.Text;
            string userCanjes = label_getUser.Text;
            string passCanjes = label_getPass.Text;
            int DESCARGUE = 0;
            string NOMBRECLIENTE = "";
            try
            {
                SqlConnection conexionStore = new SqlConnection("Data source = " + ip + ", 1433; Initial Catalog = " + bd + "; User Id = " + usuario + "; Password = " + pass + "; Integrated Security=false");
                conexionStore.Open();
                SqlCommand consulta = new SqlCommand("USE "+bd+" SELECT vTransaction.TransactionNumber AS 'DESCARGUE', vCashier.Number AS 'CAJERO', vTransaction.[Time] AS 'FECHADESCARGUE', vTransaction.Comment AS 'NOMBRECLIENTE', vDepartment.[Name] AS 'DEPARTAMENTO', vItem.[Description] AS 'PRODUCTO', vTransactionEntry.Quantity AS 'CANTIDAD' FROM [Transaction] vTransaction  FULL JOIN [TransactionEntry] vTransactionEntry ON vTransaction.TransactionNumber = vTransactionEntry.TransactionNumber FULL JOIN [Item] vItem ON vTransactionEntry.ItemID = vItem.ID FULL JOIN [Store] vStore ON vTransaction.StoreID = vStore.ID FULL JOIN [Department] vDepartment ON vItem.DepartmentID = vDepartment.ID FULL JOIN [Cashier] vCashier ON vTransaction.CashierID = vCashier.ID WHERE vTransaction.Total = '0' and vTransactionEntry.Price = '0' and vItem.ItemLookupCode != '1001' and vItem.ItemLookupCode != '001001' and vItem.ItemLookupCode != '1000' and vTransaction.StoreID = vTransactionEntry.StoreID and vTransaction.StoreID = '" + numSucursal + "' ORDER BY FECHADESCARGUE DESC", conexionStore);
                SqlDataReader reader = consulta.ExecuteReader();

                string CAJERO = "";
                string fechaCadena = "";
                DateTime FECHADESCARGUE;
                string DEPARTAMENTO = ""; ;
                string PRODUCTO = "";
                int CANTIDAD = 0;
                DateTime lastupdate = DateTime.Now;

                SqlConnection conexionHQ = new SqlConnection("Data source = " + ipCanjes + ", 1433; Initial Catalog = " + bdCanjes + "; User Id = " + userCanjes + "; Password = " + passCanjes + "; Integrated Security=false");
                conexionHQ.Open();

                while (reader.Read())
                {
                    DESCARGUE = Convert.ToInt32(reader["DESCARGUE"]);
                    CAJERO = reader["CAJERO"].ToString();
                    fechaCadena = reader["FECHADESCARGUE"].ToString();
                    FECHADESCARGUE = Convert.ToDateTime(fechaCadena);
                    NOMBRECLIENTE = reader["NOMBRECLIENTE"].ToString();
                    DEPARTAMENTO = reader["DEPARTAMENTO"].ToString();
                    PRODUCTO = reader["PRODUCTO"].ToString();
                    CANTIDAD = Convert.ToInt32(reader["CANTIDAD"]);
                    int estadoAuditor = 1;
                    DateTime FECHAAUDITADO = Convert.ToDateTime("2000-01-01 00:00:00.000");
                    int fact1 = 0;
                    int fact2 = 0;
                    int fact3 = 0;
                    int fact4 = 0;
                    int idAuditor = 7;
                    string autoPlat = "0";
                    DateTime FECHAPAGOCD = Convert.ToDateTime("2000-01-01 00:00:00.000");
                    int requisaCD = 0;
                    int estadoSucursal = 2;
                    string justificar = "0";
                    string ingresador = "SIN INGRESAR";

                    SqlCommand buscarCoincidentes = new SqlCommand("USE " + bdCanjes + " EXEC Proc_BuscarExiste '" + DESCARGUE + "','" + numSucursal + "'", conexionHQ);
                    SqlDataReader buscador = buscarCoincidentes.ExecuteReader();

                    bool founded = false;
                    while (buscador.Read())
                    {
                        string get = buscador[""].ToString();
                        if (get == "1")
                        {
                            founded = true;
                        }
                        else if (get != "1")
                        {
                            founded = false;
                        }
                    }

                    if (founded == false)
                    {
                        buscador.Close();
                        string InsertCanje = "USE "+bdCanjes+" INSERT INTO Canje (id_Sucursal, descargue_Canje, vendedor_Canje, fechaDescargue_Canje, nombreCliente_Canje, laboratorio_Canje, producto_Canje, cantidad_Canje, lastUpdate_Canje, id_EstadoAuditor, fechaAuditado_Canje, factura1_Canje, factura2_Canje, factura3_Canje, factura4_Canje, id_Auditor, autPlataforma_Canje, fechaCD_Canje, requisaCD_Canje, id_EstadoSucursal, justificante_Canje, ingresadopor_Canje) VALUES ('" + numSucursal + "', '" + DESCARGUE + "', '" + CAJERO + "', '" + FECHADESCARGUE + "', '" + NOMBRECLIENTE + "', '" + DEPARTAMENTO + "', '" + PRODUCTO + "', '" + CANTIDAD + "', '" + lastupdate + "', '" + estadoAuditor + "', '" + FECHAAUDITADO + "', '" + fact1 + "', '" + fact2 + "', '" + fact3 + "', '" + fact4 + "', '" + idAuditor + "', '" + autoPlat + "', '" + FECHAPAGOCD + "', '" + requisaCD + "', '" + estadoSucursal + "', '"+justificar+"', '"+ingresador+"')";
                        SqlCommand insertIntoCanje = new SqlCommand(InsertCanje, conexionHQ);
                        insertIntoCanje.ExecuteNonQuery();
                    }
                    else if (founded == true)
                    {
                        buscador.Close();
                    }
                }
                reader.Close();
                conexionHQ.Close();
                conexionStore.Close();
                cargarDGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Corregir Error en:" + "\n" + DESCARGUE.ToString() + " = " + NOMBRECLIENTE.ToString() + "\n\n"+ ex);
            }
        }
        private void txbx_SearchFilter_TextChanged(object sender, EventArgs e)
        {
            if (txbx_SearchFilter.Text.Length == 0)
            {
                btn_SearchFilter_Click(sender, e);
            }
            foreach (DataGridViewRow row in datagrid_Canjes.Rows)
            {
                string valor = row.Cells[1].Value.ToString();
                string encontrar = "" + txbx_SearchFilter.Text + "";
                bool encontrado = valor.StartsWith(encontrar);
                if (encontrado)
                {
                    row.Selected = true;
                    datagrid_Canjes.CurrentCell = datagrid_Canjes.Rows[row.Index].Cells[0];
                    return;
                }
            }
        }

        private void btn_SearchFilter_Click(object sender, EventArgs e)
        {
            
            int numSucursal = Convert.ToInt32(label_getNumSucursal.Text);
            string ipCanjes = label_getIP.Text;
            string bdCanjes = label_getBD.Text;
            string userCanjes = label_getUser.Text;
            string passCanjes = label_getPass.Text;

            SqlConnection conexionHQ = new SqlConnection("Data source = " + ipCanjes + ", 1433; Initial Catalog = " + bdCanjes + "; User Id = " + userCanjes + "; Password = " + passCanjes + "; Integrated Security=false");
            conexionHQ.Open();
            //SqlDataAdapter adaptador2 = new SqlDataAdapter("USE "+bdCanjes+" Select vES.descripcion_EstadoSucursal as 'ESTADO', vCanje.descargue_Canje as 'DESCARGUE', vCanje.fechaDescargue_Canje AS 'FECHA DE DESCARGUE', vCanje.producto_Canje 'PRODUCTO', vCanje.cantidad_Canje 'CANTIDAD', vCanje.laboratorio_Canje 'LABORATORIO', vCanje.vendedor_Canje as 'VENDEDOR', vCanje.nombreCliente_Canje 'CLIENTE', vCanje.lastUpdate_Canje AS 'ULTIMA ACTUALIZACION', vCanje.id_Canje AS 'IDENTIFICADOR' from Canje vCanje FULL JOIN EstadoSucursal vES ON vCanje.id_EstadoSucursal = vES.id_EstadoSucursal where id_Sucursal = '" + numSucursal + "' and vCanje.descargue_Canje like ('" + txbx_SearchFilter.Text + "%') and fechaDescargue_Canje >= DATEADD(day,-10,GETDATE()) and fechaDescargue_Canje <= getdate() ORDER BY fechaDescargue_Canje DESC", conexionHQ);
              SqlDataAdapter adaptador2 = new SqlDataAdapter("USE "+bdCanjes+ " Select vES.descripcion_EstadoSucursal AS 'ESTADO', vCanje.descargue_Canje as 'DESCARGUE', vCanje.fechaDescargue_Canje AS 'FECHA DE DESCARGUE', vCanje.producto_Canje AS 'PRODUCTO', vCanje.cantidad_Canje AS 'CANTIDAD', vCanje.laboratorio_Canje AS 'LABORATORIO', vCanje.vendedor_Canje AS 'VENDEDOR', vCanje.nombreCliente_Canje AS 'CLIENTE', vCanje.ingresadopor_Canje AS 'INGRESADO POR', vCanje.lastUpdate_Canje AS 'ULTIMA ACTUALIZACION', vCanje.id_Canje AS 'IDENTIFICADOR' from Canje vCanje FULL JOIN EstadoSucursal vES ON vCanje.id_EstadoSucursal = vES.id_EstadoSucursal where id_Sucursal = '" + numSucursal + "' and vCanje.descargue_Canje like ('" + txbx_SearchFilter.Text + "%') and fechaDescargue_Canje >= DATEADD(day,-10,GETDATE()) and fechaDescargue_Canje <= getdate() ORDER BY fechaDescargue_Canje DESC", conexionHQ);
            DataTable dtTemporal2 = new DataTable();
            adaptador2.Fill(dtTemporal2);
            if (counter == false)
            {
                dtTemporal2.Clear();
            }
            datagrid_Canjes.DataSource = dtTemporal2;
            conexionHQ.Close();
        }
        private void txbx_SearchFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                btn_SearchFilter_Click(sender, e);
            }
        }

        private void btn_Minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_Maximizar_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txbx_factura1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                txbx_factura2.Focus();
            }
            if (labelatrk_Factura1.Visible == true)
            {
                emptycounter = emptycounter - 1;
                labelatrk_Factura1.Visible = false;
            }
        }

        private void txbx_factura2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                txbx_factura3.Focus();
            }
            if (labelatrk_Factura2.Visible == true)
            {
                emptycounter = emptycounter - 1;
                labelatrk_Factura2.Visible = false;
            }
        }

        private void txbx_factura3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                txbx_factura4.Focus();
            }
            if (labelatrk_Factura3.Visible == true)
            {
                emptycounter = emptycounter - 1;
                labelatrk_Factura3.Visible = false;
            }
        }

        private void txbx_factura4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                txbx_justificante.Focus();
            }
            if (labelatrk_Factura4.Visible == true)
            {
                emptycounter = emptycounter - 1;
                labelatrk_Factura4.Visible = false;
            }
        }

        private void txbx_autPlataforma_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter) && (e.KeyChar != (Char)Keys.Separator) && !(Char.IsLetter(e.KeyChar)))
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                txbx_factura1.Focus();
            }
            if (labelatrk_AutPlataforma.Visible == true)
            {
                emptycounter = emptycounter - 1;
                labelatrk_AutPlataforma.Visible = false;
            }
        }

        private void txbx_justificante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (labelatrk_Justificante.Visible == true)
            {
                emptycounter = emptycounter - 1;
                labelatrk_Justificante.Visible = false;
            }
        }
    }
}
