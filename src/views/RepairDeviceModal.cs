using System;
using System.Drawing;
using FontAwesome.Sharp;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace TecnogadgedWin7
{
    public partial class RepairDeviceModal : Form
    {
    
        private Form1 mainForm;
        private ComboBox reparedPerson = new ComboBox();
        private ComboBox status = new ComboBox();

        //Campo de texto para agregar una descripcion de la reparacion
        private TextBox diagnostico = new TextBox();
        TextBox refaccionText = new TextBox();
        TextBox priceText = new TextBox();

        Button updateButton = new Button();

        int id = 1;
        String name = "Uziel";
        String statusNow = "NO REPARADO";

        int puntoXIcons = 50;
        int puntoXLabels = 120;


        List<string> personalNames = new List<string>();

       public RepairDeviceModal(Form1 form, int id, string name, string tipoDispositivo, string brand, string model, string problem, string statusNow, string fechaEntregar, string comment)
        {
        
            GetEmployeeName();
             string[] fechaHora = fechaEntregar.Split(' ');
            DateTime fecha;
            string fechaFormateada = string.Empty;
            if (DateTime.TryParse(fechaHora[0], out fecha))
            {
                fechaFormateada = fecha.ToString("dd 'de' MMMM 'de' yyyy", new System.Globalization.CultureInfo("es-ES"));
            }
            this.name = name;
            this.statusNow = statusNow;
            this.id = id;
            InitializeComponent();

            mainForm = form;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new Size(700, 800);

            // Titulo del modal
            Label title = new Label();
            title.Text = "Reparar dispositivo";
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.ForeColor = Color.Black;
            title.Location = new Point(390, 20);
            title.Size = new Size(250, 50);
            Controls.Add(title);

            // Titulo de datos del cliente
            Label customerData = new Label();
            customerData.Text = "Datos del cliente";
            customerData.Font = new Font("Arial", 16, FontStyle.Bold);
            customerData.ForeColor = Color.Black;
            customerData.Location = new Point(50, 20);
            customerData.Size = new Size(200, 50);
            Controls.Add(customerData);

            // Crear panel izquierdo
            Panel leftPanel = new Panel();
            leftPanel.Dock = DockStyle.Fill;
            leftPanel.BackColor = Color.Transparent;
            Controls.Add(leftPanel);

            // Subtítulo y valor del nombre
            Label nameSubtitle = new Label();
            nameSubtitle.Text = "Nombre:";
            nameSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            nameSubtitle.ForeColor = Color.Black;
            nameSubtitle.Location = new Point(puntoXLabels, 80);
            nameSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(nameSubtitle);

            Label nameLabel = new Label();
            nameLabel.Text = name;
            nameLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            nameLabel.ForeColor = Color.Black;
            nameLabel.Location = new Point(puntoXLabels, 100);
            nameLabel.Size = new Size(200, 20);
            leftPanel.Controls.Add(nameLabel);

            // Icono del nombre
            IconPictureBox nameIcon = new IconPictureBox();
            nameIcon.IconChar = IconChar.User;
            nameIcon.IconColor = Color.FromArgb(31, 30, 68);
            nameIcon.Location = new Point(puntoXIcons, 80);
            nameIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(nameIcon);

            // Subtítulo y valor del tipo de dispositivo
            Label deviceTypeSubtitle = new Label();
            deviceTypeSubtitle.Text = "Tipo de dispositivo:";
            deviceTypeSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            deviceTypeSubtitle.ForeColor = Color.Black;
            deviceTypeSubtitle.Location = new Point(puntoXLabels, 140);
            deviceTypeSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(deviceTypeSubtitle);

            Label deviceTypeLabel = new Label();
            deviceTypeLabel.Text = tipoDispositivo;
            deviceTypeLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            deviceTypeLabel.ForeColor = Color.Black;
            deviceTypeLabel.Location = new Point(puntoXLabels, 160);
            deviceTypeLabel.Size = new Size(200, 20);
            leftPanel.Controls.Add(deviceTypeLabel);

            // Icono del tipo de dispositivo
            IconPictureBox deviceTypeIcon = new IconPictureBox();
            deviceTypeIcon.IconChar = IconChar.MobileAlt;
            deviceTypeIcon.IconColor = Color.FromArgb(31, 30, 68);
            deviceTypeIcon.Location = new Point(puntoXIcons, 140);
            deviceTypeIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(deviceTypeIcon);

            // Subtítulo y valor de la marca
            Label brandSubtitle = new Label();
            brandSubtitle.Text = "Marca:";
            brandSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            brandSubtitle.ForeColor = Color.Black;
            brandSubtitle.Location = new Point(puntoXLabels, 200);
            brandSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(brandSubtitle);

            Label brandLabel = new Label();
            brandLabel.Text = brand;
            brandLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            brandLabel.ForeColor = Color.Black;
            brandLabel.Location = new Point(puntoXLabels, 220);
            brandLabel.Size = new Size(200, 20);
            leftPanel.Controls.Add(brandLabel);

            // Icono de la marca
            IconPictureBox brandIcon = new IconPictureBox();
            brandIcon.IconChar = IconChar.Tag;
            brandIcon.IconColor = Color.FromArgb(31, 30, 68);
            brandIcon.Location = new Point(puntoXIcons, 200);
            brandIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(brandIcon);

            // Subtítulo y valor del modelo
            Label modelSubtitle = new Label();
            modelSubtitle.Text = "Modelo:";
            modelSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            modelSubtitle.ForeColor = Color.Black;
            modelSubtitle.Location = new Point(puntoXLabels, 260);
            modelSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(modelSubtitle);

            Label modelLabel = new Label();
            modelLabel.Text = model;
            modelLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            modelLabel.ForeColor = Color.Black;
            modelLabel.Location = new Point(puntoXLabels, 280);
            modelLabel.Size = new Size(200, 20);
            leftPanel.Controls.Add(modelLabel);

            // Icono del modelo
            IconPictureBox modelIcon = new IconPictureBox();
            modelIcon.IconChar = IconChar.Mobile;
            modelIcon.IconColor = Color.FromArgb(31, 30, 68);
            modelIcon.Location = new Point(puntoXIcons, 260);
            modelIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(modelIcon);

            // Subtítulo y valor del problema
            Label problemSubtitle = new Label();
            problemSubtitle.Text = "Problema:";
            problemSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            problemSubtitle.ForeColor = Color.Black;
            problemSubtitle.Location = new Point(puntoXLabels, 320);
            problemSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(problemSubtitle);

            Label problemLabel = new Label();
            problemLabel.Text = problem;
            problemLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            problemLabel.ForeColor = Color.Red;
            problemLabel.Location = new Point(puntoXLabels, 340);
            problemLabel.Size = new Size(200, 60);
            leftPanel.Controls.Add(problemLabel);

            // Icono del problema
            IconPictureBox problemIcon = new IconPictureBox();
            problemIcon.IconChar = IconChar.Bug;
            problemIcon.IconColor = Color.FromArgb(31, 30, 68);
            problemIcon.Location = new Point(puntoXIcons, 320);
            problemIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(problemIcon);

            // Subtítulo y valor del comentario
            Label commentSubtitle = new Label();
            commentSubtitle.Text = "Comentarios:";
            commentSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            commentSubtitle.ForeColor = Color.Black;
            commentSubtitle.Location = new Point(puntoXLabels, 400);
            commentSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(commentSubtitle);

            TextBox commentTextBox = new TextBox();
            commentTextBox.Text = comment;
            commentTextBox.Font = new Font("Arial", 12, FontStyle.Regular);
            commentTextBox.ForeColor = Color.Gray;
            commentTextBox.Location = new Point(puntoXLabels, 420);
            commentTextBox.Size = new Size(250, 170);
            commentTextBox.Multiline = true;
            commentTextBox.ScrollBars = ScrollBars.Vertical;
            commentTextBox.ReadOnly = true;
            commentTextBox.BackColor = Color.White;
            leftPanel.Controls.Add(commentTextBox);

            // Icono del comentario
            IconPictureBox commentIcon = new IconPictureBox();
            commentIcon.IconChar = IconChar.Comment;
            commentIcon.IconColor = Color.FromArgb(31, 30, 68);
            commentIcon.Location = new Point(puntoXIcons, 400);
            commentIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(commentIcon);

            // Subtítulo y valor del fecha de entrega
            Label deliveryDateSubtitle = new Label();
            deliveryDateSubtitle.Text = "Fecha de entrega:";
            deliveryDateSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            deliveryDateSubtitle.ForeColor = Color.Black;
            deliveryDateSubtitle.Location = new Point(puntoXLabels, 600);
            deliveryDateSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(deliveryDateSubtitle);

            Label deliveryDateLabel = new Label();
            deliveryDateLabel.Text = fechaFormateada;
            deliveryDateLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            deliveryDateLabel.ForeColor = Color.Black;
            deliveryDateLabel.Location = new Point(puntoXLabels, 620);
            deliveryDateLabel.Size = new Size(200, 20);
            leftPanel.Controls.Add(deliveryDateLabel);

            // Icono de la fecha de entrega
            IconPictureBox deliveryDateIcon = new IconPictureBox();
            deliveryDateIcon.IconChar = IconChar.CalendarAlt;
            deliveryDateIcon.IconColor = Color.FromArgb(31, 30, 68);
            deliveryDateIcon.Location = new Point(puntoXIcons, 600);
            deliveryDateIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(deliveryDateIcon);

            // Subtítulo y valor de hora de entrega
            Label deliveryTimeSubtitle = new Label();
            deliveryTimeSubtitle.Text = "Hora de entrega:";
            deliveryTimeSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            deliveryTimeSubtitle.ForeColor = Color.Black;
            deliveryTimeSubtitle.Location = new Point(puntoXLabels, 670);
            deliveryTimeSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(deliveryTimeSubtitle);

            Label deliveryTimeLabel = new Label();
            DateTime hora;
            if (DateTime.TryParse(fechaHora[1], out hora))
            {
                string amPm = hora.Hour >= 12 ? "PM" : "AM"; // Determinar "AM" o "PM"
                deliveryTimeLabel.Text = hora.ToString("hh:mm", new System.Globalization.CultureInfo("es-ES")) + " " + amPm; // Formato de 12 horas con AM/PM
            }
            else
            {
                deliveryTimeLabel.Text = fechaHora[1]; // En caso de que no se pueda convertir, usar el valor original
            }
            deliveryTimeLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            deliveryTimeLabel.ForeColor = Color.Black;
            deliveryTimeLabel.Location = new Point(puntoXLabels, 690);
            deliveryTimeLabel.Size = new Size(200, 20);
            leftPanel.Controls.Add(deliveryTimeLabel);

            // Icono de la hora de entrega
            IconPictureBox deliveryTimeIcon = new IconPictureBox();
            deliveryTimeIcon.IconChar = IconChar.Clock;
            deliveryTimeIcon.IconColor = Color.FromArgb(31, 30, 68);
            deliveryTimeIcon.Location = new Point(puntoXIcons, 670);
            deliveryTimeIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(deliveryTimeIcon);

            // Selector de persona que reparo
            reparedPerson.ForeColor = Color.White;
            reparedPerson.Location = new Point(420, 120);
            reparedPerson.Size = new Size(200, 50);
            reparedPerson.BackColor = Color.FromArgb(31, 30, 68);
            reparedPerson.FlatStyle = FlatStyle.Flat;
            reparedPerson.DropDownStyle = ComboBoxStyle.DropDownList;

            // Iterar sobre la lista personalNames y agregar los nombres al ComboBox
            foreach (var personName in personalNames)
            {
                reparedPerson.Items.Add(personName);
            }

            leftPanel.Controls.Add(reparedPerson);

            // Texto para el campo de persona que reparo
            Label received_personText = new Label();
            received_personText.Text = "Persona que reparó o atendió";
            received_personText.Font = new Font("Arial", 12, FontStyle.Regular);
            received_personText.ForeColor = Color.Black;
            received_personText.Location = new Point(420, 80);
            received_personText.Size = new Size(200, 50);
            leftPanel.Controls.Add(received_personText);

            // Icono para el campo de persona que reparo
            IconPictureBox received_personIcon = new IconPictureBox();
            received_personIcon.IconChar = IconChar.User;
            received_personIcon.IconColor = Color.FromArgb(31, 30, 68);
            received_personIcon.Location = new Point(380, 120);
            received_personIcon.Size = new Size(32, 32);
            received_personIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(received_personIcon);

            // Selector de estatus
            status.ForeColor = Color.White;
            status.Location = new Point(420, 200);
            status.Size = new Size(200, 50);
            status.BackColor = Color.FromArgb(31, 30, 68);
            status.FlatStyle = FlatStyle.Flat;
            status.DropDownStyle = ComboBoxStyle.DropDownList;
            status.Items.Add("NO REPARADO");
            status.Items.Add("REPARADO");
            leftPanel.Controls.Add(status);

            // Texto para el campo de estatus
            Label statusText = new Label();
            statusText.Text = "Estatus";
            statusText.Font = new Font("Arial", 12, FontStyle.Regular);
            statusText.ForeColor = Color.Black;
            statusText.Location = new Point(420, 170);
            statusText.Size = new Size(200, 50);
            leftPanel.Controls.Add(statusText);

            // Icono para el campo de estatus
            IconPictureBox statusIcon = new IconPictureBox();
            statusIcon.IconChar = IconChar.CheckCircle;
            statusIcon.IconColor = Color.FromArgb(31, 30, 68);
            statusIcon.Location = new Point(380, 200);
            statusIcon.Size = new Size(32, 32);
            statusIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(statusIcon);

            // Campo de texto para agregar una descripcion de la reparacion
            diagnostico.ForeColor = Color.White;
            diagnostico.Location = new Point(420, 280);
            diagnostico.Multiline = true;
            diagnostico.ScrollBars = ScrollBars.Vertical;
            diagnostico.Size = new Size(200, 100);
            diagnostico.BackColor = Color.FromArgb(31, 30, 68);
            diagnostico.MaxLength = 200; 
            diagnostico.BorderStyle = BorderStyle.None;
            leftPanel.Controls.Add(diagnostico);

            // Texto para el campo de descripcion
            Label descriptionText = new Label();
            descriptionText.Text = "Diagnóstico";
            descriptionText.Font = new Font("Arial", 12, FontStyle.Regular);
            descriptionText.ForeColor = Color.Black;
            descriptionText.Location = new Point(420, 250);
            descriptionText.Size = new Size(200, 50);
            leftPanel.Controls.Add(descriptionText);

            // Icono para el campo de descripcion
            IconPictureBox descriptionIcon = new IconPictureBox();
            descriptionIcon.IconChar = IconChar.Comment;
            descriptionIcon.IconColor = Color.FromArgb(31, 30, 68);
            descriptionIcon.Location = new Point(380, 280);
            descriptionIcon.Size = new Size(32, 32);
            descriptionIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(descriptionIcon);

            // Campo de texto para agregar costo de reparacion
            priceText.ForeColor = Color.White;
            priceText.Location = new Point(420, 440);
            priceText.Size = new Size(100, 400);
            priceText.BackColor = Color.FromArgb(31, 30, 68);
            priceText.TextAlign = HorizontalAlignment.Center;
            priceText.BorderStyle = BorderStyle.FixedSingle;
            priceText.MaxLength = 10; // Limitar a 10 caracteres

            // Evento para permitir solo números
            priceText.KeyPress += (sender, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            };
            // Evento para formatear el texto con comas
            priceText.TextChanged += (sender, e) =>
            {
                if (decimal.TryParse(priceText.Text, out decimal value))
                {
                    priceText.TextChanged -= (s, ev) => { }; // Desconectar el evento temporalmente
                    priceText.Text = string.Format("{0:N0}", value);
                    priceText.SelectionStart = priceText.Text.Length; // Mover el cursor al final
                    priceText.TextChanged += (s, ev) => { }; // Reconectar el evento
                }
            };
            leftPanel.Controls.Add(priceText);

            // Texto para el campo de precio
            Label priceLabel = new Label();
            priceLabel.Text = "Asignar costo";
            priceLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            priceLabel.ForeColor = Color.Black;
            priceLabel.Location = new Point(420, 410);
            priceLabel.Size = new Size(200, 50);
            leftPanel.Controls.Add(priceLabel);

            // Icono para el campo de precio
            IconPictureBox priceIcon = new IconPictureBox();
            priceIcon.IconChar = IconChar.DollarSign;
            priceIcon.IconColor = Color.FromArgb(31, 30, 68);
            priceIcon.Location = new Point(380, 440);
            priceIcon.Size = new Size(32, 32);
            priceIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(priceIcon);

            // Campo de costo de refacción
            refaccionText.ForeColor = Color.White;
            refaccionText.Location = new Point(420, 520);
            refaccionText.Size = new Size(100, 400);
            refaccionText.BackColor = Color.FromArgb(31, 30, 68);
            refaccionText.TextAlign = HorizontalAlignment.Center;
            refaccionText.BorderStyle = BorderStyle.FixedSingle;
            refaccionText.MaxLength = 10; // Limitar a 10 caracteres
            refaccionText.KeyPress += (sender, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            };
            refaccionText.TextChanged += (sender, e) =>
            {
                if (decimal.TryParse(refaccionText.Text, out decimal value))
                {
                    refaccionText.TextChanged -= (s, ev) => { }; // Desconectar el evento temporalmente
                    refaccionText.Text = string.Format("{0:N0}", value);
                    refaccionText.SelectionStart = refaccionText.Text.Length; // Mover el cursor al final
                    refaccionText.TextChanged += (s, ev) => { }; // Reconectar el evento
                }
            };
            leftPanel.Controls.Add(refaccionText);

            // Texto para el campo de refaccion
            Label refaccionLabel = new Label();
            refaccionLabel.Text = "Costo de refacción";
            refaccionLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            refaccionLabel.ForeColor = Color.Black;
            refaccionLabel.Location = new Point(420, 490);
            refaccionLabel.Size = new Size(200, 50);
            refaccionText.Text = "0";
            leftPanel.Controls.Add(refaccionLabel);

            // Icono para el campo de refaccion
            IconPictureBox refaccionIcon = new IconPictureBox();
            refaccionIcon.IconChar = IconChar.Tools;
            refaccionIcon.IconColor = Color.FromArgb(31, 30, 68);
            refaccionIcon.Location = new Point(380, 520);
            refaccionIcon.Size = new Size(32, 32);
            refaccionIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(refaccionIcon);

            // Boton para actualizar el registro
            updateButton.Text = "Atender";
            updateButton.Font = new Font("Arial", 12, FontStyle.Bold);
            updateButton.ForeColor = Color.White;
            updateButton.BackColor = Color.FromArgb(31, 30, 68);
            updateButton.FlatStyle = FlatStyle.Flat;
            updateButton.Location = new Point(420, 680);
            updateButton.Size = new Size(200, 50);
            updateButton.Click += new EventHandler(PutARegister!);
            leftPanel.Controls.Add(updateButton);
        }
       
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
        //Funcion para cerrar el modal 
        private void CloseModal(object sender, EventArgs e)
        {
            Hide();
        }
        public void GetEmployeeName()
        {
            // Instanciar la clase DbConnect y ejecutar la consulta
            DbConnect dbConnect = new DbConnect();
            string query = "SELECT nombre FROM employees";
            DataTable dataTable = dbConnect.ExecuteQuery(query);

            // Agregar los nombres de los empleados al ComboBox
            foreach (DataRow row in dataTable.Rows)
            {
                string name = row["nombre"].ToString();
                personalNames.Add(name);
            }
        }
        private void PutARegister(object sender, EventArgs e)
        {
            // Verificar que los campos no estén vacíos
            if (reparedPerson.Text == "" || status.Text == "" || diagnostico.Text == "" || priceText.Text == "" || refaccionText.Text == "")
            {
                MessageBox.Show("Por favor, completa todos los campos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                DbConnect dbConnect = new DbConnect();

                // Obtener la fecha y hora actual del dispositivo, la del lunes y la del domingo
                string dayName = DatesCalculations.GetNowDayName();
                string nowDate = DatesCalculations.DateNowCalculate();
                string mondayDate = DatesCalculations.DateNearMonday();
                string sundayDate = DatesCalculations.DateNearSunday();
                string repairedDate = nowDate;

                // Antes de insertar el costo desformatear quitando la coma si tiene miles 
                string costoRefaccion = refaccionText.Text.Replace(",", "");
                string price = priceText.Text.Replace(",", "");

                // Convertir los valores a decimal
                decimal costoRefaccionDecimal = decimal.Parse(costoRefaccion);
                decimal priceDecimal = decimal.Parse(price);

                // Calcular el costo y el porcentaje de salario
                decimal labour = priceDecimal - costoRefaccionDecimal;
                decimal salarioPorcentaje = labour * 0.40m; // Calcular el 40% del costo

                // Construir la consulta SQL para actualizar los campos persona_reparo, estatus, diagnostico, costo y fecha_reparado
                string updateCustomerQuery = "UPDATE customers SET " +
                                            "persona_reparo = @reparedPerson, " +
                                            "estatus = @status, " +
                                            "diagnostico = @diagnostico, " +
                                            "costo = @costo, " +
                                            "refaccion = @refaccion, " +
                                            "fecha_reparado = @repairedDate " +
                                            "WHERE id = @id";

                // Crear el comando y agregar los parámetros
                using (MySqlCommand cmd = new MySqlCommand(updateCustomerQuery, dbConnect.Connection))
                {
                    cmd.Parameters.AddWithValue("@reparedPerson", reparedPerson.Text);
                    cmd.Parameters.AddWithValue("@status", status.Text);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@diagnostico", diagnostico.Text);
                    cmd.Parameters.AddWithValue("@costo", priceDecimal);
                    cmd.Parameters.AddWithValue("@repairedDate", repairedDate);
                    cmd.Parameters.AddWithValue("@refaccion", costoRefaccionDecimal);

                    // Abrir la conexión y ejecutar la consulta
                    dbConnect.OpenConnection();
                    cmd.ExecuteNonQuery();
                    dbConnect.CloseConnection(); // Cerrar la conexión después de usarla
                }

                // Obtener el ID del empleado que realizó la reparación
                int idEmpleado = getEmployeeId(reparedPerson.Text);

                // Verificar si ya existe un reporte semanal para este empleado en la semana actual
                string checkWeeklyReportQuery = "SELECT id_reporte_semana FROM report_per_week WHERE id_empleado = @idEmpleado AND fecha_inicio = @fechaInicio AND fecha_final = @fechaFinal";
                int idReporteSemanal = -1;

                using (MySqlCommand cmd = new MySqlCommand(checkWeeklyReportQuery, dbConnect.Connection))
                {
                    cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                    cmd.Parameters.AddWithValue("@fechaInicio", mondayDate);
                    cmd.Parameters.AddWithValue("@fechaFinal", sundayDate);

                    dbConnect.OpenConnection(); // Abrir la conexión
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        idReporteSemanal = Convert.ToInt32(result);
                    }
                    dbConnect.CloseConnection(); // Cerrar la conexión después de usarla
                }

                // Si no existe un reporte semanal para esta semana, crearlo
                if (idReporteSemanal == -1)
                {
                    string insertReportWeeklyQuery = "INSERT INTO report_per_week (fecha_inicio, fecha_final, id_empleado) VALUES (@fechaInicio, @fechaFinal, @idEmpleado); SELECT LAST_INSERT_ID();";

                    using (MySqlCommand cmd = new MySqlCommand(insertReportWeeklyQuery, dbConnect.Connection))
                    {
                        cmd.Parameters.AddWithValue("@fechaInicio", mondayDate);
                        cmd.Parameters.AddWithValue("@fechaFinal", sundayDate);
                        cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                        dbConnect.OpenConnection(); // Abrir la conexión
                        idReporteSemanal = Convert.ToInt32(cmd.ExecuteScalar());
                        dbConnect.CloseConnection(); // Cerrar la conexión después de usarla
                    }
                }

                // Insertar el reporte diario relacionado con el reporte semanal
                string insertDailyReportQuery = "INSERT INTO daily_report (dia, ingreso_generado, costo_refaccion, mano_obra, comision_empleado, fecha_capturada, id_reporte_semana) " +
                                            "VALUES (@dia, @ingresoGenerado, @costoRefaccion, @manoObra, @comisionEmpleado, @fechaCapturada, @idReporteSemanal)";

                using (MySqlCommand cmd = new MySqlCommand(insertDailyReportQuery, dbConnect.Connection))
                {
                    cmd.Parameters.AddWithValue("@dia", dayName);
                    cmd.Parameters.AddWithValue("@ingresoGenerado", priceDecimal);
                    cmd.Parameters.AddWithValue("@costoRefaccion", costoRefaccionDecimal);
                    cmd.Parameters.AddWithValue("@manoObra", labour);
                    cmd.Parameters.AddWithValue("@comisionEmpleado", salarioPorcentaje);
                    cmd.Parameters.AddWithValue("@fechaCapturada", nowDate);
                    cmd.Parameters.AddWithValue("@idReporteSemanal", idReporteSemanal);

                    dbConnect.OpenConnection(); // Abrir la conexión
                    cmd.ExecuteNonQuery();
                    dbConnect.CloseConnection(); // Cerrar la conexión después de usarla
                }

                // Obtener el valor del filtro desde la clase MainForm
                string filterValue = mainForm.GetFilterValue();
                string searchValue = mainForm.GetSearchValue();

                // Actualizar la vista principal y mostrar un mensaje de éxito
                mainForm.GetFilterRegisters(filterValue, searchValue);
                MessageBox.Show("Se atendió correctamente el cliente", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CloseModal(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para obtener el ID del empleado basado en su nombre
        private int getEmployeeId(string nombreEmpleado)
        {
            int idEmpleado = -1;
            DbConnect dbConnect = new DbConnect();

            string query = "SELECT id_empleado FROM employees WHERE nombre = @nombreEmpleado";

            using (MySqlCommand cmd = new MySqlCommand(query, dbConnect.Connection))
            {
                cmd.Parameters.AddWithValue("@nombreEmpleado", nombreEmpleado);

                dbConnect.OpenConnection(); // Abrir la conexión
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    idEmpleado = Convert.ToInt32(result);
                }
                dbConnect.CloseConnection(); // Cerrar la conexión después de usarla
            }

            return idEmpleado;
        }

   
    }
}