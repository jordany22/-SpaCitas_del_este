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
    public partial class FrmTerapeutas : Form
    {
        private TerapeutaRepository _repo;


        public FrmTerapeutas()
        {
            InitializeComponent();
            _repo = new TerapeutaRepository();
            CargarDatos();

        }
        private void CargarDatos()
        {
            dgvTerapeutas.DataSource = _repo.GetAll();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Validaciones.ValidarCamposFormulario(this.Controls)) return;

                Terapeuta t = new Terapeuta
                {
                    Nombre = txtNombreCompleto.Text,
                    Especialidad = txtEspecialidad.Text
                };

                _repo.Add(t);
                Helpers.ShowMessage("Terapeuta agregado correctamente");
                CargarDatos();
                LimpiarCampos();

            }
            catch (Exception ex)
            {
                Helpers.ShowError(ex.Message);
            }


        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Validaciones.ValidarCamposFormulario(this.Controls)) return;

                if (!int.TryParse(textIDActualizar.Text, out int id))
                {
                    Helpers.ShowError("El ID ingresado no es válido. Debe ser un número.");
                    return;
                }

                Terapeuta t = new Terapeuta
                {
                    TerapeutaID = id,
                    Nombre = txtNombreCompletoActualizar.Text,
                    Especialidad = txtEspecialidadActualizar.Text
                };

                bool actualizado = _repo.Update(t);
                if (actualizado)
                {
                    Helpers.ShowMessage("Terapeuta actualizado correctamente");
                    CargarDatos();
                    LimpiarCampos();
                }
                else
                {
                    Helpers.ShowError("No se pudo actualizar el terapeuta. Verifica el ID.");
                }
            }
            catch (Exception ex)
            {
                Helpers.ShowError(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!Helpers.ConfirmAction("¿Eliminar terapeuta?")) return;

            if (!int.TryParse(txtEliminar.Text, out int id))
            {
                Helpers.ShowError("El ID ingresado no es válido. Debe ser un número.");
                return;
            }

            bool eliminado = _repo.Delete(id);
            if (eliminado)
            {
                Helpers.ShowMessage("Terapeuta eliminado correctamente");
                CargarDatos();
                LimpiarCampos();
            }
            else
            {
                Helpers.ShowError("No se pudo eliminar el terapeuta. Verifica el ID.");
            }


        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            string path = Helpers.DescargarArchivoCSV("Terapeutas.csv");
            if (path != null)
                CsvExporter.ExportDataGridViewToCsv(dgvTerapeutas, path);

        }

        private void dgvTerapeutas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTerapeutas.CurrentRow != null)
            {
                txtTerapeutaID.Text = dgvTerapeutas.CurrentRow.Cells["TerapeutaID"].Value.ToString();
                txtNombreCompleto.Text = dgvTerapeutas.CurrentRow.Cells["Nombre"].Value.ToString();
                txtEspecialidad.Text = dgvTerapeutas.CurrentRow.Cells["Especialidad"].Value.ToString();
            }


        }
        private void LimpiarCampos()
        {
            txtTerapeutaID.Clear();
            txtNombreCompleto.Clear();
            txtEspecialidad.Clear();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}
