using System;
using System.Drawing;
using FontAwesome.Sharp;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace TecnogadgedWin7
{
    public partial class CreateANewPersonModal : Form
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

        //Campo de texto para agregar una descripcion de la reparacion
        private TextBox description = new TextBox();
        //Campo de fecha y hora para entrega
        private DateTimePicker delivery_date = new DateTimePicker();
        private DateTimePicker delivery_time = new DateTimePicker();



        Button sendButton = new Button();
        Button printToken = new Button();

        List<string> personalNames = new List<string>();

        public CreateANewPersonModal(Form1 form)
        {
            InitializeComponent();
            mainForm = form;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new Size(400, 300);

            // Titulo del modal
            Label title = new Label();
            title.Text = "Agregar nuevo empleado";
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.ForeColor = Color.Black;
            title.Location = new Point(50, 20);
            title.Size = new Size(300, 50);
            Controls.Add(title);

            // Crear panel izquierdo
            Panel leftPanel = new Panel();
            leftPanel.Dock = DockStyle.Fill;
            leftPanel.BackColor = Color.Transparent;
            Controls.Add(leftPanel);

            // Campo de nombre
            name.Font = new Font("Arial", 12, FontStyle.Regular);
            name.ForeColor = Color.White;
            name.Location = new Point(50, 100); // Bajado un poco
            name.Size = new Size(200, 100);
            name.BackColor = Color.FromArgb(31, 30, 68);
            name.BorderStyle = BorderStyle.FixedSingle;
            name.Font = new Font("Arial", 12, FontStyle.Regular);
            name.TextAlign = HorizontalAlignment.Center;
            name.MaxLength = 60;
            leftPanel.Controls.Add(name);

            // Texto para el campo de nombre
            Label nameText = new Label();
            nameText.Text = "Nombre";
            nameText.Font = new Font("Arial", 12, FontStyle.Regular);
            nameText.ForeColor = Color.Black;
            nameText.Location = new Point(50, 70); // Bajado un poco
            nameText.Size = new Size(200, 50);
            leftPanel.Controls.Add(nameText);

            // Icono para el campo de nombre
            IconPictureBox nameIcon = new IconPictureBox();
            nameIcon.IconChar = IconChar.User;
            nameIcon.IconColor = Color.FromArgb(31, 30, 68);
            nameIcon.Location = new Point(10, 100); // Bajado un poco
            nameIcon.Size = new Size(32, 32);
            nameIcon.BackColor = Color.Transparent;
            leftPanel.Controls.Add(nameIcon);



            // Boton de inseertar empleado
            sendButton.Text = "Agregar";
            sendButton.Font = new Font("Arial", 12, FontStyle.Regular);
            sendButton.ForeColor = Color.White;
            sendButton.BackColor = Color.FromArgb(31, 30, 68);
            sendButton.Location = new Point(50, 200); // Bajado un poco
            sendButton.Size = new Size(200, 50);
            sendButton.FlatStyle = FlatStyle.Flat;
            sendButton.FlatAppearance.BorderSize = 0;
            sendButton.Click += new EventHandler(InsertPerson);
            leftPanel.Controls.Add(sendButton);




        }


        private void InsertPerson(object sender, EventArgs e)
        {
            //Antes de insertar los datos en la tabla, se valida que los campos no esten vacios
            if (name.Text == "")
            {
                MessageBox.Show("Por favor, llene todos los campos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {

                DbConnect dbConnect = new DbConnect();
                //Query para insertar datos en la tabla
                String query = "INSERT INTO employees (nombre) " +
                               "VALUES ('" + name.Text + "')";
                dbConnect.ExecuteQuery(query);
                // Obtener el valor del filtro desde la clase MainForm
                string filterValue = mainForm.GetFilterValue();
                string searchValue = mainForm.GetSearchValue();
                mainForm.GetFilterRegisters(filterValue, searchValue);
                MessageBox.Show("Registro de empleado exitoso", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);


                mainForm.GetEmplooyesNamesOnSelector();
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


    }
}