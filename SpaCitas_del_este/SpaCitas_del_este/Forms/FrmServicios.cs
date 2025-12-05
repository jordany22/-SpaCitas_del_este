using SpaCitas_del_este.Data;
using SpaCitas_del_este.Models;
using SpaCitas_del_este.Utiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SpaCitas_del_este.Forms
{
    public partial class FrmServicios : Form
    {
        private ServicioRepository _repo;

        public FrmServicios()
        {
            InitializeComponent();
            _repo = new ServicioRepository();
            CargarDatos();

        }
        private void CargarDatos()
        {
            dgvServicios.DataSource = _repo.GetAll();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Validaciones.ValidarCamposFormulario(this.Controls)) return;

                Servicio s = new Servicio
                {
                    Nombre = txtNombreCompleto.Text,
                    DuracionMinutos = int.Parse(txtDuracionMinutos.Text),
                    Precio = decimal.Parse(txtPrecio.Text)
                };

                _repo.Add(s);
                Helpers.ShowMessage("Servicio agregado correctamente");
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
                // Validaciones previas
                if (string.IsNullOrWhiteSpace(textIDActualizar.Text) ||
                    string.IsNullOrWhiteSpace(txtNombreCompletoActualizar.Text) ||
                    string.IsNullOrWhiteSpace(textDuracionActualizar.Text) ||
                    string.IsNullOrWhiteSpace(txtprecioactualizar.Text))
                {
                    Helpers.ShowError("Todos los campos deben estar completos.");
                    return;
                }

                // Conversión segura
                if (!int.TryParse(textIDActualizar.Text, out int id))
                {
                    Helpers.ShowError("El ID debe ser un número entero válido.");
                    return;
                }
                if (!int.TryParse(textDuracionActualizar.Text, out int duracion))
                {
                    Helpers.ShowError("La duración debe ser un número entero válido.");
                    return;
                }

                if (!decimal.TryParse(txtprecioactualizar.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal precio))
                {
                    Helpers.ShowError("El precio debe ser un número decimal válido.");
                    return;
                }
               

                // Crear objeto servicio
                Servicio s = new Servicio
                {
                    ServicioID = id,
                    Nombre = txtNombreCompleto.Text.Trim(),
                    DuracionMinutos = duracion,
                    Precio = precio
                };
                // Actualizar y mostrar mensaje
                _repo.Update(s);
                Helpers.ShowMessage("Servicio actualizado correctamente");
                CargarDatos();
            }
            catch (Exception ex)
            {
                Helpers.ShowError("Error inesperado: " + ex.Message);
            }

            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!Helpers.ConfirmAction("¿Eliminar servicio?")) return;

            if (!int.TryParse(txtEliminarID.Text, out int id))
            {
                Helpers.ShowError("El ID ingresado no es válido. Debe ser un número.");
                return;
            }

            bool eliminado = _repo.Delete(id);
            if (eliminado)
            {
                Helpers.ShowMessage("Servicio eliminado correctamente");
                CargarDatos();
            }
            else
            {
                Helpers.ShowError("No se pudo eliminar el servicio. Verifica el ID.");
            }

            LimpiarCampos();

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            string path = Helpers.DescargarArchivoCSV("Servicios.csv");
            if (path != null)
                CsvExporter.ExportDataGridViewToCsv(dgvServicios, path);


        }

       
        private void LimpiarCampos()
        {
            txtID.Clear();
            txtNombreCompleto.Clear();
            txtDuracionMinutos.Clear();
            txtPrecio.Clear();
        }

        private void dgvServicios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvServicios.CurrentRow != null)
            {
                txtID.Text = dgvServicios.CurrentRow.Cells["ServicioID"].Value.ToString();
                txtNombreCompleto.Text = dgvServicios.CurrentRow.Cells["Nombre"].Value.ToString();
                txtDuracionMinutos.Text = dgvServicios.CurrentRow.Cells["DuracionMinutos"].Value.ToString();
                txtPrecio.Text = dgvServicios.CurrentRow.Cells["Precio"].Value.ToString();

            }

        }
       


    }

}


    
