using SpaCitas_del_este.Data;
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
using static SpaCitas_del_este.Utiles.Helpers;
using SpaCitas_del_este.Models;


namespace SpaCitas_del_este.Forms
{
    public partial class FrmCitas : Form
    {
        private CitaRepository _repo;

        public FrmCitas()
        {
            InitializeComponent();
            _repo = new CitaRepository();
            CargarDatos();
        }
        private void CargarDatos()
        {
            dgvCitas.DataSource = _repo.GetAll();
        }

        private void FrmCitas_Load(object sender, EventArgs e)
        {
            cmbPaciente.DataSource = new PacienteRepository().GetAll();
            cmbPaciente.DisplayMember = "NombreCompleto"; // o el campo que quieras mostrar
            cmbPaciente.ValueMember = "PacienteID";
            cmbPacienteActualizar.DataSource = new PacienteRepository().GetAll();
            cmbPacienteActualizar.DisplayMember = "NombreCompleto"; // o el campo que quieras mostrar
            cmbPacienteActualizar.ValueMember = "PacienteID";
            cmbServicio.DataSource = new ServicioRepository().GetAll();
            cmbServicio.DisplayMember = "Nombre";       // o el campo que quieras mostrar
            cmbServicio.ValueMember = "ServicioID";
            cmbServicioActualizar.DataSource = new ServicioRepository().GetAll();
            cmbServicioActualizar.DisplayMember = "Nombre";       // o el campo que quieras mostrar
            cmbServicioActualizar.ValueMember = "ServicioID";
            cmbTerapeuta.DataSource = new TerapeutaRepository().GetAll();
            cmbTerapeuta.DisplayMember = "NombreCompleto"; // o el campo que quieras mostrar
            cmbTerapeuta.ValueMember = "TerapeutaID";
            cmbTerapeutaActualizar.DataSource = new TerapeutaRepository().GetAll();
            cmbTerapeutaActualizar.DisplayMember = "NombreCompleto";
            cmbTerapeutaActualizar.ValueMember = "TerapeutaID";



        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Validaciones.ValidarCamposFormulario(this.Controls)) return;

                int servicioID = Convert.ToInt32(cmbServicio.SelectedValue);
                int duracion = CitaHelper.ObtenerDuracionPorServicio(servicioID);

                Cita c = new Cita
                {
                    PacienteID = Convert.ToInt32(cmbPaciente.SelectedValue),
                    Fecha = dtpFecha.Value.Date,
                    Hora = dtpHora.Value,
                    ServicioID = servicioID,
                    TerapeutaID = Convert.ToInt32(cmbTerapeuta.SelectedValue),
                    Duracion = duracion,
                    Estado = CitaHelper.CalcularEstado(dtpFecha.Value, dtpHora.Value, duracion)
                    


            };

                _repo.Add(c);
                Helpers.ShowMessage("Cita agregada correctamente");
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
                int servicioID = Convert.ToInt32(cmbServicioActualizar.SelectedValue);
                int duracion = CitaHelper.ObtenerDuracionPorServicio(servicioID);

                Cita c = new Cita
                {
                    CitaID = int.Parse(txtIDCita.Text),
                    PacienteID = Convert.ToInt32(cmbPacienteActualizar.SelectedValue),
                    Fecha = dtpFechaActualizar.Value.Date,
                    Hora = dtpHoraActualizar.Value,
                    ServicioID = servicioID,
                    TerapeutaID = Convert.ToInt32(cmbTerapeutaActualizar.SelectedValue),
                    Duracion = duracion,
                    Estado = CitaHelper.CalcularEstado(dtpFechaActualizar.Value, dtpHoraActualizar.Value, duracion)
                };

                _repo.Update(c);
                Helpers.ShowMessage("Cita actualizada correctamente");
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
            if (!Helpers.ConfirmAction("¿Eliminar cita?")) return;

            if (!int.TryParse(txtIDCita.Text, out int id))
            {
                Helpers.ShowError("El ID ingresado no es válido. Debe ser un número.");
                return;
            }

            bool eliminado = _repo.Delete(id);
            if (eliminado)
            {
                Helpers.ShowMessage("Cita eliminada correctamente");
                CargarDatos();
            }
            else
            {
                Helpers.ShowError("No se pudo eliminar la cita. Verifica el ID.");
            }
            LimpiarCampos();


        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            string path = Helpers.DescargarArchivoCSV("Citas.csv");
            if (path != null)
                CsvExporter.ExportDataGridViewToCsv(dgvCitas, path);

        }

        private void dgvCitas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int idCita = Convert.ToInt32(dgvCitas.CurrentRow.Cells["CitaID"].Value);
            Cita cita = _repo.GetAll().FirstOrDefault(x => x.CitaID == idCita);
            if (cita == null) return;

            txtIDCita.Text = cita.CitaID.ToString();
            cmbPaciente.SelectedValue = cita.PacienteID;
            dtpFecha.Value = cita.Fecha;
            dtpHora.Value = cita.Hora;
            cmbServicio.SelectedValue = cita.ServicioID;
            cmbTerapeuta.SelectedValue = cita.TerapeutaID;

            lblDuracion.Text = cita.Duracion.ToString() + " min";
            lblEstado.Text = cita.Estado;
            lblTiempoRestante.Text = $"{cita.TiempoRestante.Days} días {cita.TiempoRestante.Hours} horas";




        }
        private void LimpiarCampos()
        {
            txtIDCita.Clear();
            cmbPaciente.SelectedIndex = -1;
            cmbServicio.SelectedIndex = -1;
            cmbTerapeuta.SelectedIndex = -1;
            lblDuracion.Text = string.Empty;
            lblEstado.Text = string.Empty;
        }


    }
}



