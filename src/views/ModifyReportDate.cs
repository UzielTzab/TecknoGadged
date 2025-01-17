using System;
using System.Drawing;
using FontAwesome.Sharp;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Drawing.Printing;
using FontAwesome.Sharp; // Asegúrate de tener FontAwesome.Sharp como referencia

namespace TecnogadgedWin7
{
    public partial class ModifyReportDate : Form
    {
        private DateTimePicker dateTimePickerStart;
        private DateTimePicker dateTimePickerEnd;
        private Button btnUpdate;
        private Label labelStartDate = new Label();
        private Label labelEndDate = new Label();
        private IconPictureBox iconStartDate;
        private IconPictureBox iconEndDate;

        int id;

        private Form1 mainForm;

        public ModifyReportDate(Form1 form1, DateTime startDate, DateTime endTime, int id)
        {
            InitializeComponent();

            mainForm = form1;
            this.id = id;



            // Configurar y añadir los controles al formulario
            InitializeCustomComponents();

            this.Size = new Size(350, 230);

            // Establecer las fechas en los DateTimePicker
            dateTimePickerStart.Value = startDate;
            dateTimePickerEnd.Value = endTime;
        }

        private void InitializeCustomComponents()
        {
            // Crear y configurar los íconos
            iconStartDate = new IconPictureBox();
            iconStartDate.IconChar = IconChar.CalendarAlt;
            iconStartDate.IconColor = Color.FromArgb(31, 30, 68);
            iconStartDate.Location = new Point(20, 25);
            iconStartDate.Size = new Size(32, 32);
            iconStartDate.BackColor = Color.Transparent;

            iconEndDate = new IconPictureBox();
            iconEndDate.IconChar = IconChar.CalendarCheck;
            iconEndDate.IconColor = Color.FromArgb(31, 30, 68);
            iconEndDate.Location = new Point(20, 75);
            iconEndDate.Size = new Size(32, 32);
            iconEndDate.BackColor = Color.Transparent;

            // Crear y configurar los controles de fecha
            labelStartDate = new Label();
            labelStartDate.Text = "Fecha de Inicio";
            labelStartDate.Font = new Font("Arial", 12, FontStyle.Regular);
            labelStartDate.Location = new Point(60, 25);
            labelStartDate.Size = new Size(120, 32);
            labelStartDate.ForeColor = Color.FromArgb(31, 30, 68);

            labelEndDate = new Label();
            labelEndDate.Text = "Fecha de Fin";
            labelEndDate.Font = new Font("Arial", 12, FontStyle.Regular);
            labelEndDate.Location = new Point(60, 75);
            labelEndDate.Size = new Size(120, 32);
            labelEndDate.ForeColor = Color.FromArgb(31, 30, 68);

            dateTimePickerStart = new DateTimePicker();
            dateTimePickerStart.Location = new Point(190, 25);
            dateTimePickerStart.Size = new Size(120, 32);
            dateTimePickerStart.Format = DateTimePickerFormat.Short;

            dateTimePickerEnd = new DateTimePicker();
            dateTimePickerEnd.Location = new Point(190, 75);
            dateTimePickerEnd.Size = new Size(120, 32);
            dateTimePickerEnd.Format = DateTimePickerFormat.Short;

            // Crear y configurar el botón
            btnUpdate = new Button();
            btnUpdate.Text = "Actualizar";
            btnUpdate.Font = new Font("Arial", 12, FontStyle.Regular);
            btnUpdate.Location = new Point(120, 130);
            btnUpdate.Size = new Size(120, 32);
            btnUpdate.BackColor = Color.FromArgb(31, 30, 68);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.Click += BtnUpdate_Click;

            // Añadir Label de Fecha de Fin
            this.Controls.Add(labelStartDate);
            this.Controls.Add(labelEndDate);

            // Añadir los controles al formulario
            this.Controls.Add(iconStartDate);
            this.Controls.Add(dateTimePickerStart);
            this.Controls.Add(iconEndDate);
            this.Controls.Add(dateTimePickerEnd);
            this.Controls.Add(btnUpdate);

            // Configuración adicional del formulario
            this.Text = "Modificar Fechas de Reporte";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(350, 200);
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Obtener las fechas seleccionadas
            DateTime startDate = dateTimePickerStart.Value;
            DateTime endDate = dateTimePickerEnd.Value;

            if (endDate < startDate)
            {
                MessageBox.Show("La fecha de fin no puede ser anterior a la fecha de inicio.", "Error de fechas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Aquí puedes agregar el código para actualizar las fechas en la base de datos
            MessageBox.Show($"Fechas actualizadas: \nInicio: {startDate.ToShortDateString()}\nFin: {endDate.ToShortDateString()}",
                            "Fechas Modificadas",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            UpdateReportDates();



        }
        //Función para ocultar los controles superiores del formulario
        protected override CreateParams CreateParams
        {
            get
            {
                // Oculta los controles superiores del formulario
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80; // WS_EX_TOOLWINDOW
                return cp;
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCLBUTTONDOWN = 0xA1;
            const int HTCAPTION = 0x2;

            if (m.Msg == WM_NCLBUTTONDOWN && m.WParam.ToInt32() == HTCAPTION)
            {
                return;
            }
            base.WndProc(ref m);
        }

        private void UpdateReportDates()
        {
            try
            {
                // Obtener las fechas seleccionadas de los DateTimePicker
                DateTime startDate = dateTimePickerStart.Value;
                DateTime endDate = dateTimePickerEnd.Value;

                // Validar que la fecha de fin no sea anterior a la fecha de inicio
                if (endDate < startDate)
                {
                    MessageBox.Show("La fecha de fin no puede ser anterior a la fecha de inicio.", "Error de fechas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Conectar a la base de datos y realizar la actualización
                DbConnect dbConnect = new DbConnect();
                string query = "UPDATE report SET fechaInicio = @fechaInicio, fechaFin = @fechaFin WHERE id = @reportId";

                using (MySqlCommand cmd = new MySqlCommand(query, dbConnect.Connection))
                {
                    cmd.Parameters.AddWithValue("@fechaInicio", startDate);
                    cmd.Parameters.AddWithValue("@fechaFin", endDate);
                    cmd.Parameters.AddWithValue("@reportId", id);

                    // Abrir la conexión, ejecutar la consulta y cerrarla
                    dbConnect.OpenConnection();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    dbConnect.CloseConnection();

                    if (rowsAffected > 0)
                    {
                        // Mostrar un mensaje de éxito si la actualización fue exitosa
                        MessageBox.Show("Fechas del reporte actualizadas correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Llamar a un método del formulario principal para actualizar la tabla de reportes, si es necesario
                        DateTime searchDate = mainForm.searchDatePicker.Value;
                        mainForm.GetAllReportsFunction(); // Asegúrate de que este método exista en Form1
                        Hide();
                    }
                    else
                    {
                        // Mostrar un mensaje de error si no se encontró el reporte
                        MessageBox.Show("No se encontró el reporte especificado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Mostrar un mensaje de error si ocurrió algún problema durante la actualización
                MessageBox.Show("Ocurrió un error al actualizar las fechas del reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
