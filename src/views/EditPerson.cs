using System;
using System.Drawing;
using FontAwesome.Sharp;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace TecnogadgedWin7
{
    public partial class EditPerson : Form
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


        public int id;
        public string namePerson;

        Button sendButton = new Button();
        Button printToken = new Button();

        List<string> personalNames = new List<string>();

        public EditPerson(Form1 form, int id, string namePerson)
        {
            this.id = id;
            this.namePerson = namePerson;
            InitializeComponent();
            mainForm = form;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new Size(400, 300);

            // Titulo del modal
            Label title = new Label();
            title.Text = "Actualizar empleado";
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
            name.Text = namePerson;
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
            sendButton.Text = "Actualizar";
            sendButton.Font = new Font("Arial", 12, FontStyle.Regular);
            sendButton.ForeColor = Color.White;
            sendButton.BackColor = Color.FromArgb(31, 30, 68);
            sendButton.Location = new Point(50, 200); // Bajado un poco
            sendButton.Size = new Size(200, 50);
            sendButton.FlatStyle = FlatStyle.Flat;
            sendButton.FlatAppearance.BorderSize = 0;
            sendButton.Click += new EventHandler(UpdateEmployeeName);
            leftPanel.Controls.Add(sendButton);




        }
        public void UpdateEmployeeName(object sender, EventArgs e)
        {
            try
            {
                int idEmployee = id;
                string newName = name.Text; // Asumiendo que 'name' es el TextBox para el nuevo nombre

                DbConnect dbConnect = new DbConnect();
                string query = "UPDATE person SET nombre = @newName WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, dbConnect.Connection))
                {
                    cmd.Parameters.AddWithValue("@newName", newName);
                    cmd.Parameters.AddWithValue("@id", idEmployee);
                    dbConnect.OpenConnection();
                    cmd.ExecuteNonQuery();
                    dbConnect.CloseConnection();
                }

                MessageBox.Show("Nombre del empleado actualizado con éxito", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mainForm.GetEmplooyesNamesOnSelector(); // Actualizar la lista de nombres de empleados
                CloseModal(sender, e); // Cerrar el modal después de la actualización
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el nombre del empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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