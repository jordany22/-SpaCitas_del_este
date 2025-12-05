using SpaCitas_del_este.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaCitas_del_este
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void pacientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPacientes frm = new FrmPacientes();
            frm.ShowDialog();
        }

        private void terapeutasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTerapeutas frm = new FrmTerapeutas();
            frm.ShowDialog();
        }

        private void serviciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmServicios frm = new FrmServicios();
            frm.ShowDialog();
        }

        private void citasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCitas frm = new FrmCitas();
            frm.ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    
    public void ProbarConexion()
        {
            string cs = ConfigurationManager.ConnectionStrings["SpaCitas_del_este"]?.ConnectionString;

            if (string.IsNullOrEmpty(cs))
            {
                MessageBox.Show("❌ No se encontró la cadena de conexión.");
            }
            else
            {
                MessageBox.Show("✅ Cadena de conexión cargada:\n" + cs);
            }
        }

    }
}




