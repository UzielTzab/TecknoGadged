using System;
using System.Drawing;
using FontAwesome.Sharp;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace TecnogadgedWin7
{
    public partial class CustomerForm : Form
    {
        private TextBox name = new TextBox();
        private TextBox phone = new TextBox();
        private Form1 mainForm;

        //Selector de persona que recibio
        private ComboBox received_person = new ComboBox();
        //Selector de tipo de dispositivo
        private ComboBox type_device = new ComboBox();
        //Campo de texto del modelo
        private TextBox model = new TextBox();
        //Selector de marca
        private ComboBox brand = new ComboBox();

        ComboBox descriptionComboBox = new ComboBox();


        //Campo de texto para agregar una descripcion de la reparacion
        //Campo de fecha y hora para entrega
        private DateTimePicker delivery_date = new DateTimePicker();
        private DateTimePicker delivery_time = new DateTimePicker();




        Button sendButton = new Button();
        Button printToken = new Button();

        List<string> personalNames = new List<string>();

        public CustomerForm(Form1 form)
        {
            GetPersonalNames();
            InitializeComponent();
            mainForm = form;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new Size(550, 520);

            type_device.SelectedIndexChanged += TypeDevice_SelectedIndexChanged;
            brand.SelectedIndexChanged += Brand_SelectedIndexChanged;

            // Titulo del modal
            Label title = new Label();
            title.Text = "Agregar nuevo cliente";
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.ForeColor = Color.Black;
            title.Location = new Point(280, 20);
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
            name.Location = new Point(50, 50);
            name.Size = new Size(200, 100);
            name.BackColor = Color.FromArgb(31, 30, 68);
            name.BorderStyle = BorderStyle.FixedSingle;
            name.Font = new Font("Arial", 12, FontStyle.Regular);
            name.TextAlign = HorizontalAlignment.Center;
            name.MaxLength = 60;
            leftPanel.Controls.Add(name);

            //Texto para el campo de nombre
            Label nameText = new Label();
            nameText.Text = "Nombre";
            nameText.Font = new Font("Arial", 12, FontStyle.Regular);
            nameText.ForeColor = Color.Black;
            nameText.Location = new Point(50, 20);
            nameText.Size = new Size(200, 50);
            leftPanel.Controls.Add(nameText);

            // Icono para el campo de nombre
            IconPictureBox nameIcon = new IconPictureBox();
            nameIcon.IconChar = IconChar.User;
            nameIcon.IconColor = Color.FromArgb(31, 30, 68);
            nameIcon.Location = new Point(10, 50);
            nameIcon.Size = new Size(32, 32);
            nameIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(nameIcon);

            // Campo de telefono
            phone = new TextBox();
            phone.ForeColor = Color.White;
            phone.Location = new Point(50, 120);
            phone.Size = new Size(200, 50);
            phone.BackColor = Color.FromArgb(31, 30, 68);
            phone.BorderStyle = BorderStyle.FixedSingle;
            phone.Font = new Font("Arial", 12, FontStyle.Regular);
            phone.TextAlign = HorizontalAlignment.Center;
            phone.MaxLength = 10;
            phone.KeyPress += new KeyPressEventHandler((sender, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            });
            phone.Validating += new System.ComponentModel.CancelEventHandler((sender, e) =>
            {
                if (phone.Text.Length != 10)
                {
                    e.Cancel = true;
                    MessageBox.Show("El número de teléfono debe tener exactamente 10 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            });
            leftPanel.Controls.Add(phone);

            //Texto para el campo de telefono
            Label phoneText = new Label();
            phoneText.Text = "Telefono";
            phoneText.Font = new Font("Arial", 12, FontStyle.Regular);
            phoneText.ForeColor = Color.Black;
            phoneText.Location = new Point(50, 90);
            phoneText.Size = new Size(200, 50);
            leftPanel.Controls.Add(phoneText);

            // Icono para el campo de telefono
            IconPictureBox ageIcon = new IconPictureBox();
            ageIcon.IconChar = IconChar.Phone;
            ageIcon.IconColor = Color.FromArgb(31, 30, 68);
            ageIcon.Location = new Point(10, 120);
            ageIcon.Size = new Size(32, 32);
            ageIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(ageIcon);

            // Selector de tipo de dispositivo
            type_device.ForeColor = Color.White;
            type_device.Location = new Point(50, 190);
            type_device.Size = new Size(200, 50);
            type_device.BackColor = Color.FromArgb(31, 30, 68);
            type_device.FlatStyle = FlatStyle.Flat;
            type_device.DropDownStyle = ComboBoxStyle.DropDownList;
            type_device.Items.Add("Celular");
            type_device.Items.Add("Tablet");
            type_device.Items.Add("Laptop");
            type_device.Items.Add("Bocina");
            type_device.Items.Add("Otro");

            type_device.SelectedIndex = 0;
            type_device.SelectedIndexChanged += new EventHandler(TypeDevice_SelectedIndexChanged!);
            leftPanel.Controls.Add(type_device);


            //Texto para el campo de tipo de dispositivo
            Label type_deviceText = new Label();
            type_deviceText.Text = "Tipo de dispositivo";
            type_deviceText.Font = new Font("Arial", 12, FontStyle.Regular);
            type_deviceText.ForeColor = Color.Black;
            type_deviceText.Location = new Point(50, 160);
            type_deviceText.Size = new Size(200, 50);
            leftPanel.Controls.Add(type_deviceText);

            // Icono para el campo de tipo de dispositivo
            IconPictureBox type_deviceIcon = new IconPictureBox();
            type_deviceIcon.IconChar = IconChar.MobileAlt;
            type_deviceIcon.IconColor = Color.FromArgb(31, 30, 68);
            type_deviceIcon.Location = new Point(10, 190);
            type_deviceIcon.Size = new Size(32, 32);
            type_deviceIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(type_deviceIcon);

            //Selector de marca
            brand.ForeColor = Color.White;
            brand.Location = new Point(50, 260);
            brand.Size = new Size(200, 50);
            brand.BackColor = Color.FromArgb(31, 30, 68);
            brand.FlatStyle = FlatStyle.Flat;
            brand.DropDownStyle = ComboBoxStyle.DropDownList;
            leftPanel.Controls.Add(brand);
            // Inicializar las opciones de marca para el tipo de dispositivo seleccionado inicialmente
            UpdateBrandOptions("Celular");

            //Texto para el campo de marca
            Label brandText = new Label();
            brandText.Text = "Marca";
            brandText.Font = new Font("Arial", 12, FontStyle.Regular);
            brandText.ForeColor = Color.Black;
            brandText.Location = new Point(50, 230);
            brandText.Size = new Size(200, 50);
            leftPanel.Controls.Add(brandText);

            // Icono para el campo de marca
            IconPictureBox brandIcon = new IconPictureBox();
            brandIcon.IconChar = IconChar.Tag;
            brandIcon.IconColor = Color.FromArgb(31, 30, 68);
            brandIcon.Location = new Point(10, 260);
            brandIcon.Size = new Size(32, 32);
            brandIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(brandIcon);

            // Campo de modelo
            model.ForeColor = Color.White;
            model.Location = new Point(50, 330);
            model.Size = new Size(200, 50);
            model.BackColor = Color.FromArgb(31, 30, 68);
            model.BorderStyle = BorderStyle.FixedSingle;
            model.Font = new Font("Arial", 12, FontStyle.Regular);
            model.TextAlign = HorizontalAlignment.Center;
            leftPanel.Controls.Add(model);

            //Texto para el campo de modelo
            Label modelText = new Label();
            modelText.Text = "Modelo";
            modelText.Font = new Font("Arial", 12, FontStyle.Regular);
            modelText.ForeColor = Color.Black;
            modelText.Location = new Point(50, 300);
            modelText.Size = new Size(200, 50);
            leftPanel.Controls.Add(modelText);

            // Icono para el campo de modelo
            IconPictureBox modelIcon = new IconPictureBox();
            modelIcon.IconChar = IconChar.Mobile;
            modelIcon.IconColor = Color.FromArgb(31, 30, 68);
            modelIcon.Location = new Point(10, 330);
            modelIcon.Size = new Size(32, 32);
            modelIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(modelIcon);

            // Selector de persona que recibio
            received_person.ForeColor = Color.White;
            received_person.Location = new Point(300, 120);
            received_person.Size = new Size(200, 50);
            received_person.BackColor = Color.FromArgb(31, 30, 68);
            received_person.FlatStyle = FlatStyle.Flat;
            received_person.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (string name in personalNames)
            {
                received_person.Items.Add(name);
            }
            // received_person.Items.Add("Ana");
            leftPanel.Controls.Add(received_person);

            //Texto para el campo de persona que recibio
            Label received_personText = new Label();
            received_personText.Text = "Persona que recibio";
            received_personText.Font = new Font("Arial", 12, FontStyle.Regular);
            received_personText.ForeColor = Color.Black;
            received_personText.Location = new Point(300, 90);
            received_personText.Size = new Size(200, 50);
            leftPanel.Controls.Add(received_personText);

            // Icono para el campo de persona que recibio
            IconPictureBox received_personIcon = new IconPictureBox();
            received_personIcon.IconChar = IconChar.User;
            received_personIcon.IconColor = Color.FromArgb(31, 30, 68);
            received_personIcon.Location = new Point(260, 120);
            received_personIcon.Size = new Size(32, 32);
            received_personIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(received_personIcon);

            // ComboBox para el campo de descripción

            descriptionComboBox.Font = new Font("Arial", 12, FontStyle.Regular);
            descriptionComboBox.ForeColor = Color.White;
            descriptionComboBox.BackColor = Color.FromArgb(31, 30, 68);
            descriptionComboBox.Location = new Point(300, 190);
            descriptionComboBox.Size = new Size(200, 50);
            descriptionComboBox.FlatStyle = FlatStyle.Flat;
            descriptionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // Agregar opciones predeterminadas
            descriptionComboBox.Items.Add("No carga");
            descriptionComboBox.Items.Add("No prende");
            descriptionComboBox.Items.Add("No se escucha(Bocina)");
            descriptionComboBox.Items.Add("Sin señal");
            descriptionComboBox.Items.Add("Mojado");
            descriptionComboBox.Items.Add("Display roto");
            descriptionComboBox.Items.Add("Macro/Payjoy");
            descriptionComboBox.Items.Add("Bateria");
            descriptionComboBox.Items.Add("Mantenimiento de parte");
            descriptionComboBox.Items.Add("Boton malo");
            descriptionComboBox.Items.Add("Cta de Google");

            descriptionComboBox.Items.Add("Otro");

            // Suscribirse al evento SelectedIndexChanged
            descriptionComboBox.SelectedIndexChanged += new EventHandler(DescriptionComboBox_SelectedIndexChanged);

            // Agregar el ComboBox al panel
            leftPanel.Controls.Add(descriptionComboBox);

            //Texto para el campo de descripcion
            Label descriptionText = new Label();
            descriptionText.Text = "Motivo del fallo";
            descriptionText.Font = new Font("Arial", 12, FontStyle.Regular);
            descriptionText.ForeColor = Color.Black;
            descriptionText.Location = new Point(300, 160);
            descriptionText.Size = new Size(200, 50);
            leftPanel.Controls.Add(descriptionText);

            //Icono para el campo de descripcion
            IconPictureBox descriptionIcon = new IconPictureBox();
            descriptionIcon.IconChar = IconChar.Comment;
            descriptionIcon.IconColor = Color.FromArgb(31, 30, 68);
            descriptionIcon.Location = new Point(260, 190);
            descriptionIcon.Size = new Size(32, 32);
            descriptionIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(descriptionIcon);

            //Campo de fecha para entrega
            delivery_date.Location = new Point(300, 330);
            delivery_date.Size = new Size(200, 50);
            delivery_date.Format = DateTimePickerFormat.Custom;
            delivery_date.CustomFormat = "dd/MM/yyyy";
            delivery_date.Value = DateTime.Now.AddDays(1);
            leftPanel.Controls.Add(delivery_date);

            //Texto para el campo de fecha de entrega
            Label delivery_dateText = new Label();
            delivery_dateText.Text = "Fecha de entrega";
            delivery_dateText.Font = new Font("Arial", 12, FontStyle.Regular);
            delivery_dateText.ForeColor = Color.Black;
            delivery_dateText.Location = new Point(300, 300);
            delivery_dateText.Size = new Size(200, 50);
            leftPanel.Controls.Add(delivery_dateText);

            //Icono para el campo de fecha de entrega
            IconPictureBox delivery_dateIcon = new IconPictureBox();
            delivery_dateIcon.IconChar = IconChar.CalendarAlt;
            delivery_dateIcon.IconColor = Color.FromArgb(31, 30, 68);
            delivery_dateIcon.Location = new Point(260, 330);
            delivery_dateIcon.Size = new Size(32, 32);
            delivery_dateIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(delivery_dateIcon);

            //Campo de hora para entrega
            delivery_time.Location = new Point(300, 400);
            delivery_time.Size = new Size(200, 50);
            delivery_time.Format = DateTimePickerFormat.Custom;
            delivery_time.CustomFormat = "HH:mm";
            delivery_time.ShowUpDown = true;
            delivery_time.Value = DateTime.Today.AddHours(18);
            leftPanel.Controls.Add(delivery_time);

            //Texto para el campo de hora de entrega
            Label delivery_timeText = new Label();
            delivery_timeText.Text = "Hora de entrega";
            delivery_timeText.Font = new Font("Arial", 12, FontStyle.Regular);
            delivery_timeText.ForeColor = Color.Black;
            delivery_timeText.Location = new Point(300, 370);
            delivery_timeText.Size = new Size(200, 50);
            leftPanel.Controls.Add(delivery_timeText);

            //Icono para el campo de hora de entrega
            IconPictureBox delivery_timeIcon = new IconPictureBox();
            delivery_timeIcon.IconChar = IconChar.Clock;
            delivery_timeIcon.IconColor = Color.FromArgb(31, 30, 68);
            delivery_timeIcon.Location = new Point(260, 400);
            delivery_timeIcon.Size = new Size(32, 32);
            delivery_timeIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(delivery_timeIcon);

            printToken.Text = "Registrar";
            printToken.Font = new Font("Arial", 12, FontStyle.Regular);
            printToken.Location = new Point(50, 400);
            printToken.Size = new Size(200, 50);
            printToken.BackColor = Color.FromArgb(31, 30, 68);
            printToken.ForeColor = Color.White;
            printToken.FlatStyle = FlatStyle.Flat;
            printToken.FlatAppearance.BorderSize = 0;
            printToken.Click += new EventHandler(PostARegister!);
            leftPanel.Controls.Add(printToken);

        }
        // Evento para cambiar el ComboBox a un campo de escritura libre si se selecciona "Otro"
        private void DescriptionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null && comboBox.SelectedItem.ToString() == "Otro")
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox.Text = "";
            }
            else
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }
        public void GetPersonalNames()
        {
            // Instanciar la clase DbConnect y ejecutar la consulta
            DbConnect dbConnect = new DbConnect();
            string query = "SELECT nombre FROM person";
            DataTable dataTable = dbConnect.ExecuteQuery(query);

            // Agregar los nombres de los empleados al ComboBox
            foreach (DataRow row in dataTable.Rows)
            {
                string name = row["nombre"].ToString();
                personalNames.Add(name);
            }
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
        //Funcion para cerrar el modal 
        private void CloseModal(object sender, EventArgs e)
        {
            Hide();
        }

        //Boton que inserta datos en la tabla
        private void PostARegister(object sender, EventArgs e)
        {
            //Antes de insertar los datos en la tabla, se valida que los campos no esten vacios
            if (name.Text == "" || phone.Text == "" || model.Text == "" || descriptionComboBox.Text == "" || received_person.Text == "")
            {
                MessageBox.Show("Por favor, llene todos los campos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {

                DbConnect dbConnect = new DbConnect();
                //Fecha de ingreso automatica
                String date_brought = DateTime.Now.ToString("yyyy-MM-dd   HH:mm:ss");

                DateTime deliveryDate = delivery_date.Value;
                DateTime deliveryTime = delivery_time.Value;

                //Fusionar fecha y hora de entrega
                string deliveryDateTime = deliveryDate.ToString("yyyy-MM-dd") + " " + deliveryTime.ToString("HH:mm:ss");
                //Query para insertar datos en la tabla
                String query = "INSERT INTO customers (nombre, telefono, tipo_dispositivo, marca, modelo, motivo, estatus, persona_recibio, fecha_recibido, fecha_entregar) VALUES ('" + name.Text + "', '" + phone.Text + "', '" + type_device.Text + "', '" + brand.Text + "', '" + model.Text + "', '" + descriptionComboBox.Text + "', '" + "PENDIENTE" + "', '" + received_person.Text + "', '" + date_brought + "', '" + deliveryDateTime + "')";
                dbConnect.ExecuteQuery(query);
                // Obtener el valor del filtro desde la clase MainForm
                string filterValue = mainForm.GetFilterValue();
                string searchValue = mainForm.GetSearchValue();
                mainForm.GetFilterRegisters(filterValue, searchValue);
                MessageBox.Show("Registro de cliente exitoso", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PrintToken(sender, e);
                CloseModal(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void PrintReceipt(string receiptText)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (sender, e) =>
            {
                float yPos = 10;
                float leftMargin = 10;
                float topMargin = 10;
                float maxWidth = 270; // Ancho máximo de la impresión en puntos (3.78 pulgadas a 72 DPI)

                using (Font printFont = new Font("Arial", 12, FontStyle.Bold))
                {
                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Near;

                    // Dividir el texto en líneas que se ajusten al ancho de la página
                    string[] lines = receiptText.Split('\n');
                    foreach (string line in lines)
                    {
                        SizeF size = e.Graphics.MeasureString(line, printFont, (int)maxWidth, format);
                        e.Graphics.DrawString(line, printFont, Brushes.Black, new RectangleF(leftMargin, yPos, maxWidth, size.Height), format);
                        yPos += size.Height;
                    }
                }
            };

            // Configurar el tamaño de página predeterminado (A4)
            PaperSize paperSize = new PaperSize("A4", 827, 1169); // Tamaño A4 en unidades de 0.01 pulgadas
            printDocument.DefaultPageSettings.PaperSize = paperSize;
            printDocument.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10); // Márgenes de 10 unidades

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
        private void PrintToken(object sender, EventArgs e)
        {
            //Si los campos estan vacios, se muestra un mensaje de error
            if (name.Text == "" || phone.Text == "" || model.Text == "" || descriptionComboBox.Text == "")
            {
                MessageBox.Show("Por favor, llene todos los campos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string receiptText = "TEKNOGADGET\n\n";
            receiptText += "NOMBRE DEL CLIENTE:\n";
            receiptText += name.Text + "\n";
            receiptText += "--------------------------------\n";
            receiptText += "MARCA:\n";
            receiptText += brand.Text + "\n";
            receiptText += "--------------------------------\n";
            receiptText += "MODELO:\n";
            receiptText += model.Text + "\n";
            receiptText += "--------------------------------\n";

            receiptText += "MOTIVO:\n";
            receiptText += descriptionComboBox.Text + "\n";

            PrintReceipt(receiptText);
        }
        // Manejador de eventos para el cambio de selección del tipo de dispositivo
        private void TypeDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedDeviceType = type_device.SelectedItem?.ToString() ?? string.Empty;
                UpdateBrandOptions(selectedDeviceType);

                if (selectedDeviceType == "Otro")
                {
                    type_device.DropDownStyle = ComboBoxStyle.DropDown;
                    type_device.Text = "";
                }
                else
                {
                    type_device.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para actualizar las opciones de marca según el tipo de dispositivo
        private void UpdateBrandOptions(string deviceType)
        {
            try
            {
                brand.Items.Clear();

                if (deviceType == "Celular")
                {
                    brand.Items.AddRange(new string[] {
                "Samsung", "Apple", "Huawei", "Xiaomi", "Oppo", "OnePlus", "Realme", "Vivo", "Google", "Asus", "Motorola", "Sony", "LG", "Alcatel", "Nokia", "ZTE", "Otro"
            });
                }
                else if (deviceType == "Tablet")
                {
                    brand.Items.AddRange(new string[] {
                "Apple", "Samsung", "Huawei", "Xiaomi", "Lenovo", "Microsoft", "Amazon", "Asus", "Acer", "Otro"
            });
                }
                else if (deviceType == "Laptop")
                {
                    brand.Items.AddRange(new string[] {
                "Apple", "Dell", "HP", "Lenovo", "Asus", "Acer", "Microsoft", "Samsung", "MSI", "Razer", "Otro"
            });
                }
                else if (deviceType == "Bocina")
                {
                    brand.Items.AddRange(new string[] {
                "Bose", "Kaiser", "Sony", "JBL", "Harman Kardon", "Sonos", "Marshall", "Ultimate Ears", "Bang & Olufsen", "Otro"
            });
                }
                else if (deviceType == "Otro")
                {
                    // Hacer que el campo de marca sea de escritura libre
                    brand.DropDownStyle = ComboBoxStyle.DropDown;
                    brand.Text = "";
                    return;
                }

                // Restablecer el estilo de lista desplegable si no es "Otro"
                brand.DropDownStyle = ComboBoxStyle.DropDownList;

                if (brand.Items.Count > 0)
                {
                    brand.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al actualizar las opciones de marca: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Manejador de eventos para el cambio de selección de la marca
        private void Brand_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (brand.SelectedItem?.ToString() == "Otro")
                {
                    brand.DropDownStyle = ComboBoxStyle.DropDown;
                    brand.Text = "";
                }
                else
                {
                    brand.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}