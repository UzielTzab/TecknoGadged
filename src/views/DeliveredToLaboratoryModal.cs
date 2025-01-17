using System;
using System.Drawing;
using FontAwesome.Sharp;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace TecnogadgedWin7
{
    public partial class DeliveredToLaboratoryModal : Form
    {
        private Form1 mainForm;
        Button sendButton = new Button();
        int id = 1;

        int puntoXIcons = 50;
        int puntoXLabels = 100;


        public DeliveredToLaboratoryModal(Form1 form, int id, string name, string brand, string model, string status, string problem, string type_device, string date_will_deliver)
        {

            string[] fechaHora = date_will_deliver.Split(' ');
            this.id = id;
            InitializeComponent();

            // var (fecha, hora) = SepararFechaHora(date_will_deliver);

            mainForm = form;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new Size(650, 580);

            // Crear panel izquierdo
            Panel leftPanel = new Panel();
            leftPanel.Dock = DockStyle.Fill;
            leftPanel.BackColor = Color.Transparent;
            Controls.Add(leftPanel);

            // Titulo del modal
            Label title = new Label();
            title.Text = "Confirmar envío al laboratorio";
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.ForeColor = Color.Black;
            title.Location = new Point(50, 20);
            title.Size = new Size(500, 50);
            leftPanel.Controls.Add(title);

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
            deviceTypeLabel.Text = type_device;
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
            brandIcon.IconChar = IconChar.Tag; ;
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

            // Subtítulo y valor del fecha de entrega
            Label deliveryDateSubtitle = new Label();
            deliveryDateSubtitle.Text = "Fecha de entrega:";
            deliveryDateSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            deliveryDateSubtitle.ForeColor = Color.Black;
            deliveryDateSubtitle.Location = new Point(puntoXLabels + 250, 80);
            deliveryDateSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(deliveryDateSubtitle);

            Label deliveryDateLabel = new Label();
            deliveryDateLabel.Text = fechaHora[0];
            deliveryDateLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            deliveryDateLabel.ForeColor = Color.Black;
            deliveryDateLabel.Location = new Point(puntoXLabels + 250, 100);
            deliveryDateLabel.Size = new Size(200, 20);
            leftPanel.Controls.Add(deliveryDateLabel);

            // Icono de la fecha de entrega
            IconPictureBox deliveryDateIcon = new IconPictureBox();
            deliveryDateIcon.IconChar = IconChar.CalendarAlt;
            deliveryDateIcon.IconColor = Color.FromArgb(31, 30, 68);
            deliveryDateIcon.Location = new Point(puntoXIcons + 270, 80);
            deliveryDateIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(deliveryDateIcon);

            // Subtítulo y valor de la hora
            Label timeSubtitle = new Label();
            timeSubtitle.Text = "Hora de entrega:";
            timeSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            timeSubtitle.ForeColor = Color.Black;
            timeSubtitle.Location = new Point(puntoXLabels + 250, 140);
            timeSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(timeSubtitle);

            Label timeLabel = new Label();
            DateTime hora;
            if (DateTime.TryParse(fechaHora[1], out hora))
            {
                timeLabel.Text = hora.ToString("hh:mm tt"); // Formato de 12 horas con AM/PM
            }
            else
            {
                timeLabel.Text = fechaHora[1]; // En caso de que no se pueda convertir, usar el valor original
            }
            timeLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            timeLabel.ForeColor = Color.Black;
            timeLabel.Location = new Point(puntoXLabels + 250, 160);
            timeLabel.Size = new Size(200, 20);
            leftPanel.Controls.Add(timeLabel);

            // Icono de la hora
            IconPictureBox timeIcon = new IconPictureBox();
            timeIcon.IconChar = IconChar.Clock;
            timeIcon.IconColor = Color.FromArgb(31, 30, 68);
            timeIcon.Location = new Point(puntoXIcons + 270, 140);
            timeIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(timeIcon);





            // Boton para enviar los datos
            sendButton.Text = "Enviar al laboratorio";
            sendButton.Font = new Font("Arial", 12, FontStyle.Regular);
            sendButton.Location = new Point(400, 460);
            sendButton.Size = new Size(200, 50);
            sendButton.BackColor = Color.FromArgb(31, 30, 68);
            sendButton.ForeColor = Color.White;
            sendButton.FlatStyle = FlatStyle.Flat;
            sendButton.FlatAppearance.BorderSize = 0;
            sendButton.Click += new EventHandler(PutARegister!);
            leftPanel.Controls.Add(sendButton);


        }
        // private (string fecha, string hora) SepararFechaHora(string fechaHora)
        // {
        //     if (DateTime.TryParse(fechaHora, out DateTime dateTime))
        //     {
        //         string fecha = dateTime.ToString("yyyy-MM-dd");
        //         string hora = dateTime.ToString("HH:mm:ss");
        //         return (fecha, hora);
        //     }
        //     return (fechaHora, string.Empty);
        // }

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
        // //Función para ocultar el formulario al perder el foco
        // protected override void OnDeactivate(EventArgs e)
        // {
        //     // Oculta el formulario al perder el foco
        //     base.OnDeactivate(e);
        //     Hide();
        // }
        // Interceptar el mensaje de Windows para evitar mover el formulario
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
        //PutARegister, cambiar unicamente el estatus a "EN LABORATORIO"
        //             CREATE TABLE IF NOT EXISTS customers (
        //     id INT AUTO_INCREMENT PRIMARY KEY,
        //     nombre VARCHAR(50),
        //     telefono VARCHAR(20),
        //     tipo_dispositivo VARCHAR(50),
        //     marca VARCHAR(50),
        //     modelo VARCHAR(50),
        //     motivo VARCHAR (100),
        //     diagnostico VARCHAR (100),
        //     estatus VARCHAR(20),
        //     persona_recibio VARCHAR(50),
        //     persona_reparo VARCHAR(50),
        //     fecha_recibido DATE,
        //     fecha_reparado DATE
        // );

        private void PutARegister(object sender, EventArgs e)
        {

            try
            {
                DbConnect dbConnect = new DbConnect();
                // Fecha de ingreso automática
                String date_brought = DateTime.Now.ToString("yyyy-MM-dd");

                // Asegúrate de que 'id' tenga un valor válido
                if (id <= 0)
                {
                    throw new Exception("ID no válido.");
                }

                string query = "UPDATE customers SET estatus = @estatus WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, dbConnect.Connection))
                {
                    cmd.Parameters.AddWithValue("@estatus", "EN LABORATORIO");
                    cmd.Parameters.AddWithValue("@id", id);

                    dbConnect.OpenConnection();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    dbConnect.CloseConnection();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("No se encontró el registro con el ID especificado.");
                    }
                }

                string filterValue = mainForm.GetFilterValue();
                string searchValue = mainForm.GetSearchValue();
                mainForm.GetFilterRegisters(filterValue, searchValue);
                MessageBox.Show("Se envió correctamente el dispositivo al laboratorio", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CloseModal(sender, e);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}