using System;
using System.Drawing;
using FontAwesome.Sharp;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace TecnogadgedWin7
{
    public partial class EditCustomer : Form
    {
        private Form1 mainForm;
        private string Nombre;
        private string telefono;
        private string marca;
        private string modelo;
        private string motivo;
        private string fecha;
        private string hora;
        private string status;
        private string comment;

        private TextBox name = new TextBox();
        private TextBox phone = new TextBox();
        private TextBox brand = new TextBox();
        private TextBox model = new TextBox();
        private TextBox reason = new TextBox();
        private TextBox commentBox = new TextBox(); // Nuevo campo para comentarios

        private DateTimePicker datePicker = new DateTimePicker();
        private DateTimePicker timePicker = new DateTimePicker();


        public EditCustomer(Form1 form, string Nombre, string telefono, string marca, string modelo, string motivo, string fecha, string hora, string status, string comment)
        {
            this.Nombre = Nombre;
            this.telefono = telefono;
            this.marca = marca;
            this.modelo = modelo;
            this.motivo = motivo;
            this.fecha = fecha;
            this.hora = hora;
            this.status = status;
            this.comment = comment;

            InitializeComponent();
            mainForm = form;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new Size(550, 750); // Ajusté el tamaño para acomodar el nuevo campo

            // Titulo del modal
            Label title = new Label();
            title.Text = "Editar este cliente";
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.ForeColor = Color.Black;
            title.Location = new Point(50, 20);
            title.Size = new Size(250, 50);
            Controls.Add(title);

            // Crear panel izquierdo
            Panel leftPanel = new Panel();
            leftPanel.Dock = DockStyle.Fill;
            leftPanel.BackColor = Color.Transparent;
            Controls.Add(leftPanel);

            // Campo de nombre
            name.Font = new Font("Arial", 12, FontStyle.Regular);
            name.ForeColor = Color.White;
            name.Location = new Point(50, 110);
            name.Size = new Size(200, 100);
            name.BackColor = Color.FromArgb(31, 30, 68);
            name.BorderStyle = BorderStyle.FixedSingle;
            name.Font = new Font("Arial", 12, FontStyle.Regular);
            name.TextAlign = HorizontalAlignment.Center;
            name.Text = Nombre;
            name.MaxLength = 60;
            leftPanel.Controls.Add(name);

            //Texto para el campo de nombre
            Label nameText = new Label();
            nameText.Text = "Nombre";
            nameText.Font = new Font("Arial", 12, FontStyle.Regular);
            nameText.ForeColor = Color.Black;
            nameText.Location = new Point(50, 80);
            nameText.Size = new Size(200, 50);
            leftPanel.Controls.Add(nameText);

            // Icono para el campo de nombre
            IconPictureBox nameIcon = new IconPictureBox();
            nameIcon.IconChar = IconChar.User;
            nameIcon.IconColor = Color.FromArgb(31, 30, 68);
            nameIcon.Location = new Point(10, 110);
            nameIcon.Size = new Size(32, 32);
            nameIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(nameIcon);

            // Campo de telefono
            phone = new TextBox();
            phone.ForeColor = Color.White;
            phone.Location = new Point(50, 180);
            phone.Size = new Size(200, 50);
            phone.BackColor = Color.FromArgb(31, 30, 68);
            phone.BorderStyle = BorderStyle.FixedSingle;
            phone.Font = new Font("Arial", 12, FontStyle.Regular);
            phone.TextAlign = HorizontalAlignment.Center;
            phone.Text = telefono;
            phone.MaxLength = 10;
            phone.KeyPress += new KeyPressEventHandler((sender, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            });
            leftPanel.Controls.Add(phone);

            //Texto para el campo de telefono
            Label phoneText = new Label();
            phoneText.Text = "Telefono";
            phoneText.Font = new Font("Arial", 12, FontStyle.Regular);
            phoneText.ForeColor = Color.Black;
            phoneText.Location = new Point(50, 150);
            phoneText.Size = new Size(200, 50);
            leftPanel.Controls.Add(phoneText);

            // Icono para el campo de telefono
            IconPictureBox ageIcon = new IconPictureBox();
            ageIcon.IconChar = IconChar.Phone;
            ageIcon.IconColor = Color.FromArgb(31, 30, 68);
            ageIcon.Location = new Point(10, 180);
            ageIcon.Size = new Size(32, 32);
            ageIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(ageIcon);

            // Campo de marca
            brand.ForeColor = Color.White;
            brand.Location = new Point(50, 250);
            brand.Size = new Size(200, 50);
            brand.BackColor = Color.FromArgb(31, 30, 68);
            brand.BorderStyle = BorderStyle.FixedSingle;
            brand.Font = new Font("Arial", 12, FontStyle.Regular);
            brand.TextAlign = HorizontalAlignment.Center;
            brand.Text = marca;
            brand.MaxLength = 60;
            leftPanel.Controls.Add(brand);

            //Texto para el campo de marca
            Label brandText = new Label();
            brandText.Text = "Marca";
            brandText.Font = new Font("Arial", 12, FontStyle.Regular);
            brandText.ForeColor = Color.Black;
            brandText.Location = new Point(50, 220);
            brandText.Size = new Size(200, 50);
            leftPanel.Controls.Add(brandText);

            // Icono para el campo de marca
            IconPictureBox brandIcon = new IconPictureBox();
            brandIcon.IconChar = IconChar.Tag;
            brandIcon.IconColor = Color.FromArgb(31, 30, 68);
            brandIcon.Location = new Point(10, 250);
            brandIcon.Size = new Size(32, 32);
            brandIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(brandIcon);

            // Campo de modelo
            model = new TextBox();
            model.ForeColor = Color.White;
            model.Location = new Point(50, 320);
            model.Size = new Size(200, 50);
            model.BackColor = Color.FromArgb(31, 30, 68);
            model.BorderStyle = BorderStyle.FixedSingle;
            model.Font = new Font("Arial", 12, FontStyle.Regular);
            model.TextAlign = HorizontalAlignment.Center;
            model.Text = modelo;
            model.MaxLength = 60;
            leftPanel.Controls.Add(model);

            //Texto para el campo de modelo
            Label modelText = new Label();
            modelText.Text = "Modelo";
            modelText.Font = new Font("Arial", 12, FontStyle.Regular);
            modelText.ForeColor = Color.Black;
            modelText.Location = new Point(50, 290);
            modelText.Size = new Size(200, 50);
            leftPanel.Controls.Add(modelText);

            // Icono para el campo de modelo
            IconPictureBox modelIcon = new IconPictureBox();
            modelIcon.IconChar = IconChar.PhoneAlt;
            modelIcon.IconColor = Color.FromArgb(31, 30, 68);
            modelIcon.Location = new Point(10, 320);
            modelIcon.Size = new Size(32, 32);
            modelIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(modelIcon);

            // Campo de motivo
            reason = new TextBox();
            reason.ForeColor = Color.White;
            reason.Location = new Point(50, 390);
            reason.Size = new Size(200, 50);
            reason.BackColor = Color.FromArgb(31, 30, 68);
            reason.BorderStyle = BorderStyle.FixedSingle;
            reason.Font = new Font("Arial", 12, FontStyle.Regular);
            reason.TextAlign = HorizontalAlignment.Center;
            reason.Text = motivo;
            reason.MaxLength = 60;
            leftPanel.Controls.Add(reason);

            //Texto para el campo de motivo
            Label reasonText = new Label();
            reasonText.Text = "Motivo";
            reasonText.Font = new Font("Arial", 12, FontStyle.Regular);
            reasonText.ForeColor = Color.Black;
            reasonText.Location = new Point(50, 360);
            reasonText.Size = new Size(200, 50);
            leftPanel.Controls.Add(reasonText);

            // Icono para el campo de motivo
            IconPictureBox reasonIcon = new IconPictureBox();
            reasonIcon.IconChar = IconChar.Inbox;
            reasonIcon.IconColor = Color.FromArgb(31, 30, 68);
            reasonIcon.Location = new Point(10, 390);
            reasonIcon.Size = new Size(32, 32);
            reasonIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(reasonIcon);

            // Campo de comentarios
            commentBox = new TextBox();
            commentBox.ForeColor = Color.White;
            commentBox.Location = new Point(50, 460);
            commentBox.Size = new Size(450, 100);
            commentBox.BackColor = Color.FromArgb(31, 30, 68);
            commentBox.BorderStyle = BorderStyle.FixedSingle;
            commentBox.Font = new Font("Arial", 12, FontStyle.Regular);
            commentBox.Multiline = true; // Hacerlo multilínea
            commentBox.ScrollBars = ScrollBars.Vertical; // Agregar scroll vertical
            commentBox.Text = comment;
            commentBox.MaxLength = 200; // Limitar a 500 caracteres
            leftPanel.Controls.Add(commentBox);

            //Texto para el campo de comentarios
            Label commentText = new Label();
            commentText.Text = "Comentarios";
            commentText.Font = new Font("Arial", 12, FontStyle.Regular);
            commentText.ForeColor = Color.Black;
            commentText.Location = new Point(50, 430);
            commentText.Size = new Size(200, 50);
            leftPanel.Controls.Add(commentText);

            // Icono para el campo de comentarios
            IconPictureBox commentIcon = new IconPictureBox();
            commentIcon.IconChar = IconChar.Comment;
            commentIcon.IconColor = Color.FromArgb(31, 30, 68);
            commentIcon.Location = new Point(10, 460);
            commentIcon.Size = new Size(32, 32);
            commentIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(commentIcon);

            //Texto para el campo de fecha, posicionar a la derecha del campo de nombre
            Label dateText = new Label();
            dateText.Text = "Fecha de entrega";
            dateText.Font = new Font("Arial", 12, FontStyle.Regular);
            dateText.ForeColor = Color.Black;
            dateText.Location = new Point(300, 80);
            dateText.Size = new Size(200, 20);
            leftPanel.Controls.Add(dateText);

            // Campo de fecha
            datePicker.Format = DateTimePickerFormat.Custom;
            datePicker.Location = new Point(300, 110);
            datePicker.Size = new Size(200, 50);
            datePicker.CustomFormat = "dd/MM/yyyy";
            datePicker.Value = DateTime.Parse(fecha);
            leftPanel.Controls.Add(datePicker);

            IconPictureBox dateIcon = new IconPictureBox();
            dateIcon.IconChar = IconChar.CalendarAlt;
            dateIcon.IconColor = Color.FromArgb(31, 30, 68);
            dateIcon.Location = new Point(260, 110);
            dateIcon.Size = new Size(32, 32);
            dateIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(dateIcon);

            //Texto para el campo de hora, posicionar a la derecha del campo de telefono
            Label timeText = new Label();
            timeText.Text = "Hora de entrega";
            timeText.Font = new Font("Arial", 12, FontStyle.Regular);
            timeText.ForeColor = Color.Black;
            timeText.Location = new Point(300, 150);
            timeText.Size = new Size(200, 20);
            leftPanel.Controls.Add(timeText);

            // Campo de hora
            timePicker.Format = DateTimePickerFormat.Custom;
            timePicker.Location = new Point(300, 180);
            timePicker.Size = new Size(200, 50);
            timePicker.ShowUpDown = true;
            timePicker.CustomFormat = "HH:mm";
            timePicker.Value = DateTime.Parse(hora);
            leftPanel.Controls.Add(timePicker);

            // Icono para el campo de hora
            IconPictureBox timeIcon = new IconPictureBox();
            timeIcon.IconChar = IconChar.Clock;
            timeIcon.IconColor = Color.FromArgb(31, 30, 68);
            timeIcon.Location = new Point(260, 180);
            timeIcon.Size = new Size(32, 32);
            timeIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(timeIcon);

            // Botón de guardar
            Button saveButton = new Button();
            saveButton.Text = "Guardar";
            saveButton.Font = new Font("Arial", 12, FontStyle.Regular);
            saveButton.ForeColor = Color.White;
            saveButton.BackColor = Color.FromArgb(31, 30, 68);
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.Location = new Point(300, 600);
            saveButton.Size = new Size(200, 50);
            saveButton.Click += new EventHandler(SaveData);
            leftPanel.Controls.Add(saveButton);
        }
              private void SaveData(object sender, EventArgs e)
        {
            // Antes de insertar los datos en la tabla, se valida que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(brand.Text) || string.IsNullOrWhiteSpace(name.Text) || string.IsNullOrWhiteSpace(phone.Text) || string.IsNullOrWhiteSpace(model.Text) || string.IsNullOrWhiteSpace(reason.Text) || string.IsNullOrWhiteSpace(datePicker.Text) || string.IsNullOrWhiteSpace(timePicker.Text))
            {
                MessageBox.Show("Por favor, llene todos los campos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        
            if (status != "ATRASADO" && status != "PENDIENTE")
            {
                MessageBox.Show($"No puedes actualizar la fecha de entrega. El estado actual es: {status} para actualizar la fecha el registro debe tener un estado de PENDIENTE O ATRASADO", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        
            try
            {
                DbConnect dbConnect = new DbConnect();
                // Fecha de ingreso automática
                string date_brought = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        
                // Tomar la fecha y hora para crear la fecha de entrega
                DateTime Date = datePicker.Value;
                DateTime Time = timePicker.Value;
        
                // Fusionar fecha y hora de entrega
                DateTime deliveryDateTime = Date.Date.Add(Time.TimeOfDay);
        
                // Determinar el estatus basado en la fecha de entrega
                string newStatus = deliveryDateTime > DateTime.Now ? "PENDIENTE" : "ATRASADO";
        
                // Query para actualizar datos en la tabla
                string query = "UPDATE customers SET nombre = @nombre, telefono = @telefono, marca = @marca, modelo = @modelo, motivo = @motivo, fecha_entregar = @fecha_entregar, estatus = @estatus, comentarios = @comentarios WHERE nombre = @oldNombre AND telefono = @oldTelefono AND marca = @oldMarca AND modelo = @oldModelo AND motivo = @oldMotivo";
        
                using (MySqlCommand cmd = new MySqlCommand(query, dbConnect.Connection))
                {
                    cmd.Parameters.AddWithValue("@nombre", name.Text);
                    cmd.Parameters.AddWithValue("@telefono", phone.Text);
                    cmd.Parameters.AddWithValue("@marca", brand.Text);
                    cmd.Parameters.AddWithValue("@modelo", model.Text);
                    cmd.Parameters.AddWithValue("@motivo", reason.Text);
                    cmd.Parameters.AddWithValue("@fecha_entregar", deliveryDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@estatus", newStatus);
                    cmd.Parameters.AddWithValue("@comentarios", commentBox.Text);
                    cmd.Parameters.AddWithValue("@oldNombre", Nombre);
                    cmd.Parameters.AddWithValue("@oldTelefono", telefono);
                    cmd.Parameters.AddWithValue("@oldMarca", marca);
                    cmd.Parameters.AddWithValue("@oldModelo", modelo);
                    cmd.Parameters.AddWithValue("@oldMotivo", motivo);
        
                    dbConnect.OpenConnection();
                    cmd.ExecuteNonQuery();
                    dbConnect.CloseConnection();
                }
        
                // Obtener el valor del filtro desde la clase MainForm
                string filterValue = mainForm.GetFilterValue();
                string searchValue = mainForm.GetSearchValue();
                mainForm.GetFilterRegisters(filterValue, searchValue);
        
                MessageBox.Show("Registro de cliente actualizado exitosamente", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CloseModal(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Función para ocultar los controles superiores del formulario
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80; 
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
        private void CloseModal(object sender, EventArgs e)
        {
            Hide();
        }
    }
}