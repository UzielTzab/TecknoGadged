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

               //Declarar los componentes y variables a utilizar para UI
        
        private Form1 mainForm;
        
        private TextBox name = new TextBox();
        private TextBox phone = new TextBox();
        private TextBox model = new TextBox();
        
        private ComboBox received_person = new ComboBox();
        private ComboBox type_device = new ComboBox();
        private ComboBox brand = new ComboBox();
        private ComboBox descriptionComboBox = new ComboBox();
        
        private DateTimePicker delivery_date = new DateTimePicker();
        private DateTimePicker delivery_time = new DateTimePicker();
        
        private Button sendButton = new Button();
        private Button printToken = new Button();
        
        private List<string> personalNames = new List<string>();
        
        // Declarar los componentes adicionales por el checkbox
        
        private CheckBox useFreeTextCheckBox = new CheckBox();
        private TextBox freeTypeDevice = new TextBox();
        private TextBox freeBrand = new TextBox();
        private TextBox freeDescription = new TextBox();
        
        // Declarar el TextBox para comentarios
        private TextBox commentTextField = new TextBox();
        
        // Métodos de interfaz UI a renderizar en pantalla------------------------------------
        
        public CustomerForm(Form1 form)
        {
            InitializeComponent();
            GetEmplooyesNamesOnSelector();
        
            mainForm = form;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new Size(550, 600);
        
            // Configurar el CheckBox
            useFreeTextCheckBox.Text = "Dispositivo desconocido";
            useFreeTextCheckBox.Location = new Point(300, 500);
            useFreeTextCheckBox.Size = new Size(150, 20);
            useFreeTextCheckBox.CheckedChanged += UseFreeTextCheckBox_CheckedChanged;
            Controls.Add(useFreeTextCheckBox);
        
            // Configurar los TextBox para campos libres
            freeTypeDevice.Location = new Point(50, 190);
            freeTypeDevice.Size = new Size(200, 50);
            freeTypeDevice.Visible = false;
            Controls.Add(freeTypeDevice);
        
            freeBrand.Location = new Point(50, 260);
            freeBrand.Size = new Size(200, 50);
            freeBrand.Visible = false;
            Controls.Add(freeBrand);
        
            freeDescription.Location = new Point(300, 190);
            freeDescription.Size = new Size(200, 50);
            freeDescription.Visible = false;
            Controls.Add(freeDescription);
            //---------------------------------------------------------------------
        
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
            descriptionComboBox.SelectedIndex = 0;
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
        
            // Campo de comentarios
            commentTextField.Font = new Font("Arial", 12, FontStyle.Regular);
            commentTextField.ForeColor = Color.White;
            commentTextField.BackColor = Color.FromArgb(31, 30, 68);
            commentTextField.Location = new Point(300, 260);
            commentTextField.Size = new Size(200, 50);
            commentTextField.Multiline = true;
            commentTextField.ScrollBars = ScrollBars.Vertical;
            commentTextField.BorderStyle = BorderStyle.FixedSingle;
            commentTextField.MaxLength =200;
            leftPanel.Controls.Add(commentTextField);
        
            //Texto para el campo de comentarios
            Label commentText = new Label();
            commentText.Text = "Comentarios";
            commentText.Font = new Font("Arial", 12, FontStyle.Regular);
            commentText.ForeColor = Color.Black;
            commentText.Location = new Point(300, 230);
            commentText.Size = new Size(200, 50);
            leftPanel.Controls.Add(commentText);
        
                       // Campo de fecha para entrega
            delivery_date.Location = new Point(300, 350);
            delivery_date.Size = new Size(200, 50);
            delivery_date.Format = DateTimePickerFormat.Custom;
            delivery_date.CustomFormat = "dd/MM/yyyy";
            delivery_date.Value = DateTime.Now.AddDays(1);
            leftPanel.Controls.Add(delivery_date);
            
            // Texto para el campo de fecha de entrega
            Label delivery_dateText = new Label();
            delivery_dateText.Text = "Fecha de entrega";
            delivery_dateText.Font = new Font("Arial", 12, FontStyle.Regular);
            delivery_dateText.ForeColor = Color.Black;
            delivery_dateText.Location = new Point(300, 320);
            delivery_dateText.Size = new Size(200, 50);
            leftPanel.Controls.Add(delivery_dateText);
            
            // Icono para el campo de fecha de entrega
            IconPictureBox delivery_dateIcon = new IconPictureBox();
            delivery_dateIcon.IconChar = IconChar.CalendarAlt;
            delivery_dateIcon.IconColor = Color.FromArgb(31, 30, 68);
            delivery_dateIcon.Location = new Point(260, 350);
            delivery_dateIcon.Size = new Size(32, 32);
            delivery_dateIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(delivery_dateIcon);
            
            // Campo de hora para entrega
            delivery_time.Location = new Point(300, 420);
            delivery_time.Size = new Size(200, 50);
            delivery_time.Format = DateTimePickerFormat.Custom;
            delivery_time.CustomFormat = "HH:mm";
            delivery_time.ShowUpDown = true;
            delivery_time.Value = DateTime.Today.AddHours(18);
            leftPanel.Controls.Add(delivery_time);
            
            // Texto para el campo de hora de entrega
            Label delivery_timeText = new Label();
            delivery_timeText.Text = "Hora de entrega";
            delivery_timeText.Font = new Font("Arial", 12, FontStyle.Regular);
            delivery_timeText.ForeColor = Color.Black;
            delivery_timeText.Location = new Point(300, 390);
            delivery_timeText.Size = new Size(200, 50);
            leftPanel.Controls.Add(delivery_timeText);
            
            // Icono para el campo de hora de entrega
            IconPictureBox delivery_timeIcon = new IconPictureBox();
            delivery_timeIcon.IconChar = IconChar.Clock;
            delivery_timeIcon.IconColor = Color.FromArgb(31, 30, 68);
            delivery_timeIcon.Location = new Point(260, 420);
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
        // Manejar el evento CheckedChanged del CheckBox
        private void UseFreeTextCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool useFreeText = useFreeTextCheckBox.Checked;

            // Alternar visibilidad de ComboBox y TextBox
            type_device.Visible = !useFreeText;
            brand.Visible = !useFreeText;
            // descriptionComboBox.Visible = !useFreeText;

            freeTypeDevice.Visible = useFreeText;
            freeBrand.Visible = useFreeText;
            // freeDescription.Visible = useFreeText;

            // Limpiar los campos cuando se alterna
            if (useFreeText)
            {
                freeTypeDevice.Text = "";
                freeBrand.Text = "";
                // freeDescription.Text = "";
            }
            else
            {
                type_device.SelectedIndex = 0;
                brand.SelectedIndex = 0;
                // descriptionComboBox.SelectedIndex = 0;
            }
        }
            
               // Método para manejar el evento SelectedIndexChanged del ComboBox de tipo de dispositivo
       private void TypeDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedDeviceType = type_device.SelectedItem?.ToString() ?? string.Empty;

                // Deshabilitar temporalmente el manejador de eventos para evitar ciclos
                brand.SelectedIndexChanged -= Brand_SelectedIndexChanged;

                // Actualizar las opciones de marca
                UpdateBrandOptions(selectedDeviceType);

                // Volver a habilitar el manejador de eventos
                brand.SelectedIndexChanged += Brand_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error en TypeDevice_SelectedIndexChanged: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        // Método para actualizar las opciones de marca según el tipo de dispositivo
        private void UpdateBrandOptions(string deviceType)
        {
            if (brand.InvokeRequired)
            {
                brand.Invoke(new Action<string>(UpdateBrandOptions), deviceType);
                return;
            }

            try
            {
                brand.Items.Clear();

                if (deviceType == "Celular")
                {
                    brand.Items.AddRange(new string[] {
                        "Samsung", "Apple", "Huawei", "Xiaomi", "Oppo", "OnePlus", "Realme", "Vivo", "Google", "Asus", "Motorola", "Sony", "LG", "Alcatel", "Nokia", "ZTE"
                    });
                }
                else if (deviceType == "Tablet")
                {
                    brand.Items.AddRange(new string[] {
                        "Apple", "Samsung", "Huawei", "Xiaomi", "Lenovo", "Microsoft", "Amazon", "Asus", "Acer"
                    });
                }
                else if (deviceType == "Laptop")
                {
                    brand.Items.AddRange(new string[] {
                        "Apple", "Dell", "HP", "Lenovo", "Asus", "Acer", "Microsoft", "Samsung", "MSI", "Razer"
                    });
                }
                else if (deviceType == "Bocina")
                {
                    brand.Items.AddRange(new string[] {
                        "Bose", "Kaiser", "Sony", "JBL", "Harman Kardon", "Sonos", "Marshall", "Ultimate Ears", "Bang & Olufsen"
                    });
                }

                // Restablecer el estilo de lista desplegable
                brand.DropDownStyle = ComboBoxStyle.DropDownList;

                if (brand.Items.Count > 0)
                {
                    brand.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error en UpdateBrandOptions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        // Manejador de eventos para el cambio de selección de la marca
        private void Brand_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Asegurarse de que el ComboBox mantenga su estilo de lista desplegable
                brand.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error en Brand_SelectedIndexChanged: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        // Manejador de eventos para el cambio de selección de la descripción
          // Método para manejar el evento SelectedIndexChanged del ComboBox de descripción
        private void DescriptionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null && comboBox.SelectedItem?.ToString() == "Otro")
                {
                    // Ocultar el ComboBox y mostrar el TextBox
                    comboBox.Visible = false;
                    freeDescription.Visible = true;
                    freeDescription.Location = comboBox.Location;
                    freeDescription.Size = comboBox.Size;
                    freeDescription.Text = "";
                    freeDescription.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error en DescriptionComboBox_SelectedIndexChanged: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        // Validar que el campo de marca no esté vacío antes de realizar cualquier acción
        private void ValidateSelectorsAreNotNull()
        {
            if (string.IsNullOrWhiteSpace(brand.Text))
            {
                MessageBox.Show("El campo de marca no puede estar vacío.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                brand.Focus();
            }
            if (string.IsNullOrWhiteSpace(type_device.Text))
            {
                MessageBox.Show("El campo de tipo de dispositivo no puede estar vacío.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                type_device.Focus();
            }
            if (string.IsNullOrWhiteSpace(descriptionComboBox.Text))
            {
                MessageBox.Show("El campo de motivo no puede estar vacío.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                descriptionComboBox.Focus();
            }
        }
        //Función que trae a todos los empleados y los muestra en el selector de empleado
       public void GetEmplooyesNamesOnSelector()
        {
            try
            {
                DbConnect dbConnect = new DbConnect();
                string query = "SELECT nombre FROM employees";
                DataTable dataTable = dbConnect.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    string name = row["nombre"].ToString();
                    personalNames.Add(name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener nombres de empleados: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        //Funcion para cerrar el modal 
        private void CloseModal(object sender, EventArgs e)
        {
            this.Close(); // Cerrar el formulario correctamente
        }

        // Procesos de insertar el registro
        private void PostARegister(object sender, EventArgs e)
        {
            try
            {
                // Validar campos vacíos
                if (string.IsNullOrWhiteSpace(name.Text) || string.IsNullOrWhiteSpace(phone.Text) || string.IsNullOrWhiteSpace(model.Text) || string.IsNullOrWhiteSpace(received_person.Text))
                {
                    MessageBox.Show("Por favor, llene todos los campos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if(string.IsNullOrWhiteSpace(commentTextField.Text)){
                    commentTextField.Text = "Sin comentarios";
                };
        
                string deviceType = useFreeTextCheckBox.Checked ? freeTypeDevice.Text : type_device.Text;
                string deviceBrand = useFreeTextCheckBox.Checked ? freeBrand.Text : brand.Text;
                string deviceDescription = descriptionComboBox.Visible ? descriptionComboBox.Text : freeDescription.Text;
        
                if (string.IsNullOrWhiteSpace(deviceType) || string.IsNullOrWhiteSpace(deviceBrand) || string.IsNullOrWhiteSpace(deviceDescription))
                {
                    MessageBox.Show("Por favor, llene todos los campos de tipo, marca y motivo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
        
                DbConnect dbConnect = new DbConnect();
                string date_brought = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string deliveryDateTime = delivery_date.Value.ToString("yyyy-MM-dd") + " " + delivery_time.Value.ToString("HH:mm:ss");
        
                string query = "INSERT INTO customers (nombre, telefono, tipo_dispositivo, marca, modelo, motivo, estatus, persona_recibio, fecha_recibido, fecha_entregar, comentarios) VALUES (@nombre, @telefono, @tipo_dispositivo, @marca, @modelo, @motivo, @estatus, @persona_recibio, @fecha_recibido, @fecha_entregar, @comentarios)";
                MySqlParameter[] parameters = {
                    new MySqlParameter("@nombre", name.Text),
                    new MySqlParameter("@telefono", phone.Text),
                    new MySqlParameter("@tipo_dispositivo", deviceType),
                    new MySqlParameter("@marca", deviceBrand),
                    new MySqlParameter("@modelo", model.Text),
                    new MySqlParameter("@motivo", deviceDescription),
                    new MySqlParameter("@estatus", "PENDIENTE"),
                    new MySqlParameter("@persona_recibio", received_person.Text),
                    new MySqlParameter("@fecha_recibido", date_brought),
                    new MySqlParameter("@fecha_entregar", deliveryDateTime),
                    new MySqlParameter("@comentarios", commentTextField.Text)
                };
        
                // Convertir MySqlParameter[] a Dictionary<string, object>
                Dictionary<string, object> parameterDictionary = new Dictionary<string, object>();
                foreach (var param in parameters)
                {
                    parameterDictionary.Add(param.ParameterName, param.Value);
                }
        
                dbConnect.ExecuteQuery(query, parameterDictionary);
        
                // Actualizar la tabla principal en Form1
                if (mainForm != null)
                {
                    string filterValue = mainForm.GetFilterValue(); // Obtener el valor del filtro actual
                    string searchValue = mainForm.GetSearchValue(); // Obtener el valor de búsqueda actual
                    mainForm.GetFilterRegisters(filterValue, searchValue); // Actualizar la tabla
                }
        
                MessageBox.Show("Registro de cliente exitoso", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PrintToken(sender, e);
                CloseModal(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       private void PrintReceipt(string receiptText)
        {
            try
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += (sender, e) =>
                {
                    float yPos = 10;
                    float leftMargin = 10;
                    float topMargin = 10;
                    float maxWidth = 270;

                    using (Font printFont = new Font("Arial", 12, FontStyle.Bold))
                    {
                        StringFormat format = new StringFormat();
                        format.Alignment = StringAlignment.Near;
                        format.LineAlignment = StringAlignment.Near;

                        string[] lines = receiptText.Split('\n');
                        foreach (string line in lines)
                        {
                            SizeF size = e.Graphics.MeasureString(line, printFont, (int)maxWidth, format);
                            e.Graphics.DrawString(line, printFont, Brushes.Black, new RectangleF(leftMargin, yPos, maxWidth, size.Height), format);
                            yPos += size.Height;
                        }
                    }
                };

                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = printDocument;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al imprimir: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       private void PrintToken(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name.Text) || string.IsNullOrWhiteSpace(phone.Text) || string.IsNullOrWhiteSpace(model.Text) || string.IsNullOrWhiteSpace(descriptionComboBox.Text))
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error al imprimir ticket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
    }
}