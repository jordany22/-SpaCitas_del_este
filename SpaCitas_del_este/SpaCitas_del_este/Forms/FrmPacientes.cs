using SpaCitas_del_este.Data;
using SpaCitas_del_este.Models;
using SpaCitas_del_este.Utiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaCitas_del_este.Forms
{
    public partial class FrmPacientes : Form
    {
        private PacienteRepository _repo;
        public FrmPacientes()
        {
            InitializeComponent();
            _repo = new PacienteRepository();
            CargarDatos();
        }
        private void CargarDatos()
        {
            dgvPacientes.DataSource = _repo.GetAll();
        }

        private void FrmPacientes_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Validaciones.ValidarCamposFormulario(this.Controls)) return;

                Paciente p = new Paciente
                {
                    
                    Nombre = txtNombreCompleto.Text,
                    Telefono = txtTelefono.Text,
                    Email = txtCorreoEletronico.Text
                };

                _repo.Add(p);
                Helpers.ShowMessage("Paciente agregado correctamente");
                CargarDatos();
            }
            catch (Exception ex)
            {
                Helpers.ShowError(ex.Message);
            }
            LimpiarCampos();

        }



        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Paciente p = new Paciente
                {
                    PacienteID = int.Parse(textIDActualizar.Text),
                    Nombre = txtNombreCompletoActualizar.Text,
                    Telefono = txtTelefonoActualizar.Text,
                    Email = txtCorreoelectronicoActualizar.Text
                };

                _repo.Update(p);
                Helpers.ShowMessage("Paciente actualizado");
                CargarDatos();
            }
            catch (Exception ex)
            {
                Helpers.ShowError(ex.Message);
            }
            LimpiarCampos();

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!Helpers.ConfirmAction("¿Eliminar paciente?")) return;

            if (!int.TryParse(txtID.Text, out int id))
            {
                Helpers.ShowError("El ID ingresado no es válido. Debe ser un número.");
                return;
            }

            bool eliminado = _repo.Delete(id);
            if (eliminado)
            {
                Helpers.ShowMessage("Paciente eliminado correctamente");
                CargarDatos();
            }
            else
            {
                Helpers.ShowError("No se pudo eliminar el paciente. Verifica el ID.");
            }
            LimpiarCampos();



        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            string path = Helpers.DescargarArchivoCSV("Pacientes.csv");
            if (path != null)
                CsvExporter.ExportDataGridViewToCsv(dgvPacientes, path);
        }

        private void dgvPacientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dgvPacientes.CurrentRow.Cells["PacienteID"].Value.ToString();
            txtNombreCompleto.Text = dgvPacientes.CurrentRow.Cells["Nombre"].Value.ToString();
            txtTelefono.Text = dgvPacientes.CurrentRow.Cells["Telefono"].Value.ToString();
            txtCorreoEletronico.Text = dgvPacientes.CurrentRow.Cells["Email"].Value.ToString();
        }
        private void LimpiarCampos()
        {
            txtID.Clear();
            txtNombreCompleto.Clear();
            txtTelefono.Clear();
            txtCorreoEletronico.Clear();
        }


    }
}
