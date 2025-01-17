using System;
using System.Drawing;
using FontAwesome.Sharp;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace TecnogadgedWin7
{
    public partial class AccountForm : Form
    {
        private Form1 mainForm;

        Button button1 = new Button();


        Button loginButton = new Button();
        TextBox passwordInput = new TextBox();
        TextBox userInput = new TextBox();

        public AccountForm(Form1 form)
        {
            this.KeyPreview = true; // Permite que el formulario reciba eventos de teclado antes que los controles

            InitializeComponent();
            mainForm = form;

            // Configurar el formulario para que ocupe toda la pantalla
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            // Calcular el centro del formulario
            int centerX = this.ClientSize.Width / 2;

            // Imagen del logo
            PictureBox pictureBox = new PictureBox();
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "brand", "Tek.png");

            if (File.Exists(imagePath))
            {
                pictureBox.Image = Image.FromFile(imagePath);
                pictureBox.Size = new Size(200, 200);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Location = new Point(centerX - pictureBox.Width / 2, 100); // 0 + 300
                this.Controls.Add(pictureBox);
            }
            else
            {
                MessageBox.Show("La imagen no se encontró en la ruta especificada: " + imagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Titulo del negocio
            Label label = new Label();
            label.Text = "Tekno-Gadged";
            label.Font = new Font("Arial", 30, FontStyle.Bold);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Size = new Size(400, 50);
            label.Location = new Point(centerX - label.Width / 2, 300); // 50 + 300
            this.Controls.Add(label);


            // Texto centrado "Iniciar sesión"
            Label label1 = new Label();
            label1.Text = "Iniciar sesión";
            label1.Font = new Font("Arial", 20, FontStyle.Bold);
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Size = new Size(200, 50);
            label1.Location = new Point(centerX - label1.Width / 2, 350); // 50 + 300
            this.Controls.Add(label1);

            // Icono de usuario
            IconPictureBox iconPictureBox1 = new IconPictureBox();
            iconPictureBox1.IconChar = IconChar.User;
            iconPictureBox1.Size = new Size(40, 40);
            iconPictureBox1.IconColor = Color.Black;
            iconPictureBox1.IconSize = 40;
            iconPictureBox1.Location = new Point(centerX - iconPictureBox1.Width / 2, 400); // 100 + 300
            this.Controls.Add(iconPictureBox1);

            // Campo de texto para el usuario
            userInput.Size = new Size(200, 20);
            userInput.Location = new Point(centerX - userInput.Width / 2, 450); // 150 + 300
            userInput.TextAlign = HorizontalAlignment.Center;
            this.Controls.Add(userInput);

            // Icono de candado
            IconPictureBox iconPictureBox2 = new IconPictureBox();
            iconPictureBox2.IconChar = IconChar.Lock;
            iconPictureBox2.Size = new Size(40, 40);
            iconPictureBox2.IconColor = Color.Black;
            iconPictureBox2.IconSize = 40;
            iconPictureBox2.Location = new Point(centerX - iconPictureBox2.Width / 2, 500); // 200 + 300
            this.Controls.Add(iconPictureBox2);

            // Campo de texto para la contraseña
            passwordInput.Size = new Size(200, 20);
            passwordInput.Location = new Point(centerX - passwordInput.Width / 2, 550); // 250 + 300
            passwordInput.PasswordChar = '*';
            passwordInput.TextAlign = HorizontalAlignment.Center;
            passwordInput.UseSystemPasswordChar = true;
            this.Controls.Add(passwordInput);

            // Botón de iniciar como administrador
            loginButton.Text = "Iniciar";
            loginButton.Font = new Font("Arial", 12, FontStyle.Regular);
            loginButton.Size = new Size(200, 40);
            loginButton.BackColor = Color.FromArgb(31, 30, 68);
            loginButton.ForeColor = Color.White;
            loginButton.FlatStyle = FlatStyle.Flat;
            loginButton.FlatAppearance.BorderSize = 0;
            loginButton.Location = new Point(centerX - loginButton.Width / 2, 600); // 300 + 300
            loginButton.Click += new EventHandler(Login!);
            Controls.Add(loginButton);

            // Iniciar como empleado
            button1.Text = "Soy empleado";
            button1.Font = new Font("Arial", 12, FontStyle.Regular);
            button1.Size = new Size(200, 40);
            button1.BackColor = Color.FromArgb(31, 30, 68);
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.Location = new Point(centerX - button1.Width / 2, 650); // 350 + 300
            button1.Click += new EventHandler(LoginAsEmployee!);
            Controls.Add(button1);

            // Manejar el evento de cambio de tamaño del formulario para volver a centrar los controles
            this.Resize += (s, e) =>
            {
                centerX = this.ClientSize.Width / 2;
                label1.Location = new Point(centerX - label1.Width / 2, 350); // 50 + 300
                iconPictureBox1.Location = new Point(centerX - iconPictureBox1.Width / 2, 400); // 100 + 300
                userInput.Location = new Point(centerX - userInput.Width / 2, 450); // 150 + 300
                iconPictureBox2.Location = new Point(centerX - iconPictureBox2.Width / 2, 500); // 200 + 300
                passwordInput.Location = new Point(centerX - passwordInput.Width / 2, 550); // 250 + 300
                loginButton.Location = new Point(centerX - loginButton.Width / 2, 600); // 300 + 300
                button1.Location = new Point(centerX - button1.Width / 2, 650); // 350 + 300
            };

            this.KeyDown += new KeyEventHandler(EnterClick);

        }
        private void EnterClick(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;  // Indica que el evento ha sido manejado
                e.SuppressKeyPress = true; // Evita que el "ding" del sistema se produzca al presionar Enter
                Login(sender, e); // Llama a la función de login
            }
        }



        //Cerrar la ventana
        private void Login(object sender, EventArgs e)
        {
            if (userInput.Text == "Admin" && passwordInput.Text == "Admin@123")
            {
                mainForm.User = userInput.Text;
                mainForm.Password = passwordInput.Text;
                this.Close();
            }
            else
            {
                //Mensaje de error
                MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void LoginAsEmployee(object sender, EventArgs e)
        {
            mainForm.User = userInput.Text;
            mainForm.Password = passwordInput.Text;
            this.Close();
        }
    }

}
